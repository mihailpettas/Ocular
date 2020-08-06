Shader "Hidden/MKGlowComposite" 
{
	Properties 
	{ 
		_MainTex("", 2D) = "Black" {}
		_GlowTex("", 2D) = "Black" {} 
	}
	Subshader 
	{
		ZTest off 
		Fog { Mode Off }
		Cull back
		Lighting Off
		ZWrite Off

		Pass 
		{
			Blend One Zero
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 2.0

			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform float2 _MainTex_TexelSize;
			uniform float4 _MainTex_ST;
			uniform sampler2D _MKGlowTex;
			uniform float4 _MKGlowTex_ST;
			uniform float2 _MKGlowTex_TexelSize;

			struct Input
			{
				float2 texcoord : TEXCOORD0;
				float4 vertex : POSITION;
			};
			
			struct Output 
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
			};
			
			Output vert (Input i)
			{
				Output o;
				UNITY_INITIALIZE_OUTPUT(Output,o);
				o.pos = UnityObjectToClipPos (i.vertex);
				o.uv = UnityStereoScreenSpaceUVAdjust(i.texcoord.xy, _MainTex_ST);
				o.uv1 = UnityStereoScreenSpaceUVAdjust(i.texcoord.xy, _MKGlowTex_ST);
				#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						o.uv.y = 1-o.uv.y;
					if (_MKGlowTex_TexelSize.y < 0)
							o.uv1.y = 1-o.uv1.y;
				#endif
				
				return o;
			}

			fixed4 frag( Output i ) : SV_TARGET
			{
				fixed4 m = tex2D(_MainTex, i.uv.xy);
				fixed4 g = tex2D(_MKGlowTex, i.uv1.xy);
				return g+m;
			}
			ENDCG
		}

		Pass 
		{
			Blend One Zero
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 2.0

			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform float2 _MainTex_TexelSize;
			uniform float4 _MainTex_ST;
			uniform sampler2D _MKGlowTex;
			uniform float4 _MKGlowTex_ST;
			uniform float2 _MKGlowTex_TexelSize;

			struct Input
			{
				float2 texcoord : TEXCOORD0;
				float4 vertex : POSITION;
			};
			
			struct Output 
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
			};
			
			Output vert (Input i)
			{
				Output o;
				UNITY_INITIALIZE_OUTPUT(Output,o);
				o.pos = UnityObjectToClipPos (i.vertex);
				o.uv = o.uv1 = i.texcoord;
				#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						o.uv.y = 1-o.uv.y;
					if (_MKGlowTex_TexelSize.y < 0)
							o.uv1.y = 1-o.uv1.y;
				#endif
				return o;
			}

			fixed4 frag( Output i ) : SV_TARGET
			{

					fixed4 m = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv.xy, _MainTex_ST));
					fixed4 g = tex2D(_MKGlowTex, i.uv1.xy);
					//fixed4 m = tex2D( _MainTex, i.uv.xy);
					//fixed4 g = tex2D(_MKGlowTex, i.uv1.xy);

				return g+m;
			}
			ENDCG
		}
	}
	FallBack Off
}
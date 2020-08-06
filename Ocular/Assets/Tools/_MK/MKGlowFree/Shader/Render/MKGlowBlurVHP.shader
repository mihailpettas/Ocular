// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/MKGlowBlurVHP"
{
	Properties{_Color ("", Color) = (1,1,1,1)}

	SubShader
	{
		ZTest Off
		Fog{ Mode Off }
		Cull Off
		Lighting Off
		ZWrite Off

		Pass
		{
			Blend One Zero
			Name "HB"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile MK_GLOW_BLUR_HQ

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform fixed4 _Color;
			uniform half _ShiftX;
			uniform fixed4 _MainTex_TexelSize;

			#include "MKGlowInc.cginc"

			struct Input
			{
				float2 texcoord : TEXCOORD0;
				float4 vertex : POSITION;
			};

			struct Output
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				half off : TEXCOORD1;
			};

			Output vert(Input i)
			{
				Output o;
				UNITY_INITIALIZE_OUTPUT(Output,o);
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uv = UnityStereoScreenSpaceUVAdjust(i.texcoord.xy, _MainTex_ST);
				o.off = _MainTex_TexelSize.x * _ShiftX;
				#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						o.uv.y = 1-o.uv.y;
				#endif
				return o;
			}

			fixed4 frag(Output i) : SV_TARGET
			{
				fixed4 c = fixed4(0,0,0,0);
				half2 offUv = half2(i.off, 0);
				Blur(c, _MainTex, i.uv, offUv);
				c.rgb = c.rgb * _Color.rgb;

				return c * _Color.a;
			}

		ENDCG
		}

		Pass
		{
			Blend One One
			Name "VB"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile MK_GLOW_BLUR_HQ

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform fixed4 _Color;
			uniform half _ShiftY;
			uniform fixed4 _MainTex_TexelSize;

			#include "MKGlowInc.cginc"

			struct Input
			{
				float2 texcoord : TEXCOORD0;
				float4 vertex : POSITION;
			};

			struct Output
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				half off : TEXCOORD1;
			};

			Output vert(Input i)
			{
				Output o;
				UNITY_INITIALIZE_OUTPUT(Output,o);
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uv = UnityStereoScreenSpaceUVAdjust(i.texcoord.xy, _MainTex_ST);
				o.off = _MainTex_TexelSize.y * _ShiftY;
				#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						o.uv.y = 1-o.uv.y;
				#endif
				return o;
			}

			fixed4 frag(Output i) : SV_TARGET
			{
				fixed4 c = fixed4(0,0,0,0);
				half2 offUv = half2(0, i.off);
				Blur(c, _MainTex, i.uv, offUv);
				c.rgb = c.rgb * _Color.rgb;

				return c * _Color.a;
			}

			ENDCG
		}
	}
	Fallback off
}
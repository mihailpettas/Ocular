#ifndef MK_GLOW_INC
	#define MK_GLOW_INC

	void Blur(inout fixed4 c, sampler2D tex, float2 uv, fixed2 offUv)
	{
		#if MK_GLOW_BLUR_HQ
		for (int p = 0; p < 5; p++)
		{
			c += tex2D(tex, uv.xy + offUv * p);
			c += tex2D(tex, uv.xy - offUv * p);
		}
		c /= 10;
		#else
			c += tex2D(tex, uv.xy + offUv);
			c += tex2D(tex, uv.xy - offUv);
		c /= 2;
		#endif
	}
#endif
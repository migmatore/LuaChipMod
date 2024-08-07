Shader "CorvoShader/CAtmosphere" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_TransparentColor ("TransparentColor", Vector) = (0,0,0,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_intensity ("Intensity", Range(0, 10)) = 1.5
		_maxIntensity ("Max Intensity", Range(0, 100)) = 4
		_fade ("Atmos Fade", Range(1, 10000000)) = 1000
		_ALightThreshold ("Light Threshold", Float) = 0
		_ALightContrast ("Light Contrast", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}
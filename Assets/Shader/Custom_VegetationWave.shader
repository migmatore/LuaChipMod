Shader "Custom/VegetationWave" {
	Properties {
		_MainTex ("Albedo and alpha (RGBA)", 2D) = "white" {}
		_Cutoff ("Base Alpha cutoff", Range(0, 0.9)) = 0.5
		_WindStrength ("Wind Strength", Range(0, 1)) = 0.5
		_WindSpeed ("Wind Speed", Range(0, 1)) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Transparent/Cutout/VertexLit"
}
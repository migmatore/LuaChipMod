Shader "Decal/Roughness" {
	Properties {
		_MainTex ("Shape Map", 2D) = "white" {}
		_Multiplier ("Multiplier", Range(0, 1)) = 1
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_GlossTex ("Roughness Map", 2D) = "white" {}
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
		_NormalCutoff ("Normal Cutoff", Range(0, 1)) = 0.5
		_MaskBase ("Mask Base", Range(0, 1)) = 0
		_MaskLayers ("Layers", Vector) = (0.5,0.5,0.5,0.5)
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
}
Shader "Custom/Lava" {
	Properties {
		_NoiseMap ("Lava Noise Map", 2D) = "white" {}
		_NoiseScale ("Noise Scale", Range(-200, 200)) = 1
		_NoiseSpeed ("Noise Speed", Range(-1, 1)) = 1
		_BedrockTex ("Bedrock Texture (RGBA)", 2D) = "white" {}
		_BedrockScale ("Bedrock Texture Scale", Float) = 1
		_EmissionStrength ("Emission Strength", Range(0, 1000)) = 0
		_EmissionColor ("Emission Color", Vector) = (0,0,0,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
}
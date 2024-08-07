Shader "Custom/WorldspaceOverlayTerrainUpgraded" {
	Properties {
		_Tint ("Tint", Vector) = (1,1,1,1)
		_BaseSpecular ("Base Specular", Vector) = (1,1,1,1)
		_Smoothness ("Base Smoothness", Float) = 0
		[Header(Top Textures)] _MainTexTop ("Top Base Texture", 2D) = "white" {}
		_SpecularTop ("Top Specular", Vector) = (0.5,0.5,0.5,0.5)
		_SpecularMapTop ("Top Specular Map", 2D) = "white" {}
		_SmoothnessTop ("Top Smoothness", Range(0, 1)) = 0.5
		_DetailTexTop ("Top Detail Texture", 2D) = "white" {}
		_BumpMapTop ("Top Bumpmap", 2D) = "bump" {}
		_BumpAmountTop ("Top Bump amount", Range(0, 3)) = 1
		_DetailScaleTop ("Top Detail Texture Scale", Range(0, 3)) = 1
		_ScaleTop ("Top Texture Scale", Float) = 1
		[Header(Side Textures)] _MainTexSide ("Side Base Texture", 2D) = "white" {}
		_SpecularSide ("Side Specular", Vector) = (0.5,0.5,0.5,0.5)
		_SpecularMapSide ("Side Specular Map", 2D) = "white" {}
		_SmoothnessSide ("Side Smoothness", Range(0, 1)) = 0.5
		_DetailTexSide ("Side Detail Texture", 2D) = "white" {}
		_BumpMapSide ("Side Bumpmap", 2D) = "bump" {}
		_BumpAmountSide ("Side Bump amount", Range(0, 3)) = 1
		_DetailScaleSide ("Side Detail Texture Scale", Range(0, 3)) = 1
		_ScaleSide ("Side Texture Scale", Float) = 1
		[Header(The Rest)] _DetailDistance ("Detail Distance", Range(0, 100)) = 10
		_BedrockTex ("Bedrock Texture (RGB)", 2D) = "white" {}
		_BedrockScale ("Bedrock Texture Scale", Float) = 1
		_BedrockLevel ("Bedrock Level", Float) = 0
		_BedrockStrength ("Bedrock Strength", Range(0, 1)) = 1
		_Offset ("Offset", Float) = -600
		_NoiseMap ("BedRockNoise", 2D) = "white" {}
		_OreTex ("Ore Texture (RGB)", 2D) = "white" {}
		_OreTexScale ("Ore Texture Scale", Float) = 1
		_OreColor ("Color Around Ore", Vector) = (1,1,1,1)
		_OreBrightness ("Brightness of Ore Color", Range(0, 100)) = 50
		_EmissionStrength ("Emission Strength", Range(0, 1000)) = 0
		_EmissionColor ("Emission Color", Vector) = (0,0,0,1)
		_NoiseScale ("Noise Scale", Range(-50, 50)) = 1
		_NoiseSpeed ("Noise Speed", Range(-1, 1)) = 1
		_WorldOrigin ("World Origin", Vector) = (0,0,0,1)
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
	Fallback "VertexLit"
}
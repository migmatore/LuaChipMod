Shader "Custom/WorldspaceOverlayTerrain" {
	Properties {
		_Tint ("Tint", Vector) = (1,1,1,1)
		_MainTex ("Base Texture", 2D) = "white" {}
		_Specular ("Specular", Vector) = (0.5,0.5,0.5,0.5)
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
		_Scale ("Texture Scale", Float) = 1
		_DetailTex ("Detail Texture", 2D) = "white" {}
		_DetailDistance ("Detail Distance", Range(0, 100)) = 10
		_DetailScale ("Detail Texture Scale", Float) = 1
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_BumpAmount ("Bump amount", Range(0, 3)) = 1
		_BedrockTex ("Bedrock Texture (RGB)", 2D) = "white" {}
		_BedrockScale ("Bedrock Texture Scale", Float) = 1
		_BedrockLevel ("Bedrock Level", Float) = 0
		_BedrockStrength ("Bedrock Strength", Range(0, 1)) = 1
		_Offset ("Offset", Float) = -600
		_OreTex ("Ore Texture (RGB)", 2D) = "white" {}
		_OreTexScale ("Ore Texture Scale", Float) = 1
		_OreColor ("Color Around Ore", Vector) = (1,1,1,1)
		_OreBrightness ("Brightness of Ore Color", Range(0, 100)) = 50
		_WorldOrigin ("World Origin", Vector) = (0,0,0,1)
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
	Fallback "VertexLit"
}
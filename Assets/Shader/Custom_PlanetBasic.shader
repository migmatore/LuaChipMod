Shader "Custom/PlanetBasic" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		[Normal] _NormalTex ("Normal Texture", 2D) = "bump" {}
		_SpecularTex ("Specular Texture", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		[Header(Clouds)] _CloudTex ("Texture", 2D) = "white" {}
		_CloudOpacity ("Opacity", Range(0, 1)) = 0.5
		_CloudShadow ("Shadow", Range(0, 1)) = 0
		_CloudSpeed ("Speed", Float) = 0
		_CloudParallax ("Parallax", Range(0, 0.5)) = 0.01
		_CloudDistortion ("Distortion", Range(0, 1)) = 0
		[Header(Fresnel)] _FresnelColor ("Fresnel", Vector) = (1,1,1,1)
		_RimColorLow ("Rim Low", Vector) = (0,0,0,1)
		_RimColorHigh ("Rim High", Vector) = (0,0,0,1)
		_FresnelPower ("Fresnel Power", Range(0, 10)) = 0.2
		_FresnelEmission ("Fresnel Emission", Range(0, 5)) = 0
		[Header(Variables)] _SunDirection ("Sun Direction", Vector) = (0,0,1,0)
		_SimSpeed ("Sim Speed", Float) = 1
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
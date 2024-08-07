Shader "Custom/Respawn" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		[HDR] _Emission ("Emission", Vector) = (0,0,0,0)
		_MainTex ("Albedo", 2D) = "white" {}
		_Normal ("Normal", 2D) = "bump" {}
		_MetallicSmooth ("Metallic (RGB) Smooth (A)", 2D) = "white" {}
		_AO ("AO", 2D) = "white" {}
		[HDR] _EdgeColor1 ("Edge Color", Vector) = (1,1,1,1)
		_Noise ("Noise", 2D) = "white" {}
		[Toggle] _Use_Gradient ("Use Gradient?", Float) = 1
		_Gradient ("Gradient", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		[PerRendererData] _Cutoff ("Cutoff", Range(0, 1)) = 0
		_EdgeSize ("EdgeSize", Range(0, 1)) = 0.2
		_NoiseStrength ("Noise Strength", Range(0, 1)) = 0.4
		_DisplaceAmount ("Displace Amount", Float) = 1.5
		_cutoff ("cutoff", Range(0, 1)) = 0
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
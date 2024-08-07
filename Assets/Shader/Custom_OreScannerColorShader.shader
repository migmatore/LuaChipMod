Shader "Custom/OreScannerColorShader" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		_FOV ("FOV", Range(0.05, 1)) = 0.1
		_StartDistance ("Start Distance", Range(0, 50)) = 4
		_ScannerInput ("Scanner Input", 3D) = "defaulttexture" {}
		_TabletPosition ("Tablet Position", Vector) = (0,0,0,0)
		_TabletScreenDirection ("Tablet Direction", Vector) = (0,0,0,0)
		_ScanSize ("Scan Size", Range(10, 1000)) = 100
		_Noise ("Noise", Range(0, 1)) = 0.5
		_ScannerInputOffset ("Scanner Input Offset", Vector) = (0,0,0,1)
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
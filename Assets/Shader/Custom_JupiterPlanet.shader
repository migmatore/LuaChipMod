Shader "Custom/JupiterPlanet" {
	Properties {
		_MainTex ("Main Texture", 2D) = "black" {}
		_Brightness ("Brightness", Range(0.1, 1.5)) = 1
		_Normals ("Normal Map", 2D) = "black" {}
		_NormalStrength ("Normal Strength", Range(0, 2)) = 0.6
		_Lights ("Light Map", 2D) = "black" {}
		_SpecMap ("Specular Map", 2D) = "black" {}
		_LightScale ("Night Lights Intensity", Float) = 1
		_Shininess ("Reflection Shininess", Range(0.03, 2)) = 1
		_ReflectionColor ("Reflection Color", Vector) = (0.5,0.5,0.34,1)
		_AtmosNear ("_AtmosNear", Vector) = (0.1686275,0.7372549,1,1)
		_AtmosFar ("_AtmosFar", Vector) = (0.4557808,0.5187039,0.9850746,1)
		_AtmosFalloff ("_AtmosFalloff", Float) = 3
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
	Fallback "Diffuse"
}
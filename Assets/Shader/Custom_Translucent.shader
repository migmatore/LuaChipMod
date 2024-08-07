Shader "Custom/Translucent" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normal (Normal)", 2D) = "bump" {}
		_Color ("Main Color", Vector) = (1,1,1,1)
		_SpecMap ("Spec (RGB) Gloss (A)", 2D) = "white" {}
		_Shininess ("Shininess", Range(0.03, 1)) = 0.078125
		_Thickness ("Thickness (R)", 2D) = "bump" {}
		_Power ("Subsurface Power", Float) = 1
		_Distortion ("Subsurface Distortion", Float) = 0
		_Scale ("Subsurface Scale", Float) = 0.5
		_SubColor ("Subsurface Color", Vector) = (1,1,1,1)
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
	Fallback "Bumped Diffuse"
}
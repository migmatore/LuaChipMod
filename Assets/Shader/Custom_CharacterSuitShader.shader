Shader "Custom/CharacterSuitShader" {
	Properties {
		[NoScaleOffset] _MainTex ("Albedo (RGB)", 2D) = "white" {}
		[NoScaleOffset] _SpecularMap ("Specular", 2D) = "white" {}
		_SpecularHighlights ("Specular Highlights", Range(0, 1)) = 1
		_Smoothness ("Smoothness", Range(0, 1)) = 1
		[NoScaleOffset] _Normal ("Normal", 2D) = "white" {}
		_NormalStrength ("Normal Strength", Range(0, 2)) = 1
		[NoScaleOffset] _MaskTex ("Mask (R)", 2D) = "white" {}
		_MaskColor ("Mask Color", Vector) = (1,1,1,1)
		_Color ("Color", Vector) = (1,1,1,1)
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
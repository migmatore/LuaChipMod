Shader "Custom/3dPrinting" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_ConstructionLevel ("Construction Level", Range(0, 1)) = 1
		_ConstructionCutoffLevel ("Construction Cutoff Level", Range(0, 0.2)) = 0
		_ConstructionColor ("Construction Color", Vector) = (1,1,0,1)
		_Cutoff ("Cutoff", Range(0, 1)) = 0.5
		_ObjectHeight ("Object Height", Float) = 0.28
		_WobbleMagnitude ("Wobble Magnitude", Range(0, 1)) = 0.28
		_WobbleFrequency ("Wobble Frequency", Range(0, 100)) = 0.28
		_WobbleSpeed ("Wobble Speed", Range(0, 20)) = 0.28
		_ObjectPosition ("Objects Position", Vector) = (0,0,0,0)
		[HideInInspector] _MainTex ("Albedo and alpha (RGBA)", 2D) = "white" {}
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
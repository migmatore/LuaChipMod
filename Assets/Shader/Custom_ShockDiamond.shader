Shader "Custom/ShockDiamond" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_FlameColor ("FlameColor", Vector) = (1,1,1,1)
		_RimColor ("Rim Color", Vector) = (1,0,0,1)
		_RimPower ("Rim Power", Range(0, 1)) = 0
		_EmissionPower ("Emission Power", Range(0, 10)) = 1
		_Power ("Engine Power", Range(0, 1)) = 1
		_PatternPower ("Pattern Power", Range(0, 1)) = 0
		_PatternScale ("Pattern Scale", Float) = 10
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		[PerRendererData] _Opacity ("Opacity", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}
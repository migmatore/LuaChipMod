Shader "Custom/WaterTest" {
	Properties {
		_BumpAmt ("Distortion", Range(0.0001, 256)) = 5
		_BumpSpeed ("Speed", Range(-0.5, 0.5)) = 0.1
		_WaveAmt ("Wave", Range(0, 1)) = 0.5
		_BumpMap ("Normalmap1", 2D) = "bump" {}
		_BumpMap2 ("Normalmap2", 2D) = "bump" {}
		_Color ("Water Color", Vector) = (1,1,1,1)
		_Specularity ("Specularity", Range(32, 4024)) = 10
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
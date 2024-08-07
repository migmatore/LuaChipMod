Shader "CameraFilterPack/Colors_HSV" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_HueShift ("HueShift", Range(0, 360)) = 0
		_Sat ("Saturation", Float) = 1
		_Val ("Value", Float) = 1
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
}
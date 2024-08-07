Shader "Custom/SkyboxStars" {
	Properties {
		_MainTex ("Albedo Texture", 2D) = "white" {}
		_TintColor ("Tint Color", Vector) = (1,1,1,1)
		_Brightness ("Brightness", Range(0, 1)) = 1
		_SunPosition ("Sun Position", Vector) = (0,0,0,0)
		_HorizonHeight ("Horizon Height", Float) = 0
		_HorizonFade ("Horizon Fade", Float) = 0
		_Minimum ("Minimum", Range(0, 1)) = 0
		_Atmosphere ("Atmosphere", Range(0, 1)) = 0
		_TwinkleFade ("Twinkle Fade", Range(0, 1)) = 1
		_Magnitude ("Apparent Magnitude", Range(0.9, 5)) = 1
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
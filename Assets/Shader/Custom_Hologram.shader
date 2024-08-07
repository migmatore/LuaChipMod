Shader "Custom/Hologram" {
	Properties {
		[Header(Color)] _Color ("Color", Vector) = (1,0,0,1)
		_MainTex ("MainTexture", 2D) = "white" {}
		[Header(General)] _Brightness ("Brightness", Range(0.1, 6)) = 4
		_Alpha ("Alpha", Range(0, 1)) = 0.097
		_Direction ("Direction", Vector) = (0,1,0,0)
		[Header(Scanlines)] [Toggle] _ScanEnabled ("Scanlines Enabled", Float) = 1
		_ScanTiling ("Scan Tiling", Range(0.01, 1000)) = 160
		_ScanSpeed ("Scan Speed", Range(-2, 2)) = 0
		[Space(10)] [Header(Fresnel)] _FresnelColor ("Fresnel Color", Vector) = (1,1,1,1)
		_FresnelPower ("Fresnel Power", Range(0.1, 10)) = 1.45
		_Offset ("Offset", Float) = -300
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
}
Shader "Custom/Building" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Progress ("Construction Level", Range(0, 1)) = 1
		_ConstructionColor ("Construction Color", Vector) = (1,0,1,1)
		_Cutoff ("Cutoff", Range(0, 1)) = 0.5
		_ObjectHeight ("Object Height", Float) = 0.28
		_ObjectPosition ("Objects Position", Vector) = (0,0,0,0)
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
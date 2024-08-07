Shader "Decal/Mask" {
	Properties {
		_Layer1 ("Layer1", Range(0, 1)) = 0
		_Layer2 ("Layer2", Range(0, 1)) = 0
		_Layer3 ("Layer3", Range(0, 1)) = 0
		_Layer4 ("Layer4", Range(0, 1)) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}
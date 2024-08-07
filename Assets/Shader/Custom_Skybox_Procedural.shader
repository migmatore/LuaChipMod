Shader "Custom/Skybox/Procedural" {
	Properties {
		[KeywordEnum(None, Simple, High Quality)] _SunDisk ("Sun", Float) = 2
		_SunSize ("Sun Size", Range(0, 1)) = 0.04
		_AtmosphereThickness ("Atmoshpere Thickness", Range(0, 5)) = 1
		_SkyTint ("Sky Tint", Vector) = (0.5,0.5,0.5,1)
		_GroundColor ("Ground", Vector) = (0.369,0.349,0.341,1)
		_SkyColor ("Sky Color", Vector) = (0.5,0.5,0.5,1)
		[ToggleOff] _Eclipse ("Eclipse", Float) = 0
		_Exposure ("Exposure", Range(0, 8)) = 1.3
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
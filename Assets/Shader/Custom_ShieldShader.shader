Shader "Custom/ShieldShader" {
	Properties {
		_Opacity ("Opacity", Range(0, 1)) = 0.5
		_ViewDistance ("View Distance", Range(0, 1)) = 0.5
		_Color ("Shield Color", Vector) = (1,1,1,1)
		_LineColor ("Line Color", Vector) = (1,1,1,1)
		_PulseAmount ("Pulse Amount", Range(0, 1)) = 0.5
		_LineThickness ("Line Thickness", Range(0, 1)) = 1
		_LineBrightness ("Line Brightness", Range(0, 1)) = 1
		_PointThickness ("Point Thickness", Range(0, 1)) = 1
		_PointBrightness ("Point Brightness", Range(0, 1)) = 1
		_Texture ("Texture", 2D) = "defaulttexture" {}
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
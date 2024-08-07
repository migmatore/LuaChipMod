Shader "Custom/Blackhole" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_Exit ("Wormhole Exit (Render Texture)", Cube) = "black" {}
		[Toggle] _IsWormhole ("Is Wormhole", Range(0, 1)) = 0
		_LensOnly ("Lens Only", Range(0, 1)) = 0
		_Cutoff ("Lensing Cutoff Value", Range(0, 1)) = 0.01
		_AccretionRadius ("Accretion Disk Radius", Float) = 10
		_AccretionTexture ("Accretion Disk Texture", 2D) = "white" {}
		_Twist ("Accretion Disk Twist", Float) = 1
		_Temperature ("Accretion Disk Base Temperature", Range(0, 4)) = 1
		_Speed ("Accretion Disk Animation Speed", Range(0, 1)) = 0.1
		_Redshift ("Accretion Disk Redshift Effect", Range(0, 1)) = 0.5
		_Emissive ("Emissive", Range(0, 10)) = 0.5
		[Toggle] _Flip ("Flipped Projection Correction", Range(0, 1)) = 1
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
	Fallback "Unlit/Texture"
}
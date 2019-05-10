// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CustomOutline"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Metallic("Metallic", 2D) = "white" {}
		_SmoothnessIntensity("Smoothness Intensity", Range( 0 , 2)) = 0
		_AO("AO", 2D) = "white" {}
		_AlbedoDarkness("AlbedoDarkness", Range( 0 , 10)) = 0
		_ScreenTiling("Screen Tiling", Range( 0 , 20)) = 18
		_Float1("Float 1", Float) = 1.2
		_ScreenIntensity("Screen Intensity", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float _AlbedoDarkness;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _ScreenTiling;
		uniform float _Float1;
		uniform float _ScreenIntensity;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _SmoothnessIntensity;
		uniform sampler2D _AO;
		uniform float4 _AO_ST;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _Normal, uv_Normal ) ,2.0 );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = ( _AlbedoDarkness * tex2D( _Albedo, uv_Albedo ) ).rgb;
			float2 temp_cast_1 = (1.163825).xx;
			float2 temp_cast_2 = (_ScreenTiling).xx;
			float2 uv_TexCoord15 = i.uv_texcoord * temp_cast_2;
			float2 panner20 = ( _Time.z * temp_cast_1 + ( uv_TexCoord15 * 150.0 ));
			float simplePerlin2D21 = snoise( panner20 );
			float2 temp_cast_3 = (4.693237).xx;
			float2 temp_cast_4 = (uv_TexCoord15.y).xx;
			float2 panner16 = ( _Time.x * temp_cast_3 + temp_cast_4);
			float simplePerlin2D18 = snoise( panner16 );
			float4 temp_cast_5 = (simplePerlin2D18).xxxx;
			o.Emission = ( ( simplePerlin2D21 + CalculateContrast(_Float1,temp_cast_5) ) * _ScreenIntensity ).rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float4 tex2DNode4 = tex2D( _Metallic, uv_Metallic );
			o.Metallic = tex2DNode4.r;
			o.Smoothness = ( tex2DNode4.a * _SmoothnessIntensity );
			float2 uv_AO = i.uv_texcoord * _AO_ST.xy + _AO_ST.zw;
			o.Occlusion = tex2D( _AO, uv_AO ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
-407;140;1548;787;1282.129;1040.479;1.029981;True;True
Node;AmplifyShaderEditor.CommentaryNode;32;-1167.81,-1106.324;Float;False;1460.081;845.3661;TV Screen tiling;13;16;15;19;17;24;20;25;26;27;30;31;18;21;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-1441.103,-802.2374;Float;False;Property;_ScreenTiling;Screen Tiling;6;0;Create;True;0;0;False;0;18;10.86;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1088.135,-543.7518;Float;False;Constant;_Speed;Speed;8;0;Create;True;0;0;False;0;4.693237;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-1096.082,-913.939;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;19;-981.2418,-439.9582;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;26;-805.4235,-748.0192;Float;False;Constant;_Float0;Float 0;8;0;Create;True;0;0;False;0;150;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-830.7288,-908.3245;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1023.729,-1056.324;Float;False;Constant;_GrainSpeed;GrainSpeed;8;0;Create;True;0;0;False;0;1.163825;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;16;-703.7078,-654.4556;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-194.9685,-632.8638;Float;False;Property;_Float1;Float 1;7;0;Create;True;0;0;False;0;1.2;1.78;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;18;-435.0251,-686.3094;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;20;-564.7285,-1004.324;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;21;-357.728,-1036.324;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;31;-208.7,-822.8245;Float;False;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;35;225.8267,-12.08856;Float;False;Property;_ScreenIntensity;Screen Intensity;8;0;Create;True;0;0;False;0;0;0.261;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;17.27151,-827.3245;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;424.2728,-143.9992;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-797.0432,-108.5753;Float;False;Property;_AlbedoDarkness;AlbedoDarkness;5;0;Create;True;0;0;False;0;0;0.57;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1271.969,866.1647;Float;False;Property;_SmoothnessIntensity;Smoothness Intensity;3;0;Create;True;0;0;False;0;0;0.47;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-1219.26,334.668;Float;True;Property;_Metallic;Metallic;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1219.142,-40.45271;Float;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-1017.102,754.1085;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-551.4674,27.95477;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;34;705.7617,104.0842;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;3;-1221.593,148.8167;Float;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;2;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-1038.037,959.0906;Float;True;Property;_AO;AO;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;929.118,470.5405;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;CustomOutline;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;36;0
WireConnection;25;0;15;0
WireConnection;25;1;26;0
WireConnection;16;0;15;2
WireConnection;16;2;17;0
WireConnection;16;1;19;1
WireConnection;18;0;16;0
WireConnection;20;0;25;0
WireConnection;20;2;24;0
WireConnection;20;1;19;3
WireConnection;21;0;20;0
WireConnection;31;1;18;0
WireConnection;31;0;30;0
WireConnection;27;0;21;0
WireConnection;27;1;31;0
WireConnection;33;0;27;0
WireConnection;33;1;35;0
WireConnection;5;0;4;4
WireConnection;5;1;6;0
WireConnection;11;0;13;0
WireConnection;11;1;2;0
WireConnection;34;0;33;0
WireConnection;0;0;11;0
WireConnection;0;1;3;0
WireConnection;0;2;34;0
WireConnection;0;3;4;0
WireConnection;0;4;5;0
WireConnection;0;5;7;0
ASEEND*/
//CHKSM=00FFC7344738B1E301A49CB33E278CB1E32A20C0
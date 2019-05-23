// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PlantShader"
{
	Properties
	{
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 15
		_MaxIntensity("MaxIntensity", Range( 0 , 0.015)) = 0
		_WindIntensity("Wind Intensity", Range( 0 , 0.5)) = 0.5
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_BaseColor("BaseColor", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha noshadow exclude_path:deferred vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _WindIntensity;
		uniform float _MaxIntensity;
		uniform sampler2D _BaseColor;
		uniform float4 _BaseColor_ST;
		uniform float _Cutoff = 0.5;
		uniform float _EdgeLength;


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


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 temp_cast_0 = (2.0).xx;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float2 panner73 = ( _Time.x * temp_cast_0 + ase_worldPos.xy);
			float simplePerlin2D75 = snoise( ( panner73 * 0.8 ) );
			float clampResult78 = clamp( ( simplePerlin2D75 - _WindIntensity ) , 0.0 , _MaxIntensity );
			float3 temp_cast_2 = (clampResult78).xxx;
			v.vertex.xyz += temp_cast_2;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BaseColor = i.uv_texcoord * _BaseColor_ST.xy + _BaseColor_ST.zw;
			float4 tex2DNode1 = tex2D( _BaseColor, uv_BaseColor );
			o.Albedo = tex2DNode1.rgb;
			o.Alpha = tex2DNode1.a;
			clip( tex2DNode1.a - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
-652;440;1906;692;836.5491;1182.532;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;36;-870.0944,-1283.965;Float;False;1563.03;664.8443;Comment;8;71;72;73;74;75;76;81;82;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TimeNode;71;-796.8669,-1004.559;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;74;-676.8669,-805.5587;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;72;-626.8669,-1149.559;Float;True;Constant;_WindSpeed;WindSpeed;3;0;Create;True;0;0;False;0;2;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;73;-230.8669,-1059.559;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;82;29.45087,-761.5321;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;0.8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;36.45087,-991.5321;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;77;660.7429,-460.0161;Float;False;Property;_WindIntensity;Wind Intensity;6;0;Create;True;0;0;False;0;0.5;0.5;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;75;173.1331,-1016.559;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;80;751.9755,-869.9935;Float;True;Property;_MaxIntensity;MaxIntensity;5;0;Create;True;0;0;False;0;0;0.008;0;0.015;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;76;516.7429,-1085.016;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;78;1125.743,-987.0161;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;897.2122,-627.0973;Float;True;Property;_BaseColor;BaseColor;8;0;Create;True;0;0;False;0;dd97fbe6623d7084ea30df0dac6fd02d;dd97fbe6623d7084ea30df0dac6fd02d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1616.963,-997.4371;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;PlantShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;0;False;0;Custom;0.5;True;False;0;True;TransparentCutout;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;7;-1;-1;0;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;73;0;74;0
WireConnection;73;2;72;0
WireConnection;73;1;71;1
WireConnection;81;0;73;0
WireConnection;81;1;82;0
WireConnection;75;0;81;0
WireConnection;76;0;75;0
WireConnection;76;1;77;0
WireConnection;78;0;76;0
WireConnection;78;2;80;0
WireConnection;0;0;1;0
WireConnection;0;9;1;4
WireConnection;0;10;1;4
WireConnection;0;11;78;0
ASEEND*/
//CHKSM=E9EAFA06946441AFF6CC33A45B55A797D9CC1731
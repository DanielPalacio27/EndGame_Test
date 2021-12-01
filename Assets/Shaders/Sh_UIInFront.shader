Shader "URP/UIInFront"
{
    Properties
	{
		[PerRenderData] _MainTex("Base Color", COLOR) = (1.0, 1.0, 1.0, 1.0)	

		_Color ("Tint", Color) = (1,1,1,1)
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]
		
        Pass
        {						
			Tags { "LightMode" = "UniversalForward" }
			HLSLPROGRAM
			#pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			float4 _Color;

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 texcoord : TEXCOORD0;
				float4	color : COLOR;
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float2 uv : TEXCOORD1;
				float4	color : COLOR;
			};

			Varyings vert (Attributes input)
			{
				Varyings output = (Varyings)0;
				output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
				output.uv = input.texcoord;
				output.color = input.color;
				return output;
			}

			float4 frag(Varyings input) : SV_TARGET
			{
				float4 finalColor;
				finalColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv) * input.color;
				return float4(finalColor.rgb, finalColor.a);
			}
			ENDHLSL
        }
    }
}


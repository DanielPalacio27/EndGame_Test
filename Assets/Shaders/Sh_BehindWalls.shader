Shader "URP/BehindWalls"
{
    Properties
	{
		_BaseColor("Base Color", COLOR) = (1.0, 1.0, 1.0, 1.0)	
		_Alpha("Alpha", Range(0.0, 1.0)) = 1.0
		_LambertMinThreshold("Lambert Min Threshold", Range(0, 1.0)) = 1.0
        _LambertMaxThreshold("Lambert Max Threshold", Range(0, 1.0)) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "RenderPipeline"="UniversalPipeline"}
		
		Blend One OneMinusSrcAlpha
        LOD 100
		
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
			float4 _BaseColor;

			float _Alpha;
			float _LambertMinThreshold, _LambertMaxThreshold;

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float3 normal : TEXCOORD0;
				float2 uv : TEXCOORD1;
			};

			Varyings vert (Attributes input)
			{
				Varyings output = (Varyings)0;
				output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
				output.normal = TransformObjectToWorldNormal(input.normalOS);
				output.uv = input.texcoord;
				return output;
			}

			float4 frag(Varyings input) : SV_TARGET
			{
				float3 finalColor;

				float NdotL = (dot(_MainLightPosition.xyz, input.normal));
				float halfLambert = NdotL * 0.45 + 0.45;
				float diffuseReflection = smoothstep(_LambertMinThreshold, _LambertMaxThreshold, halfLambert) + 0.1;	

				finalColor.rgb = diffuseReflection * _BaseColor.rgb;
				return float4(finalColor, _Alpha);
			}
			ENDHLSL
        }
    }
}


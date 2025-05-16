Shader "Custom/ScanlineOverlay"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _ScanlineTex ("Scanline Texture", 2D) = "white" {}
        _Strength ("Blend Strength", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        LOD 100
        
        Pass
        {
            ZTest Always
            ZWrite Off
            Cull Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            TEXTURE2D(_ScanlineTex);
            SAMPLER(sampler_ScanlineTex);
            
            float _Strength;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }
            
            float4 frag(v2f i) : SV_Target
            {
                // Sample the main texture (camera render)
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                
                // Sample the scanline texture
                float scanValue = SAMPLE_TEXTURE2D(_ScanlineTex, sampler_ScanlineTex, i.uv).r;
                
                // Apply the scanline effect
                col.rgb = lerp(col.rgb, col.rgb * scanValue, _Strength);
                
                return col;
            }
            ENDHLSL
        }
    }
}
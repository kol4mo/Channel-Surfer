Shader "Custom/VHSGlitchEffect"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Float) = 0.1
        _NoiseStrength ("Noise Strength", Float) = 0.1
        _TimeSpeed ("Glitch Speed", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            Name "VHSGlitch"
            ZTest Always Cull Off ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

           TEXTURE2D_X(_BlitTexture);
           SAMPLER(sampler_BlitTexture);
           sampler2D _NoiseTex;
           float _ScanlineIntensity;
           float _NoiseStrength;
           float _TimeSpeed;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex); // correct transformation
                o.uv = v.uv;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float2 uv = i.uv;
                float time = _Time.y * _TimeSpeed;

                uv.x += sin(uv.y * 800.0 + time * 40.0) * 0.003;
                float scanline = sin(uv.y * 1200.0) * _ScanlineIntensity;

                float noise = tex2D(_NoiseTex, uv * 3.0 + time).r;
                noise = (noise - 0.5) * _NoiseStrength;

                float3 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_BlitTexture, uv).rgb;
                col += scanline + noise;

                return float4(col, 1.0);
            }
            ENDHLSL
        }
    }
}

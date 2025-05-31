Shader "Custom/RetroTV"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Distortion ("Distortion Amount", Range(0, 1)) = 0.2
        _Vignette ("Vignette Strength", Range(0, 1)) = 0.5
        _ScanlineStrength ("Scanline Strength", Range(0, 1)) = 0.3
        _LineCount ("Scanline Count", Range(50, 1000)) = 300
        _ColorOffset ("Color Offset", Range(0, 0.01)) = 0.003
        _EdgeFeather ("Edge Feather", Range(0, 1)) = 0.1
        _NoiseStrength ("Noise Strength", Range(0, 0.2)) = 0.05
        _PixelSize ("Pixel Size", Range(1, 10)) = 2
        _JitterStrength ("Jitter Strength", Range(0, 1)) = 0.005
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Distortion;
            float _Vignette;
            float _ScanlineStrength;
            float _LineCount;
            float _ColorOffset;
            float _EdgeFeather;
            float _NoiseStrength;
            float _PixelSize;
            float _JitterStrength;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv * 2.0 - 1.0;

                // Nonlinear barrel distortion (stronger at edges)
                float dist = length(uv);
                uv += uv * pow(dist, 4) * _Distortion;

                // Circular edge mask
                float mask = smoothstep(1.0, 1.0 - _EdgeFeather, dist);

                uv = (uv + 1.0) * 0.5;

                // Time-based jitter
                float jitterX = (sin(_Time.y * 30.0) * 0.5 + 0.5) * _JitterStrength;
                uv.x += jitterX;

                // Pixelation
                float2 pixelStep = _PixelSize * _MainTex_TexelSize.xy;
                uv = floor(uv / pixelStep) * pixelStep;

                // Only sample inside valid range, else black
                if (uv.x < 0 || uv.x > 1 || uv.y < 0 || uv.y > 1)
                    return float4(0, 0, 0, 1);

                // Chromatic aberration
                float2 offset = float2(_ColorOffset, 0);
                float r = tex2D(_MainTex, uv + offset).r;
                float g = tex2D(_MainTex, uv).g;
                float b = tex2D(_MainTex, uv - offset).b;
                float3 color = float3(r, g, b);

                // Horizontal scanlines
                float scanline = sin(uv.y * _LineCount * 3.1415);
                color *= 1.0 - _ScanlineStrength * (0.5 + 0.5 * scanline);

                // Vignette
                float vignette = smoothstep(0.8, 0.5, dist) * _Vignette;
                color *= 1.0 - vignette;

                // Noise
                float noise = frac(sin(dot(uv.xy + _Time.y, float2(12.9898, 78.233))) * 43758.5453);
                color += (noise - 0.5) * _NoiseStrength;

                // Edge mask fade
                color *= 1.0 - (1.0 - mask);

                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}

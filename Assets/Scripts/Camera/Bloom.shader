Shader "Custom/FakeBlurBloom"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Threshold ("Brightness Threshold", Range(0, 2)) = 1
        _Intensity ("Bloom Intensity", Range(0, 10)) = 2
        _BloomColor ("Bloom Color", Color) = (1, 1, 1, 1)
        _BlurSize ("Blur Size", Range(0, 5)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Threshold;
            float _Intensity;
            fixed4 _BloomColor;
            float _BlurSize;

            fixed4 frag(v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float2 offsets[8] = {
                    float2(-1, 0), float2(1, 0), float2(0, -1), float2(0, 1),
                    float2(-1, -1), float2(1, -1), float2(-1, 1), float2(1, 1)
                };

                float brightness = dot(col.rgb, fixed3(0.299, 0.587, 0.114));
                fixed3 bloomSum = (brightness > _Threshold) ? col.rgb : 0;

                // 简单周围多点采样，假扩散
                for (int j = 0; j < 8; j++)
                {
                    float2 uvOffset = i.uv + offsets[j] * _BlurSize * _MainTex_TexelSize.xy;
                    fixed3 sampleCol = tex2D(_MainTex, uvOffset).rgb;
                    float sampleBrightness = dot(sampleCol, fixed3(0.299, 0.587, 0.114));
                    if (sampleBrightness > _Threshold)
                    {
                        bloomSum += sampleCol;
                    }
                }

                bloomSum /= 9.0; // 自身 + 8 周围平均
                bloomSum *= _BloomColor.rgb * _Intensity;

                fixed3 result = col.rgb + bloomSum;

                return fixed4(result, col.a);
            }
            ENDCG
        }
    }
}

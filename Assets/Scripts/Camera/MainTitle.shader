Shader "UI/MainTitle_RainbowExpand"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GridSize ("Grid Size", Float) = 10
        _LineWidth ("Line Width", Float) = 0.02
        _ScanSpeed ("Scan Speed", Float) = 1
        _ScanWidth ("Scan Width", Float) = 0.1
        _NoiseStrength ("Noise Strength", Range(0, 1)) = 0.3
        _ExpandSpeed ("Expand Speed", Float) = 1
        _ExpandWidth ("Expand Width", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _GridSize;
            float _LineWidth;
            float _ScanSpeed;
            float _ScanWidth;
            float _NoiseStrength;
            float _ExpandSpeed;
            float _ExpandWidth;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float3 HSVtoRGB(float3 c)
            {
                float4 K = float4(1.0, 2.0/3.0, 1.0/3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
            }

            float random(float2 co)
            {
                return frac(sin(dot(co, float2(12.9898,78.233))) * 43758.5453);
            }

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // 网格线
                float2 gridUV = uv * _GridSize;
                float2 lineMask = step(1.0 - _LineWidth, frac(gridUV)) + step(frac(gridUV), _LineWidth);
                float gridLine = clamp(lineMask.x + lineMask.y, 0.0, 1.0);

                // X/Y 双向扫描
                float scanX = smoothstep(0.0, _ScanWidth, abs(frac(uv.x + _Time.y * _ScanSpeed) - 0.5) * 2.0);
                float scanY = smoothstep(0.0, _ScanWidth, abs(frac(uv.y + _Time.y * _ScanSpeed) - 0.5) * 2.0);
                float combinedScan = max(scanX, scanY);

                // 中心扩散扫描
                float2 center = float2(0.5, 0.5);
                float dist = distance(uv, center);
                float expandWave = smoothstep(0.0, _ExpandWidth, abs(frac(dist - _Time.y * _ExpandSpeed)));

                // 电流噪点
                float noise = random(uv * _Time.y) * _NoiseStrength;

                // 动态彩虹色 (hue 按时间+位置变化)
                float hue = frac(_Time.y * 0.2 + uv.x + uv.y);
                float3 rainbowColor = HSVtoRGB(float3(hue, 1, 1));

                // 最终亮度：网格线 * (扫描 + 扩散) + 电流
                float activeLine = gridLine * (combinedScan + expandWave) + noise * gridLine;

                // 彩虹染色
                float3 color = lerp(float3(0, 0, 0), rainbowColor, saturate(activeLine));

                return float4(color, saturate(activeLine) * 0.3);
            }

            ENDCG
        }
    }
}

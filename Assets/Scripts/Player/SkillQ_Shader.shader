Shader "Custom/GlowEdgeSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _GlowColor ("Glow Color", Color) = (0, 1, 1, 1)
        _GlowStrength ("Glow Strength", Range(0, 5)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _GlowColor;
            float _GlowStrength;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texCol = tex2D(_MainTex, i.uv);
                float alpha = texCol.a;

                float edge = smoothstep(0.2, 0.5, alpha);
                fixed4 glow = _GlowColor * (1.0 - edge) * _GlowStrength;

                // 关键修改：让 glow 也跟随主透明度淡出
                glow *= _Color.a;

                fixed4 finalCol = texCol * _Color;
                finalCol.rgb += glow.rgb;
                finalCol.a = texCol.a * _Color.a;

                return finalCol;
            }

            ENDCG
        }
    }
}

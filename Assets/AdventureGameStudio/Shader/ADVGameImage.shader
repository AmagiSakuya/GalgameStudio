Shader "Hidden/ADVGame/Image"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Toggle] _USE_RULE_TEX("Use RuleTex",Float) = 0
        _RuleTex("RuleTex", 2D) = "black" {}
        _SoftRange("SoftRange",Range(0.0,1.0)) = 0.0
        _Progress("Progress",Range(0.0,1.0)) = 0.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _USE_RULE_TEX_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            uniform sampler2D _MainTex;
            uniform sampler2D _RuleTex;
            uniform float _SoftRange;
            uniform float _Progress;

            float4 frag (v2f i) : SV_Target
            {
                float4 texColor = tex2D(_MainTex, i.uv);

                #ifdef _USE_RULE_TEX_ON
                    float maskValue = tex2D(_RuleTex, i.uv).r;
                    float offset = lerp(-_SoftRange, _SoftRange, _Progress);
                    float minValue = _Progress - _SoftRange + offset;
                    float maxValue = _Progress + _SoftRange + offset;
                    float alpha = smoothstep(minValue, maxValue, maskValue);
                #else
                    float alpha = 1.0 -  _Progress;
                #endif

                return float4(texColor.rgb, alpha);
            }
            ENDCG
        }
    }
}

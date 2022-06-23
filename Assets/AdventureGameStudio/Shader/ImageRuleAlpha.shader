Shader "Hidden/ADVGame/ImageRuleAlpha"
{
    Properties
    {
        [HideInInspector] _MainTex ("Texture", 2D) = "black" {}
        [NoScaleOffset] _RuleTex("RuleTex", 2D) = "red" {}
        _SmoothMin("SmoothMin",Range(0.0,1.0)) = 0.0
        _SmoothMax("SmoothMax",Range(0.0,1.0)) = 1.0
        _Pow("Pow",Range(0.0,1.0)) = 0.0
        _Intensity("Intensity",Range(0.0,1.0)) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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
            uniform float _Pow;
            uniform float _SmoothMin;
            uniform float _SmoothMax;
            uniform float _Intensity;

            float4 frag (v2f i) : SV_Target
            {
                float4 texColor = tex2D(_MainTex, i.uv);
                float maskValue = tex2D(_RuleTex, i.uv).r;
                float alpha = smoothstep(_SmoothMin, _SmoothMax, 1.0 - i.uv.g);
                alpha = pow(alpha, _Pow);
                return float4(texColor.rgb , alpha * _Intensity);
            }
            ENDCG
        }
    }
}

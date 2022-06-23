Shader "Hidden/ADVGame/CharacterImage"
{
	Properties
	{
		[HideInInspector] _MainTex("Texture", 2D) = "black" {}
		[Toggle] _USE_RULE_TEX("Use RuleTex",Float) = 0
		[NoScaleOffset] _RuleTex("RuleTex", 2D) = "black" {}
		_SoftRange("SoftRange",Range(0.0,1.0)) = 0.05
		_Progress("Progress",Range(0.0,1.0)) = 1.0

		_BodyTex("BodyTex", 2D) = "black" {}
		_FaceTex("FaceTex", 2D) = "red" {}
		_AppendTex1("AppendTex1", 2D) = "red" {}
		_AppendTex2("AppendTex2", 2D) = "red" {}
		_AppendTex3("AppendTex3", 2D) = "red" {}
	}
		SubShader
	{
		Tags { "Queue" = "Transparent" }
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
				float4 object_pos : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				o.object_pos = v.vertex;
				return o;
			}

			uniform sampler2D _BodyTex;
			uniform float4 _BodyTex_ST;
			uniform sampler2D _FaceTex;
			uniform float4 _FaceTex_ST;
			uniform sampler2D _AppendTex1;
			uniform float4 _AppendTex1_ST;
			uniform sampler2D _AppendTex2;
			uniform float4 _AppendTex2_ST;
			uniform sampler2D _AppendTex3;
			uniform float4 _AppendTex3_ST;

			uniform sampler2D _RuleTex;
			uniform float _SoftRange;
			uniform float _Progress;
			uniform float _USE_RULE_TEX;

			float4 frag(v2f i) : SV_Target
			{
				float4 texColor = tex2D(_BodyTex, float2(i.uv / _BodyTex_ST.xy - float2(_BodyTex_ST.z / _BodyTex_ST.x,_BodyTex_ST.w / _BodyTex_ST.y)));
				float alpha_tex = texColor.a;
				float4 faceColor = tex2D(_FaceTex, float2(i.uv / _FaceTex_ST.xy - float2(_FaceTex_ST.z / _FaceTex_ST.x, _FaceTex_ST.w / _FaceTex_ST.y)));

				float4 appendtex1 = tex2D(_AppendTex1, float2(i.uv / _AppendTex1_ST.xy - float2(_AppendTex1_ST.z / _AppendTex1_ST.x, _AppendTex1_ST.w / _AppendTex1_ST.y)));
				float4 appendtex2 = tex2D(_AppendTex2, float2(i.uv / _AppendTex2_ST.xy - float2(_AppendTex2_ST.z / _AppendTex2_ST.x, _AppendTex2_ST.w / _AppendTex2_ST.y)));
				float4 appendtex3 = tex2D(_AppendTex3, float2(i.uv / _AppendTex3_ST.xy - float2(_AppendTex3_ST.z / _AppendTex3_ST.x, _AppendTex3_ST.w / _AppendTex3_ST.y)));

				texColor = lerp(texColor, faceColor, faceColor.a);
				/*texColor = lerp(texColor, appendtex1, appendtex1.a);
				texColor = lerp(texColor, appendtex2, appendtex2.a);
				texColor = lerp(texColor, appendtex3, appendtex3.a);*/

				float maskValue = tex2D(_RuleTex, i.uv).r;
				float offset = lerp(-_SoftRange, _SoftRange, _Progress);
				float minValue = _Progress - _SoftRange + offset;
				float maxValue = _Progress + _SoftRange + offset;
				float alpha1 = smoothstep(minValue, maxValue, maskValue);
				float alpha2 = 1.0 - _Progress;
				float alpha = _USE_RULE_TEX == 0 ? alpha2 : alpha1;
				alpha = lerp(0, alpha_tex, 1.0 - alpha);
				return float4(texColor.rgb, alpha);
			}
			ENDCG
		}
	}
}

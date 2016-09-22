Shader "Wp/ScrollBackground"{
	Properties{
		_BackgroundTex("BackgroundTex", 2D) = "white" {}
		_BackgroundTexSpeed("BackgroundTexSpeed", Float) = 0.1

		_NearTex("NeargroundTex", 2D) = "white" {}
		_NearTexSpeed("NearTexSpeed", Float) = 0.2

		_Multiplier("Multiplier", Float) = 1.0
	}

	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _BackgroundTex;
			float4 _BackgroundTex_ST;
			sampler2D _NearTex;
			float4 _NearTex_ST;

			float _BackgroundTexSpeed;
			float _NearTexSpeed;

			float _Multiplier;

			struct a2v{
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f{
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
			};

			v2f vert(a2v v){
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.pos);
				o.uv.xy = TRANSFORM_TEX(v.texcoord, _BackgroundTex) + frac(float2(_Time.y * _BackgroundTexSpeed, 0));
				o.uv.zw = TRANSFORM_TEX(v.texcoord, _NearTex) + frac(float2(_Time.y * _NearTexSpeed, 0));
				return o;
			}

			fixed4 frag(v2f i) : SV_Target{
				fixed4 bk = tex2D(_BackgroundTex, i.uv.xy);
				fixed4 near = tex2D(_NearTex, i.uv.zw);

				fixed4 color = lerp(bk, near, near.a);
				color.rgb = color.rgb * _Multiplier;
				return color;
			}
			ENDCG
		}
	}
	Fallback Off
}
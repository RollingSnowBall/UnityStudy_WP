	Shader "Wp/Animation"{
	Properties{
		_MainTex("MainTex", 2D) = "white" {}
		_Row("Row", Float) = 3
		_Col("Col", Float) = 5
		_Speed("Speed", Float) = 1.0
	}

	SubShader{
		Pass{
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Row;
			float _Col;
			float _Speed;

			struct a2v{
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(a2v v){
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.pos);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target{
				float time = _Time.y * _Speed;

				float tileWidth = 1 / _Col;
				float tileHight = 1 / _Row;

				float2 uv;
				uv.x = i.uv.x * tileWidth;
				uv.y = i.uv.y * tileHight;

				float x = floor(time / tileWidth) * tileWidth;
				float y = floor(time) * tileHight;

				return fixed4(tex2D(_MainTex, uv + float2(x, y)).rgb, 0.0);
			} 

			ENDCG
		}
	}
	Fallback Off
}
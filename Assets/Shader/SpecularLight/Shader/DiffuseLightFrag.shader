// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Wp/DiffuseLightFrag"{
    Properties{
        _DiffuseColor("DiffuseColor", Color) = (1, 1, 1, 1)
    }
    SubShader{
        Pass{
            Tags{
                "LightModel" = "ForwardBase"
            }

            CGPROGRAM
            #include "Lighting.cginc"

            #pragma vertex vert
            #pragma fragment frag

            fixed4 _DiffuseColor;

            struct a2v{
                float4 pos : POSITION;
                float4 normal : NORMAL;
            };

            struct v2f{
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            v2f vert(a2v v){
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.pos);
                o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target{
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 diffuse = _LightColor0.rgb * _DiffuseColor.rgb * max(0, dot(worldNormal, worldLightDir));
                fixed3 color = ambient + diffuse;
                return fixed4(color, 1.0);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
Shader "Wp/DiffuseLightVert"{
    Properties{
        _DiffuseColor("DiffuseColor", Color) = (1, 1, 1, 1)
    }

    SubShader{
        Pass{
            Tags{
                "LightModel" = "ForwardBase"
            }

            CGPROGRAM

            fixed4 _DiffuseColor;

            #include "Lighting.cginc"

            #pragma vertex vert
            #pragma fragment frag

            struct a2v{
                float4 pos : POSITION;
                float4 normal : NORMAL;
            };

            struct v2f{
                float4 pos : SV_POSITION;
                fixed3 color : COLOR;
            };

            v2f vert(a2v v){
//              v2f o;
//              o.pos = mul(UNITY_MATRIX_MVP, v.pos);
//              fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
//              fixed3 worldNormal = normalize(mul(v.normal, (float3x3)_World2Object));
////                fixed3 worldNormal = normalize(mul((float3x3)_Object2World, v.normal));
//              fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
//              fixed3 diffuse = _LightColor0.rgb * _DiffuseColor.rgb * max(0, dot(worldNormal, worldLight));
//              o.color = diffuse + ambient;
//              return o;

                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.pos);
                o.color = UNITY_LIGHTMODEL_AMBIENT.xyz + _LightColor0.rgb * _DiffuseColor.rgb * max(0, dot(normalize(mul((fixed3x3)unity_ObjectToWorld, v.normal)), normalize(_WorldSpaceLightPos0.xyz)));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target{
                return fixed4(i.color, 1.0);
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
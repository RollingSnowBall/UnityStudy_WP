Shader "Wp/SpecularLightFrag"{
    Properties{
        _Diffuse("Diffuse", Color) = (1, 1, 1, 1)
        _Specular("Specular", Color) = (1, 1, 1, 1)
        _Gloss("Gloss", Range(1, 256)) = 20
    }

    SubShader{
        Pass{
            Tags{
                "LightMode" = "ForwardBase"
            }

            CGPROGRAM
            #include "Lighting.cginc"

            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Diffuse;
            fixed4 _Specular;
            float _Gloss;

            struct a2v{
                float4 pos : POSITION;
                float4 normal : NORMAL;
            };

            struct v2f{
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert(a2v v){
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.pos);
                o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                o.worldPos = mul(unity_ObjectToWorld, v.pos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target{
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 worldNormal = normalize(i.worldNormal);

                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * max(0, dot(worldNormal, worldLightDir));

                fixed3 reflectDir = reflect(-worldLightDir, worldNormal);
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);

                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(reflectDir, viewDir)), _Gloss);
                return fixed4(UNITY_LIGHTMODEL_AMBIENT.xyz + diffuse + specular, 1.0);
            }
            ENDCG
        }
    }
    Fallback "Specular"
}
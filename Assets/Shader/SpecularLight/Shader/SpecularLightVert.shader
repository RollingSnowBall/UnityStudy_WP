// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Wp/SpecularLightVert"{
    Properties{
        _Diffuse("Diffuse", Color) = (1, 1, 1, 1)
        _Specular("Specular", Color) = (1, 1, 1, 1)
        _Gloss("Gloss", Range(8.0, 256)) = 20
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
                float3 normal : NORMAL;
            };

            struct v2f{
                float4 pos : SV_POSITION;
                fixed3 color : COLOR;
            };

            v2f vert(a2v v){
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.pos);
                fixed3 diffuse = UNITY_LIGHTMODEL_AMBIENT.xyz + _Diffuse.rgb * _LightColor0.rgb * max(0, dot(normalize(mul(unity_ObjectToWorld, v.normal)), normalize(_WorldSpaceLightPos0.xyz)));

                fixed3 worldNormal = normalize(mul(unity_ObjectToWorld, v.normal));
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.pos).xyz);
                fixed3 reflectDir = normalize(reflect(-worldLightDir, worldNormal));
                fixed3 specular = _LightColor0.xyz * _Specular.xyz * pow(saturate(dot(reflectDir, viewDir)), _Gloss);

//              fixed3 specular = _LightColor0.xyz * _Specular.xyz * pow(max(0, dot(normalize(_WorldSpaceCameraPos.xyz - mul(_Object2World, v.pos).xyz), reflect(-normalize(_WorldSpaceLightPos0.xyz), normalize(mul(_Object2World, v.normal))))), _Gloss);


                o.color = diffuse + specular;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target{
                return fixed4(i.color, 1.0);
            }

            ENDCG
        }
    }

    Fallback "Specular" 
}
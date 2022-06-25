Shader "CustomMake/OutLight3"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutLightColor("OutLight Color",Color)=(1,1,1,1)
        _OutLightFactor("OutLight Factor",float)=1
        _AlphaThreshold("Alpha Threshold",float)=0.1
    }
    SubShader
    {
        Tags {"RenderType"="Transparent"  "Queue"="Transparent" "LightMode"="ForwardBase"}
        Cull Off Blend SrcAlpha OneMinusSrcAlpha
        ZTest Always
        CGINCLUDE
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float4 _MainTex_TexelSize;
        float4 _OutLightColor;
        float _OutLightFactor;
        float _AlphaThreshold;

        struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

        ENDCG

        Pass
        {
            
            CGPROGRAM
            #pragma vertex vert 
            #pragma fragment frag

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texcolor = tex2D(_MainTex, i.uv);
                //方案1 抗锯齿效果更好，亮度相对低些
                float sumAlpha=1;
                sumAlpha*=tex2D(_MainTex,i.uv+_MainTex_TexelSize.xy*half2(1,0)*_OutLightFactor).a;
                sumAlpha*=tex2D(_MainTex,i.uv+_MainTex_TexelSize.xy*half2(-1,0)*_OutLightFactor).a;
                sumAlpha*=tex2D(_MainTex,i.uv+_MainTex_TexelSize.xy*half2(0,1)*_OutLightFactor).a;
                sumAlpha*=tex2D(_MainTex,i.uv+_MainTex_TexelSize.xy*half2(0,-1)*_OutLightFactor).a;
                texcolor.rgb=lerp(_OutLightColor.rgb,texcolor.rgb,sumAlpha);
                return texcolor;

                //方案二 抗锯齿效果不如方案一，但描边亮度会更高
                //float sumAlpha=0;
                //sumAlpha+=step(_AlphaThreshold,tex2D(_MainTex,i.uv+_MainTex_TexelSize.xy*half2(1,0)*_OutLightFactor).a);
                //sumAlpha+=step(_AlphaThreshold,tex2D(_MainTex,i.uv+_MainTex_TexelSize.xy*half2(-1,0)*_OutLightFactor).a);
                //sumAlpha+=step(_AlphaThreshold,tex2D(_MainTex,i.uv+_MainTex_TexelSize.xy*half2(0,1)*_OutLightFactor).a);
                //sumAlpha+=step(_AlphaThreshold,tex2D(_MainTex,i.uv+_MainTex_TexelSize.xy*half2(0,-1)*_OutLightFactor).a);
                //return step(_AlphaThreshold,texcolor.w)*texcolor+step(_AlphaThreshold,1-texcolor.w)*saturate(sumAlpha)*_OutLightColor;
            }
            ENDCG
        }
    }
}

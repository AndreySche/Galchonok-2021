
Shader "Custom/TextOutline" {
    Properties {
        _MainTex ("Font Texture", 2D) = "white" {}
        _Color ("Text Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    }

    SubShader {

        Tags {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        Lighting Off Cull Off ZTest Always ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

		// Первый проход реализует цвет фона текстового содержимого и расширяется наружу _OutlineWidth
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                //UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform float4 _MainTex_TexelSize;
            uniform fixed4 _Color;
            uniform fixed4 _OutlineColor;


            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _Color;
                o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
                return o;
            }
			 // Определяем координаты 8 пикселей вокруг каждого пикселя.
            static const float2 dirList[9]={
                float2(-1,-1),float2(0,-1),float2(1,-1),
                float2(-1,0),float2(0,0),float2(1,0),
                float2(-1,1),float2(0,1),float2(1,1)
            };
			 // Получить значение прозрачности позиции dirIndex в dirList.
            float getDirPosAlpha(float index, float2 xy){
				float2 curPos = xy;
                float2 dir = dirList[index];
				float2 dirPos = curPos + dir * _MainTex_TexelSize.xy * 0.6;
                return tex2D(_MainTex, dirPos).a;
            };
			 // Для каждого пикселя передаем параметр фрагмента v2f i, чтобы получить в общей сложности 9 пикселей вокруг субпикселя и самого себя для наложения прозрачности.
			 // Тогда в результате непрозрачная область увеличивается, образуя черную рамку.
            float getShadowAlpha(float2 xy){
                float a = 0;
				float index = 0;
                a += getDirPosAlpha(index, xy);
                a += getDirPosAlpha(index++, xy);
                a += getDirPosAlpha(index++, xy);
                a += getDirPosAlpha(index++, xy);
                a += getDirPosAlpha(index++, xy);
                a += getDirPosAlpha(index++, xy);
                a += getDirPosAlpha(index++, xy);
                a += getDirPosAlpha(index++, xy);
                a += getDirPosAlpha(index++, xy);
                a = clamp(a,0,1);
                return a;
            }


                         // Поскольку при рендеринге текстового содержимого область текста, которая не визуализируется, является прозрачной, то есть прозрачность имеет значение 0,
			 // Итак, пока область с содержимым выходит за пределы области с нулевой прозрачностью на несколько пикселей, она сможет формировать эффект обводки.
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _OutlineColor;
				float2 xy = i.texcoord.xy;
                col.a *= getShadowAlpha(xy);
                return col;
            }
            ENDCG
        }
		 // Второй проход, текстовое содержимое отображается в обычном порядке.
				
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float4 texcoord : TEXCOORD0;
                //UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform float4 _MainTex_TexelSize;
            uniform fixed4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _Color;
                step(v.texcoord, v.vertex.xy);
                o.texcoord = TRANSFORM_TEX(v.texcoord.xy,_MainTex);
                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = i.color;
				col.a = tex2D(_MainTex, i.texcoord).a;
                return col;
            }
            ENDCG
        }
    }
}


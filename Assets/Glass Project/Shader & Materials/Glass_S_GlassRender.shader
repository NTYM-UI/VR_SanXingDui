Shader "Glass/GlassRender"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}//RenderTexture
        _Curvature("Glass Curvature", Range(0.0001,0.2)) = 0.0515
        _ShapeOffectPower("ShapeOffectPower", Range(3,8)) = 5
        _MagnifyingPower("MagnifyingPower", Range(-1,2.5)) = 0.5
        //_a("Mian", float fixed half 2D bump Color) = 1/ "black" {} / "bump" {} / (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode" = "UniversalForward"}

        LOD 100

        Pass //可以理解为 PS 的图层概念，先渲染什么，再渲染什么，用何种方式进行叠加(混合：Blend)
        {
            CGPROGRAM //CG语言：是Shader的一种语言，还有 HLSL GLSL(OpenGL),对应的平台不一

            #pragma vertex vert     //顶点定义名称
            #pragma fragment frag   //片元定义名称

            #include "UnityCG.cginc"//类似于 C# 的 Using

            // 模型的信息是自带的，结构体就是获取 模型 本身的信息，传递给 Vertex(Render)，继续传给 Fragment(片元着色器)；
            struct appdata //Object 2 Vertex
            {
                float4 vertex : POSITION; // POSITION 是Shader的一种语义，只是用来定义顶点信息；
                float2 uv : TEXCOORD0;    // 就是传统意义上的 uv
            };

            struct v2f //Vertex 2 Fragment
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
//==========================================
// 定义自定义的参数（属性：Properties），相当于Shader Graph 将参数拖出来使用
            //定义：RenderTex
            sampler2D _MainTex;
            float4 _MainTex_ST; //tex 的 Tilling 和 Offect

            float _Curvature; //double float fixed 精度 half
            fixed _ShapeOffectPower;
            fixed _MagnifyingPower;
//==========================================
            //顶点着色器；
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); //MVP 矩阵的 API，此行的作用就是将 顶点结构体拿到的 模型顶点参数 转换到 v2f 的结构体中；
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); //把模型的 UV 和 v2f 的 uv 挂钩；
                return o;
            }

            //片元着色器
            fixed4 frag (v2f i) : SV_Target
            {
                    //核心算法：只能领会，但是需要不断的实践和数学理解

                    float2 uv = float2(i.uv.x, i.uv.y - 1); //根据之前 v2f uv 去重新计算一个适用的 二维UV；
                    float2 center = float2(0.5, -0.5);

                    float ax = ((uv.x - center.x) * (uv.x - center.x))/_Curvature + ((uv.y - center.y) * (uv.y - center.y)) /_Curvature;
                    
                    float radius = _ShapeOffectPower;

                    float depth = radius * _MagnifyingPower;

                    float dx = (-depth/radius)*ax + (depth/(radius*radius))*ax*ax;

                    float f = ax + dx;//凸透镜

                    if (ax > radius)
                    {f = ax;}

                    float2 magnifierArea = center + (uv - center) * f / ax; //计算一个放大镜 放大的 范围值

                    fixed4 col = fixed4(tex2D(_MainTex, float2(1, -1) * magnifierArea).rgb, 1);//得到一个最后的颜色；"*" 1.传统上的乘法； 
                    //2.如果用在两个颜色的话，外在表现就是混合；
                    //解释一下啊：
                              //half4 color_1 = (0.5, 0.5, 0.5, 1); rgba : a ==> Alpha
                              //half4 color_2 = (1, 0, 0, 1);
                       // +  ==>  half4 color_3 = color_1 + color_2;  ==>  half4 color_3 = half4(1.5, 0.5, 0.5, 2); false : 原因就是color的定义范围 (0, 0, 0, 0) ==> (1, 1, 1, 1)
                                //half4 color_3 = half4(1, 0.5, 0.5, 1); true 
                       // *  ==>  建议：颜色的问题是 黑白问题，需要不断去尝试实验，几次就知道最后是一个什么结果；
                    // float4 a = float4(1,2,3,4); xyzw
                    //float3 b = a.xyz;  ==>  float3 b = float3(1,2,3);
                    //float3 c = a.yzx;  ==>  float3 c = float3(2,3,1);

                return col;
            }
            ENDCG
        }

        //pass
        // Name "Shadow"
    }

    //默认渲染管线下使用的 SubShader
    //subshader{
    //    Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase"}
    //}
}

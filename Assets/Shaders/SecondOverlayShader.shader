Shader"Custom/SecondOverlayShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TransitionTex ("Transition Texture", 2D) = "white" {}
        _TransitionThreshold ("Transition Threshold", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float2 texcoord : TEXCOORD0;
};

struct v2f
{
    float2 texcoord : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
sampler2D _TransitionTex;
float _TransitionThreshold;

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.texcoord = v.texcoord;
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 texColor = tex2D(_MainTex, i.texcoord);
    half4 transitionColor = tex2D(_TransitionTex, i.texcoord);

                // Calculate the transition value
    half transitionValue = (transitionColor.r + transitionColor.g + transitionColor.b) / 3;

                // Use the transition value and threshold to determine the final color
    half4 finalColor = texColor * (1 - step(_TransitionThreshold, transitionValue));

    return finalColor;
}
            ENDCG
        }
    }
}

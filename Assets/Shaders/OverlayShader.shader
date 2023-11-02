Shader"Custom/OverlayShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
        _OverlayOpacity ("Overlay Opacity", Range(0, 1)) = 0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" }
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
sampler2D _OverlayTex;
float _OverlayOpacity;

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
    half4 overlayColor = tex2D(_OverlayTex, i.texcoord);
    half4 finalColor = lerp(texColor, overlayColor, _OverlayOpacity);
    return finalColor;
}
            ENDCG
        }
    }
}

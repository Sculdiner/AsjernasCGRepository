//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2018 //
/// Shader generate with Shadero 1.9.0                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Customs/CardHighlightShader"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_SourceNewTex_1("_SourceNewTex_1(RGB)", 2D) = "white" { }
_ShadowLight_Precision_1("_ShadowLight_Precision_1", Range(1, 32)) = 27.571
_ShadowLight_Size_1("_ShadowLight_Size_1", Range(0, 16)) = 0.607
_ShadowLight_Color_1("_ShadowLight_Color_1", COLOR) = (1,0.3529412,0,1)
_ShadowLight_Intensity_1("_ShadowLight_Intensity_1", Range(0, 4)) = 2.114
_ShadowLight_PosX_1("_ShadowLight_PosX_1", Range(-1, 1)) = 0
_ShadowLight_PosY_1("_ShadowLight_PosY_1", Range(-1, 1)) = 0
_ShadowLight_NoSprite_1("_ShadowLight_NoSprite_1", Range(0, 1)) = 1
_HdrCreate_Value_1("_HdrCreate_Value_1", Range(-2, 2)) = 0.611
KaleidoscopeUV_PosX_1("KaleidoscopeUV_PosX_1",  Range(-2, 2)) = 0.53
KaleidoscopeUV_PosY_1("KaleidoscopeUV_PosY_1",  Range(-2, 2)) = 0.35
KaleidoscopeUV_Number_1("KaleidoscopeUV_Number_1", Range(0, 6)) = 1.104
_Generate_Fire_PosX_1("_Generate_Fire_PosX_1", Range(-1, 2)) = 0.03
_Generate_Fire_PosY_1("_Generate_Fire_PosY_1", Range(-1, 2)) = 0.3
_Generate_Fire_Precision_1("_Generate_Fire_Precision_1", Range(0, 1)) = 0.069
_Generate_Fire_Smooth_1("_Generate_Fire_Smooth_1", Range(0, 1)) = 0.473
_Generate_Fire_Speed_1("_Generate_Fire_Speed_1", Range(-2, 2)) = 0.182
_AlphaAsAura_Fade_1("_AlphaAsAura_Fade_1", Range(0, 1)) = 1
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15

}

SubShader
{

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

// required for UI.Mask
Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp]
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};

sampler2D _MainTex;
float _SpriteFade;
sampler2D _SourceNewTex_1;
float _ShadowLight_Precision_1;
float _ShadowLight_Size_1;
float4 _ShadowLight_Color_1;
float _ShadowLight_Intensity_1;
float _ShadowLight_PosX_1;
float _ShadowLight_PosY_1;
float _ShadowLight_NoSprite_1;
float _HdrCreate_Value_1;
float KaleidoscopeUV_PosX_1;
float KaleidoscopeUV_PosY_1;
float KaleidoscopeUV_Number_1;
float _Generate_Fire_PosX_1;
float _Generate_Fire_PosY_1;
float _Generate_Fire_Precision_1;
float _Generate_Fire_Smooth_1;
float _Generate_Fire_Speed_1;
float _AlphaAsAura_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float Generate_Fire_hash2D(float2 x)
{
return frac(sin(dot(x, float2(13.454, 7.405)))*12.3043);
}

float Generate_Fire_voronoi2D(float2 uv, float precision)
{
float2 fl = floor(uv);
float2 fr = frac(uv);
float res = 1.0;
for (int j = -1; j <= 1; j++)
{
for (int i = -1; i <= 1; i++)
{
float2 p = float2(i, j);
float h = Generate_Fire_hash2D(fl + p);
float2 vp = p - fr + h;
float d = dot(vp, vp);
res += 1.0 / pow(d, 8.0);
}
}
return pow(1.0 / res, precision);
}

float4 Generate_Fire(float2 uv, float posX, float posY, float precision, float smooth, float speed, float black)
{
uv += float2(posX, posY);
float t = _Time*60*speed;
float up0 = Generate_Fire_voronoi2D(uv * float2(6.0, 4.0) + float2(0, -t), precision);
float up1 = 0.5 + Generate_Fire_voronoi2D(uv * float2(6.0, 4.0) + float2(42, -t ) + 30.0, precision);
float finalMask = up0 * up1  + (1.0 - uv.y);
finalMask += (1.0 - uv.y)* 0.5;
finalMask *= 0.7 - abs(uv.x - 0.5);
float4 result = smoothstep(smooth, 0.95, finalMask);
result.a = saturate(result.a + black);
return result;
}
float2 KaleidoscopeUV(float2 uv, float posx, float posy, float number)
{
uv = uv - float2(posx, posy);
float r = length(uv);
float a = abs(atan2(uv.y, uv.x));
float sides = number;
float tau = 3.1416;
a = fmod(a, tau / sides);
a = abs(a - tau / sides / 2.);
uv = r * float2(cos(a), sin(a));
return uv;
}
float4 HdrCreate(float4 txt,float value)
{
if (txt.r>0.98) txt.r=2;
if (txt.g>0.98) txt.g=2;
if (txt.b>0.98) txt.b=2;
return lerp(saturate(txt),txt, value);
}
float4 AlphaAsAura(float4 origin, float4 overlay, float blend)
{
float4 o = origin;
o = o.a;
if (o.a > 0.99) o = 0;
float4 aura = lerp(origin, origin + overlay, blend);
o = lerp(origin, aura, o);
return o;
}
float4 ShadowLight(sampler2D source, float2 uv, float precision, float size, float4 color, float intensity, float posx, float posy,float fade)
{
int samples = precision;
int samples2 = samples *0.5;
float4 ret = float4(0, 0, 0, 0);
float count = 0;
for (int iy = -samples2; iy < samples2; iy++)
{
for (int ix = -samples2; ix < samples2; ix++)
{
float2 uv2 = float2(ix, iy);
uv2 /= samples;
uv2 *= size*0.1;
uv2 += float2(-posx,posy);
uv2 = saturate(uv+uv2);
ret += tex2D(source, uv2);
count++;
}
}
ret = lerp(float4(0, 0, 0, 0), ret / count, intensity);
ret.rgb = color.rgb;
float4 m = ret;
float4 b = tex2D(source, uv);
ret = lerp(ret, b, b.a);
ret = lerp(m,ret,fade);
return ret;
}
float4 frag (v2f i) : COLOR
{
float4 _ShadowLight_1 = ShadowLight(_SourceNewTex_1,i.texcoord,_ShadowLight_Precision_1,_ShadowLight_Size_1,_ShadowLight_Color_1,_ShadowLight_Intensity_1,_ShadowLight_PosX_1,_ShadowLight_PosY_1,_ShadowLight_NoSprite_1);
float4 HdrCreate_1 = HdrCreate(_ShadowLight_1,_HdrCreate_Value_1);
float2 KaleidoscopeUV_1 = KaleidoscopeUV(i.texcoord,KaleidoscopeUV_PosX_1,KaleidoscopeUV_PosY_1,KaleidoscopeUV_Number_1);
float4 _Generate_Fire_1 = Generate_Fire(KaleidoscopeUV_1,_Generate_Fire_PosX_1,_Generate_Fire_PosY_1,_Generate_Fire_Precision_1,_Generate_Fire_Smooth_1,_Generate_Fire_Speed_1,0);
float4 AlphaAsAura_1 = AlphaAsAura(HdrCreate_1, _Generate_Fire_1, _AlphaAsAura_Fade_1); 
float4 FinalResult = AlphaAsAura_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}

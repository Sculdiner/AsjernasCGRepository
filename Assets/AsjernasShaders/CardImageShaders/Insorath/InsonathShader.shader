//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2018 //
/// Shader generate with Shadero 1.9.0                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Customs/InsonathShader"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_NewTex_3("NewTex_3(RGB)", 2D) = "white" { }
AnimatedMouvementUV_X_1("AnimatedMouvementUV_X_1", Range(-1, 1)) = 0.146
AnimatedMouvementUV_Y_1("AnimatedMouvementUV_Y_1", Range(-1, 1)) = 0
AnimatedMouvementUV_Speed_1("AnimatedMouvementUV_Speed_1", Range(-1, 1)) = 0.1
_Generate_Voronoi_Size_1116("_Generate_Voronoi_Size_1116", Range(0, 128)) = 36.343
_Generate_Voronoi_Seed_1116("_Generate_Voronoi_Seed_1116", Range(1, 2)) = 1.991
_NewTex_4("NewTex_4(RGB)", 2D) = "white" { }
_NewTex_2("NewTex_2(RGB)", 2D) = "white" { }
_Add_Fade_2("_Add_Fade_2", Range(0, 4)) = 1.179
_MaskRGBA_Fade_1("_MaskRGBA_Fade_1", Range(0, 1)) = 0
_FillColor_Color_1("_FillColor_Color_1", COLOR) = (1,0.965566,0.6556604,1)
_FadeToAlpha_Fade_2("_FadeToAlpha_Fade_2", Range(0, 1)) = 0.23
_OperationBlendMask_Fade_1("_OperationBlendMask_Fade_1", Range(0, 1)) = 0
_OperationBlend_Fade_3("_OperationBlend_Fade_3", Range(0, 1)) = 1
AnimatedZoomUV_AnimatedZoomUV_Zoom_1("AnimatedZoomUV_AnimatedZoomUV_Zoom_1", Range(0.2, 4)) = 1
AnimatedZoomUV_AnimatedZoomUV_PosX_1("AnimatedZoomUV_AnimatedZoomUV_PosX_1", Range(-1, 2)) = 0.5
AnimatedZoomUV_AnimatedZoomUV_PosY_1("AnimatedZoomUV_AnimatedZoomUV_PosY_1", Range(-1, 2)) = 0.5
AnimatedZoomUV_AnimatedZoomUV_Intensity_1("AnimatedZoomUV_AnimatedZoomUV_Intensity_1", Range(0, 4)) = 0.2
AnimatedZoomUV_AnimatedZoomUV_Speed_1("AnimatedZoomUV_AnimatedZoomUV_Speed_1", Range(-10, 10)) = 0.85
_NewTex_1("NewTex_1(RGB)", 2D) = "white" { }
_SourceNewTex_1("_SourceNewTex_1(RGB)", 2D) = "white" { }
ZoomUV_Zoom_1("ZoomUV_Zoom_1", Range(0.2, 4)) = 2.893
ZoomUV_PosX_1("ZoomUV_PosX_1", Range(-3, 3)) = 0.1
ZoomUV_PosY_1("ZoomUV_PosY_1", Range(-3, 3)) =0.1
_Generate_Fire_PosX_1("_Generate_Fire_PosX_1", Range(-1, 2)) = 0
_Generate_Fire_PosY_1("_Generate_Fire_PosY_1", Range(-1, 2)) = -0.384
_Generate_Fire_Precision_1("_Generate_Fire_Precision_1", Range(0, 1)) = 0.05
_Generate_Fire_Smooth_1("_Generate_Fire_Smooth_1", Range(0, 1)) = 0.5
_Generate_Fire_Speed_1("_Generate_Fire_Speed_1", Range(-2, 2)) = 0.536
_PremadeGradients_Offset_1("_PremadeGradients_Offset_1", Range(-1, 1)) =0
_PremadeGradients_Fade_1("_PremadeGradients_Fade_1", Range(0, 1)) =1
_PremadeGradients_Speed_1("_PremadeGradients_Speed_1", Range(-2, 2)) =0
_CircleFade_PosX_1("_CircleFade_PosX_1", Range(-1, 2)) = 0.5
_CircleFade_PosY_1("_CircleFade_PosY_1", Range(-1, 2)) = 0.5
_CircleFade_Size_1("_CircleFade_Size_1", Range(-1, 1)) = 0.411
_CircleFade_Dist_1("_CircleFade_Dist_1", Range(0, 1)) = 0.2
_FadeToAlpha_Fade_1("_FadeToAlpha_Fade_1", Range(0, 1)) = 0.646
_Displacement_Value_1("_Displacement_Value_1", Range(-0.3, 0.3)) = 0.027
_Add_Fade_1("_Add_Fade_1", Range(0, 4)) = 1
_OperationBlend_Fade_2("_OperationBlend_Fade_2", Range(0, 1)) = 1
_OperationBlend_Fade_1("_OperationBlend_Fade_1", Range(0, 1)) = 1
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

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" "DisableBatching" = "True"}
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
sampler2D _NewTex_3;
float AnimatedMouvementUV_X_1;
float AnimatedMouvementUV_Y_1;
float AnimatedMouvementUV_Speed_1;
float _Generate_Voronoi_Size_1116;
float _Generate_Voronoi_Seed_1116;
sampler2D _NewTex_4;
sampler2D _NewTex_2;
float _Add_Fade_2;
float _MaskRGBA_Fade_1;
float4 _FillColor_Color_1;
float _FadeToAlpha_Fade_2;
float _OperationBlendMask_Fade_1;
float _OperationBlend_Fade_3;
float AnimatedZoomUV_AnimatedZoomUV_Zoom_1;
float AnimatedZoomUV_AnimatedZoomUV_PosX_1;
float AnimatedZoomUV_AnimatedZoomUV_PosY_1;
float AnimatedZoomUV_AnimatedZoomUV_Intensity_1;
float AnimatedZoomUV_AnimatedZoomUV_Speed_1;
sampler2D _NewTex_1;
sampler2D _SourceNewTex_1;
float ZoomUV_Zoom_1;
float ZoomUV_PosX_1;
float ZoomUV_PosY_1;
float _Generate_Fire_PosX_1;
float _Generate_Fire_PosY_1;
float _Generate_Fire_Precision_1;
float _Generate_Fire_Smooth_1;
float _Generate_Fire_Speed_1;
float _PremadeGradients_Offset_1;
float _PremadeGradients_Fade_1;
float _PremadeGradients_Speed_1;
float _CircleFade_PosX_1;
float _CircleFade_PosY_1;
float _CircleFade_Size_1;
float _CircleFade_Dist_1;
float _FadeToAlpha_Fade_1;
float _Displacement_Value_1;
float _Add_Fade_1;
float _OperationBlend_Fade_2;
float _OperationBlend_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float4 UniColor(float4 txt, float4 color)
{
txt.rgb = lerp(txt.rgb,color.rgb,color.a);
return txt;
}
float2 ZoomUV(float2 uv, float zoom, float posx, float posy)
{
float2 center = float2(posx, posy);
uv -= center;
uv = uv * zoom;
uv += center;
return uv;
}
float4 Circle_Fade(float4 txt, float2 uv, float posX, float posY, float Size, float Smooth)
{
float2 center = float2(posX, posY);
float dist = 1.0 - smoothstep(Size, Size + Smooth, length(center - uv));
txt.a *= dist;
return txt;
}
float4 DisplacementUV(float2 uv,sampler2D source,float x, float y, float value)
{
return tex2D(source,lerp(uv,uv+float2(x,y),value));
}
float4 OperationBlend(float4 origin, float4 overlay, float blend)
{
float4 o = origin; 
o.a = overlay.a + origin.a * (1 - overlay.a);
o.rgb = (overlay.rgb * overlay.a + origin.rgb * origin.a * (1 - overlay.a)) / (o.a+0.0000001);
o.a = saturate(o.a);
o = lerp(origin, o, blend);
return o;
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
float4 Color_PreGradients(float4 rgba, float4 a, float4 b, float4 c, float4 d, float offset, float fade, float speed)
{
float gray = (rgba.r + rgba.g + rgba.b) / 3;
gray += offset+(speed*_Time*20);
float4 result = a + b * cos(6.28318 * (c * gray + d));
result.a = rgba.a;
result.rgb = lerp(rgba.rgb, result.rgb, fade);
return result;
}
float3 Generate_Voronoi_hash3(float2 p, float seed)
{
float3 q = float3(dot(p, float2(127.1, 311.7)),
dot(p, float2(269.5, 183.3)),
dot(p, float2(419.2, 371.9)));
return frac(sin(q) * 43758.5453 * seed);
}
float4 Generate_Voronoi(float2 uv, float size,float seed, float black)
{
float2 p = floor(uv*size);
float2 f = frac(uv*size);
float k = 1.0 + 63.0*pow(1.0, 4.0);
float va = 0.0;
float wt = 0.0;
for (int j = -2; j <= 2; j++)
{
for (int i = -2; i <= 2; i++)
{
float2 g = float2(float(i), float(j));
float3 o = Generate_Voronoi_hash3(p + g, seed)*float3(1.0, 1.0, 1.0);
float2 r = g - f + o.xy;
float d = dot(r, r);
float ww = pow(1.0 - smoothstep(0.0, 1.414, sqrt(d)), k);
va += o.z*ww;
wt += ww;
}
}
float4 result = saturate(va / wt);
result.a = saturate(result.a + black);
return result;
}
float2 AnimatedMouvementUV(float2 uv, float offsetx, float offsety, float speed)
{
speed *=_Time*50;
uv += float2(offsetx, offsety)*speed;
uv = fmod(uv,1);
return uv;
}
float4 OperationBlendMask(float4 origin, float4 overlay, float4 mask, float blend)
{
float4 o = origin; 
origin.rgb = overlay.a * overlay.rgb + origin.a * (1 - overlay.a) * origin.rgb;
origin.a = overlay.a + origin.a * (1 - overlay.a);
origin.a *= mask;
origin = lerp(o, origin,blend);
return origin;
}
float4 FadeToAlpha(float4 txt,float fade)
{
return float4(txt.rgb, txt.a*fade);
}

float2 AnimatedZoomUV(float2 uv, float zoom, float posx, float posy, float radius, float speed)
{
float2 center = float2(posx, posy);
uv -= center;
zoom -= radius * 0.1;
zoom += sin(_Time * speed * 20) * 0.1 * radius;
uv = uv * zoom;
uv += center;
return uv;
}
float4 frag (v2f i) : COLOR
{
float4 NewTex_3 = tex2D(_NewTex_3, i.texcoord);
float2 AnimatedMouvementUV_1 = AnimatedMouvementUV(i.texcoord,AnimatedMouvementUV_X_1,AnimatedMouvementUV_Y_1,AnimatedMouvementUV_Speed_1);
float4 _Generate_Voronoi_1116 = Generate_Voronoi(AnimatedMouvementUV_1,_Generate_Voronoi_Size_1116,_Generate_Voronoi_Seed_1116,0);
float4 NewTex_4 = tex2D(_NewTex_4, i.texcoord);
float4 NewTex_2 = tex2D(_NewTex_2, i.texcoord);
NewTex_4 = lerp(NewTex_4,NewTex_4*NewTex_4.a + NewTex_2*NewTex_2.a,_Add_Fade_2);
_Generate_Voronoi_1116.a = lerp(NewTex_4.r * _Generate_Voronoi_1116.a, (1 - NewTex_4.r) * _Generate_Voronoi_1116.a,_MaskRGBA_Fade_1);
float4 FillColor_1 = UniColor(_Generate_Voronoi_1116,_FillColor_Color_1);
float4 FadeToAlpha_2 = FadeToAlpha(FillColor_1,_FadeToAlpha_Fade_2);
NewTex_4 = lerp(NewTex_4,NewTex_4*NewTex_4.a + NewTex_2*NewTex_2.a,_Add_Fade_2);
float4 OperationBlendMask_1 = OperationBlendMask(FadeToAlpha_2, NewTex_3, NewTex_4, _OperationBlendMask_Fade_1); 
float4 OperationBlend_3 = OperationBlend(NewTex_3, OperationBlendMask_1, _OperationBlend_Fade_3); 
float2 AnimatedZoomUV_1 = AnimatedZoomUV(i.texcoord,AnimatedZoomUV_AnimatedZoomUV_Zoom_1,AnimatedZoomUV_AnimatedZoomUV_PosX_1,AnimatedZoomUV_AnimatedZoomUV_PosY_1,AnimatedZoomUV_AnimatedZoomUV_Intensity_1,AnimatedZoomUV_AnimatedZoomUV_Speed_1);
float4 NewTex_1 = tex2D(_NewTex_1,AnimatedZoomUV_1);
float2 ZoomUV_1 = ZoomUV(i.texcoord,ZoomUV_Zoom_1,ZoomUV_PosX_1,ZoomUV_PosY_1);
float4 _Generate_Fire_1 = Generate_Fire(ZoomUV_1,_Generate_Fire_PosX_1,_Generate_Fire_PosY_1,_Generate_Fire_Precision_1,_Generate_Fire_Smooth_1,_Generate_Fire_Speed_1,0);
float4 _PremadeGradients_1 = Color_PreGradients(_Generate_Fire_1,float4(0.55,0.55,0.55,1),float4(0.8,0.8,0.8,1),float4(0.29,0.29,0.29,1),float4(0.54,0.59,0.6900001,1),_PremadeGradients_Offset_1,_PremadeGradients_Fade_1,_PremadeGradients_Speed_1);
float4 _CircleFade_1 = Circle_Fade(_PremadeGradients_1,ZoomUV_1,_CircleFade_PosX_1,_CircleFade_PosY_1,_CircleFade_Size_1,_CircleFade_Dist_1);
float4 FadeToAlpha_1 = FadeToAlpha(_CircleFade_1,_FadeToAlpha_Fade_1);
float4 _Displacement_1 = DisplacementUV(AnimatedZoomUV_1,_SourceNewTex_1,FadeToAlpha_1.r*FadeToAlpha_1.a,FadeToAlpha_1.g*FadeToAlpha_1.a,_Displacement_Value_1);
_Displacement_1 = lerp(_Displacement_1,_Displacement_1*_Displacement_1.a + FadeToAlpha_1*FadeToAlpha_1.a,_Add_Fade_1);
float4 OperationBlend_2 = OperationBlend(NewTex_1, _Displacement_1, _OperationBlend_Fade_2); 
float4 OperationBlend_1 = OperationBlend(OperationBlend_3, OperationBlend_2, _OperationBlend_Fade_1); 
float4 FinalResult = OperationBlend_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}

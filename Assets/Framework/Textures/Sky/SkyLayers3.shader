// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 

Shader "Ace/Sky/LayeredSkySingleUV" {
Properties {
	_MainTex ("Base layer (RGB)", 2D) = "white" {}
	_MainTex2 ("Base layer (RGB)", 2D) = "white" {}
 
	_ScrollX ("Base layer Scroll speed X", Float) = 1.0
	_ScrollY ("Base layer Scroll speed Y", Float) = 0.0
	_Scroll2X ("2nd layer Scroll speed X", Float) = 1.0
	_Scroll2Y ("2nd layer Scroll speed Y", Float) = 0.0
	_AMultiplier ("Layer Multiplier", Float) = 0.5
	_ColorTint("Color",Color) = (1,1,1,1)
}

SubShader {
	Tags {   "RenderType"="Opaque" }
	
	Lighting Off Fog { Mode Off }
	ZWrite On
	
	LOD 100
	
		
	CGINCLUDE
 
	#include "UnityCG.cginc"
	sampler2D _MainTex,_MainTex2;
	 

	float4 _MainTex_ST;
	 
	
	float _ScrollX;
	float _ScrollY;
	float _Scroll2X;
	float _Scroll2Y;
	float _AMultiplier;
	float4 _ColorTint;
	
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	 
		fixed4 color : TEXCOORD2;		
	};

	
	v2f vert (appdata_full v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord.xy,_MainTex) + frac(float2(_ScrollX, _ScrollY) * _Time);
		 
		o.color = _ColorTint * _AMultiplier;

		return o;
	}
	ENDCG


	Pass {
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest		
		fixed4 frag (v2f i) : COLOR
		{
			fixed4 o;
			fixed4 tex = tex2D (_MainTex, i.uv);
			fixed4 tex2 = tex2D (_MainTex2, i.uv);
			
			o = (tex * tex2) * i.color;
			
			return o;
		}
		ENDCG 
	}	
}
}

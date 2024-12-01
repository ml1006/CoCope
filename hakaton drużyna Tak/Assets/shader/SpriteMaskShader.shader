// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteMaskShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _MaskTargetX("Mask Target X", Float) = 1
		[PerRendererData] _MaskTargetY("Mask Target Y", Float) = 0
		[PerRendererData] _RenderDistance("Render Distance", Float) = 1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				fixed4 worldPos : WORLDPOS;
			};
			
			fixed4 _Color;
			float _MaskTargetX;
			float _MaskTargetY;
			float _RenderDistance;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.worldPos = mul(unity_ObjectToWorld, fixed4(IN.vertex.x, IN.vertex.y, 0, 1));
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
				fixed4 mas = fixed4(_MaskTargetX, _MaskTargetY, IN.worldPos.z, 1);

				float2 d = IN.worldPos - mas;
				float ang = acos(dot(d, float2(0.0,1.0)) / (length(d)));
				float offset = sin(ang / 0.02 + _Time.w) * 0.0125 * _RenderDistance;
				float dist = length(d) - offset;

				if (dist <= _RenderDistance)
				{
					float luma = dot(c.rgb, fixed3(0.299, 0.587, 0.114)) * 0.9;
					float t = 1.2 - smoothstep(_RenderDistance - 2.0, _RenderDistance, dist);

					luma *= t > 1.0 ? 1.0 : t;

					c.rgb = lerp(float3(luma, luma, luma), c.rgb, smoothstep(_RenderDistance - 0.5, _RenderDistance, dist));
				}

				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}

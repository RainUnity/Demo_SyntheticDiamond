// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
#ifndef HARD_SURFACE_PRO_SCREEN_SPACE_REFELCTION_INCLUDED
#define HARD_SURFACE_PRO_SCREEN_SPACE_REFELCTION_INCLUDED

struct v2f
                {
					float4 pos : SV_POSITION;
					float4 viewDir : TEXCOORD0;
					float2 uv_BumpMap : TEXCOORD1;
					float3 TtoV0  : TEXCOORD2;
					float3 TtoV1  : TEXCOORD3;
					float4 GPscreenPos : TEXCOORD4;
                }; 
		
		// define variables
			
			#ifdef HardsurfaceNormal
				sampler2D _BumpMap;
			#endif
			
			sampler2D _GrabTexture;
			
			#ifdef HardsurfaceSpecular
					sampler2D _Spec_Gloss_Reflec_Masks;
			#endif			
			
			#ifdef HardsurfaceCutOut
				sampler2D _MainTex;
				fixed _Cutoff;
			#endif
			
			fixed  _Metalics;
			float4 _BumpMap_ST;
			 half  _FrezPow;
			 half  _FrezFalloff;
			fixed4 _Color;
			fixed  _Reflection;
			
			
			
				
		v2f vert (appdata_tan v)
                {
                    v2f o;
					
					o.pos = UnityObjectToClipPos (v.vertex);
					
					// calculates distance to vert, and base blend for realtime relection
					float3 vertworldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
					float3 viewVect = _WorldSpaceCameraPos - vertworldpos;
					half dist = length (viewVect);
					o.viewDir.w = min (1/dist,dist/1);

					o.uv_BumpMap = TRANSFORM_TEX( v.texcoord, _BumpMap );
					
					o.GPscreenPos.xy = (float2(o.pos.x, o.pos.y) + o.pos.w) * 0.5;
					o.GPscreenPos.zw = o.pos.zw;
					 
                    TANGENT_SPACE_ROTATION; 
					o.viewDir.xyz = mul( rotation, ObjSpaceViewDir( v.vertex ) );
					o.TtoV0 = mul(rotation, UNITY_MATRIX_IT_MV[0].xyz);
					o.TtoV1 = mul(rotation, UNITY_MATRIX_IT_MV[1].xyz);

                    return o;
                }
				
		half4 frag( v2f IN ) : COLOR 
		{
		
		  #ifdef HardsurfaceCutOut
			clip ((_Color.a * tex2D(_MainTex,IN.uv_BumpMap).a) - _Cutoff);
		  #endif
			
		  #ifdef HardsurfaceNormal
				fixed3 Bumpnormal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		   #else
				fixed3 Bumpnormal = fixed3(0,0,1);
		   #endif			

			// screenspaced normal direction
			fixed2 vn;
			vn.x = dot(IN.TtoV0, Bumpnormal);
			vn.y = dot(IN.TtoV1, Bumpnormal);
			
			// lays over the normal in the direction its biased to.
			fixed2 absvn = abs(vn);
			fixed maxvn = max(absvn.x,absvn.y);
			maxvn = 1 / maxvn;
			vn = vn * maxvn;
			
			
			#ifdef SHADER_API_D3D9
				vn.y *= _ProjectionParams.x;
			#endif
			
			// calculate view to normal angle
			IN.viewDir.xyz = normalize(IN.viewDir.xyz);
			fixed SurfAngle= dot(IN.viewDir.xyz,Bumpnormal);
			SurfAngle = pow(SurfAngle,2 * (1-IN.viewDir.w));
		
			// screen space coords for capture
			fixed2 grabTexcoord = IN.GPscreenPos.xy / IN.GPscreenPos.w; 
			
			// warps the screensapce coords by the screenspace normals, which are tweeked by distance and viewangle
			fixed2 screenuv;
			screenuv = saturate(grabTexcoord + (vn * SurfAngle * (1-SurfAngle * IN.viewDir.w)));

			fixed2 screenedges =  abs(screenuv * 2 - 1);
			screenedges = 1 - (screenedges * screenedges);
			
			// fresnal RefCapture falloff
			fixed frez = pow(1-SurfAngle,_FrezFalloff)*_FrezPow;
			
			#ifdef SHADER_API_D3D9
				screenuv.y = 1 - screenuv.y;
			#endif	
			
			//Reflection texture fetch
			fixed3 RefCapture = tex2D(_GrabTexture,screenuv).rgb;
			RefCapture *= (_Reflection + frez);
			
			//StaticMask to make sure Screenspaced reflections dont show where it warps.
			fixed StaticMask = 1 - saturate(SurfAngle * 1.15);

			// Reflection Mask
			#ifdef HardsurfaceSpecular
					fixed ReflecMask = tex2D(_Spec_Gloss_Reflec_Masks, IN.uv_BumpMap).g;
			#endif
			
			// uses the lowest falloff value available
			#ifdef HardsurfaceSpecular
					fixed RefCaptureblend = min(_Reflection + frez, StaticMask) * ReflecMask;
			#else
				fixed RefCaptureblend = min(_Reflection + frez, StaticMask);
			#endif		
						
			//Reflection Luminance
			fixed ReflectiveLum = saturate(Luminance(RefCapture));
			ReflectiveLum *= ReflectiveLum * ReflectiveLum;
			
			// based on metalics value use a tinted or untinted RefCapture
			
			fixed3 reflectionColor = lerp (RefCapture, RefCapture *_Color.rgb, (1 - ReflectiveLum) *_Metalics);
			fixed reflectionOpacity = RefCaptureblend * min(screenedges.x,screenedges.y);
			return float4 (reflectionColor,reflectionOpacity);
		}
	  
#endif

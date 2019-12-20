// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
#ifndef HARD_SURFACE_PRO_SCREEN_SPACE_REFRACTION_INCLUDED
#define HARD_SURFACE_PRO_SCREEN_SPACE_REFRACTION_INCLUDED

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
			sampler2D _BumpMap;
			sampler2D _GrabTexture;
			#ifdef HardsurfaceCutOut
				sampler2D _MainTex;
				fixed _Cutoff;
			#endif
			float4 _BumpMap_ST;
			fixed4 _Color;
			fixed _Density;
			
		
		v2f vert (appdata_tan v)
                {
                    v2f o;
					
					o.pos = UnityObjectToClipPos (v.vertex);
					
					// calculates distance to vert, and base blend for realtime refraction
					float3 vertworldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
					float3 viewVect = _WorldSpaceCameraPos - vertworldpos;
					half dist = length (viewVect);
					o.viewDir.w = 1/dist;
					
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

			//screenspaced normal direction
			fixed2 vn;
			vn.x = dot(IN.TtoV0, Bumpnormal);
			vn.y = dot(IN.TtoV1, Bumpnormal);
			
			#ifdef SHADER_API_D3D9
				vn.y *= -_ProjectionParams.x;
			#endif
	
			// Surface angle to Camera
			IN.viewDir.xyz = normalize(IN.viewDir.xyz);
			fixed SurfAngle= dot(IN.viewDir.xyz,Bumpnormal)*.6666; //.66 allows for magnify effect;
			
			// screen space coords for capture
			fixed2 grabTexcoord = IN.GPscreenPos.xy / IN.GPscreenPos.w; 
			
			#ifdef SHADER_API_D3D9
				grabTexcoord.y = 1-grabTexcoord.y;
			#endif
			
			// Referaction Warp
			vn =  vn * (1-SurfAngle) * -_Density * IN.viewDir.w;
			
			// Screen edge blend 
			fixed2 screenedges =  abs(saturate(grabTexcoord + vn) * 2 - 1);
			screenedges = 1 - ((screenedges - 0.6666) * 2.99940011997);
			vn = vn * screenedges;
			
			// chromatic Aberation
			fixed3 refAberationColor;

			#ifdef ShaderModel3
				fixed4 dispersionRatio = fixed4(.12,.12,.06,.06) * _Density;
				dispersionRatio = 1 - dispersionRatio;
				dispersionRatio.xyzw *= fixed4(vn,vn);
				
				refAberationColor.r = tex2D(_GrabTexture,grabTexcoord+dispersionRatio.xy).r;
				refAberationColor.g = tex2D(_GrabTexture,grabTexcoord+dispersionRatio.zw).g;
				refAberationColor.b = tex2D(_GrabTexture,grabTexcoord+vn).b;
			#else
				refAberationColor = tex2D(_GrabTexture,grabTexcoord+vn).rgb;
			#endif
			
			return float4 (refAberationColor,1);

		}
	  
#endif

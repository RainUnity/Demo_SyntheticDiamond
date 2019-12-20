//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
#ifndef HARD_SURFACE_INCLUDED
#define HARD_SURFACE_INCLUDED

samplerCUBE _Cube;

#ifdef HardsurfaceNormal
	sampler2D _BumpMap;
#endif

#ifdef HardsurfaceDiffuse
	sampler2D _MainTex;	
#endif

#ifdef HardsurfaceCutOut
#else
	fixed _EdgeAlpha;
#endif

#ifdef HardsurfaceSpecular
	sampler2D _Spec_Gloss_Reflec_Masks;
#endif

fixed4 _Color;

#ifdef ShaderModel3
	 half _Shininess;
	fixed _Gloss;
#endif

fixed _Reflection;
fixed _FrezPow;
 half _FrezFalloff;
fixed _Metalics;

struct Input {
	float3 worldNormal;
	float2 uv_MainTex;
	# ifdef HardsurfaceNormal
		float2 uv_BumpMap;
	#endif
	float3 worldRefl;
	INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) {

	#ifdef HardsurfaceSpecular
				fixed3 SpecGlossRefMask = tex2D(_Spec_Gloss_Reflec_Masks, IN.uv_BumpMap).rgb;
		#ifdef ShaderModel3

			_Shininess *= SpecGlossRefMask.r * SpecGlossRefMask.r * SpecGlossRefMask.r;
			_Gloss *= SpecGlossRefMask.g;
		#endif
	#endif
	
	#ifdef HardsurfaceNormal
		#ifdef HardsurfaceBackface
			#ifdef SHADER_API_D3D9 
				#ifdef ShaderModel3
					o.Normal = UnpackNormal(tex2Dlod(_BumpMap, float4(IN.uv_MainTex,1,(1-_Shininess)*3)));
				#else
					o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
				#endif
			#else
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			#endif
		#else
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		#endif
	#else
		   o.Normal = fixed3(0,0,1);
	#endif
		
	//Calculate Reflection vector
	fixed3 worldRefl = normalize(WorldReflectionVector (IN, o.Normal));
	fixed3 worldNormal = WorldNormalVector  (IN, o.Normal);
	
	fixed SurfAngle= abs(dot (worldRefl,worldNormal));
	fixed frez = pow(1-SurfAngle,_FrezFalloff) ;
	
	
	#ifdef HardsurfaceDiffuse
		_Color*= tex2D(_MainTex, IN.uv_MainTex);
	#endif
	
	#ifdef HardsurfaceCutOut
	#else
		_Color.a *= (1-(frez * _EdgeAlpha ));
	#endif
	
	frez*=_FrezPow;
	
	#ifdef HardsurfaceBackface
		fixed Diffusion = (1-_Color.a);
		Diffusion *= Diffusion;
		_Reflection *= Diffusion;
		frez *= Diffusion;
		#ifdef ShaderModel3
			_Shininess *=  Diffusion ;
			_Gloss *= Diffusion;
			_SpecColor.rgb = lerp(_Color.rgb, _SpecColor.rgb, Diffusion); 
		#endif	
	#endif
	
	// Decalre variables for platform specific variations;
	fixed3 CubeTex;
	
	#ifdef SHADER_API_D3D9
		#ifdef ShaderModel3
			CubeTex = texCUBElod(_Cube,float4(worldRefl,(1-_Shininess)*6)).rgb;
		#else
			CubeTex = texCUBE(_Cube,worldRefl).rgb;
		#endif
	#else
		CubeTex = texCUBE(_Cube,worldRefl).rgb;
	#endif
	
	// Add Fresnel Falloff to Reflection & calculate Reflection Luminace for Blending with diffuse
	_Reflection += frez;
	
	#ifdef HardsurfaceSpecular
			_Reflection *= SpecGlossRefMask.b;
	#endif 
		
	
	CubeTex.rgb *= _Reflection;
	
	fixed ReflectiveLum = saturate(Luminance(CubeTex.rgb));
	ReflectiveLum *= ReflectiveLum;

	#ifdef ShaderModel3
		o.Specular = _Shininess ;
		#ifdef HardsurfaceSpecular
			o.Gloss = _Gloss + (frez * SpecGlossRefMask.g);
		#else
			o.Gloss = _Gloss + frez;
		#endif
	#endif
		
	o.Emission = lerp (CubeTex.rgb, CubeTex.rgb *_Color.rgb, (1-ReflectiveLum) *_Metalics);
	
	#ifdef HardsurfaceCutOut
		o.Alpha =  _Color.a;
	#else
		o.Alpha =  min(_Color.a + ReflectiveLum,1);
	#endif	

	o.Albedo =  _Color.rgb *  (1-saturate(_Reflection)); 	
		
}

#endif

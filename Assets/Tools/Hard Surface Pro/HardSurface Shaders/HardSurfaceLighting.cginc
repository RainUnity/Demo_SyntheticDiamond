//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
#ifndef HARD_SURFACE_LIGHTING_INCLUDED
#define HARD_SURFACE_PRO_LIGHTING_INCLUDED


inline fixed4 LightingBlinnPhongHardsurfaceFront (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
{
	fixed3 h = normalize (lightDir + viewDir);
	
	fixed diff = dot (s.Normal, lightDir);
	fixed difftrans = abs(diff) * (1 - s.Alpha); 
	diff = max(diff,difftrans);

	half nh = dot (s.Normal, h);
	nh = max(0,nh);
	
	half spec = pow (nh, s.Specular*128.0) * s.Gloss;
	
	fixed4 c;
	c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * _SpecColor.rgb * spec) * (atten * 2);
	c.a = (s.Alpha + _LightColor0.a * _SpecColor.a * spec * atten);
	return c;
}

inline fixed4 LightingBlinnPhongHardsurfaceBack (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
{
	fixed3 h = normalize (lightDir + viewDir);
		
	fixed diff = dot (-s.Normal, lightDir);
	fixed difftrans = abs(diff) * (1 - s.Alpha); 
	diff = max(diff,difftrans);

	half nh = dot (-s.Normal, h);
	nh = max(0,nh);
	
	half spec = pow (nh, s.Specular*128.0) * s.Gloss;
	
	fixed4 c;
	c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * _SpecColor.rgb * spec ) * (atten * 2);
	c.a = (s.Alpha + _LightColor0.a * _SpecColor.a * spec * atten);
	return c;
}

 half4 LightingBlinnPhongHardsurfaceFrontSM2 (SurfaceOutput s, half3 lightDir, half atten) {
	
		  fixed NdotL = dot (s.Normal, lightDir);
		  fixed Ntrans = abs(NdotL) * (1- s.Alpha) ;
		  fixed NFinal = max(NdotL,Ntrans);
          fixed4 c;
          c.rgb = s.Albedo * _LightColor0.rgb * (NFinal * atten * 2);
          c.a = s.Alpha;
          return c;
      }
	  
 half4 LightingBlinnPhongHardsurfaceBackSM2 (SurfaceOutput s, half3 lightDir, half atten) {
		  fixed NdotL = dot (-s.Normal, lightDir);
		  fixed Ntrans = abs(NdotL) * (1 - s.Alpha) ;
		  fixed NFinal = max(NdotL,Ntrans);
          fixed4 c;
          c.rgb = s.Albedo * _LightColor0.rgb * (NFinal * atten * 2);
          c.a = s.Alpha;
          return c;
      }

#endif

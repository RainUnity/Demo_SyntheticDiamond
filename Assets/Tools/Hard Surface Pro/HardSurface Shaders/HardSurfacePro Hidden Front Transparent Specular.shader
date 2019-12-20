
Shader "Hidden/Hardsurface Pro Front Transparent Specular"{

SubShader { // Shader Model 3

	// Front Faces pass
	
	//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	 zwrite off Cull Back Ztest Lequal
	 Blend SrcAlpha OneMinusSrcAlpha
	 colormask RGBA
	
	CGPROGRAM
		
		#define HardsurfaceDiffuse
		#define HardsurfaceNormal
		#define HardsurfaceSpecular
		#define ShaderModel3
		
		#pragma target 3.0
		#include "HardSurfaceLighting.cginc"	
		#include "HardSurface.cginc"	
		#pragma surface surf BlinnPhongHardsurfaceFront
		

	ENDCG
	
}

SubShader { // Shader Model 2

	// Front Faces pass
	
	//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	 zwrite off Cull Back Ztest Lequal
	 Blend SrcAlpha OneMinusSrcAlpha
	 colormask RGBA
	
	CGPROGRAM
	
		#define HardsurfaceDiffuse
		#define HardsurfaceNormal
		#define HardsurfaceSpecular
	
		#include "HardSurfaceLighting.cginc"	
		#include "HardSurface.cginc"	
		#pragma surface surf BlinnPhongHardsurfaceFrontSM2  
		

	ENDCG
	
}
	Fallback "Diffuse"
}

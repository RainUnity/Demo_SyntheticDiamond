
Shader "Hidden/Hardsurface Pro Front Cut Out Opaque Specular"{

SubShader { // Shader Model 3

	// Front Faces pass
	
	//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	 zwrite on Cull Back Ztest Lequal
	 Blend off
	 colormask RGBA
	
	CGPROGRAM
		
		#define HardsurfaceDiffuse
		#define HardsurfaceNormal
		#define HardsurfaceSpecular
		#define ShaderModel3
		#define HardsurfaceCutOut
		
		#pragma target 3.0
		#include "HardSurfaceLighting.cginc"	
		#include "HardSurface.cginc"	
		#pragma surface surf BlinnPhongHardsurfaceFront alphatest:_Cutoff fullforwardshadows
		

	ENDCG
	
}

SubShader { // Shader Model 2

	// Front Faces pass
	
	//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	 zwrite on Cull Back Ztest Lequal
	 Blend off
	 colormask RGBA
	
	CGPROGRAM
	
		#define HardsurfaceDiffuse
		#define HardsurfaceNormal
	    #define HardsurfaceSpecular Out of Instructions :( 65 ALU
		#define HardsurfaceCutOut

	
		#include "HardSurfaceLighting.cginc"	
		#include "HardSurface.cginc"	
		#pragma surface surf BlinnPhongHardsurfaceFrontSM2 alphatest:_Cutoff  

	ENDCG
	
}
	Fallback "Diffuse"
}

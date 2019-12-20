//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
Shader "Hidden/Hardsurface Pro Front Opaque Bump"{

SubShader { // Shader model 3

	// Front Faces pass
	
	//Tags {"Queue"="Geometry" "RenderType"="Opaque" "IgnoreProjector"="False" }
	 zwrite on Cull Back Ztest Lequal
	 Blend off
	 colormask RGBA
	
	CGPROGRAM
		
		#define HardsurfaceOpaque
		#define HardsurfaceDiffuse
		#define HardsurfaceNormal
		#define ShaderModel3
		
		#pragma target 3.0
		#include "HardSurfaceLighting.cginc"	
		#include "HardSurface.cginc"	
		#pragma surface surf BlinnPhongHardsurfaceFront fullforwardshadows
		

	ENDCG
	
}

SubShader { // Shader Model 2

	// Front Faces pass
	
	//Tags {"Queue"="Geometry" "RenderType"="Opaque" "IgnoreProjector"="False" }
	 zwrite on Cull Back Ztest Lequal
	 Blend off
	 colormask RGBA
	
	CGPROGRAM
		
		#define HardsurfaceOpaque
		#define HardsurfaceDiffuse
		#define HardsurfaceNormal

		
		#include "HardSurfaceLighting.cginc"	
		#include "HardSurface.cginc"	
		#pragma surface surf BlinnPhongHardsurfaceFrontSM2  
		

	ENDCG
	
}
	Fallback "Diffuse"
}

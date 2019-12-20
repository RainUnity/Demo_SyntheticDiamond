//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
Shader "Hidden/Hardsurface Pro Back Cut Out Diffuse"{
SubShader { // Shader Model 3

	// Back Faces pass
	
	//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	 zwrite off Cull Front Ztest Lequal
	 Blend SrcAlpha OneMinusSrcAlpha
	 colormask RGBA
	
	CGPROGRAM
		
		#define HardsurfaceDiffuse
		#define HardsurfaceBackface
		#define ShaderModel3
		#define HardsurfaceCutOut

		#pragma target 3.0
		#include "HardSurfaceLighting.cginc"	
		#include "HardSurface.cginc"	
		#pragma surface surf BlinnPhongHardsurfaceBack alphatest:_Cutoff
		

	ENDCG
	
}

SubShader { // Shader Model 2

	// Back Faces pass
	
	//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	 zwrite off Cull Front Ztest Lequal
	 Blend SrcAlpha OneMinusSrcAlpha
	 colormask RGBA
	
	CGPROGRAM
		
		#define HardsurfaceDiffuse
		#define HardsurfaceCutOut
		//#define HardsurfaceBackface // out of arithmatic calculations 67 :(

		#include "HardSurfaceLighting.cginc"	
		#include "HardSurface.cginc"	
		#pragma surface surf BlinnPhongHardsurfaceBackSM2 alphatest:_Cutoff  
		

	ENDCG
	
}
	Fallback "Diffuse"
}

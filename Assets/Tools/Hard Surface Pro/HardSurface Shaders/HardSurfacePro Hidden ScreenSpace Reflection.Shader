//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
Shader "Hidden/Hardsurface Pro ScreenSpace Reflection"{

// ShaderModel3

SubShader {
	 
	 Pass
	 {
		 Name "SSREFLECTION"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
		
		#define ShaderModel3
  	
		#pragma target 3.0 
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
		
	Pass
	 {
		 Name "SSREFLECTIONBUMP"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
		
		#define ShaderModel3
		#define HardsurfaceNormal
		
		#pragma target 3.0 
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
		
	Pass
	 {
		 Name "SSREFLECTIONSPECULAR"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
  	
		#define ShaderModel3
		#define HardsurfaceNormal
		#define HardsurfaceSpecular
		
		#pragma target 3.0 
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
	 }
		
	Pass
	 {
		 Name "SSREFLECTIONCUTOUT"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
		
		#define ShaderModel3
		#define HardsurfaceCutOut 
  	
		#pragma target 3.0
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
		
	Pass
	 {
		 Name "SSREFLECTIONBUMPCUTOUT"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
		
		#define ShaderModel3
		#define HardsurfaceNormal
		#define HardsurfaceCutOut 
		
		#pragma target 3.0 
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
		
	
	Pass
	 {
		 Name "SSREFLECTIONSPECULARCUTOUT"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
  	
		#define ShaderModel3
		#define HardsurfaceNormal
		#define HardsurfaceSpecular
		#define HardsurfaceCutOut 
		
		#pragma target 3.0 
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
	}
	
	// ShaderModel2 
	
	SubShader {
	 
	 Pass
	 {
		 Name "SSREFLECTION"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
  	
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}

		
	Pass
	 {
		 Name "SSREFLECTIONBUMP"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
  	
		#define HardsurfaceNormal
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
	
	Pass
	 {
		 Name "SSREFLECTIONSPECULAR"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
  	
		#define HardsurfaceNormal
		#define HardsurfaceSpecular
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
	
	Pass
	 {
		 Name "SSREFLECTIONCUTOUT"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
		
		#define HardsurfaceCutOut 
  	
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}

		
	Pass
	 {
		 Name "SSREFLECTIONBUMPCUTOUT"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
  	
		#define HardsurfaceNormal
		#define HardsurfaceCutOut 
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
	
	Pass
	 {
		 Name "SSREFLECTIONSPECULARCUTOUT"
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
		zwrite off Ztest Lequal Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		colormask RGBA
       
		CGPROGRAM
  	
		#define HardsurfaceNormal
		#define HardsurfaceSpecular
		#define HardsurfaceCutOut 
		
		#pragma vertex vert
		#pragma fragment frag
		
		#pragma fragmentoption ARB_precision_hint_fastest
		
		#include "UnityCG.cginc"
		#include "HardSurfaceProScreenSpaceReflection.cginc"
		 
		ENDCG
	  
		}
	}
	Fallback "Diffuse"
}


//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
Shader "Hidden/Hardsurface Pro ScreenSpace Refraction"{

//Shader Model 3
SubShader {

		 Pass
		 {
			 Name "SSREFRACTION"
			 
			//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
			zwrite on Ztest Lequal Cull back
			Blend off
			colormask RGBA
		   
			CGPROGRAM
		
			#pragma target 3.0
			#define ShaderModel3
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#include "UnityCG.cginc"	
			#include "HardSurfaceProScreenSpaceRefraction.cginc"
			 
			ENDCG
		  
			}
		
		Pass
		 {
			 Name "SSREFRACTIONCUTOUT"

			//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
			zwrite on Ztest Lequal Cull back
			Blend off
			colormask RGBA
		   
			CGPROGRAM
		
			#pragma target 3.0
			 #define ShaderModel3
			 
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma fragmentoption ARB_precision_hint_fastest
			 
			#define HardsurfaceCutOut
			
			#include "UnityCG.cginc"
			#include "HardSurfaceProScreenSpaceRefraction.cginc"
			 
			ENDCG
		  
			}
			
		Pass
		 {
			 Name "SSREFRACTIONBUMP"

			//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
			zwrite on Ztest Lequal Cull back
			Blend off
			colormask RGBA
		   
			CGPROGRAM
		
			#pragma target 3.0
			#define ShaderModel3
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma fragmentoption ARB_precision_hint_fastest
			 
			#define HardsurfaceNormal
			
			#include "UnityCG.cginc"
			#include "HardSurfaceProScreenSpaceRefraction.cginc"
			 
			ENDCG
		  
			}
		
		Pass
		 {
			 Name "SSREFRACTIONBUMPCUTOUT"

			//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
			zwrite on Ztest Lequal Cull back
			Blend off
			colormask RGBA
		   
			CGPROGRAM
		
			#pragma target 3.0
			#define ShaderModel3
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma fragmentoption ARB_precision_hint_fastest
			 
			#define HardsurfaceNormal
			#define HardsurfaceCutOut
			
			#include "UnityCG.cginc"
			#include "HardSurfaceProScreenSpaceRefraction.cginc"
			 
			ENDCG
		  
			}
		}
		
		//Shader Model 2
		SubShader {

		 Pass
		 {
			 Name "SSREFRACTION"
			 
			//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
			zwrite on Ztest Lequal Cull back
			Blend off
			colormask RGBA
		   
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#include "UnityCG.cginc"	
			#include "HardSurfaceProScreenSpaceRefraction.cginc"
			 
			ENDCG
		  
			}		 
			
		Pass
		 {
			 Name "SSREFRACTIONCUTOUT"
			 
			//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
			zwrite on Ztest Lequal Cull back
			Blend off
			colormask RGBA
		   
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma fragmentoption ARB_precision_hint_fastest
			#define HardsurfaceCutOut
			
			#include "UnityCG.cginc"	
			#include "HardSurfaceProScreenSpaceRefraction.cginc"
			 
			ENDCG
		  
			}
			
		Pass
		 {
			 Name "SSREFRACTIONBUMP"

			//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
			zwrite on Ztest Lequal Cull back
			Blend off
			colormask RGBA
		   
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma fragmentoption ARB_precision_hint_fastest
			 
			#define HardsurfaceNormal
			
			#include "UnityCG.cginc"
			#include "HardSurfaceProScreenSpaceRefraction.cginc"
			 
			ENDCG
		  
			}
			
		Pass
		 {
			 Name "SSREFRACTIONBUMPCUTOUT"

			//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "Always"}
			zwrite on Ztest Lequal Cull back
			Blend off
			colormask RGBA
		   
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma fragmentoption ARB_precision_hint_fastest
			 
			#define HardsurfaceNormal
			#define HardsurfaceCutOut
			
			#include "UnityCG.cginc"
			#include "HardSurfaceProScreenSpaceRefraction.cginc"
			 
			ENDCG
		  
			}
		}
		Fallback "Diffuse"
	}

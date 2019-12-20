//Hard Surface Shader Package, Written for the Unity engine by Bruno Rime: http://www.behance.net/brunorime brunorime@gmail.com
Shader "HardSurface/Hardsurface Pro/Cut Out/Opaque Bump"{

Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (1, 1, 1, 1)
	_Shininess ("Shininess", Range (0.01, 3)) = 1.5
	_Gloss("Gloss", Range (0.00, 1)) = .5
	_Reflection("Reflection", Range (0.00, 1)) = 0.5
	_Cube ("Reflection Cubemap", Cube) = "Black" { TexGen CubeReflect }
	_FrezPow("Fresnel Reflection",Range(0,2)) = .25
	_FrezFalloff("Fresnal/EdgeAlpha Falloff",Range(0,10)) = 4
	_Metalics("Metalics",Range(0,1)) = .5
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	
	_MainTex ("Diffuse(RGB) Alpha(A)",2D) = "White" {}
	_BumpMap ("Normalmap", 2D) = "Bump" {}

	
}

	SubShader {
		

		// Front Faces
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		UsePass "Hidden/Hardsurface Pro Front Cut Out Opaque Bump/FORWARD"
		
		// Reflection Capture / Will self reflect with capture placed here
		GrabPass {
		Tags {"LightMode" = "Always"}
		}
		
		// Reflection Pass
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" "LightMode" = "Always"}
		UsePass "Hidden/Hardsurface Pro ScreenSpace Reflection/SSREFLECTIONBUMPCUTOUT"
	} 
	FallBack "Transparent/Cutout/VertexLit"
}

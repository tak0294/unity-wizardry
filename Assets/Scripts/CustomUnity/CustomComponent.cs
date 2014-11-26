using UnityEngine;
using System.Collections;

public static class CustomComponent {
	
	public static void addUIButtonMessage(GameObject set_obj,string functionName, GameObject targer, UIButtonMessage.Trigger Trigger ){
		set_obj.AddComponent<UIButtonMessage>();
		set_obj.GetComponent<UIButtonMessage>().trigger      = Trigger;
		set_obj.GetComponent<UIButtonMessage>().functionName = functionName;
		set_obj.GetComponent<UIButtonMessage>().target       = targer;
	}
	
	public static void updateRendererShader(GameObject set_obj,string shader_name){
		Shader shader = Shader.Find(shader_name);
		//Mobile/Unlit (Supports Lightmap)
		//Diffuse
		
		Renderer[] renderers = set_obj.GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in renderers){
			renderer.material.shader = shader;
		}
	}
	
	public static void executeShakeAnchor(GameObject obj,float x,float y){
		UIAnchor[] anchors = obj.GetComponentsInChildren<UIAnchor>();
		foreach(UIAnchor anchor in anchors){
			anchor.relativeOffset = new Vector2(x,y);
		}
	}
	
	public static void executeAlphaLabel(GameObject obj,float a){
		UILabel[] labels = obj.GetComponentsInChildren<UILabel>();
		foreach(UILabel label in labels){
			label.alpha = a;
		}
	}
}

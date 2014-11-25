using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CustomObject{
		
	private static List<Dictionary<string, object>> ScrollButtonList
		= new List<Dictionary<string, object>>();
	

	// MainPanel直下にプレハブを配置
	public static GameObject createObject(string name, string path){
		GameObject MainPanel = GameObject.Find("View2DRoot/Camera/Anchor/MainPanel");
		
		return createObject(name, path, MainPanel);
	}
	
	// 指定のオブジェクト直下にプレハブを配置
	public static GameObject createObject(string name, string path, GameObject parentObj){
		GameObject check_obj     = GameObject.Find( name );
		if(check_obj != null){
			return check_obj;
		}
		
		GameObject load_object   = MonoBehaviour.Instantiate(Resources.Load("Prefabs/"+ path)) as GameObject;
		load_object.name = name;
		CustomTransform.setTranslateObject(parentObj, load_object);
		
		load_object.SetActive(true);
		
		return load_object;
	}
	
	public static GameObject createObject(string name, string path, GameObject ParentObj,bool isCheck){
		GameObject check_obj     = GameObject.Find( name );
		if(check_obj != null && isCheck){
			return check_obj;
		}
		
		GameObject load_object   = MonoBehaviour.Instantiate(Resources.Load("Prefabs/"+ path)) as GameObject;
		load_object.name = name;
		CustomTransform.setTranslateObject(ParentObj, load_object);
		
		load_object.SetActive(true);
		
		return load_object;
	}

	// 指定のオブジェクト直下に空オブジェクトを配置
	public static GameObject createNewObject(string name, GameObject parentObj){
		GameObject obj = new GameObject(name);
	
		CustomTransform.setTranslateObject(parentObj, obj);
		CustomTransform.setPosition(obj, 0, 0, 0);
		CustomTransform.setScale(obj, 1, 1, 1);
		
		return obj;
	}
	
	// オブジェクトの削除
	public static void clear(string name){
		GameObject dest_obj = GameObject.Find(name);
		if(dest_obj != null){
			dest_obj.SetActive(false);
			MonoBehaviour.Destroy(dest_obj);
			Resources.UnloadUnusedAssets();
		}
	}
	
	// オブジェクトの削除
	public static void clear(GameObject obj){
		if(obj != null){
			obj.SetActive(false);
			MonoBehaviour.Destroy(obj);
			Resources.UnloadUnusedAssets();
		}
	}
	
	
	
	// 子オブジェクトを取得
	public static GameObject getChild(GameObject obj, string child_name){
		Transform trans = obj.transform.Find(child_name);
		
		if(! trans){
			return null;
		}
		return trans.gameObject;
	}
	
}


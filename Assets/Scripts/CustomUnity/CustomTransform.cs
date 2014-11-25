using UnityEngine;
using System.Collections;

public static class CustomTransform
{
		
	public static void addPositionX (GameObject obj, float x, bool isLocal)
	{
		Vector3 set_position = new Vector3 ();
		if (isLocal) {
			set_position = obj.transform.localPosition;
			obj.transform.localPosition = new Vector3 (set_position.x + x, set_position.y, set_position.z);
		} else {
			set_position = obj.transform.position;
			obj.transform.position = new Vector3 (set_position.x + x, set_position.y, set_position.z);
		
		}
	}
			
	public static void addPositionY (GameObject obj, float y, bool isLocal)
	{
		Vector3 set_position = new Vector3 ();
		if (isLocal) {
			set_position = obj.transform.localPosition;
			obj.transform.localPosition = new Vector3 (set_position.x, set_position.y + y, set_position.z);
		} else {
			set_position = obj.transform.position;
			obj.transform.position = new Vector3 (set_position.x, set_position.y + y, set_position.z);
		}
	}
		
	public static void addPositionZ (GameObject obj, float z, bool isLocal)
	{
		Vector3 set_position = new Vector3 ();
		if (isLocal) {
			set_position = obj.transform.localPosition;
			obj.transform.localPosition = new Vector3 (set_position.x, set_position.y, set_position.z + z);
		} else {
			set_position = obj.transform.position;
			obj.transform.position = new Vector3 (set_position.x, set_position.y, set_position.z + z);
		}
	}
		
	public static void addRotateX (GameObject obj, float x)
	{
		float set_x = obj.transform.localRotation.x + x;
		float set_y = obj.transform.localRotation.y;
		float set_z = obj.transform.localRotation.z;
		obj.transform.Rotate (set_x, set_y, set_z);
	}
		
	public static void addRotateY (GameObject obj, float y)
	{
		float set_x = obj.transform.localRotation.x;
		float set_y = obj.transform.localRotation.y + y;
		float set_z = obj.transform.localRotation.z;
		obj.transform.Rotate (set_x, set_y, set_z);
	}
		
	public static void addRotateZ (GameObject obj, float z)
	{
		float set_x = obj.transform.localRotation.x;
		float set_y = obj.transform.localRotation.y;
		float set_z = obj.transform.localRotation.z + z;
		obj.transform.Rotate (set_x, set_y, set_z);
	}
		
	public static void setPosition (GameObject obj, float x, float y, float z, bool isLocal)
	{
		if (isLocal) {
			obj.transform.localPosition = new Vector3 (x, y, z);
		} else {
			obj.transform.position = new Vector3 (x, y, z);
		}
	}
	
	public static void setPositionX (GameObject obj, float x, bool isLocal)
	{
		Vector3 set_position = new Vector3 ();
		if (isLocal) {
			set_position = obj.transform.localPosition;
			obj.transform.localPosition = new Vector3 (x, set_position.y, set_position.z);
		} else {
			set_position = obj.transform.position;
			obj.transform.position = new Vector3 (x, set_position.y, set_position.z);
		}
	}
	
	public static void setPositionY (GameObject obj, float y, bool isLocal)
	{
		Vector3 set_position = new Vector3 ();
		if (isLocal) {
			set_position = obj.transform.localPosition;
			obj.transform.localPosition = new Vector3 (set_position.x, y, set_position.z);
		} else {
			set_position = obj.transform.position;
			obj.transform.position = new Vector3 (set_position.x, y, set_position.z);
		}
	}
	
		
	public static void setPosition (GameObject obj, float x, float y, float z)
	{
		obj.transform.localPosition = new Vector3 (x, y, z);
	}

	public static void setPosition (GameObject set_obj, GameObject target, bool isLocal)
	{
		if(isLocal){
			set_obj.transform.localPosition = target.transform.localPosition;
		}else{
			set_obj.transform.position = target.transform.position;
		}
	}
	
	public static void setRotate (GameObject obj, float x, float y, float z)
	{
		obj.transform.Rotate (x, y, z);
	}

	public static void setScale (GameObject obj, float x, float y, float z)
	{
		obj.transform.localScale = new Vector3 (x, y, z);
	}
		
	public static void setTranslateObject (GameObject parent_obj, GameObject child_obj)
	{
		Transform tmp_transform = child_obj.transform;
		float position_x = tmp_transform.localPosition.x;
		float position_y = tmp_transform.localPosition.y;
		float position_z = tmp_transform.localPosition.z;
		float scale_x = tmp_transform.localScale.x;
		float scale_y = tmp_transform.localScale.y;
		float scale_z = tmp_transform.localScale.z;
			
		child_obj.transform.parent = parent_obj.transform;
		child_obj.transform.localPosition = new Vector3 (position_x, position_y, position_z);
		child_obj.transform.localScale = new Vector3 (scale_x, scale_y, scale_z);
	}
		
}


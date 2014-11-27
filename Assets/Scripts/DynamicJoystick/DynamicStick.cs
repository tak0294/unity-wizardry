using UnityEngine;
using System.Collections;

public class DynamicStick : MonoBehaviour 
{
	Vector2 joyOffset = new Vector2(0,0);
	Vector2 joyCenter = Vector2.zero;
	Vector2 screenSize = new Vector2();
	
	public GameObject joyStick;
	public Transform stickTrans;
	UIAnchor joyAnchor = null;
	bool isTouching = false;
	public float maxOffset = 80f;
	
	
	public bool hasDynamic = true;
	Vector2 originalOffset;
	
	//This methord can let you get how mach the joystick handle deviate the joystick center.
	//Return a vector2 between (0f,0f) and (1f,1f).
	public Vector2 _JoyOffset
	{
		get
		{
			return Vector2.Scale(joyOffset, new Vector2(1/maxOffset,1/maxOffset));
		}
	}


	void Start () 
	{
		if(maxOffset <=0)
			maxOffset = 80;
		joyAnchor = joyStick.GetComponent<UIAnchor>();
		screenSize =new Vector2(Screen.width, Screen.height);
		originalOffset = joyAnchor.relativeOffset;
		
		if(hasDynamic)
			joyStick.SetActiveRecursively(false);
		else
		{
			joyStick.SetActiveRecursively(true);
			
			joyCenter = new Vector2(joyAnchor.relativeOffset.x * Screen.width, joyAnchor.relativeOffset.y * Screen.height);
		}

	}
	//Reverse the dynamic status.
	public void SwitchDynamic()
	{
		hasDynamic = !hasDynamic;
		if(!hasDynamic)
		{
				joyStick.SetActiveRecursively(true);
				joyAnchor.relativeOffset = originalOffset;
				joyCenter = new Vector2(originalOffset.x * Screen.width, originalOffset.y * Screen.height);
		}
	}
	
	//Set the dynamic status.
	public void SetDynamic(bool isDynamic)
	{
		hasDynamic = isDynamic;
		if(!hasDynamic)
		{
				joyStick.SetActiveRecursively(true);
				joyAnchor.relativeOffset = originalOffset;
				joyCenter = new Vector2(originalOffset.x * Screen.width, originalOffset.y * Screen.height);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
#if UNITY_EDITOR
		if(hasDynamic)
		{
			if(Input.GetMouseButton(0))
			{
				Vector2 tempXY = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
				if(!isTouching)
				{
					joyCenter = tempXY;
					joyStick.SetActiveRecursively(true);
					joyAnchor.relativeOffset = new Vector2(joyCenter.x/Screen.width,joyCenter.y/Screen.height);
					isTouching = true;
				}
				joyOffset = tempXY - joyCenter;
				
				if(joyOffset.magnitude >maxOffset)
				{
					joyOffset.Normalize();
					joyOffset = Vector2.Scale(joyOffset, new Vector2(maxOffset,maxOffset));
				}
				
				stickTrans.localPosition = new Vector3(joyOffset.x,joyOffset.y,0);
			}
			else
			{
				if(isTouching)
				{
					joyStick.SetActiveRecursively(false);
					isTouching = false;
					joyOffset = Vector2.zero;
					stickTrans.localPosition = Vector3.zero;
				}
			}
		}
		else
		{
			if(joyStick.active == false)
			{
				joyStick.SetActiveRecursively(true);
				joyAnchor.relativeOffset = originalOffset;
				joyCenter = new Vector2(originalOffset.x * Screen.width, originalOffset.y * Screen.height);
			}

			if(Input.GetMouseButton(0))
			{
				Vector2 tempXY = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
				if(!isTouching)
				{
					isTouching = true;
				}
				joyOffset = tempXY - joyCenter;
				
				if(joyOffset.magnitude >maxOffset)
				{
					joyOffset.Normalize();
					joyOffset = Vector2.Scale(joyOffset, new Vector2(maxOffset,maxOffset));
				}
				stickTrans.localPosition = new Vector3(joyOffset.x,joyOffset.y,0);
			}
			else
			{
				if(isTouching)
				{
					isTouching = false;
					joyOffset = Vector2.zero;
					stickTrans.localPosition = Vector3.zero;
				}
			}
		}

#else
		if(hasDynamic)
		{
			if(Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				if(!isTouching )
				{
					joyCenter = touch.position;
					joyStick.SetActiveRecursively(true);
					joyAnchor.relativeOffset = new Vector2(joyCenter.x/Screen.width, joyCenter.y/Screen.height);	
					isTouching = true;
				}
				
				joyOffset  = touch.position - joyCenter;
				
				if(joyOffset.magnitude >maxOffset)
				{
					joyOffset.Normalize();
					joyOffset = Vector2.Scale(joyOffset, new Vector2(maxOffset,maxOffset));
				}
	
				stickTrans.localPosition = new Vector3(joyOffset.x, joyOffset.y,0);
				
			}
			else
			{
				if(isTouching)
				{
					joyStick.SetActiveRecursively(false);
					isTouching = false;
					joyOffset = Vector2.zero;
					stickTrans.localPosition = Vector3.zero;
				}
			}
		}
		else
		{
			if(joyStick.active == false)
			{
				joyStick.SetActiveRecursively(true);
				joyAnchor.relativeOffset = originalOffset;
				joyCenter = new Vector2(originalOffset.x * Screen.width, originalOffset.y * Screen.height);
			}
			
			if(Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				if(!isTouching )
				{
					isTouching = true;
				}
				
				joyOffset  = touch.position - joyCenter;
				
				if(joyOffset.magnitude >maxOffset)
				{
					joyOffset.Normalize();
					joyOffset = Vector2.Scale(joyOffset, new Vector2(maxOffset,maxOffset));
				}
	
				stickTrans.localPosition = new Vector3(joyOffset.x, joyOffset.y,0);
				
			}
			else
			{
				if(isTouching)
				{
					isTouching = false;
					joyOffset = Vector2.zero;
					stickTrans.localPosition = Vector3.zero;
				}
			}
			
		}

#endif
	}
}


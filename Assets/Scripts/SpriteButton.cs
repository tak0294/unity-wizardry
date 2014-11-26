using UnityEngine;
using System.Collections;

public class SpriteButton : MonoBehaviour
{
	private MainScript m_mainScript;
	
	public static bool Up   = false;
	public static bool Down = false;
	public static bool Left = false;
	public static bool Right = false;
	
	// Use this for initialization
	void Start () {
		this.m_mainScript = GameObject.Find("Main").GetComponent<MainScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/**********************************
	 * 上下左右の入力.
	 **********************************/ 
	public void OnPressUp(){  Up = true; }
	public void OnReleaseUp(){Up = false;}
	
	public void OnPressDown(){  Down = true;}
	public void OnReleaseDown(){Down = false;}
	
	public void OnPressRight(){	Right = true;}
	public void OnReleaseRight(){Right = false;}
	
	public void OnPressLeft(){ Left = true;}
	public void OnReleaseLeft(){Left = false;}
	
	public static bool GetIsAnyDirection()
	{
		return (Up || Down || Left || Right);
	}
}

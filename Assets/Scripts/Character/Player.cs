using UnityEngine;
using System.Collections;

public class Player : Actor {

	public DungeonController m_dungeonCtrl;
	private DynamicStick m_joystick;

	// Use this for initialization
	void Start () {
		//this.initialize();
		Debug.Log("player Start");
		m_joystick = GameObject.Find("Joystick").GetComponent<DynamicStick>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos 	= this.m_sprite.transform.position;
		Vector3 vel     = Vector3.zero;
		vel.x = m_joystick._JoyOffset.x;
		vel.y = m_joystick._JoyOffset.y;

		if(Input.GetKey(KeyCode.RightArrow)) {
			
			vel.x = 0.1f;
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			vel.x = -0.1f;
		}
		
		if(Input.GetKey(KeyCode.DownArrow)) {
			vel.y = -0.1f;
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			vel.y = 0.1f;
		}
		

		vel.Normalize();
		vel *= 0.1f;

		if(this.m_dungeonCtrl.GetModel().CanGoThru(Mathf.RoundToInt(pos.x + vel.x), Mathf.RoundToInt(pos.y*-1)) == false) {
			vel.x = 0f;
		}
		if(this.m_dungeonCtrl.GetModel().CanGoThru(Mathf.RoundToInt(pos.x), Mathf.RoundToInt((pos.y + vel.y)*-1)) == false) {
			vel.y = 0f;
		}

		if(vel.y != 0f || vel.x != 0f) {
			if(vel.y == 0f) {
				if(vel.x > 0f){
					GetComponent<Animator>().Play("pright");
				}else{
					GetComponent<Animator>().Play("pleft");
				}
			}else{
				if(vel.y > 0f){
					GetComponent<Animator>().Play("pup");
				}else{
					GetComponent<Animator>().Play("pdown");
				}
			}
		}

		pos.x += vel.x;
		pos.y += vel.y;

		this.m_sprite.transform.position = pos;
	}
}

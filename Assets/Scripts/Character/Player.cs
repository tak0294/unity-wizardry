using UnityEngine;
using System.Collections;

public class Player : Actor {

	public DungeonController m_dungeonCtrl;

	// Use this for initialization
	void Start () {
		//this.initialize();
		Debug.Log("player Start");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.m_sprite.transform.position;
		if(Input.GetKey(KeyCode.RightArrow)) {
			pos.x += 0.1f;
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			pos.x -= 0.1f;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			pos.y -= 0.1f;
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			pos.y += 0.1f;
		}

		//Debug.Log(this.m_dungeonCtrl.GetModel().GetMapAt(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)));

		this.m_sprite.transform.position = pos;
	}
}

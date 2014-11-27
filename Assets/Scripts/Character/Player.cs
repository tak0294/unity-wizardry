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
			GetComponent<Animator>().Play("pright");
			pos.x += 0.1f;
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			GetComponent<Animator>().Play("pleft");
			pos.x -= 0.1f;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			GetComponent<Animator>().Play("pdown");
			pos.y -= 0.1f;
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			GetComponent<Animator>().Play("pup");
			pos.y += 0.1f;
		}

		//Debug.Log(this.m_dungeonCtrl.GetModel().GetMapAt(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)));

		this.m_sprite.transform.position = pos;
	}
}

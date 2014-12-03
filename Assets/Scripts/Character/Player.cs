using UnityEngine;
using System.Collections;

public class Player : Actor {

	public DungeonController m_dungeonCtrl;
	private Vector3 m_vel = Vector3.zero;
	private int m_walked = 0;

	// Use this for initialization
	void Start () {
		//this.initialize();
		Debug.Log("player Start");

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos 	= this.m_sprite.transform.position;


		if(Input.GetKey(KeyCode.RightArrow)) {
			m_vel.x = 0.1f;
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			m_vel.x = -0.1f;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			m_vel.y = -0.1f;
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			m_vel.y = 0.1f;
		}

		if(m_walked  > 10) {
			m_vel.x = 0;
			m_vel.y = 0;
			m_walked = 0;
			int rnd = Random.Range(0,10);
		
			if(rnd == 0) {
				m_vel.y = 0.1f;
			}else if(rnd == 2){
				m_vel.y = -0.1f;
			}else if(rnd == 4) {
				m_vel.x = 0.1f;
			}else if(rnd == 6) {
				m_vel.x = -0.1f;
			}
		}
		

		m_vel.Normalize();
		m_vel *= 0.1f;

		if(this.m_dungeonCtrl.GetModel().CanGoThru(Mathf.RoundToInt(pos.x + m_vel.x), Mathf.RoundToInt(pos.y*-1)) == false) {
			m_vel.x = 0f;
		}
		if(this.m_dungeonCtrl.GetModel().CanGoThru(Mathf.RoundToInt(pos.x), Mathf.RoundToInt((pos.y + m_vel.y)*-1)) == false) {
			m_vel.y = 0f;
		}


		m_walked++;
		if(m_vel.y != 0f || m_vel.x != 0f) {


			if(m_vel.y == 0f) {
				if(m_vel.x > 0f){
					GetComponent<Animator>().Play("pright");
				}else{
					GetComponent<Animator>().Play("pleft");
				}
			}else{
				if(m_vel.y > 0f){
					GetComponent<Animator>().Play("pup");
				}else{
					GetComponent<Animator>().Play("pdown");
				}
			}
		}

		if (m_vel.y == 0f && m_vel.x == 0f) {

			GetComponent<Animator> ().enabled = false;
		}else{
			GetComponent<Animator> ().enabled = true;
		}

		pos.x += m_vel.x;
		pos.y += m_vel.y;

		this.m_sprite.transform.position = pos;
	}
}

using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	private DynamicStick m_joystick;

	private Camera m_mainCamera;
	private bool isMovingCamera = false;
	
	private enum MoveDirection
	{
		VERTICAL,
		HORIZONTAL,
	}
	
	private int MoveMaxCount = 16;
	private int m_moveCount = 0;
	private int m_moveBlockValue = 0;
	private MoveDirection m_moveDirection  = 0;
	
	private DungeonController m_dungeonCtrl;
	private Player m_player;
	
	// Use this for initialization
	void Start () {
		m_joystick = GameObject.Find("Joystick").GetComponent<DynamicStick>();

		m_dungeonCtrl = gameObject.AddComponent<DungeonController>();
		m_dungeonCtrl.BuildDungeon();
		m_player = CustomObject.createObject("Player", "PlayerChara").GetComponent<Player>();
		m_player.initialize();
		m_player.m_dungeonCtrl = m_dungeonCtrl;

		/*************************************
		 * Cameraの取得.
		 **************************************/
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		m_mainCamera = camera.GetComponent<Camera>();
		
		
		/**************************************
		 * カメラの初期値.
		 **************************************/ 
		Room room = m_dungeonCtrl.GetModel().GetLeftTopRoom();
		CustomTransform.setPosition(m_mainCamera.gameObject
		                            , room.rect.x
		                            , -room.rect.y
		                            , -10);
		Debug.Log (room.rect.x +", " + room.rect.y);
		m_player.GetSprite().transform.position = new Vector3(room.rect.x+1, -room.rect.y-1, 0);

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel     = Vector3.zero;
		vel.x = m_joystick._JoyOffset.x;
		vel.y = m_joystick._JoyOffset.y;

		Vector3 pos = Camera.main.transform.position;
		Vector3 sp_pos = m_player.GetSprite().transform.position;
		pos.x += (sp_pos.x - pos.x) * 0.07f;
		pos.y += (sp_pos.y - pos.y) * 0.07f;

		Camera.main.transform.position = pos;

		//Mouse Click
		if(Input.GetMouseButton(0)) {
			Vector3    aTapPoint   = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);
			
			if (aCollider2d) {
				GameObject obj = aCollider2d.transform.gameObject;
				this.m_player.setDestination(obj.transform.position.x, obj.transform.position.y);
			}
		}
	}
	
}

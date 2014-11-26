using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {
	
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
		m_dungeonCtrl = gameObject.AddComponent<DungeonController>();
		m_dungeonCtrl.BuildDungeon();
		m_player = CustomObject.createObject("Player", "Player").GetComponent<Player>();
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

		m_player.GetSprite().transform.position = new Vector3(room.rect.x, -room.rect.y, 0);

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.transform.position;
		Vector3 sp_pos = m_player.GetSprite().transform.position;
		pos.x += (sp_pos.x - pos.x) * 0.1f;
		pos.y += (sp_pos.y - pos.y) * 0.1f;

		Camera.main.transform.position = pos;
	}
	
}

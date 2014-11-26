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

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.transform.position;
		pos.y += 0.01f;
		Camera.main.transform.position = pos;
	}
	
}

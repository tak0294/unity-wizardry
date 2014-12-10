using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	public float orthoZoomSpeed = 0.05f;        // The rate of change of the orthographic size in orthographic mode.

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
	private AStarRouteSearcher m_playerRouter = new AStarRouteSearcher();

	// Use this for initialization
	void Start () {
		//m_joystick = GameObject.Find("Joystick").GetComponent<DynamicStick>();

		m_dungeonCtrl = gameObject.AddComponent<DungeonController>();
		m_dungeonCtrl.BuildDungeon();
		m_playerRouter.setDungeonController(m_dungeonCtrl);
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
		//vel.x = m_joystick._JoyOffset.x;
		//vel.y = m_joystick._JoyOffset.y;

		Vector3 pos = Camera.main.transform.position;
		Vector3 sp_pos = m_player.GetSprite().transform.position;
		pos.x += (sp_pos.x - pos.x) * 0.07f;
		pos.y += (sp_pos.y - pos.y) * 0.07f;

		Camera.main.transform.position = pos;

		//Mouse Click
		if(Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
			m_playerRouter.initialize();

			Vector3    aTapPoint;
			if(Input.GetMouseButtonDown(0)) {
				aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}else{
				aTapPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
			}
			Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);
			
			if (aCollider2d) {
				GameObject obj = aCollider2d.transform.gameObject;
				m_playerRouter.findPath(m_player.GetSprite().transform.position, obj.transform.position);
				//this.m_player.setDestination(obj.transform.position.x, obj.transform.position.y);
				BetterList<Vector2> path = m_playerRouter.getPath();
				//Debug.Log(m_player.GetSprite().transform.position);
				//Debug.Log(obj.transform.position.x + "," + obj.transform.position.y);
				//Debug.Log(path[0].x + "," + path[0].y);
				//this.m_player.setDestination(path[0].x, path[0].y*-1);
				this.m_player.setPath(path);
			}
		}

		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) * 0.5f;
			
			// If the camera is orthographic...
			if (Camera.main.isOrthoGraphic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				
				// Make sure the orthographic size never drops below zero.
				Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
			}

		}

		GameObject wallObj;
		BetterList<GameObject> floorList = this.m_dungeonCtrl.GetView().m_floorList;
		BetterList<Cell> floorCellList   = this.m_dungeonCtrl.GetView().m_floorCellList;
		int len = floorList.size;
		for(int ii=0;ii<len;ii++) {
			wallObj = floorList[ii];
			float distance = Mathf.Abs(m_player.GetSprite().transform.position.x - wallObj.transform.position.x) + Mathf.Abs(m_player.GetSprite().transform.position.y - wallObj.transform.position.y);
			float alpha = 0.4f;
			if(distance < 3)	alpha = 0.6f;
			if(distance < 2)	alpha = 1.0f;

			if(alpha == 1.0f) {
				floorCellList[ii].ChangeNormalFloor();
			}
			else if(alpha == 0.4f) {
				floorCellList[ii].ChangeDarkFloor();
			}
			//CustomObject.getChild(wallObj,"sprite").renderer.material.color = new Color (1, 1, 1, alpha);
		}
	}
	
}

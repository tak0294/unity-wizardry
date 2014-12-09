using UnityEngine;
using System.Collections;

public class DungeonController : MonoBehaviour {
	
	private DungeonModel m_dungeonModel;
	private DungeonView  m_dungeonView;
	
	// Initialize
	void Awake()
	{
		Debug.Log("DungeonCtonroller Start");
		m_dungeonModel = gameObject.AddComponent<DungeonModel>();
		m_dungeonView  = gameObject.AddComponent<DungeonView>();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/***************************************
	 * ダンジョンを構築.
	 ***************************************/
	public void BuildDungeon()
	{
		m_dungeonModel.Initialize();
		m_dungeonView.Initialize(m_dungeonModel);
	}

	public DungeonView GetView() {
		return m_dungeonView;
	}

	public DungeonModel GetModel()
	{
		return m_dungeonModel;
	}
	
	public DungeonModel.FLOOR_TYPE GetDungeonMapAt(int x, int y)
	{
		return this.m_dungeonModel.GetMapAt(x,y);
	}
}

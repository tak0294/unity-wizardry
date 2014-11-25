using UnityEngine;
using System.Collections;

public class DungeonView : MonoBehaviour {
	
	private GameObject m_rootView;
	
	// Initialize
	void Awake() 
	{
		m_rootView = GameObject.Find("Main");
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Initialize(DungeonModel dungeon)
	{
		DungeonModel.FLOOR_TYPE[,] mapData = dungeon.GetMap();
		for(int ii=0;ii<DungeonModel.MAP_H;ii++)
		{
			for(int jj=0;jj<DungeonModel.MAP_W;jj++)
			{
				/*******************************************************
				 * 壁.
				 *******************************************************/ 
				if(mapData[ii,jj] == DungeonModel.FLOOR_TYPE.WALL)
				{
					GameObject cell = CustomObject.createObject("cell"+ii+"_"+jj, "Ground", m_rootView);
					CustomTransform.setPosition(cell, -(DungeonModel.MAP_W/2*32) + jj*32, 360 - ii*32, 0);
				}
				/******************************************************
				 * 床.
				 *******************************************************/ 
				else if(mapData[ii, jj] != DungeonModel.FLOOR_TYPE.WALL && 
					    mapData[ii, jj] != DungeonModel.FLOOR_TYPE.NONE)
				{
					GameObject cell = CustomObject.createObject("cell"+ii+"_"+jj, "Ground2", m_rootView);
					CustomTransform.setPosition(cell, -(DungeonModel.MAP_W/2*32) + jj*32, 360 - ii*32, 32);
				}
			}
		}
		
	}
}

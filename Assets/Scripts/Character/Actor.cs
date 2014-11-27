using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	
	public enum MOVE_DIRECTION
	{
		NONE,
		UP,
		DOWN,
		RIGHT,
		LEFT,
	}
	
	protected GameObject m_sprite;
	protected int m_hp;
	protected int m_mp;
	protected int m_mapX;
	protected int m_mapY;
	protected MOVE_DIRECTION m_oldMoveDirection  = MOVE_DIRECTION.NONE;
	protected MOVE_DIRECTION m_nextMoveDirection = MOVE_DIRECTION.NONE;
	
	/*****************
	 * Scroll animation.
	 *****************/ 
	protected int m_animationFrame = 15;
	protected int m_animationBlockSize = 2;
	protected int m_animationElapsedFrame = 0;
	

	public void SetMapX(int x)
	{
		this.m_mapX = x;
	}
	
	public void AddMapX(int x)
	{
		this.m_mapX += x;
	}
	
	public void SetMapY(int y)
	{
		this.m_mapY = y;
	}
	
	public void AddMapY(int y)
	{
		this.m_mapY += y;
	}

	public GameObject GetSprite() {
		return this.m_sprite;
	}
	
	public void initialize()
	{
		this.m_sprite = this.gameObject; //CustomObject.getChild(this.gameObject, "Sprite");
		Debug.Log(this.m_sprite);
	}
	
}

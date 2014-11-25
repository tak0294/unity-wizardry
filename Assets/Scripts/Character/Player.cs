using UnityEngine;
using System.Collections;

public class Player : Actor {

	// Use this for initialization
	void Start () {
		this.initialize();
		Debug.Log("player Start");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(m_nextMoveDirection != Actor.MOVE_DIRECTION.NONE)
		{
			/***************************************
			 * 行動開始の最初のフレーム.
			 ***************************************/ 
			if(m_oldMoveDirection != m_nextMoveDirection)
			{
				if(m_nextMoveDirection == Actor.MOVE_DIRECTION.UP)
					this.m_background.spriteName = "chr1_bk";
				else if(m_nextMoveDirection == Actor.MOVE_DIRECTION.DOWN)
					this.m_background.spriteName = "chr1_fr";
				else if(m_nextMoveDirection == Actor.MOVE_DIRECTION.LEFT)
					this.m_background.spriteName = "chr1_left";
				else if(m_nextMoveDirection == Actor.MOVE_DIRECTION.RIGHT)
					this.m_background.spriteName = "chr1_right";
				
				m_animationElapsedFrame = 0;
			}

			int moveAmount = m_animationBlockSize;
			if(m_nextMoveDirection == Actor.MOVE_DIRECTION.LEFT ||
			   m_nextMoveDirection == Actor.MOVE_DIRECTION.DOWN)
				moveAmount = -m_animationBlockSize;;
			
			//float moveTickSize = (float)moveAmount/(float)m_animationFrame;
			Vector3 pos = this.m_sprite.transform.localPosition;
			if(m_nextMoveDirection == Actor.MOVE_DIRECTION.UP || m_nextMoveDirection == Actor.MOVE_DIRECTION.DOWN)
				pos.y += moveAmount;
			else
				pos.x += moveAmount;
			this.m_sprite.transform.localPosition = pos;
			
			m_animationElapsedFrame++;
			if(m_animationElapsedFrame == m_animationFrame)
				m_nextMoveDirection = Actor.MOVE_DIRECTION.NONE;

		}else{
			m_sprite.transform.localPosition = new Vector3(DungeonModel.GetDrawPositionX(this.m_mapX),
														   DungeonModel.GetDrawPositionY(this.m_mapY), -48);

		}

		m_oldMoveDirection = m_nextMoveDirection;
	}
}

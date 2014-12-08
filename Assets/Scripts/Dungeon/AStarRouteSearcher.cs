using UnityEngine;
using System.Collections;

public class AStarRouteSearcher {

	private DungeonController m_dungeonCtrl;
	private BetterList<AStarPoint> m_openList = new BetterList<AStarPoint>();
	private BetterList<Vector2> m_closedList  = new BetterList<Vector2>();

	private int[] dx = {0, 1, 0, -1};   // X方向移動用配列
	private int[] dy = {1, 0, -1, 0};   // Y方向移動用配列

	private Vector2 m_startPoint = Vector2.zero;
	private Vector2 m_goalPoint  = Vector2.zero;

	public void setDungeonController(DungeonController dController) {
		this.m_dungeonCtrl = dController;
	}

	public void initialize() {
		this.m_openList = new BetterList<AStarPoint>();
		this.m_closedList = new BetterList<Vector2>();
	}

	private float hs(Vector2 node1, Vector2 node2)
	{
		return Mathf.Abs(node2.x - node1.x) + Mathf.Abs(node2.y - node1.y);
	}

	private void ClosePoint(AStarPoint p) {
		p.SetClosed();
		//Closedリストへ追加.
		this.m_closedList.Add(p.getPos());
		float currentCost = p.getCost();
		//４方向を開けておく.
		for(int ii=0;ii<4;ii++) {
			if(this.m_dungeonCtrl.GetModel().CanGoThru(this.m_startPoint.x + dx[ii], this.m_startPoint.y + dy[ii])) {
				AStarPoint tmp_p = new AStarPoint();
				tmp_p.setCost(currentCost + 1);
				tmp_p.setPos(p.getPos().x + dx[ii], p.getPos().y + dy[ii]);
				tmp_p.CalcHs(m_goalPoint);
				m_openList.Add(tmp_p);
			}
		}
	}

	private AStarPoint GetPointWillBeClose() {
		return null;
	}

	private bool searchRoute() {
		float score = 999999;
		int index = -1;
		AStarPoint p = null;
		for(int ii=0;ii<this.m_openList.size;ii++) {
			if(score > this.m_openList[ii].GetScore()) {
				score = this.m_openList[ii].GetScore();
				index = ii;
				p = this.m_openList[ii];
				this.m_openList.RemoveAt(ii);
			}
		}

		if(p == null) {
			return false;
		}

		//pを確定ノードとする.
		this.m_closedList.Add (p.getPos ());

		Debug.Log ("search_index = " + index);
		return true;
	}

	public void findPath(Vector3 pos, Vector3 dest) {
		m_startPoint.x = Mathf.CeilToInt(pos.x);
		m_startPoint.y = Mathf.CeilToInt(pos.y * -1);
		m_goalPoint.x  = Mathf.CeilToInt(dest.x);
		m_goalPoint.y  = Mathf.CeilToInt(dest.y * -1);

		//スタート地点のNodeを作成.
		AStarPoint p = new AStarPoint();
		p.setCost(0);
		p.setPos(m_startPoint.x, m_startPoint.y);
		p.CalcHs(m_goalPoint);
		ClosePoint(p);

		//探索開始.
		searchRoute ();

		//Debug.Log("pos:" + m_startPoint);
		//Debug.Log("dest:" + m_goalPoint);
		//Debug.Log(m_openList);

	}
}

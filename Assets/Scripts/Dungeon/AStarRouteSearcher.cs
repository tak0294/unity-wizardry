using UnityEngine;
using System.Collections;

public class AStarRouteSearcher {

	private DungeonController m_dungeonCtrl;
	private BetterList<AStarPoint> m_openList = new BetterList<AStarPoint>();
	private BetterList<Vector2> m_closedList  = new BetterList<Vector2>();
	private BetterList<Vector2> m_path		  = new BetterList<Vector2>();

	private int[] dx = {0, 1, 0, -1/*, 1,1,-1,-1*/};   // X方向移動用配列
	private int[] dy = {1, 0, -1, 0/*,-1,1, 1,-1*/};   // Y方向移動用配列

	private Vector2 m_startPoint = Vector2.zero;
	private Vector2 m_goalPoint  = Vector2.zero;

	private int m_searchedCount = 0;

	public void setDungeonController(DungeonController dController) {
		this.m_dungeonCtrl = dController;
	}

	public BetterList<Vector2> getPath() {
		return this.m_path;
	}

	public void initialize() {
		this.m_openList   = new BetterList<AStarPoint>();
		this.m_closedList = new BetterList<Vector2>();
		this.m_path		  = new BetterList<Vector2>();
		this.m_searchedCount = 0;
	}

	private float hs(Vector2 node1, Vector2 node2) {
		return Mathf.Abs(node2.x - node1.x) + Mathf.Abs(node2.y - node1.y);
	}

	private void ClosePoint(AStarPoint p) {
		p.SetClosed();
		//Closedリストへ追加.
		this.m_closedList.Add(p.getPos());
		float currentCost = p.getCost();
		//４方向を開けておく.
		for(int ii=0;ii<4;ii++) {
			if(this.m_dungeonCtrl.GetModel().CanGoThru(p.getPos().x + dx[ii], p.getPos().y + dy[ii])) {
				
				Vector2 tmp_v = new Vector2(p.getPos().x + dx[ii], p.getPos().y + dy[ii]);
				if(this.isClosedPoint(tmp_v)) {
					//Debug.Log("CLOSED!");
					continue;
				}
				AStarPoint tmp_p = new AStarPoint();
				tmp_p.setCost(currentCost + 1);
				tmp_p.setPos(p.getPos().x + dx[ii], p.getPos().y + dy[ii]);
				tmp_p.CalcHs(m_goalPoint);
				tmp_p.setParent(p);
				
				m_openList.Add(tmp_p);
			}
		}
	}

	private bool isClosedPoint(Vector2 p) {
		for(int ii=0;ii<m_closedList.size;ii++) {
			//if(p.Equals(m_closedList[ii])) {
			if(p.x == m_closedList[ii].x && p.y == m_closedList[ii].y) {
				return true;
			}
		}
		return false;
	}

	private bool searchRoute() {

		if(this.m_searchedCount > 1000) {
			Debug.Log("too much search!");
			return false;
		}
		this.m_searchedCount++;

		float score = 999999;
		int index = -1;
		AStarPoint p = null;
		for(int ii=0;ii<this.m_openList.size;ii++) {
			if(score > this.m_openList[ii].GetScore()) {
				score = this.m_openList[ii].GetScore();
				index = ii;
				p = this.m_openList[ii];
			}
		}

		if(p == null) {
			return false;
		}

		//pを確定ノードとする.
		this.m_openList.RemoveAt (index);
		this.m_closedList.Add (p.getPos ());

		if(p.getPos().x == m_goalPoint.x && p.getPos().y == m_goalPoint.y) {
			Debug.Log("GOAL!");
			m_path.Add(p.getPos());
			AStarPoint path = p.getParent();
			int cnt = 0;
			while(path != null) {
				//Debug.Log(path.getPos().x + ", " + path.getPos().y);
				m_path.Add(path.getPos());
				path = path.getParent();
				cnt++;
			}
			return false;
		}

		ClosePoint(p);

		//Debug.Log ("search_index = " + index);
		return this.searchRoute();
	}

	public void findPath(Vector3 pos, Vector3 dest) {
		m_startPoint.x = Mathf.RoundToInt(pos.x);
		m_startPoint.y = Mathf.RoundToInt(pos.y * -1);
		m_goalPoint.x  = Mathf.RoundToInt(dest.x);
		m_goalPoint.y  = Mathf.RoundToInt(dest.y * -1);

		//Debug.Log("GOAL=" + m_goalPoint.x + "," +m_goalPoint.y);
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

using UnityEngine;
using System.Collections;

public class AStarPoint {
	enum _STATUS {
		NONE,
		OPEN,
		CLOSED,
	};

	_STATUS m_status = _STATUS.NONE;
	private float m_cost = 0;
	private float m_heuristic = 0;
	private AStarPoint m_parent = null;
	private Vector2 m_pos = Vector2.zero;

	public void setPos(float x, float y) {
		this.m_pos.x = x;
		this.m_pos.y = y;
	}

	public Vector2 getPos() {
		return this.m_pos;
	}

	public void CalcHs(Vector2 goalNode) {
		this.m_heuristic = Mathf.Abs(goalNode.x - m_pos.x) + Mathf.Abs(goalNode.y - m_pos.y);
	}

	public void setHeuristic(float cost) {
		this.m_heuristic = cost;
	}

	public void setCost(float cost) {
		this.m_cost = cost;
	}

	public float getCost() {
		return this.m_cost;
	}

	public void setParent(AStarPoint parent) {
		this.m_parent = parent;
	}

	public void SetClosed() {
		this.m_status = _STATUS.CLOSED;
	}

	public float GetScore() {
		return this.m_cost + this.m_heuristic;
	}
}

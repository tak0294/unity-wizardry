using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
	SpriteRenderer MainSpriteRenderer;
	public Sprite NormalFloor = null;
	public Sprite HalkDarkFloor = null;
	public Sprite DarkFloor = null;

	private Sprite CurrentSprite = null;

	// Use this for initialization
	void Start () {
		MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	public void ChangeDarkFloor() {
		if(DarkFloor != null && CurrentSprite != DarkFloor) {
			CurrentSprite = MainSpriteRenderer.sprite = DarkFloor;

		}
	}

	public void ChangeNormalFloor() {
		if(NormalFloor != null && CurrentSprite != NormalFloor) {
			CurrentSprite = MainSpriteRenderer.sprite = NormalFloor;
		}
	}
}

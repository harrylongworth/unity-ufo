using UnityEngine;
using System.Collections;

public class SpriteManager : MonoBehaviour {

	public Sprite[] sprites;

	public int spriteID = 0;
	public bool setSprite = false;

	private SpriteRenderer renderer;
	private Sprite currentSprite;

	void Start () {
		//Set Sprites

		renderer = GetComponent<SpriteRenderer> ();

		/*
		if (setSprite) {
			if (spriteID > (sprites.Length)) {			
				spriteID = 0;
			}

		} else {
			// select random sprite
			int randomSpriteID = (int) Mathf.Round(Random.Range(0,sprites.Length));
			spriteID = randomSpriteID; 

		}

		currentSprite = sprites [spriteID ];
		renderer.sprite = currentSprite;
	*/
	}
	

	public void SetSpriteByID(int id) {

		if (id < sprites.Length) {
			spriteID = id;
		} else {
			spriteID = 0;
		}
			
		currentSprite = sprites [spriteID ];

		if (renderer == null) {
			renderer = GetComponent<SpriteRenderer> ();
		}

		renderer.sprite = currentSprite;

	}

	public Sprite GetCurrentSprite() {
		return currentSprite;
	}

	public int GetCurrentSpriteID() {
		return spriteID;
	}

	public int GetLength() {
		return sprites.Length;
	}

	public string GetName(int spriteIndex) {
		return sprites [spriteIndex].name;
	}

	public void SetSpriteRandom() {
		
		int randomSpriteID = (int)Mathf.Round (Random.Range (0, sprites.Length));
		spriteID = randomSpriteID; 

		currentSprite = sprites [spriteID];

		renderer = GetComponent<SpriteRenderer> ();

		renderer.sprite = currentSprite;
	}// END SetSpriteRandom

	public Sprite GetSpriteByID(int id) {
		return sprites [id];
	}
}

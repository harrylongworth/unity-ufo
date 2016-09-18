using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public Sprite[] sprites;
	public int spriteID = 0;
	public bool setSprite = false;

	// Use this for initialization
	void Start () {

		//Set Sprites
		Sprite currentSprite;



		if (setSprite) {
			if (spriteID < (sprites.Length)) {			
				currentSprite = sprites [spriteID];
			} else {
				currentSprite = sprites [0];
			}

		} else {
			// select random sprite
			int randomSpriteID = (int) Mathf.Round(Random.Range(0,sprites.Length));
			currentSprite = sprites [randomSpriteID ];
		}


		var renderer = GetComponent<SpriteRenderer> ();

		renderer.sprite = currentSprite;


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

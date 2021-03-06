﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Sprite[] sprites;
	public int spriteID = 0;
	public bool setSprite = false;
	public float speed;
	public float acceleration;
	public float rotationSpeed;
	public GameController gameController;
	public SimpleTouchPadPlayer touchPadCenter;
	public Canvas canvas;
	public GameObject explosion;
	public float centerCalibrate;

	public float shieldSpriteSize=30f;
	public float smallestShieldSize=5f;

	public int currentShield;
	public int currentLives;

	private float currentSpeed;
	private Rigidbody2D rb2d;
	private Transform playerPosition;
	private int targetTally;


	private int playerDamage;
	private int score;

	private AudioSource explodeAudio;
	private AudioSource bounceAudio;
	private Vector2 currentDirection;
	// private Quaternion playerRotation = Quaternion.identity;

	private GameObject shieldSprite;
	private float currentShieldSpriteSize;
	private float changeShieldSpriteBy;


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


		//Damage
		playerDamage = 0;
		gameController.displayDamage.text = "Damage: "+playerDamage.ToString();

		//Score
		score = 0;
		gameController.displayScore.text = "Score: "+score.ToString();


		// gameController = GameObject.FindGameObjectWithTag ("GameController");
		rb2d = GetComponent<Rigidbody2D>();

		AudioSource[] tempAudio = GetComponents<AudioSource> ();
		explodeAudio = tempAudio[0];
		bounceAudio = tempAudio [1];

		currentDirection = new Vector2(0,1.0f);
		currentSpeed = speed;

		shieldSprite = GameObject.FindGameObjectWithTag ("Shield");

		if (gameController.invincible) {
			
			gameController.displayShield.enabled = false;

			Destroy (shieldSprite);

			gameController.displayLives.enabled=false;

		} else {

			currentShield = gameController.shield;
			gameController.displayShield.text = "Shield: " +currentShield.ToString();

			shieldSprite.transform.localScale = new Vector3 (shieldSpriteSize, shieldSpriteSize, 0f);
			currentShieldSpriteSize = shieldSpriteSize;

			// set the shield sprite size increment 
			changeShieldSpriteBy = (shieldSpriteSize - smallestShieldSize) / gameController.shield;
			// Debug.Log (changeShieldSpriteBy);

			currentLives = gameController.lives;
			gameController.displayLives.text = "Lives: " + currentLives.ToString ();

		}

	} 
		
	// Update is called once per frame
	void FixedUpdate () {

		// "Turn off" inertia:
		rb2d.velocity = Vector2.zero;
		// currentSpeed = speed;

		if (!gameController.paused) {
			//Player Touchpad
			Vector2 touchPosition = touchPadCenter.GetPosition ();
			// Debug.Log (touchPosition);

			if (touchPosition != Vector2.zero) { // ie screen has been touched

				Vector2 ScreenCenter = new Vector2 (Screen.width / 2, Screen.height / 2);
				// Debug.Log (ScreenCenter);

				Vector2 minusCenter = touchPosition - ScreenCenter;
				minusCenter = minusCenter.normalized;
				// Debug.Log (minusCenter);

				float touchAngle = Mathf.Atan2 (minusCenter.x, minusCenter.y) * 180 / Mathf.PI;
				if (touchAngle < 0) { 
					touchAngle = touchAngle + 360;
				}
				//Debug.Log (touchAngle);

				float playerAngle = 360 - transform.rotation.eulerAngles.z;
				// Debug.Log (playerAngle);

				float turnAngle = Mathf.DeltaAngle (playerAngle, touchAngle);
				// Debug.Log (turnAngle);

				float turnDirection = Mathf.Sign (turnAngle);
				// Debug.Log (turnDirection);



				if (Mathf.Abs (turnAngle) > centerCalibrate) { // get rid of shudder
					rb2d.AddTorque (turnDirection * -1.0f * rotationSpeed);

					// Accelerate based on distance of tap from center
					float strength2 = Vector2.Distance (ScreenCenter, touchPosition);
					// Debug.Log (strength2);

					currentSpeed = speed + (strength2 / (Mathf.Max (Screen.width, Screen.height))) * acceleration;
				} 

			} else {
				currentSpeed = speed; // reset speed to default speed
			}// END Screen has been touched

			// Keyboard Input
			float moveHorizontal = Input.GetAxis ("Horizontal");
			// float moveVertical = Input.GetAxis ("Vertical");

			if(Input.GetKey(KeyCode.Space)||Input.GetKey(KeyCode.UpArrow)) { 
				currentSpeed = (acceleration/4) * speed; 
			} 

			if(Input.GetKeyUp(KeyCode.Space)||Input.GetKeyUp(KeyCode.UpArrow)) { 
				currentSpeed = speed; 
			}

			// Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

			if (moveHorizontal != 0.0f) {
				rb2d.AddTorque (moveHorizontal * rotationSpeed * -1);
			}

			float radianAngle = -1*(Mathf.Deg2Rad * (transform.eulerAngles.z-360.0f));
			// Debug.Log (radianAngle);

			currentDirection = new Vector2 (Mathf.Sin (radianAngle),Mathf.Cos (radianAngle));

			rb2d.AddForce (currentDirection * currentSpeed);

		} // END if Paused


	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		// Debug.Log (other.tag);

		if (other.gameObject.CompareTag ("Target")) {
			// other.gameObject.SetActive (false);

			// string message = gameController.currentTargetName + " is current target and hit " + other.gameObject.name;
			// Debug.Log (message);
			// Debug.Log (other.gameObject.name);

			if (gameController.enableQuests) {
				if ((other.gameObject.name == gameController.currentTargetName) || (other.gameObject.name == gameController.currentTargetName + "(Clone)")) { // to do 

					IncrementScore ();
					// Is the current target (Quest)
					//Debug.Log("matches");
					gameController.NextTarget ();
					// Is a quest target:
					Instantiate (explosion, other.transform.position, other.transform.rotation);

					Destroy (other.gameObject);
					explodeAudio.Play ();

					targetTally++;

					// CHECK VICTORY conditions
					if (targetTally > gameController.targetsNeededToWin) {
						//restart level
						Time.timeScale=0;

						Application.LoadLevel (Application.loadedLevel);
						Debug.Log ("RESTART! - Level Complete");
						// SceneManager.LoadScene (SceneManager.GetActiveScene()); // need to update

					}

				} else {
					// Not the current target

					gameController.playCurrentTargetAudio ();

					// Debug.Log("No match! "+gameController.currentTargetName+" is current target and hit "+other.gameObject.name);
					IncrementPlayerDamage ();

					// Bounce! 
					// transform.Rotate (0, 0, 180);

					bounceAudio.Play ();

				} // END pickup if

			} else {
				// QUESTS disabled

				IncrementScore ();

				Instantiate (explosion, other.transform.position, other.transform.rotation);

				Destroy (other.gameObject);

				explodeAudio.Play ();

				targetTally++;

				// CHECK VICTORY conditions
				if (targetTally > gameController.targetsNeededToWin) {
					//restart level
					Time.timeScale=0;
					Debug.Log ("RESTART! - Level Complete");
					Application.LoadLevel (Application.loadedLevel);

					// SceneManager.LoadScene (SceneManager.GetActiveScene()); // need to update

				}
			} // END if quests enabled

		} 
		
	} // end OnTriggerEnter2D


	public void IncrementScore() {
		score++;
		gameController.displayScore.text = "Score: "+score.ToString();
	}

	public void IncrementPlayerDamage() {

		playerDamage++;
		gameController.displayDamage.text = "Damage: " + playerDamage.ToString ();

		if (!gameController.invincible) {
			
			currentShield --;


			if (currentShield <= 0) {
				// Time.timeScale=0;
				currentLives--;
				Instantiate (explosion, transform.position, transform.rotation);
				explodeAudio.Play ();

				if (currentLives <= 0) {
					// Game Over
					Time.timeScale=0;
					Application.LoadLevel (Application.loadedLevel);
					Debug.Log ("Game Over! - Restart");
				} else {
					currentShield = gameController.shield;
					currentShieldSpriteSize = shieldSpriteSize;
			
				}
			} else {
				currentShieldSpriteSize -= changeShieldSpriteBy;
			}

			// update shieldSprite size
			shieldSprite.transform.localScale = new Vector3 (currentShieldSpriteSize, currentShieldSpriteSize, 0f);

			// Update Display
			gameController.displayShield.text = "Shield: " +currentShield.ToString();
			gameController.displayLives.text = "Lives: " + currentLives.ToString ();

		} else {

		}
	}


} // END class
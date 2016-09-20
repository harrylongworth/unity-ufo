using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// public Sprite[] sprites;
	// public int spriteID = 0;
	// public bool setSprite = false;

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

	public int initialShield = 5;
	public int initialLives = 3;
	public int currentShield;
	public int currentLives;

	private float currentSpeed;
	private Rigidbody2D rb2d;
	private Transform playerPosition;
	public int targetTally;


	public int playerDamage;
	public int score;

	// private AudioSource explodeAudio;
	// private AudioSource bounceAudio;
	private AudioClipManager audioClipManager;

	private Vector2 currentDirection;
	// private Quaternion playerRotation = Quaternion.identity;

	private GameObject shieldSprite;
	private float currentShieldSpriteSize;
	private float changeShieldSpriteBy;


	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D>();

		audioClipManager = GetComponent<AudioClipManager> ();

	} // END Start
		
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

			// string message = gameController.currentTargetIndex + " is current target and hit " + other.gameObject.name;
			// Debug.Log (message);
			// Debug.Log (other.gameObject.name);

			if (gameController.enableQuests) {
				if (other.gameObject.name == gameController.currentTargetIndex.ToString()) { // to do 

					IncrementScore ();
					// Is the current target (Quest)
					//Debug.Log("matches");
					gameController.NextTarget ();
					// Is a quest target:
					Instantiate (explosion, other.transform.position, other.transform.rotation);

					Destroy (other.gameObject);

					audioClipManager.PlayClip (0);
					// explodeAudio.Play ();

					targetTally++;

					// CHECK VICTORY conditions
					if (targetTally > gameController.targetsNeededToWin) {
						//restart level
						Time.timeScale=0;

						gameController.currentLevel++;
						gameController.NewLevel ();
						// Application.LoadLevel (Application.loadedLevel);
						Debug.Log ("RESTART! - Level Complete");
						// SceneManager.LoadScene (SceneManager.GetActiveScene()); // need to update

					}

				} else {
					// Not the current target

					// gameController.PlayCurrentTargetAudio ();

					// Debug.Log("No match! "+gameController.currentTargetName+" is current target and hit "+other.gameObject.name);
					IncrementPlayerDamage ();

					// Bounce! 
					// transform.Rotate (0, 0, 180);

					audioClipManager.PlayClip (1);
					// bounceAudio.Play ();

				} // END pickup if

			} else {
				// QUESTS disabled

				IncrementScore ();

				Instantiate (explosion, other.transform.position, other.transform.rotation);

				Destroy (other.gameObject);

				audioClipManager.PlayClip (1);
				// explodeAudio.Play ();

				targetTally++;

				// CHECK VICTORY conditions
				if (targetTally > gameController.targetsNeededToWin) {
					//restart level
					Time.timeScale=0;
					Debug.Log ("RESTART! - Level Complete");

					gameController.currentLevel++;
					gameController.NewLevel ();
					// Application.LoadLevel (Application.loadedLevel);

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


				//explodeAudio.Play ();
				audioClipManager.PlayClip (0);

				if (currentLives <= 0) {
					// Game Over
					Time.timeScale=0;

					gameController.NewGame ();

					// Application.LoadLevel (Application.loadedLevel);
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

	public void NewLevel() {
		// Set Random player icon
		GetComponent<SpriteManager> ().SetSpriteRandom ();
		targetTally = 0;



	}

	public void NewGame() {

		targetTally = 0;
		score = 0;
		gameController.displayScore.text = "Score: "+score.ToString();

		playerDamage = 0;
		gameController.displayDamage.text = "Damage: " + playerDamage.ToString ();

		currentShield = initialShield;
		gameController.displayShield.text = "Shield: " +currentShield.ToString();

		currentLives = initialLives;
		gameController.displayLives.text = "Lives: " + currentLives.ToString ();

		//Damage
		playerDamage = 0;
		gameController.displayDamage.text = "Damage: "+playerDamage.ToString();

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
} // END class
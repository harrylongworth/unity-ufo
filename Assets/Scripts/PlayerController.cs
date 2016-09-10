using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float acceleration;
	public float rotationSpeed;
	public GameController gameController;
	public SimpleTouchPadPlayer touchPadCenter;
	public Canvas canvas;
	public GameObject explosion;
	public float centerCalibrate;

	private float currentSpeed;
	private Rigidbody2D rb2d;
	private Transform playerPosition;
	private int targetTally;
	private AudioSource explodeAudio;
	private Vector2 currentDirection;
	// private Quaternion playerRotation = Quaternion.identity;


	// Use this for initialization
	void Start () {
		// gameController = GameObject.FindGameObjectWithTag ("GameController");
		rb2d = GetComponent<Rigidbody2D>();
		explodeAudio = GetComponent<AudioSource> ();
		currentDirection = new Vector2(0,1.0f);
		currentSpeed = speed;
	
	} 
		
	// Update is called once per frame
	void FixedUpdate () {

		// "Turn off" inertia:
		rb2d.velocity = Vector2.zero;
		// currentSpeed = speed;

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
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		// Debug.Log (other.tag);

		if (other.gameObject.CompareTag ("Target")) {
			// other.gameObject.SetActive (false);

			// string message = gameController.currentTargetName + " is current target and hit " + other.gameObject.name;
			// Debug.Log (message);
			// Debug.Log (other.gameObject.name);

			if ((other.gameObject.name==gameController.currentTargetName)||(other.gameObject.name==gameController.currentTargetName+"(Clone)")) 
			{ // to do 

				gameController.incrementScore ();
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
					Application.LoadLevel (Application.loadedLevel);
					Debug.Log ("RESTART! - Level Complete");
					// SceneManager.LoadScene (SceneManager.GetActiveScene()); // need to update

				}

			} else {
				// Not the current target

				// Debug.Log("No match! "+gameController.currentTargetName+" is current target and hit "+other.gameObject.name);
				gameController.incrementPlayerDamage();
				// Bounce! 
				transform.Rotate (0, 0, 180);

			} // END pickup if


		} 
		
	} // end OnTriggerEnter2D


} // END class
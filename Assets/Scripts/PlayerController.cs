using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float acceleration;
	public float rotationSpeed;
	public float targetMass;
	public float completionToWin;
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
	private Quaternion playerRotation = Quaternion.identity;


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
		float moveVertical = Input.GetAxis ("Vertical");

		if(Input.GetKey(KeyCode.Space)||Input.GetKey(KeyCode.UpArrow)) { 
			currentSpeed = (acceleration/2) * speed; 
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
		if (other.gameObject.CompareTag ("PickUp")) {
			// other.gameObject.SetActive (false);

			if (false) { // to do 
				// Not a current target (Quest)
				// Bounce! 
				transform.Rotate (0, 0, 180);
			} else {
				// Is a quest target:
				Instantiate (explosion, other.transform.position, other.transform.rotation);

				Destroy (other.gameObject);
				explodeAudio.Play ();

				targetTally++;


				if (targetTally > completionToWin * gameController.targets) {
					Application.LoadLevel (Application.loadedLevel);
					// SceneManager.LoadScene (SceneManager.GetActiveScene()); // need to update

				}

			} // END pickup if


		} else {
			// Bounce off
			transform.Rotate (0, 0, 180);

		} // END if else
		
	} // end OnTriggerEnter2D

} // END class
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float rotationSpeed;
	public float targetMass;
	public float completionToWin;
	public GameController gameController;
	public SimpleTouchPad touchPad;
	public SimpleTouchPadPlayer touchPad2;
	public Canvas canvas;
	public GameObject explosion;
	public float centerCalibrate;

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
	
	} 
	
	// Update is called once per frame
	void FixedUpdate () {

		rb2d.velocity = Vector2.zero;

		// TouchPad
		Vector2 direction = touchPad.GetDirection ();
		float strength = touchPad.GetStrength ();
		// Debug.Log (strength);

		rb2d.AddTorque (direction.x * rotationSpeed * -1 * strength);
		/*
		if (direction != Vector2.zero) {
			rb2d.AddForce (direction*speed);
			currentDirection = rb2d.velocity;
			// Debug.Log (direction);
		}
		*/
		 


		//Player Touchpad
		Vector2 touchPosition = touchPad2.GetPosition ();
		// Debug.Log (touchPosition);

		if (touchPosition != Vector2.zero) { // ie screen has been touched
			
			// float playerAngle = transform.eulerAngles.z;
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

			float turnAngle = Mathf.DeltaAngle(playerAngle,touchAngle);
			// Debug.Log (turnAngle);

			//Vector2 moveTo = Vector2.MoveTowards(ScreenCenter,touchPosition,1.0f);
			//Debug.Log (moveTo);

			// Mathf.DeltaAngle(
			// float turnDirection = 

			float turnDirection = Mathf.Sign(turnAngle);
			// Debug.Log (turnDirection);

			float strength2 = Vector2.Distance (ScreenCenter, touchPosition);
			// Debug.Log (strength2);

			if (Mathf.Abs(turnAngle) > centerCalibrate) { // get rid of shudder
				rb2d.AddTorque (turnDirection * -1.0f * rotationSpeed);
			}
			// rb2d.AddTorque (turnDirection * rotationSpeed * strength2);

			//rb2d.AddTorque (direction2.x * rotationSpeed * -1 * strength2);

			/*
			if (direction2 != Vector2.zero) {
				rb2d.AddForce (direction2*speed);
				currentDirection = rb2d.velocity;
				// Debug.Log (direction2);
			}
			*/
		}

		// Keyboard Input
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		if (moveHorizontal != 0.0f) {
			rb2d.AddTorque (moveHorizontal * rotationSpeed * -1);
		}

		/*
		if (movement != Vector2.zero) {
			rb2d.AddForce (movement*speed);
			currentDirection = rb2d.velocity;
			Debug.Log (movement);
		}
*/

		// Debug.Log (currentDirection);
		// rb2d.AddForce (currentDirection*speed);
		// rb2d.velocity = currentDirection*speed;

		//Debug.Log (rb2d.GetVector());

		// playerRotation = Quaternion.Euler(0, 0, rb2d.rotation);

		// transform.rotation = playerRotation;
		// transform.rotation = rb2d.rotation
		// rb2d.angularVelocity = 0.0f;


		// Apply force in the direction player is currently poingint
		// currentDirection = new Vector2(transform.rotation.x,transform.rotation.y);

		/*
		if (rb2d.velocity != Vector2.zero) {
			currentDirection = rb2d.velocity;
		}
*/

		float radianAngle = -1*(Mathf.Deg2Rad * (transform.eulerAngles.z-360.0f));
		// Debug.Log (radianAngle);

		currentDirection = new Vector2 (Mathf.Sin (radianAngle),Mathf.Cos (radianAngle));


		// Debug.Log (transform.eulerAngles);

		// Apply force in the direction player is currently poingint
		// var tempvec3 = new Vector3(1,1,1);
		// var tid = Vector3.one;
		// var rotationtemp = transform.rotation * tid;	
		// currentDirection = new Vector2(transform.rotation.x,transform.rotation.y);
		// currentDirection=rotationtemp;


		// Debug.Log (transform.rotation.z);
		// Debug.Log (currentDirection);

		rb2d.AddForce (currentDirection * speed);
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
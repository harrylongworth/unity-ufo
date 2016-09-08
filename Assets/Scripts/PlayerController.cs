using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float rotationSpeed;
	public float targetMass;
	public float completionToWin;
	public GameController gameController;
	public SimpleTouchPad touchPad;
	public SimpleTouchPad touchPad2;
	public GameObject explosion;

	private Rigidbody2D rb2d;
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


		// TouchPad
		Vector2 direction = touchPad.GetDirection ();


		rb2d.AddTorque (direction.x * rotationSpeed * -1);
		/*
		if (direction != Vector2.zero) {
			rb2d.AddForce (direction*speed);
			currentDirection = rb2d.velocity;
			// Debug.Log (direction);
		}
		*/
		 
		Vector2 direction2 = touchPad2.GetDirection ();
		// rb2d.AddForce (direction2*speed);
		rb2d.AddTorque (direction2.x * rotationSpeed * -1);

		/*
		if (direction2 != Vector2.zero) {
			rb2d.AddForce (direction2*speed);
			currentDirection = rb2d.velocity;
			// Debug.Log (direction2);
		}
		*/

	
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
		Debug.Log (currentDirection);

		rb2d.AddForce (currentDirection * speed);
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			// other.gameObject.SetActive (false);

			Instantiate(explosion,other.transform.position,other.transform.rotation);

			Destroy(other.gameObject);
			explodeAudio.Play();

			targetTally++;
			// rb2d.mass = rb2d.mass + targetMass;


			if (targetTally > completionToWin * gameController.targets) {
				Application.LoadLevel (Application.loadedLevel);
				// SceneManager.LoadScene (SceneManager.GetActiveScene());

			}

		}
		
	} // end OnTriggerEnter2D

} // END class
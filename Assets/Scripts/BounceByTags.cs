using UnityEngine;
using System.Collections;


public class BounceByTags : MonoBehaviour {

	public  string[] bounceByTags;
	public float delayBetweenChecks = 0.1f;
	public bool enableBouncing = false;

	// public AudioSource bounceAudio;

	private float lastBounceTime;
	private Rigidbody2D rb2d;

	void Start() {
		lastBounceTime = Time.time;
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		// Debug.Log (tag+" collided with "+other.tag);

		if (enableBouncing) {

			if (bounceByTags != null) {

				float timeSinceLastBounce = Time.time - lastBounceTime;
				if (timeSinceLastBounce > delayBetweenChecks) {
					for (var i = 0; i < bounceByTags.Length; i++) {


						// Debug.Log("currently checking "+bounceByTags[i]);
						if (other.gameObject.CompareTag (bounceByTags [i])) {
							// Bounce! 

							Bounce (gameObject, 50f);

							lastBounceTime = Time.time;


						}


					} // END For



				} // END last bounce time check
			} // END Null check




		} // END bouncing enabled check
			
	} // end OnTriggerEnter2D


	public static void Bounce(GameObject bouncing,float bounceDistance) {

		Rigidbody2D rb2d = bouncing.GetComponent<Rigidbody2D> ();
		// Debug.Log (name+" with tag "+tag+"is off map");
		// player.transform.position = Vector3.zero;

		float bouncedX = bouncing.transform.position.x;
		float bouncedY = bouncing.transform.position.y;

		// Bounce off
		if (Mathf.Abs (bouncedX) > Mathf.Abs (bouncedY)) {
			//bounceDistance = Mathf.Abs(player.transform.position.x * 0.1f);
			bouncedX = bouncedX - Mathf.Sign (bouncedY) * bounceDistance;

		} else {
			//bounceDistance = Mathf.Abs(player.transform.position.y * 0.1f);
			bouncedY = bouncedY - Mathf.Sign (bouncedY) * bounceDistance;
			// Debug.Log (bouncedY);
		}


		bouncing.transform.position = new Vector3 (bouncedX, bouncedY, 0.0f);


		bouncing.transform.Rotate (0, 0, 180);



		float radianAngle = -1 * (Mathf.Deg2Rad * (bouncing.transform.eulerAngles.z - 360.0f + Random.Range (0, 90)));

		Vector2 newDirection = new Vector2 (Mathf.Sin (radianAngle),Mathf.Cos (radianAngle)) * rb2d.velocity.magnitude;
		// * rb2d.velocity.magnitude

		// Debug("Current Velocity:
		rb2d.velocity = newDirection;

	}
}

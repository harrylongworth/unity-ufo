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

							// Debug.Log("Bouncing "+tag+ " off "+other.tag );

							transform.Rotate (0, 0, 180);
							//Debug.Log ("Bounced " + name +" off "+other.name);

							float radianAngle = -1*(Mathf.Deg2Rad * (transform.eulerAngles.z-360.0f+Random.Range(0,90)));
							// Debug.Log (radianAngle);

							Vector2 newDirection = new Vector2 (Mathf.Sin (radianAngle),Mathf.Cos (radianAngle)) * rb2d.velocity.magnitude;
							// * rb2d.velocity.magnitude

							// Debug("Current Velocity:
							rb2d.velocity = newDirection;

							lastBounceTime = Time.time;
							// bounceAudio.Play ();
							// Bounce
						}


					} // END For



				} // END last bounce time check
			} // END Null check




		} // END bouncing enabled check
			
	} // end OnTriggerEnter2D
}

using UnityEngine;
using System.Collections;

public class CheckNotOffMap : MonoBehaviour {

	public int halfMapSide = 1024;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		CatchOffMap ();
	
	}

	void CatchOffMap () {

		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		float maxBackgroundDim = 0.0f;
		bool isOffMap=false;
		float absPlayerX = Mathf.Abs (transform.position.x);
		float absPlayerY = Mathf.Abs (transform.position.y);


		if (absPlayerX > absPlayerY) {
			//X Direction
			maxBackgroundDim = halfMapSide - borderX; // add a border around edge
			if(absPlayerX>maxBackgroundDim) {isOffMap=true;}

		} else {
			// Y Direction
			maxBackgroundDim = halfMapSide - borderY;
			if(absPlayerY>maxBackgroundDim) {isOffMap=true;}
		}

		if (isOffMap) {

			// Debug.Log (name+" with tag "+tag+"is off map");
				// player.transform.position = Vector3.zero;

				float bounceDistance = 10.0f;
				float bouncedX = transform.position.x;
				float bouncedY = transform.position.y;

				// Bounce off
				if (Mathf.Abs (transform.position.x) > Mathf.Abs (transform.position.y)) {
					//bounceDistance = Mathf.Abs(player.transform.position.x * 0.1f);
					bouncedX = transform.position.x - Mathf.Sign (transform.position.x) * bounceDistance;

				} else {
					//bounceDistance = Mathf.Abs(player.transform.position.y * 0.1f);
					bouncedY = transform.position.y - Mathf.Sign (transform.position.y) * bounceDistance;
					// Debug.Log (bouncedY);
				}


				transform.position = new Vector3 (bouncedX, bouncedY, 0.0f);
				transform.Rotate (0, 0, 180);
			float radianAngle = -1*(Mathf.Deg2Rad * (transform.eulerAngles.z-360.0f+Random.Range(0,90)));
			// Debug.Log (radianAngle);

			Vector2 newDirection = new Vector2 (Mathf.Sin (radianAngle),Mathf.Cos (radianAngle)) * rb2d.velocity.magnitude;
			// * rb2d.velocity.magnitude

			// Debug("Current Velocity:
			rb2d.velocity = newDirection;


	
		} 

	}


}

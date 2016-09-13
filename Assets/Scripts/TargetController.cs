using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	private GameObject player;
	public bool bounceOffEdge = true;

	public float maxVelocity = 200f;
	private Rigidbody2D rb2d;
	private int halfMapSide = 1024;
	public string targetName;
	public bool isIndicator=false;

	private Vector3 offset;

	// Use this for initialization
	void Start () {

		if (tag != "Barrier") {
			rb2d = GetComponent<Rigidbody2D> ();

			rb2d.velocity = Random.insideUnitCircle * Random.Range (0.1f, maxVelocity);
		}




	}

	void Update() {

		if (tag == "Indicator") {
			player = GameObject.FindGameObjectWithTag ("Player");
			offset = new Vector3(0.5f,-1*(Screen.height/2)*0.1f,5.0f);

			// offset = new Vector3(-1*(Screen.width/2)*0.6f,-1*(Screen.height/2)*0.6f,0.0f);
			// offset = Vector3.zero;

			rb2d.velocity=Vector2.zero;
			// rb2d.velocity = player.GetComponent<Rigidbody2D>().velocity;

			transform.position = player.transform.position + offset;
			// transform.position = player.transform.position;
		} else {
			CatchOffMap ();
		}
			
	} // END Update

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
			// player.transform.position = Vector3.zero;

			if (bounceOffEdge) {
				float bounceDistance = 10.0f;
				float bouncedX = transform.position.x;
				float bouncedY = transform.position.y;

				// Bounce off
				if (Mathf.Abs(transform.position.x)>Mathf.Abs(transform.position.y)) 
				{
					//bounceDistance = Mathf.Abs(transform.position.x * 0.1f);
					bouncedX = transform.position.x-Mathf.Sign(transform.position.x)*bounceDistance;
					rb2d.velocity = new Vector2(-1.0f*rb2d.velocity.x,rb2d.velocity.y);

				}	else {
					//bounceDistance = Mathf.Abs(transform.position.y * 0.1f);
					bouncedY = transform.position.y-Mathf.Sign(transform.position.y)*bounceDistance;
					// Debug.Log (bouncedY);
					rb2d.velocity = new Vector2(rb2d.velocity.x,rb2d.velocity.y*-1.0f);
				}


				transform.position = new Vector3 (bouncedX, bouncedY, 0.0f);

			} else {


				float newX = transform.position.x;
				float newY = transform.position.y;

				// Bounce off
				if (Mathf.Abs (transform.position.x) > Mathf.Abs (transform.position.y)) {
					//bounceDistance = Mathf.Abs(player.transform.position.x * 0.1f);
					newX = newX*-0.99f;
				} else {
					newY = newY * -0.99f;
				}

				transform.position = new Vector3 (newX, newY, 0.0f);


			}






		} 

	}

}

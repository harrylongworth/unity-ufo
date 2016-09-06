using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public SimpleTouchPad touchPad;
	public SimpleTouchPad touchPad2;

	private Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D>();
	
	} 
	
	// Update is called once per frame
	void FixedUpdate () {

		// TouchPad
		Vector2 direction = touchPad.GetDirection ();
		rb2d.AddForce (direction*speed);

		Vector2 direction2 = touchPad2.GetDirection ();
		rb2d.AddForce (direction2*speed);

		// Keyboard
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		rb2d.AddForce (movement*speed);


	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			other.gameObject.SetActive (false);
		}
		
	}
}
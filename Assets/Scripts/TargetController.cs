using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	private GameObject player;


	public float maxVelocity = 200f;
	private Rigidbody2D rb2d;
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
			offset = new Vector3(0.5f,-1*(Screen.height/2)*0.15f,5.0f);

			// offset = new Vector3(-1*(Screen.width/2)*0.6f,-1*(Screen.height/2)*0.6f,0.0f);
			// offset = Vector3.zero;

			rb2d.velocity=Vector2.zero;
			// rb2d.velocity = player.GetComponent<Rigidbody2D>().velocity;

			transform.position = player.transform.position + offset;
			// transform.position = player.transform.position;
		} 
			
	} // END Update



}

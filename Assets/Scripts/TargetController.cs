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

		rb2d = GetComponent<Rigidbody2D> ();

		rb2d.velocity = Random.insideUnitCircle * Random.Range (0.1f, maxVelocity);


	}

	void Update() {

		if (tag == "Indicator") {
			player = GameObject.FindGameObjectWithTag ("Player");
			offset = new Vector3(0.5f,+1*(Screen.height/2)*0.3f,5.0f);

			// offset = new Vector3(-1*(Screen.width/2)*0.6f,-1*(Screen.height/2)*0.6f,0.0f);
			// offset = Vector3.zero;

			rb2d.velocity=Vector2.zero;
			// rb2d.velocity = player.GetComponent<Rigidbody2D>().velocity;

			transform.position = player.transform.position + offset;
			// transform.position = player.transform.position;
		} 

	} // END Update

	public GameObject [] spawn(int setsToSpawn,float halfMapSide) {


		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		// halfMapSide = (int)background
		int spawnRange = (int) Mathf.Round(halfMapSide*0.9f);
		float spawnRangeX = spawnRange-borderX;
		float spawnRangeY = spawnRange-borderY;

		int spriteCount = GetComponent<SpriteManager> ().GetLength ();
		int totalObjects = setsToSpawn * spriteCount;
			
		GameObject[] objectArray = new GameObject[totalObjects];
		int arrayCounter = 0;

		// Build Targets
		for (int i = 0; i < setsToSpawn; i++) {
			for (int x = 0; x < spriteCount; x++) {

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRangeX, spawnRangeX), Random.Range (-spawnRangeY, spawnRangeY),0.0f);
				Quaternion spawnRotation = Quaternion.identity;
				var targetTemp = (GameObject) Instantiate (gameObject, spawnPosition, spawnRotation);
				targetTemp.name = x.ToString();
				targetTemp.GetComponent<SpriteManager> ().SetSpriteByID (x);

				objectArray [arrayCounter] = targetTemp;
				arrayCounter++;

				// Debug.Log (targetTemp.name);
			}
		} // END for

		return objectArray;

	}

}

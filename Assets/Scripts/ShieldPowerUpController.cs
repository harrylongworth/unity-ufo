using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShieldPowerUpController : MonoBehaviour {

	public static List <GameObject> powerUps;

	private GameObject player;
	public float maxVelocity = 200f;
	private Rigidbody2D rb2d;
	private Vector3 offset;


	public static void DestroyAll () {

		if (powerUps != null) {
			foreach (GameObject item in powerUps) {
				if (item != null) {
					// Debug.Log ("Destroying " + item.name);
					GameObject.Destroy (item.gameObject);
				}

			} // END for
			powerUps = null;

		} // END if

	} // END DestroyAll

	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();

		rb2d.velocity = Random.insideUnitCircle * Random.Range (0.1f, maxVelocity);


	}



	public static void Spawn(float halfMapSide, GameObject targetType) {


		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		// halfMapSide = (int)background
		int spawnRange = (int) Mathf.Round(halfMapSide*0.9f);
		float spawnRangeX = spawnRange-borderX;
		float spawnRangeY = spawnRange-borderY;

		if (powerUps == null) {
			powerUps = new List<GameObject> ();
		}

		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRangeX, spawnRangeX), Random.Range (-spawnRangeY, spawnRangeY),1.0f);
		Quaternion spawnRotation = Quaternion.identity;
		GameObject targetTemp = (GameObject) Instantiate (targetType, spawnPosition, spawnRotation);

		powerUps.Add(targetTemp);

	}

}

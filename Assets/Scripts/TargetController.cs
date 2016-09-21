using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	public static GameObject [] objectArray;
	public bool disableQuestIndicator = true;

	private GameObject player;
	public float maxVelocity = 200f;
	private Rigidbody2D rb2d;
	private Vector3 offset;


	public static void DestroyAll () {

		if (objectArray != null) {
			foreach (GameObject item in objectArray) {

				GameObject.Destroy (item);
			}

		} // END if

	} // END DestroyAll

	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();

		rb2d.velocity = Random.insideUnitCircle * Random.Range (0.1f, maxVelocity);


	}

	void Update() {

	} // END Update

	public static void Spawn(int setsToSpawn,float halfMapSide, GameObject targetType) {


		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		// halfMapSide = (int)background
		int spawnRange = (int) Mathf.Round(halfMapSide*0.9f);
		float spawnRangeX = spawnRange-borderX;
		float spawnRangeY = spawnRange-borderY;

		int spriteCount = 0;
		if (targetType.name == "ShieldPowerUp") {
			spriteCount = 1;
		} else {
			spriteCount = targetType.GetComponent<SpriteManager> ().GetLength ();
		}

		int totalObjects = setsToSpawn * spriteCount;
			
		objectArray = new GameObject[totalObjects];
		int arrayCounter = 0;

		// Build Targets
		for (int i = 0; i < setsToSpawn; i++) {
			for (int x = 0; x < spriteCount; x++) {

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRangeX, spawnRangeX), Random.Range (-spawnRangeY, spawnRangeY),1.0f);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject targetTemp = (GameObject) Instantiate (targetType, spawnPosition, spawnRotation);

				if (targetType.name == "ShieldPowerUp") {
					targetTemp.name = "ShieldPowerUp";
				} else {
					targetTemp.name = x.ToString();
					targetTemp.GetComponent<SpriteManager> ().SetSpriteByID (x);
				}


				objectArray [arrayCounter] = targetTemp;
				arrayCounter++;

				// string debugMessage = "Sprite " + targetTemp.GetComponent<SpriteRenderer> ().sprite.name + " issued ID " + targetTemp.name;
				// Debug.Log (debugMessage);

			}
		} // END for

	}

	public int GetLength() {
		return GetComponent<SpriteManager> ().GetLength ();
	}

	public string GetName(int currentTargetIndex) {
		return GetComponent<SpriteManager> ().GetName (currentTargetIndex);
	}

	public Sprite GetSprite(int currentTargetIndex) {
		return GetComponent<SpriteManager> ().GetSpriteByID(currentTargetIndex);
	}
}

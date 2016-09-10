using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject [] targets;
	public int targetsNeededToWin;
	public GameObject player;
	public GameObject background;
	public int targetSets;
	public int halfMapSide;

	public Text currentTargetText;

	private string[] targetNames;
	public string currentTargetName;
	public int currentTargetIndex;
	private GameObject currentTargetIndicator;


	// Use this for initialization
	void Start () {
		targetNames = new string[targets.Length];

		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		// halfMapSide = (int)background
		int spawnRange = (int) Mathf.Round(halfMapSide*0.9f);
		float spawnRangeX = spawnRange-borderX;
		float spawnRangeY = spawnRange-borderY;

		for (int i = 0; i < targetSets; i++) {
			for (int x = 0; x < targets.Length; x++) {

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRangeX, spawnRangeX), Random.Range (-spawnRangeY, spawnRangeY),0.0f);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (targets[x], spawnPosition, spawnRotation);
				if (i == 0) {
					targetNames[x]=targets[x].name;
					// Debug.Log (targetNames[x]);
				} 
			
			}
		} // END for

		currentTargetName = targetNames [0];
		currentTargetIndex = 0;
		Destroy (currentTargetIndicator);
		currentTargetText.text=currentTargetName;
		currentTargetIndicator = (GameObject) Instantiate (targets[currentTargetIndex], new Vector3(20,20,1), Quaternion.identity);
		currentTargetIndicator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		currentTargetIndicator.tag = "Indicator";
		currentTargetIndicator.name = "Indicator";
		currentTargetIndicator.transform.localScale = new Vector3(0.5f,0.5f,0.0f);

	} // END start

	void Update() {
		CatchOffMap ();
	} // END Update

	void CatchOffMap () {

		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		float maxBackgroundDim = 0.0f;
		bool isOffMap=false;
		float absPlayerX = Mathf.Abs (player.transform.position.x);
		float absPlayerY = Mathf.Abs (player.transform.position.y);


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

			float bounceDistance = 10.0f;
			float bouncedX = player.transform.position.x;
			float bouncedY = player.transform.position.y;

			// Bounce off
			if (Mathf.Abs(player.transform.position.x)>Mathf.Abs(player.transform.position.y)) 
			{
				//bounceDistance = Mathf.Abs(player.transform.position.x * 0.1f);
				bouncedX = player.transform.position.x-Mathf.Sign(player.transform.position.x)*bounceDistance;

			}	else {
				//bounceDistance = Mathf.Abs(player.transform.position.y * 0.1f);
				bouncedY = player.transform.position.y-Mathf.Sign(player.transform.position.y)*bounceDistance;
				// Debug.Log (bouncedY);
			}


			player.transform.position = new Vector3 (bouncedX, bouncedY, 0.0f);
			player.transform.Rotate (0, 0, 180);
		} 

	}

		
	public string[] GetTargetNames() {
		return targetNames;
	}

	public void NextTarget () {

		Debug.Log (currentTargetName);

		currentTargetIndex++;
		if (currentTargetIndex > targets.Length-1) {
			currentTargetIndex = 0;
		} 

		currentTargetName = targets [currentTargetIndex].name;

		currentTargetText.text=currentTargetName;
		currentTargetIndicator = (GameObject) Instantiate (targets[currentTargetIndex], new Vector3(20,20,1), Quaternion.identity);
		currentTargetIndicator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		currentTargetIndicator.tag = "Indicator";
		currentTargetIndicator.name = "Indicator";
		currentTargetIndicator.transform.localScale = new Vector3(0.5f,0.5f,0.0f);

	}

} // FND class

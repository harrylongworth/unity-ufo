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

	public Text displayTime;
	public Text displayDamage;
	public Text displayScore;

	private string[] targetNames;
	public string currentTargetName;
	public int currentTargetIndex;
	private GameObject currentTargetIndicator;
	private float startTime;

	private GameObject targetTemp;

	private float timeTicker;
	private int playerDamage;
	private int score;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		//Damage
		playerDamage = 0;
		displayDamage.text = "Damage: "+playerDamage.ToString();

		//Score
		score = 0;
		displayScore.text = "Score: "+score.ToString();

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
				targetTemp = (GameObject) Instantiate (targets[x], spawnPosition, spawnRotation);
				if (i == 0) {
					targetNames[x]=targets[x].name;
					// targetNames [x] = targetTemp.name;
					// Debug.Log (targetNames[x]);
					// Debug.Log (targetTemp.name);
				} 
				// Debug.Log (targetTemp.name);
			}
		} // END for

		currentTargetName = targetNames [0];
		currentTargetIndex = 0;
		Destroy (currentTargetIndicator);
		displayTime.text = "Time: 0";
		currentTargetIndicator = (GameObject) Instantiate (targets[currentTargetIndex], new Vector3(20,20,1), Quaternion.identity);
		currentTargetIndicator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		currentTargetIndicator.tag = "Indicator";
		currentTargetIndicator.name = "Indicator";
		currentTargetIndicator.transform.localScale = new Vector3(0.5f,0.5f,0.0f);

	} // END start

	void Update() {

		timeTicker = Mathf.Round(Time.time - startTime);
		displayTime.text = "Time: "+timeTicker.ToString();

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



		currentTargetIndex++;
		if (currentTargetIndex > targets.Length-1) {
			currentTargetIndex = 0;
		} 

		currentTargetName = targets [currentTargetIndex].name;

		displayTime.text=currentTargetName;
		currentTargetIndicator = (GameObject) Instantiate (targets[currentTargetIndex], new Vector3(20,20,1), Quaternion.identity);
		currentTargetIndicator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		currentTargetIndicator.tag = "Indicator";
		currentTargetIndicator.name = "Indicator";
		currentTargetIndicator.transform.localScale = new Vector3(0.5f,0.5f,0.0f);

	}

	public void incrementPlayerDamage() {
		playerDamage++;
		displayDamage.text = "Damage: "+playerDamage.ToString();
	}


	public void incrementScore() {
		score++;
		displayScore.text = "Score: "+score.ToString();
	}

} // FND class

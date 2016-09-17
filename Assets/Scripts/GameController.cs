using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject [] targets;
	public GameObject barrier;
	public GameObject MovementGUI;
	public bool paused=false;

	public float edgeSpacing=10f;
	public bool enableQuests = true;
	public bool showQuestName = true;
	public bool targetIndicatorEnabled = true;
	public bool sayQuestName = true;

	public bool invincible=false;
	public int lives=3;
	public int shield=5;

	public bool bounceOffEdge = false;
	public int targetsNeededToWin;
	public GameObject player;
	public GameObject background;
	public int targetSets;
	public int halfMapSide;


	public Text displayTime;
	public Text displayDamage;
	public Text displayScore;
	public Text displayQuestName;
	public Text displayShield;
	public Text displayLives;
	public Text pauseButton;

	private string[] targetNames;
	public string currentTargetName;
	public int currentTargetIndex;
	private GameObject currentTargetIndicator;
	private float startTime;

	private GameObject targetTemp;
	private GameObject currentTargetObject;

	private float timeTicker;

	private float pausedtime;

	private SimpleTouchPadPlayer touchPad;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		// targetIndicatorEnabled = tossCoin ();


		// touchPad = MovementGUI.GetComponent<SimpleTouchPadPlayer> ();

		targetNames = new string[targets.Length];

		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		// halfMapSide = (int)background
		int spawnRange = (int) Mathf.Round(halfMapSide*0.9f);
		float spawnRangeX = spawnRange-borderX;
		float spawnRangeY = spawnRange-borderY;

		// Build Targets
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

		// Build Map Edge

		float paddingX = Screen.width / 2;
		float paddingY = Screen.height / 2;

		float xEdge = halfMapSide - paddingX; 
		float yEdge = halfMapSide - paddingY;

		int xLoop = (int) Mathf.Round((yEdge * 2) / edgeSpacing)+1; // X loop
		int yLoop = (int) Mathf.Round(xEdge * 2 / edgeSpacing)+1; // Y loop

 
		float currentY=-yEdge;

		for (int i = 0; i < xLoop; i++) {

			Quaternion spawnRotation = Quaternion.identity;

			Vector3 spawnPositionTop = new Vector3 (xEdge,currentY,0.0f);
			Instantiate (barrier, spawnPositionTop, spawnRotation);

			Vector3 spawnPositionBottom = new Vector3 (-xEdge,currentY,0.0f);
			Instantiate (barrier, spawnPositionBottom, spawnRotation);

			currentY += edgeSpacing;
		}

		float currentX=-xEdge;

		for (int i = 0; i < yLoop; i++) {

			Quaternion spawnRotation = Quaternion.identity;

			Vector3 spawnPositionLeft = new Vector3 (currentX,yEdge,0.0f);
			Instantiate (barrier, spawnPositionLeft, spawnRotation);

			Vector3 spawnPositionRight = new Vector3 (currentX,-yEdge,0.0f);
			Instantiate (barrier, spawnPositionRight, spawnRotation);

			currentX += edgeSpacing;
		}


		if (enableQuests) {


			currentTargetObject = targets [0];

			playCurrentTargetAudio ();

			currentTargetName = targetNames [0];

			if (showQuestName) {
				displayQuestName.enabled = true;
			} else {
				displayQuestName.enabled = false;
			}

			displayQuestName.text = currentTargetName;

			currentTargetIndex = 0;
			displayTime.text = "Time: 0";

			// targets[currentTargetIndex].GetComponent<AudioSource>().Play();

			if (targetIndicatorEnabled) {
				currentTargetIndicator = (GameObject)Instantiate (targets [currentTargetIndex], new Vector3 (20, 20, 2), Quaternion.identity);
				currentTargetIndicator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				currentTargetIndicator.tag = "Indicator";
				currentTargetIndicator.name = "Indicator";
				currentTargetIndicator.transform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
				currentTargetIndicator.GetComponent<BounceByTags> ().bounceByTags = null;
			} 


		} else {
			displayQuestName.enabled = false;

			displayDamage.enabled = false;
		}		// END enableQuests is true



	} // END start

	void Update() {

		// Detect Pause / Play

		/*
		Vector2 touchPadPosition = touchPad.GetPosition ();
		if (touchPadPosition.x > Screen.width * 0.9 / 2 || touchPadPosition.y > Screen.height * 0.9 / 2) {

			Debug.Log ("Paused is" + paused);

			if (paused) {
				onPlay ();
			} else { 
				onPause ();
			}
		}

		*/

		if (Time.timeScale == 0 && !paused) {
			if (Input.anyKeyDown) {
				Time.timeScale = 1f;
			}
		}


		timeTicker = Mathf.Round(Time.time - startTime);
		displayTime.text = "Time: "+timeTicker.ToString();

	} // END Update


		
	public string[] GetTargetNames() {
		return targetNames;
	}

	public void NextTarget () {

		currentTargetIndex++;
		if (currentTargetIndex > targets.Length-1) {
			currentTargetIndex = 0;
		} 

		currentTargetObject = targets [currentTargetIndex];
		currentTargetName = currentTargetObject.name;

		// targets [currentTargetIndex].GetComponent<AudioSource>().Play();
		// displayTime.text=currentTargetName;

		if (targetIndicatorEnabled) {
			currentTargetIndicator = (GameObject) Instantiate (targets[currentTargetIndex], new Vector3(20,20,2), Quaternion.identity);
			currentTargetIndicator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

			currentTargetIndicator.tag = "Indicator";
			currentTargetIndicator.name = "Indicator";
			currentTargetIndicator.transform.localScale = new Vector3(0.5f,0.5f,1.0f);
			currentTargetIndicator.GetComponent<BounceByTags> ().bounceByTags = null;

		}

		playCurrentTargetAudio ();

		displayQuestName.text = currentTargetName;
			
	}



	public bool tossCoin() {
		float randomNumber = Random.Range (0.0f, 1.0f);
		if (randomNumber < 0.5f) {
			return true;
		} else {
			return false;
		}

	} // end tosscoin

	public void playCurrentTargetAudio() {

		if (sayQuestName) {
			// Debug.Log ("Play Audio?");

			AudioSource[] tempAudio = GetComponents<AudioSource> ();
			AudioSource currentAudio = tempAudio [currentTargetIndex];

			currentAudio.volume = 1.0f;
			currentAudio.PlayDelayed (0.5f);

		}

		//  BELOW should work but doesn't :( so try adding audio direct to Game Controller

		//AudioSource tempAudio = targets[currentTargetIndex].GetComponent<AudioSource> ();
			
		// AudioSource tempAudio = currentTargetObject.GetComponent<AudioSource> ();
		// AudioSource tempAudio = GetComponent<AudioSource> ();

//		tempAudio.volume = 1;

//		if (tempAudio!=null) {
//			Debug.Log (tempAudio.name);
//			tempAudio.Play ();
		//
	//	} else {
	//		Debug.Log ("ERROR: AudioSource is NULL");
	//	}

		// targets [currentTargetIndex].GetComponent<AudioSource> ().Play ();


	} // end playCurrent

	public void pressPauseButton() {

		if (paused) {
			onPlay ();
		} else { 
			onPause ();
		}

	}

	public void onPause() {
		if (Time.timeScale == 1) {
				Time.timeScale = 0f;
		}

		paused = true;
		pausedtime = Time.time;

		pauseButton.text = "Play";

	}

	public void onPlay() {
		
		if (Time.timeScale == 0 || (Time.time-pausedtime>0.3)) {
			Time.timeScale = 1f;
		}

		paused = false;
		pauseButton.text = "Pause";


	}


} // FND class

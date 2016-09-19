﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	// public GameObject [] targets;
	public GameObject mapEdge;
	public GameObject player;
	public GameObject background;

	public GameObject MovementGUI;
	public bool paused=false;

	public bool enableQuests = true;
	public bool showQuestName = true;
	public bool targetIndicatorEnabled = true;
	public bool sayQuestName = true;

	public bool invincible=false;
	public int lives=3;
	public int shield=5;

	public bool bounceOffEdge = false;
	public int targetsNeededToWin;
	public int targetSetsNeededToWin = 1;


	public int targetSets;
	public int halfMapSide=1024;


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

	private TargetManager targetManager;

	private GameObject targetTemp;

	private float pausedtime;

	private SimpleTouchPadPlayer touchPad;

	public int currentLevel = 0;
	private GameObject currentTarget;

	// Use this for initialization
	void Start () {
		targetManager = GetComponent<TargetManager> ();

		NewGame ();


	} // END start

	void Update() {

		// Restart from Pause if currently paused and no longer paused 
		if (Time.timeScale == 0 && !paused) {
			if (Input.anyKeyDown) {
				Time.timeScale = 1f;
			}
		}


	} // END Update


		
	public void NextTarget () {


		currentTargetIndex++;

		var currentTarget = targetManager.GetTarget(currentLevel);

		if (currentTargetIndex > currentTarget.GetComponent<TargetController>().GetLength()-1) {
			currentTargetIndex = 0;
		} 

		currentTargetName = currentTarget.GetComponent<TargetController>().GetName (currentTargetIndex);

		if (targetIndicatorEnabled) {
			currentTargetIndicator = (GameObject) Instantiate (currentTarget, new Vector3(20,20,2), Quaternion.identity);
			currentTargetIndicator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

			currentTargetIndicator.tag = "Indicator";
			currentTargetIndicator.name = "Indicator";
			currentTargetIndicator.transform.localScale = new Vector3(0.5f,0.5f,1.0f);
			currentTargetIndicator.GetComponent<BounceByTags> ().bounceByTags = null;
			currentTargetIndicator.GetComponent<SpriteManager> ().SetSpriteByID (currentTargetIndex);

		}

		displayQuestName.text = currentTargetName;

		// To Do
		//**********************
		// playCurrentTargetAudio ();
	}



	public bool TossCoin() {
		float randomNumber = Random.Range (0.0f, 1.0f);
		if (randomNumber < 0.5f) {
			return true;
		} else {
			return false;
		}

	} // end tosscoin

	public void PlayCurrentTargetAudio() {

		/*
		if (sayQuestName) {
			// Debug.Log ("Play Audio?");

			AudioSource[] tempAudio = GetComponents<AudioSource> ();
			AudioSource currentAudio = tempAudio [currentTargetIndex];

			currentAudio.volume = 1.0f;
			currentAudio.PlayDelayed (0.5f);

		}

*/
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
			OnPlay ();
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

		pauseButton.text = ">";

	}

	public void OnPlay() {
		
		if (Time.timeScale == 0 || (Time.time-pausedtime>0.3)) {
			Time.timeScale = 1f;
		}

		paused = false;
		pauseButton.text = "| |";


	}

	public void NewGame() {

		currentLevel=0;
		player.GetComponent<PlayerController>().NewGame();

		NewLevel ();


	}

	public void NewLevel() {
		currentTargetIndex = 0;
			
		if (currentLevel > targetManager.GetLength ()) {
			currentLevel = 0;
		}

		displayTime.GetComponent<TimeTicker> ().ResetTicker (); 

		// Set Random Background
		background.GetComponent<SpriteManager> ().SetSpriteRandom ();

		player.GetComponent<PlayerController>().NewLevel();


		// Spawn Map Edge
		MapEdgeController.Spawn (halfMapSide,mapEdge);

		var currentTarget = targetManager.GetTarget(currentLevel);

		TargetController.Spawn (targetSets,halfMapSide,currentTarget);

		if (enableQuests) {
			targetsNeededToWin = ((currentTarget.GetComponent<TargetController>().GetLength ()-1) * targetSetsNeededToWin)+1;

			currentTargetIndex --;
			NextTarget ();

			if (showQuestName) {
				displayQuestName.enabled = true;
			} else {
				displayQuestName.enabled = false;
			}

			displayTime.text = "Time: 0";

		} else {
			displayQuestName.enabled = false;

			displayDamage.enabled = false;
		}		// END enableQuests is true

	} // END NewLevel

} // FND class

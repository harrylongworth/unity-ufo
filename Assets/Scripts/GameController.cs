using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	// public GameObject [] targets;
	public GameObject mapEdge;
	private GameObject[] mapEdgeObjects = null;

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
	public GameObject player;
	public GameObject background;
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

	private GameObject targetTemp;
	private GameObject currentTargetObject;


	private float pausedtime;

	private SimpleTouchPadPlayer touchPad;

	private GameObject[] targets;

	// Use this for initialization
	void Start () {


		NewGame ();

		if (enableQuests) {

			/*
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
*/

		} else {
			displayQuestName.enabled = false;

			displayDamage.enabled = false;
		}		// END enableQuests is true



	} // END start

	void Update() {

		// Restart from Pause if currently paused and no longer paused 
		if (Time.timeScale == 0 && !paused) {
			if (Input.anyKeyDown) {
				Time.timeScale = 1f;
			}
		}


	} // END Update


		
	public string[] GetTargetNames() {
		return targetNames;
	}

	public void NextTarget () {


		currentTargetIndex++;

		/*
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
			*/

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
		
		NewLevel ();
	}

	public void NewLevel() {
		displayTime.GetComponent<TimeTicker> ().ResetTicker (); 


		// MAP EDGE:
		// Delete previous map edge if exists
		if (mapEdgeObjects != null) {
			foreach (GameObject item in mapEdgeObjects) {

				GameObject.Destroy (item);
			}

		} // END if

		// Spawn Map Edge
		mapEdgeObjects = mapEdge.GetComponent<MapEdgeController> ().Spawn (halfMapSide);

	} // END NewLevel

} // FND class

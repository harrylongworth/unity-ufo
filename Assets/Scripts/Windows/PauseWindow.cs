using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseWindow : MonoBehaviour {

	public GameObject gameManager;
	public GameObject continueButton; 
	public bool canContinue = false;

	public void OnContinue() {
		
		gameManager.GetComponent<GameController> ().OnPlay ();
		CloseWindow ();

	}

	public void onNewGame() {
		
		gameManager.GetComponent<GameController> ().NewGame ();
		CloseWindow ();

	}

	public void CloseWindow() {
		gameObject.SetActive (false);
	}

	public void OpenWindow() {
		gameObject.SetActive (true);

		if (canContinue) {
			continueButton.SetActive (true);
		} else {
			continueButton.SetActive (false);
		}
	}

}

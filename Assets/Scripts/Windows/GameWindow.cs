using UnityEngine;
using System.Collections;

public class GameWindow : MonoBehaviour {

	public void CloseWindow() {
		gameObject.SetActive (false);
	}

	public void OpenWindow() {
		gameObject.SetActive (true);

	}
}

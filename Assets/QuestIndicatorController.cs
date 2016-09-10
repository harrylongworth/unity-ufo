using UnityEngine;
using System.Collections;

public class QuestIndicatorController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {


		offset = new Vector3(player.transform.position.x-(Screen.width-20),player.transform.position.y-(Screen.height-20));

	}

	void LateUpdate () {
		// LateUpdate runs after all items in update have been processed (e.g. player has moved)

		transform.position = player.transform.position + offset;

	} // LateUpdate

}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
	
		offset = transform.position - player.transform.position;

	}

	void LateUpdate () {
		// LateUpdate runs after all items in update have been processed (e.g. player has moved)

		transform.position = player.transform.position + offset;
	
	} // LateUpdate

}

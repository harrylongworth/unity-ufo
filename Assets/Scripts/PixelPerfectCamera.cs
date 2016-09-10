using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {

	public GameObject player;
	public static float pixeltoUnits = 1f;
	public static float scale = 1f;

	public Vector2 nativeResolution = new Vector2 (320, 320);

	private Vector3 offset;


	void Awake () {
		pixeltoUnits = 1.0f;
		var camera = GetComponent<Camera> ();
		if (camera.orthographic) {
			scale = Screen.height / nativeResolution.y;
			// Debug.Log (scale);
			pixeltoUnits *= scale;
			camera.orthographicSize = (Screen.height/2.0f)/pixeltoUnits;
			// Debug.Log (camera.orthographicSize);
		} // End if

	} // END Awake


	// Use this for initialization
	void Start () {


		offset = transform.position - player.transform.position;

	}

	void LateUpdate () {
		// LateUpdate runs after all items in update have been processed (e.g. player has moved)

		transform.position = player.transform.position + offset;

	} // LateUpdate
	
} // END Class

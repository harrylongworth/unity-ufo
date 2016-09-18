using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public GameController gameController;
	public Vector2 cameraZoomRange;
	public float zoomBy;
	public float zoomByMouse;
	public float touchZoomSpeed = 0.5f; 

	private float currentZoom;



	// Use this for initialization
	void Start () {
		var camera = GetComponent<Camera> ();
		currentZoom = camera.orthographicSize;
	
	}

	// Update is called once per frame
	void Update () {

		if (!gameController.paused) {

			// Debug.Log (Input.mouseScrollDelta);

			// from Unity : https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom
			var camera = GetComponent<Camera> ();

			if (Input.touchCount == 2)
			{

				// Debug.Log ("Two Touches Detected!");

				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				// If the camera is orthographic...
				if (camera.orthographic)
				{

					// Debug.Log ("Zooming Camera");

					Debug.Log(deltaMagnitudeDiff);

					// ... change the orthographic size based on the change in distance between the touches.
					if(deltaMagnitudeDiff <0.1) {ZoomIn();} else {if(deltaMagnitudeDiff >0.1){ZoomOut();}}


					// Make sure the orthographic size never drops below zero.
					// currentZoom = Mathf.Max(camera.orthographicSize, 0.1f);
				}
				else
				{

					// Not implementing as not used in this game

					/*
				// Otherwise change the field of view based on the change in distance between the touches.
					camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

					// Clamp the field of view to make sure it's between 0 and 180.
					camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
					*/
				}
			}


			// Mouse based zoom
			if (Input.mouseScrollDelta.y>0.1f) { ZoomInMouse (); }
			if (Input.mouseScrollDelta.y<-0.1f) { ZoomOutMouse (); }

			// Z and X key based zoom
			if (Input.GetKey (KeyCode.Z)) { ZoomIn (); }
			if (Input.GetKey (KeyCode.X)) { ZoomOut ();}

			// set current zoom

			if (camera.orthographic) {
				camera.orthographicSize = currentZoom;
				// Debug.Log (camera.orthographicSize);
			} // End if

			// Debug.Log (camera.orthographicSize);



		} // FEND Paused check



	
	} // END Update

	void ZoomIn() {
		if (currentZoom >= cameraZoomRange.x) {
			currentZoom-=zoomBy;
		}
	}

	void ZoomOut() {
		if (currentZoom <= cameraZoomRange.y) {
			currentZoom+=zoomBy;
		}
			
	}

	void ZoomInMouse() {
		if (currentZoom >= cameraZoomRange.x) {
			currentZoom-=zoomByMouse;
		}
	}

	void ZoomOutMouse() {
		if (currentZoom <= cameraZoomRange.y) {
			currentZoom+=zoomByMouse;
		}

	}


}

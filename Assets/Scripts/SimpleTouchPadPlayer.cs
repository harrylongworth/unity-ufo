using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPadPlayer : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public Vector2 touchPosition;
	private bool touched;
	private int pointerID;

	void Awake () {
		touchPosition = Vector2.zero;
		touched = false;
	}

	void Start() {
	}

	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			touchPosition = data.position;

						// SHOW Joystick
			// Vector3 joyPosition = new Vector3 (data.position.x, data.position.y,0.0f);
			// Quaternion joyRotation = Quaternion.identity;
			// Instantiate (joystick, joyPosition, joyRotation);

		}
	}

	public void OnDrag (PointerEventData data) {
		if (data.pointerId == pointerID) {

			touchPosition = data.position;

			// UPDATE Joystick
			// Vector3 joyPosition = new Vector3 (data.position.x, data.position.y,0.0f);
			// Quaternion joyRotation = Quaternion.identity;

			// joystick.transform.position = joyPosition;
		}
	}

	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			touchPosition = Vector2.zero;
			touched = false;

			// Destroy(joystick);
		}
	}

	public Vector2 GetPosition () {
		//smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);

		//return smoothDirection;
		return touchPosition;
	}

	public Vector2 GetCenter () {

		return new Vector2 (transform.position.x, transform.position.y);

	}
}
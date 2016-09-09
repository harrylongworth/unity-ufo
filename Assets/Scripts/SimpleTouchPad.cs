﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothing;
	public int calibrateStrength;
	// public GameObject joystick;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private bool touched;
	private int pointerID;
	private float strength;

	void Awake () {
		direction = Vector2.zero;
		touched = false;
		strength = 0;
	}

	void Start() {
	}

	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;

			// SHOW Joystick
			// Vector3 joyPosition = new Vector3 (data.position.x, data.position.y,0.0f);
			// Quaternion joyRotation = Quaternion.identity;
			// Instantiate (joystick, joyPosition, joyRotation);

		}
	}

	public void OnDrag (PointerEventData data) {
		if (data.pointerId == pointerID) {
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;

			strength = directionRaw.magnitude;

			// UPDATE Joystick
			// Vector3 joyPosition = new Vector3 (data.position.x, data.position.y,0.0f);
			// Quaternion joyRotation = Quaternion.identity;

			// joystick.transform.position = joyPosition;
		}
	}

	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			direction = Vector2.zero;
			touched = false;

			// Destroy(joystick);
		}
	}

	public Vector2 GetDirection () {
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		//return smoothDirection;
		return direction;
	}

	public float GetStrength () {
		return strength/calibrateStrength;
	} // END GetStrength
}
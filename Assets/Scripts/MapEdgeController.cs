using UnityEngine;
using System.Collections;

public class MapEdgeController : MonoBehaviour {

	public float edgeSpacing=80f;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject[] Spawn(int halfMapSide) {

		// Build Map Edge


		float paddingX = Screen.width / 2;
		float paddingY = Screen.height / 2;

		float xEdge = halfMapSide - paddingX; 
		float yEdge = halfMapSide - paddingY;

		int xLoop = (int) Mathf.Round((yEdge * 2) / edgeSpacing)+1; // X loop
		int yLoop = (int) Mathf.Round(xEdge * 2 / edgeSpacing)+1; // Y loop

		float currentY=-yEdge;

		int totalObjects = xLoop * 2 + yLoop * 2;
		GameObject [] objectsArray = new GameObject[totalObjects];

		int arrayPointer = 0;

		for (int i = 0; i < xLoop; i++) {

			Quaternion spawnRotation = Quaternion.identity;

			Vector3 spawnPositionTop = new Vector3 (xEdge,currentY,0.0f);
			objectsArray[arrayPointer] = (GameObject) Instantiate (gameObject, spawnPositionTop, spawnRotation);
			arrayPointer++;

			Vector3 spawnPositionBottom = new Vector3 (-xEdge,currentY,0.0f);
			objectsArray[arrayPointer] = (GameObject) Instantiate (gameObject, spawnPositionBottom, spawnRotation);
			arrayPointer++;

			currentY += edgeSpacing;
		}

		float currentX=-xEdge;

		for (int i = 0; i < yLoop; i++) {

			Quaternion spawnRotation = Quaternion.identity;

			Vector3 spawnPositionLeft = new Vector3 (currentX,yEdge,0.0f);
			objectsArray[arrayPointer] = (GameObject) Instantiate (gameObject, spawnPositionLeft, spawnRotation);
			arrayPointer++;

			Vector3 spawnPositionRight = new Vector3 (currentX,-yEdge,0.0f);
			objectsArray[arrayPointer] = (GameObject) Instantiate (gameObject, spawnPositionRight, spawnRotation);
			arrayPointer++;

			currentX += edgeSpacing;
		}

		return objectsArray;
	}
}

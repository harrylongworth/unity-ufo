using UnityEngine;
using System.Collections;

public class MapEdgeController : MonoBehaviour {

	public static GameObject[] mapEdgeObjects;
	public static float edgeSpacing=80f;


	public static void DestroyAll () {
		
		if (mapEdgeObjects != null) {
			foreach (GameObject item in mapEdgeObjects) {

				GameObject.Destroy (item);
			}

		} // END if
	
	} // END DestroyAll

	public static void Spawn(int halfMapSide, GameObject mapEdge) {

		// Clean up any existing objects
		MapEdgeController.DestroyAll ();


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
			objectsArray[arrayPointer] = (GameObject) Instantiate (mapEdge, spawnPositionTop, spawnRotation);
			objectsArray [arrayPointer].name = "MapEdge" + arrayPointer.ToString ();
			arrayPointer++;

			Vector3 spawnPositionBottom = new Vector3 (-xEdge,currentY,0.0f);
			objectsArray[arrayPointer] = (GameObject) Instantiate (mapEdge, spawnPositionBottom, spawnRotation);
			objectsArray [arrayPointer].name = "MapEdge" + arrayPointer.ToString ();
			arrayPointer++;

			currentY += edgeSpacing;
		}

		float currentX=-xEdge;

		for (int i = 0; i < yLoop; i++) {

			Quaternion spawnRotation = Quaternion.identity;

			Vector3 spawnPositionLeft = new Vector3 (currentX,yEdge,0.0f);
			objectsArray[arrayPointer] = (GameObject) Instantiate (mapEdge, spawnPositionLeft, spawnRotation);
			objectsArray [arrayPointer].name = "MapEdge" + arrayPointer.ToString ();
			arrayPointer++;

			Vector3 spawnPositionRight = new Vector3 (currentX,-yEdge,0.0f);
			objectsArray[arrayPointer] = (GameObject) Instantiate (mapEdge, spawnPositionRight, spawnRotation);
			objectsArray [arrayPointer].name = "MapEdge" + arrayPointer.ToString ();
			arrayPointer++;

			currentX += edgeSpacing;
		}

	} // END Spawn

} // END Class

using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {
	
	public GameObject[] targets;
	public int currentLevel = 0;
	public int targetSets = 3;
	public float halfMapSide = 1024;

	public void spawn(int setsToSpawn,int targetType) {

		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		// halfMapSide = (int)background
		int spawnRange = (int) Mathf.Round(halfMapSide*0.9f);
		float spawnRangeX = spawnRange-borderX;
		float spawnRangeY = spawnRange-borderY;

		// Build Targets
		for (int i = 0; i < setsToSpawn; i++) {
			for (int x = 0; x < targets.Length; x++) {

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRangeX, spawnRangeX), Random.Range (-spawnRangeY, spawnRangeY),0.0f);
				Quaternion spawnRotation = Quaternion.identity;
				var targetTemp = (GameObject) Instantiate (targets[targetType], spawnPosition, spawnRotation);
				targetTemp.name = x.ToString();
				// Debug.Log (targetTemp.name);
			}
		} // END for

	}

	void Start () {

		spawn (targetSets, currentLevel);
	}


	public GameObject GetTarget(int id) {

		if (id < targets.Length) {
			return targets [id];
		} else {
			return targets[id];
		}

	}

}

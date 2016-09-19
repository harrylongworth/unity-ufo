using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {
	
	public GameObject[] targets;

	public int currentLevel = 0;
	public int targetSets = 3;

	private GameObject currentTarget;



	void Start () {

	}


	public GameObject GetTarget(int id) {

		if (id < targets.Length) {
			return targets [id];
		} else {
			return targets[id];
		}

	}

	public int GetLength() {
		return targets.Length;
	}

}

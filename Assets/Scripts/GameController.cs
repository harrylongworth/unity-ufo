using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject target;
	public int targets;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < targets; i++) {
			Vector3 spawnPosition = new Vector3 (Random.Range (-40, 40), Random.Range (-40, 40));
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (target, spawnPosition, spawnRotation);
		}
	
	} // END start
		
} // FND class

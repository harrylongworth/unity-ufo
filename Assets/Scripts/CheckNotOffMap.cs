using UnityEngine;
using System.Collections;

public class CheckNotOffMap : MonoBehaviour {

	public int halfMapSide = 1024;


	
	// Update is called once per frame
	void Update () {

		CatchOffMap ();
	
	}

	void CatchOffMap () {

		float borderX = Screen.width / 2;
		float borderY = Screen.height / 2;
		float maxBackgroundDim = 0.0f;
		bool isOffMap=false;
		float absPlayerX = Mathf.Abs (transform.position.x);
		float absPlayerY = Mathf.Abs (transform.position.y);


		if (absPlayerX > absPlayerY) {
			//X Direction
			maxBackgroundDim = halfMapSide - borderX; // add a border around edge
			if(absPlayerX>maxBackgroundDim) {isOffMap=true;}

		} else {
			// Y Direction
			maxBackgroundDim = halfMapSide - borderY;
			if(absPlayerY>maxBackgroundDim) {isOffMap=true;}
		}

		if (isOffMap) {
			BounceByTags.Bounce (gameObject,100);
		} 

	}




}

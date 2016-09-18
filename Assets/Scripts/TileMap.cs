using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	public int textureSize=1024;
	//public GameObject camera;

	// Use this for initialization
	void Start () {

		//camera = GameObject.FindGameObjectWithTag ("MainCamera");
		//PixelPerfectCamera pixelPerfect = camera.GetComponent<PixelPerfectCamera> ();
		//float newWidth = Mathf.Ceil (Screen.width / (textureSize * PixelPerfectCamera.scale));
		//float newHeight = Mathf.Ceil (Screen.height / (textureSize * PixelPerfectCamera.scale));

		float newWidth = Mathf.Ceil (Screen.width / textureSize);
		float newHeight = Mathf.Ceil (Screen.height / textureSize);

		transform.localScale = new Vector3 (newWidth * textureSize ,newHeight * textureSize,1);

		GetComponent<Renderer> ().material.mainTextureScale = new Vector3 (newWidth, newHeight, 1);
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

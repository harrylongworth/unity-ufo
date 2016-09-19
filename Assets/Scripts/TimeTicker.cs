using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTicker : MonoBehaviour {


	public float timeTicker;

	private float startTime;

	// Use this for initialization
	void Start () {

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
		timeTicker = Mathf.Round(Time.time - startTime);
		gameObject.GetComponent<Text>().text = "Time:  "+timeTicker.ToString();
	}

	public void ResetTicker () {
		startTime = Time.time;
		Update ();
	}
}

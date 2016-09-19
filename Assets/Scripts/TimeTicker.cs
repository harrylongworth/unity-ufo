using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTicker : MonoBehaviour {


	public float timeTicker;

	private float startTime;
	private Text displayText;

	// Use this for initialization
	void Start () {

		displayText = GetComponent<Text> ();
		
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
		timeTicker = Mathf.Round(Time.time - startTime);
		displayText.text = "Time:  "+timeTicker.ToString();
	}

	public void ResetTicker () {
		startTime = Time.time;
		Update ();
	}
}

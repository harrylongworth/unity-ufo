using UnityEngine;
using System.Collections;

public class AudioClipManager : MonoBehaviour {

	public AudioClip[] clips;
	public int clipID = 0;
	// public bool setSprite = false;

	private AudioSource source;
	private AudioClip currentClip;

	void Start () {
		//Set Sprites

		source = GetComponent<AudioSource> ();

		if (clips.Length > 0) {
			currentClip = clips [clipID];
			source.clip = currentClip;
		}
	
	}
	

	public void PlayClip(int id) {

		if (id < clips.Length) {
			clipID = id;
		} else {
			clipID = 0;
		}

		currentClip = clips [clipID ];
		source.clip = currentClip;

		source.Play ();

	}

	public AudioClip GetCurrentClip() {
		return currentClip;
	}

	public int GetCurrentClipID() {
		return clipID;
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestIndicatorController : MonoBehaviour {

	public void SetImage(Sprite sprite) {

		GetComponent <Image> ().sprite = sprite;

	} // END SetImage
	
}

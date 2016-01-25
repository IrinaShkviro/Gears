using UnityEngine;
using System.Collections;

public class HighPressure : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter(Collider col) {
		Debug.Log ("In high pressure");
		if (col.tag == "Player") {
			Debug.Log ("Accept settings");
			PlayerSettings.Instance.ChangePressure (25);
		}
	}
}

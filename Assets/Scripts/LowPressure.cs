using UnityEngine;
using System.Collections;

public class LowPressure : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			PlayerSettings.Instance.ChangePressure (-25);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Rotater : Activator {

	public GameObject toRotate;

	// Use this for initialization
	void Start () {
		toRotate.GetComponent<RotateScript>().enabled = false;
	}

	public override void MakeAction() {
		toRotate.GetComponent<RotateScript>().enabled = true;
	}
}

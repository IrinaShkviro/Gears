using UnityEngine;
using System.Collections;

public class Mover : Activator {

	public GameObject toMove;

	// Use this for initialization
	void Start () {
		toMove.GetComponent<MovePlatform>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void MakeAction() {
		toMove.GetComponent<MovePlatform>().enabled = true;
	}
}

using UnityEngine;
using System.Collections;

public class Visualizer : Activator {

	public GameObject toVisualize;

	// Use this for initialization
	void Start () {
		toVisualize.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void MakeAction() {
		Debug.Log ("In visualizer make action");
		toVisualize.SetActive (true);
	}
}

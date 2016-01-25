﻿using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void MakeAction() {
	}

	protected void OnCollisionEnter(Collision col) {
		Debug.Log ("Collision in activator");
		MakeAction ();
	}
}

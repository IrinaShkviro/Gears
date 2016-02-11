using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class Level : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.tag = "HardPlatform";
		gameObject.layer = 8;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

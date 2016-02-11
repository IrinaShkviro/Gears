using UnityEngine;
using System.Collections;

public class OpenerScript : Activator {

	public GameObject toOpen;

	public override void MakeAction() {
		toOpen.GetComponent<DoorScript>().Open();
	}
}

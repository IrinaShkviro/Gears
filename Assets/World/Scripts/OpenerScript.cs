using UnityEngine;
using System.Collections;

public class OpenerScript : Activator {

	public GameObject[] toOpen;

	public override void MakeAction() {
		for (int i = 0; i < toOpen.Length; i++) {
			if (toOpen [i]) {
				toOpen [i].GetComponent<DoorScript> ().Open ();
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameInfo.rareRec = 0;
			GameInfo.simpleRec = 0;
			PlayerSettings.Instance.Pressure = 50;
			Application.LoadLevel("test");
		}
	}
}

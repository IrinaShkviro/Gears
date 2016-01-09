using UnityEngine;
using System.Collections;

public class FanScript : MonoBehaviour {

	public float speed = 0;

	void OnTriggerStay(Collider other)
	{
		print("hey");
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerScript>().FanMove(speed);
		}
	}
	void OnTriggerExit(Collider other)
	{
		print("bye");
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerScript>().FanMove(0);
		}
	}
}

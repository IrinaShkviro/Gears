using UnityEngine;
using System.Collections;

public class SimpleRecScript : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag=="Player")
		{
			GameInfo.simpleRec+=10;
			RecourcesUIScript recUIScript = GameObject.FindObjectOfType(typeof(RecourcesUIScript)) as RecourcesUIScript;
			recUIScript.Change();
			Destroy(gameObject);
		}
	}
}

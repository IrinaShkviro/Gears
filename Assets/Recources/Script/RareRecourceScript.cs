using UnityEngine;
using System.Collections;

public class RareRecourceScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag=="Player")
		{
			GameInfo.rareRec ++;
			RecourcesUIScript recUIScript = GameObject.FindObjectOfType(typeof(RecourcesUIScript)) as RecourcesUIScript;
			recUIScript.Change();
			Destroy(gameObject);
		}
	}
}

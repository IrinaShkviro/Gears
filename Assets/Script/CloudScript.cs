using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour 
{
	private Collider cloudCol;
	void Start()
	{
		cloudCol = GetComponent<Collider>();
	}

	void OnCollisionExit(Collision other)
	{
		if (other.collider.tag == "Player")
		{
			print ("Disabled (collision exit)");
			cloudCol.enabled = false;
		}
	}
	void OnCollisionStay(Collision other)
	{
		if (other.collider.tag == "Player")
		{
			if (other.transform.position.y - transform.position.y<=0.9f) 
			{
				print ("Disabled (collision stay)");
				cloudCol.enabled = false;
			}
		}
		else if (other.collider.tag == "Human")
		{
			if (other.transform.position.y - transform.position.y<=0.5f) 
			{
				cloudCol.enabled = false;
			}
		}
	}
}

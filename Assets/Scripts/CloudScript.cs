﻿using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour 
{
	private MeshCollider cloudCol;
	void Start()
	{
		cloudCol = GetComponent<MeshCollider>();
	}

	void OnCollisionExit(Collision other)
	{
		if (other.collider.tag == "Player" || other.collider.tag == "Human" )
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
			if (other.transform.position.y - transform.position.y<=0.15f) 
			{
				print ("Disabled(collision stay)");
				cloudCol.enabled = false;
			}
		}
	}
}

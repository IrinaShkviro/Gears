using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float interpVelocity;
	//public GameObject target;
	public Vector3 offset;
	private GameObject target;
	private Vector3 targetPos;
	private bool isHuman = false;

	// Use this for initialization
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		targetPos = transform.position;
	}
	// Update is called once per frame
	void LateUpdate()
	{
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;
			
			Vector3 targetDirection = (target.transform.position - posNoZ);
			
			interpVelocity = targetDirection.magnitude * 5f;
			
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
			
			transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
			
		}
	}
}

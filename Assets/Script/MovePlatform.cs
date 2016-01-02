using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour 
{
	public bool direction = true;
	public float start;
	public float end;
	public float speed;
	private new Transform transform;

	void Start()
	{
		transform = GetComponent<Transform>();
	}
	void Update () 
	{
		if (direction)
		{
			if (transform.position.x >= end && speed > 0 || transform.position.x <= start && speed < 0)
			{
				speed = -speed;
			}
			transform.Translate(new Vector3(Time.deltaTime * speed, 0f, 0f));
		}
		else
		{
			if (transform.position.y >= end && speed > 0 || transform.position.y <= start && speed < 0)
			{
				speed = -speed;
			}
			transform.Translate(new Vector3(0f, Time.deltaTime * speed, 0f));
		}
	}
}
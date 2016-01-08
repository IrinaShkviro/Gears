using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePlatform : MonoBehaviour 
{
	public List<Vector3> points = new List<Vector3>();
	public float speed = 0.05f;

	private new Transform transform;
	private Vector3 nextPoint;
	private int currentSector;

	public enum MoveType
	{
		Once,
		Cycle,
		Partol
	}
	public MoveType type;

	private bool isActive;
	public bool IsActive
	{
		get { return isActive; }
		set { isActive = value; }
	}

	public Vector3 GetNextPoint()
	{
		float dist = 0;
		Vector3 newPoint = Vector3.zero; 
		if (currentSector < points.Count) {
			dist = Vector3.Distance (nextPoint, points[currentSector]);
			newPoint = 
				nextPoint + (points [currentSector] - points [currentSector - 1]).normalized * speed;
		}
		if (Vector3.Distance (newPoint, nextPoint) >= dist) {
			currentSector ++;
			if (currentSector >= points.Count)
			{
				switch(type)
				{
				case MoveType.Cycle:
					currentSector = 1;
					newPoint = points[0];
					break;
				case MoveType.Once:
					isActive = false;
					break;
				case MoveType.Partol:
					currentSector = 1;
					points.Reverse();
					break;
				}
			}
			else
			{
				newPoint = nextPoint + 
					(points [currentSector] - points [currentSector - 1]).normalized * speed;
			}
		}
		nextPoint = newPoint;
		return nextPoint;
	}

	void Start()
	{
		transform = GetComponent<Transform>();
		nextPoint = points [0];
		transform.position = points [0];
		currentSector = 1;
		isActive = true;
	}
	void Update () 
	{
		if (isActive) {
			transform.position = GetNextPoint ();
		}
	}
}
using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public float[] angles = new float[2];
	public float speed = 0.05f;
	public Vector3 pivot;

	private new Transform transform;
	private float deltaAngle;
	private bool forth;

	public enum RotateType
	{
		Once,
		Cycle,
		Partol
	}
	public RotateType type;

	private bool isActive;
	public bool IsActive
	{
		get { return isActive; }
		set { isActive = value; }
	}

	void Start()
	{
		transform = GetComponent<Transform>();
		SetStartSettings ();
		isActive = true;
	}

	void SetStartSettings(){
		if (angles [1] > angles [0]) {
			forth = true;
		} else {
			forth = false;
		}
		deltaAngle = angles [1] - angles [0];
		transform.localRotation = Quaternion.Euler(0f, 0f, angles[0]);
	}

	void FixedUpdate()
	{
		if (isActive) {
			if (forth && transform.eulerAngles.z >= angles [1]
				|| !forth && transform.eulerAngles.z <= angles [1]) {
				switch(type) {
				case RotateType.Cycle:
					SetStartSettings ();
					break;
				case RotateType.Once:
					isActive = false;
					break;
				case RotateType.Partol:
					float curAngle = angles [1];
					angles [1] = angles [0];
					angles [0] = curAngle;
					SetStartSettings ();
					break;
				}
			} else {
				transform.position += transform.rotation * pivot;
				transform.rotation *= Quaternion.AngleAxis (deltaAngle * Time.deltaTime, Vector3.forward);
				transform.position -= transform.rotation * pivot;
			}
		}
	}
}

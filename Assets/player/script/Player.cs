using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject humanPref;
	public GameObject robotObject;

	private PlayerController playerController;
	private Transform playerPos;
	private Rigidbody robotBody;
	private GameObject humanObject;

	private enum PlayerType
	{
		Robot,
		Human
	}
	private PlayerType playerType;

	// Use this for initialization
	void Start () {		
		playerType = PlayerType.Robot;
		robotBody = robotObject.GetComponent<Rigidbody> ();
		robotBody.isKinematic = false;
		playerPos = robotObject.GetComponent<Transform>();
		playerController = GetComponent<PlayerController>();
		playerController.Player = robotObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (playerType == PlayerType.Robot) {
				robotBody.isKinematic = true;
				humanObject = Instantiate (humanPref, new Vector3 (playerPos.position.x + 1.05f,
					playerPos.position.y, 
					playerPos.position.z - 0.01f), Quaternion.identity) as GameObject;
				humanObject.transform.SetParent (playerPos.parent);
				playerController.Player = humanObject;
				playerType = PlayerType.Human;
			} else {
				playerController.Player = robotObject;
				robotBody.isKinematic = false;
				robotBody.WakeUp ();
				Destroy(humanObject);
				playerType = PlayerType.Robot;
			}
		}	
	}
}

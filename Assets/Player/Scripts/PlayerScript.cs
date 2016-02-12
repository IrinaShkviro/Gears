using UnityEngine;
using System.Collections;
public class PlayerScript : MonoBehaviour {

	public GameObject humanPref;
	public Transform[] yCheckers;
	public Transform[] checkers;
	public Transform[] rightCheckers;
	public Transform[] leftCheckers;
	private CameraScript cameraScript;
	private GameObject human;
	private Rigidbody player;
	private Transform playerPos;

	private PlayerScript script;
	private int pressureStep;
	private int pressure;

	private BoxCollider lastCloud;
	private bool jump;
	public bool ground;
	public int timeForJump = 0;
	private float fanSpeed = 0;
	public float maxFallSpeed = 0;
	private int stopMove = 0;
	private bool movePlatformRight = false;
	private bool movePlatformLeft = false;

	void Start ()
	{
		player = GetComponent<Rigidbody>();
		playerPos = GetComponent<Transform>();	
		script = GetComponent<PlayerScript>();
		cameraScript = GameObject.FindObjectOfType(typeof(CameraScript)) as CameraScript;
		PlayerSettings.Instance.SetPlayer (this);
	}

	void Update()
	{
		float move = Input.GetAxis("Horizontal");
	
		/*
		if (player.velocity.y<maxFallSpeed)
		{
			player.velocity = new Vector3(move*speed + fanSpeed, maxFallSpeed, 0f);
		}
		else
		{
			player.velocity = new Vector3(move*speed + fanSpeed, player.velocity.y, 0f);
		}
		*/

		if (movePlatformLeft && move<0)
		{
			player.velocity = new Vector3(-1f, player.velocity.y, 0f);
		}
		else if (movePlatformRight && move>0)
		{
			player.velocity = new Vector3(1f, player.velocity.y, 0f);
		}
		else
		{
			player.velocity = new Vector3(move*PlayerSettings.Instance.Speed + fanSpeed, player.velocity.y, 0f);
		}

		if (jump && player.velocity.y <= 0)
		{
			jump = false;
		}

		if (ground && Mathf.Abs(player.velocity.y) < 0.01f)
		{
			if (timeForJump > 0)
			{
				timeForJump--;
			}
		}	

		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) &&
			jump==false && ground && timeForJump<=0)
		{
			player.velocity = new Vector3(player.velocity.x, 0f, 0f);
			player.AddForce(new Vector3(0f, PlayerSettings.Instance.JumpForce, 0f));
			jump = true;
			ground = false;
			timeForJump = 8;
		}

		if (Input.GetKeyDown(KeyCode.Space) 
			&& PlayerSettings.Instance.CurPressureState == PlayerSettings.PressureState.Low)
		{
			human = Instantiate(humanPref, new Vector3(playerPos.position.x + 1.05f,
			                               playerPos.position.y, 
			                               playerPos.position.z-0.01f), Quaternion.identity) as GameObject;
			player.isKinematic = true;
			playerPos.position = new Vector3 (playerPos.position.x, playerPos.position.y, playerPos.position.z + 2f);
			script.enabled = false;
			cameraScript.ChangeTarget();
		}
	}
	void LateUpdate()
	{
		CheckForCloudPlatform();
		CheckForYMove();
		if (lastCloud)
		{
			CheckForLastCloud();
		}
		CheckRight();
		CheckLeft();
	}
	private void CheckLeft()
	{
		if (CheckMoveX(Vector3.left, leftCheckers))
		{
			movePlatformLeft = true;
		}
		else
		{
			movePlatformLeft = false;
		}
	}
	private void CheckRight()
	{
		if (CheckMoveX(Vector3.right, rightCheckers))
		{
			movePlatformRight = true;
		}
		else
		{
			movePlatformRight = false;
		}
	}

	private bool CheckMoveX(Vector3 heading, Transform[] rays)
	{
		float smallDistance = 0.08f;
		int platformLayer = LayerMask.NameToLayer("Platform");
		int layerMask = 1 << platformLayer;
		foreach(Transform check in rays)
		{
			Debug.DrawRay(check.position, heading, Color.green);
			RaycastHit hit;
			if (Physics.Raycast(check.position, heading, out hit, smallDistance, layerMask))
			{
				BoxCollider col = hit.collider.GetComponent<BoxCollider>();
				if (col.tag == "HardMovePlatform")
				{
					return true;
				}
			}
		}
		return false;
	}
	private bool CheckForCloudPlatform()
	{
		float smallDistance = 0.15f;
		int platformLayer = LayerMask.NameToLayer("Platform");
		int layerMask = 1 << platformLayer;
		int count = 0;
		foreach(Transform check in checkers)
		{
			Debug.DrawRay(check.position, Vector3.down, Color.green);
			RaycastHit hit;
			if (Physics.Raycast(check.position, Vector3.down, out hit, smallDistance, layerMask))
			{
				BoxCollider col = hit.collider.GetComponent<BoxCollider>();
				if ((col.tag == "CloudPlatform" || col.tag == "CloudMovePlatform") &&
					player.velocity.y < 0 && col.enabled == false)
				{
					col.enabled = true;
					lastCloud = col;
					print("Enabled. (ray hit)");
				}
				ground = true;
				return true;
			}
			else
			{
				count++;
			}
		}
		if (count == checkers.Length)
		{
			ground = false;
			timeForJump = 8;
		}
		return false;
	}

	private bool CheckForYMove()
	{
		float smallDistance = 0.15f;
		int platformLayer = LayerMask.NameToLayer("Platform");
		int layerMask = 1 << platformLayer;
		int count = 0;
		foreach(Transform check in yCheckers)
		{
			Debug.DrawRay(check.position, Vector3.down, Color.green);
			RaycastHit hit;
			if (Physics.Raycast(check.position, Vector3.down, out hit, smallDistance, layerMask))
			{
				if (playerPos.parent != hit.transform)
				{
					Collider col;
					col = hit.collider.GetComponent<BoxCollider>();
					if (col.enabled)
					{
						if (col.tag == "HardMovePlatform" || col.tag == "CloudMovePlatform")
						{
							if (player.velocity.y < 0)
							{
								playerPos.position = new Vector3(playerPos.position.x, col.transform.position.y + 1f, playerPos.position.z);
								player.velocity = new Vector3(player.velocity.x, 0, 0);
								playerPos.SetParent(col.transform);
							}
							return true;
						} 
						else
						{
							count++;
						}
					}
				}
			}
			else
			{
				count++;
			}
		}
		if (count == yCheckers.Length)
		{
			playerPos.SetParent(null);
		}
		return false;
	}
	private void CheckForLastCloud()
	{
		if (playerPos.position.y - lastCloud.transform.position.y <= 0.8f)
		{
			lastCloud.enabled = false;
			lastCloud = null;
			print ("Disabled. (last platform)");
		}
	}

	public void FanMove(float speed1)
	{
		fanSpeed = speed1;
	}

	public void CanMove()
	{
		playerPos.position = new Vector3 (playerPos.position.x, playerPos.position.y, playerPos.position.z - 2f);
		player.isKinematic = false;
		player.WakeUp();
		if (lastCloud)
		{
			lastCloud.enabled = true;
		}
	}
}

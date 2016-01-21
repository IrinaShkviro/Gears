using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxFallSpeed = 0;
	public float speed = 3;
	public int timeForJump = 0;
	public float jumpForce = 0;
	public Transform[] yCheckers;
	public Transform[] checkers;
	public bool ground;

	private float fanSpeed = 0;
	private BoxCollider lastCloud;
	public bool jump;

	private Transform playerPos;
	private Rigidbody playerBody;
	public GameObject Player
	{
		set 
		{ 
			playerBody = value.GetComponent<Rigidbody>();
		}
	}

	// Use this for initialization
	void Start () 
	{
		playerPos = GetComponent<Transform>();	
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis("Horizontal");

		if (playerBody.velocity.y < maxFallSpeed)
		{
			playerBody.velocity = new Vector3(move*speed + fanSpeed, maxFallSpeed, 0f);
		}
		else
		{
			playerBody.velocity = new Vector3(move*speed + fanSpeed, playerBody.velocity.y, 0f);
		}

		if (jump && playerBody.velocity.y <= 0)
		{
			jump = false;
		}

		if (ground && Mathf.Abs(playerBody.velocity.y) < 0.01f)
		{
			if (timeForJump > 0)
			{
				timeForJump--;
			}
		}

		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) &&
			jump==false && ground && timeForJump<=0)
		{
			playerBody.velocity = new Vector3(playerBody.velocity.x, 0f, 0f);
			playerBody.AddForce(new Vector3(0f,jumpForce,0f));
			jump = true;
			ground = false;
			timeForJump = 8;
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
				print(playerBody.velocity.y);

				if ((col.tag == "CloudPlatform" || col.tag == "CloudMovePlatform") &&
					playerBody.velocity.y < 0 && col.enabled == false)
				{
					Debug.Log ("must be enabled");
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
							if (playerBody.velocity.y < 0)
							{
								playerBody.position = new Vector3(playerBody.position.x, col.transform.position.y + 1f, playerBody.position.z);
								print (col.transform.position.y);
								print(playerBody.position.y);
								playerBody.velocity = new Vector3(playerBody.velocity.x, 0, 0);
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
		if (playerBody.position.y - lastCloud.transform.position.y <= 0.8f)
		{
			lastCloud.enabled = false;
			lastCloud = null;
			print ("Disabled. (last platform)");
		}
	}
}
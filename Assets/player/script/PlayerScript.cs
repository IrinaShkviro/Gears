using UnityEngine;
using System.Collections;
public class PlayerScript : MonoBehaviour {

	public GameObject humanPref;
	public Transform[] yCheckers;
	public Transform[] checkers;
	private GameObject human;
	private Rigidbody player;
	private Transform playerPos;
	private PlayerScript script;
	private BoxCollider lastCloud;
	private bool jump;
	public bool ground;
	public int timeForJump = 0;
	private float fanSpeed = 0;
	public float maxFallSpeed = 0;
	public float speed = 0;
	public float jumpForce = 0;
	private int stopMove = 0;
	void Start ()
	{
		player = GetComponent<Rigidbody>();
		playerPos = GetComponent<Transform>();	
		script = GetComponent<PlayerScript>();
	}

	void Update()
	{
		float move = Input.GetAxis("Horizontal");
	
		if (player.velocity.y<maxFallSpeed)
		{
			player.velocity = new Vector3(move*speed + fanSpeed, maxFallSpeed, 0f);
		}
		else
		{
			player.velocity = new Vector3(move*speed + fanSpeed, player.velocity.y, 0f);
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
			player.AddForce(new Vector3(0f,jumpForce,0f));
			jump = true;
			ground = false;
			timeForJump = 8;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			human = Instantiate(humanPref, new Vector3(playerPos.position.x + 1.05f,
			                               playerPos.position.y, 
			                               playerPos.position.z-0.01f), Quaternion.identity) as GameObject;
			player.isKinematic = true;
			script.enabled = false;
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
	/*
	void OnCollisionStay(Collision other)
	{
		if (other.collider.tag == "HardPlatform" || other.collider.tag == "HardMovePlatform")
		{
			Transform colPos = other.collider.GetComponent<Transform>();
			float range = playerPos.position.y - colPos.position.y;
			if (colPos.position.x > playerPos.position.x && range<0.95f && range>-0.95f)
			{
				stopMove = 1;
				print ("done");
			}
			else if (colPos.position.x < playerPos.position.x && range<0.95f && range>-0.95)
			{
				stopMove = -1;
			}
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.collider.tag == "HardPlatform" || other.collider.tag == "HardMovePlatform")
		{
			stopMove = 0;
			print ("stop stop");
		}
	}*/
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
		player.isKinematic = false;
		player.WakeUp();	
	}
}

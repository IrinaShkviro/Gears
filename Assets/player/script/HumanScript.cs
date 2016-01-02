using UnityEngine;
using UnityEngine;
using System.Collections;

public class HumanScript : MonoBehaviour {
	
	//private HelpScript help;
	
	public Transform[] yCheckers;
	public Transform[] checkers;
	private PlayerScript robot;
	private Rigidbody player;
	private Transform playerPos;
	private BoxCollider lastCloud;
	private bool jump;
	public bool ground;
	private int timeForJump = 0;
	private float fanSpeed = 0;
	public float maxFallSpeed = 0;
	public float speed = 0;
	public float jumpForce = 0;
	
	void Start ()
	{
		//help = GameObject.FindObjectOfType(typeof(HelpScript)) as HelpScript;
		player = GetComponent<Rigidbody>();
		playerPos = GetComponent<Transform>();
		robot = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
	}
	
	void Update () 
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
		
		if (jump)
		{
			if (player.velocity.y<=0)
			{
				jump = false;
			}
		}
		
		if (ground)
		{
			if (timeForJump>0)
			{
				timeForJump--;
			}
		}
		
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			if (jump==false && ground && timeForJump<=0)
			{
				player.velocity = new Vector3(player.velocity.x, 0f, 0f);
				player.AddForce(new Vector3(0f,jumpForce,0f));
				jump = true;
				ground = false;
				timeForJump = 8;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			robot.enabled = true;
			robot.CanMove();
			Destroy(gameObject);
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
		float smallDistance = 0.05f;
		int platformLayer = LayerMask.NameToLayer("Platform");
		int layerMask = 1 << platformLayer;
		int count = 0;
		foreach(Transform check in checkers)
		{
			Debug.DrawRay(check.position, Vector3.down, Color.green);
			RaycastHit hit;
			if (Physics.Raycast(check.position, Vector3.down,out hit, smallDistance, layerMask))
			{
				BoxCollider col = hit.collider.GetComponent<BoxCollider>();
				if (col.enabled == false)
				{
					if (col.tag == "CloudPlatform" || col.tag == "CloudMovePlatform")
					{
						if (player.velocity.y<0)
						{
							col.enabled = true;
							lastCloud = col;
							print("Enabled cos ray hit");
						}
					}
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
			//help.isPlayerOnCloudPlatform = false;
		}
		return false;
	}
	private bool CheckForYMove()
	{
		float smallDistance = 0.06f;
		int platformLayer = LayerMask.NameToLayer("Platform");
		int layerMask = 1 << platformLayer;
		int count = 0;
		foreach(Transform check in yCheckers)
		{
			Debug.DrawRay(check.position, Vector3.down, Color.green);
			RaycastHit hit;
			if (Physics.Raycast(check.position, Vector3.down,out hit, smallDistance, layerMask))
			{
				if (playerPos.parent != hit.transform)
				{
					Collider col;
					col = hit.collider.GetComponent<BoxCollider>();
					if (col.tag == "CloudMovePlatform")
					{
						if (player.velocity.y <0)
						{
							playerPos.position = new Vector3(playerPos.position.x, col.transform.position.y+0.655f, playerPos.position.z);
							playerPos.SetParent(col.transform);
						}
						return true;
					} 
					else if (col.tag == "HardMovePlatform")
					{
						if (player.velocity.y <0)
						{
							playerPos.position = new Vector3(playerPos.position.x, col.transform.position.y+0.655f, playerPos.position.z);
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
		if (playerPos.position.y - lastCloud.transform.position.y<=0.45f)
		{
			lastCloud.enabled = false;
			lastCloud = null;
			print ("Disabled cos of CHECK!!!");
		}
	}
	public void FanMove(float speed1)
	{
		fanSpeed = speed1;
	}
}
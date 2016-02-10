using UnityEngine;
using System.Collections;

public sealed class PlayerSettings {

	private int pressure, maxPressure, minPressure, lowBorder, highBorder;
	private float speed, jumpForce;

	public enum PressureState
	{
		Low,
		Medium,
		High
	}
	private PressureState curPressureState;
	public PressureState CurPressureState
	{
		get { return curPressureState;}
	}

	private PressureUIScript pressUIScript;
	private PlayerSettings(){
		pressure = 50;
		lowBorder = 35;
		highBorder = 65;
		speed = 3;
		jumpForce = 300;
		minPressure = 0;
		maxPressure = 100;
		curPressureState = PressureState.Medium;
		pressUIScript = GameObject.FindObjectOfType(typeof(PressureUIScript)) as PressureUIScript;
	}
		
	private static readonly PlayerSettings instance = new PlayerSettings();
	public static PlayerSettings Instance
	{
		get { return instance; }
	}

	private Vector3 lastCheck;
	public Vector3 LastCheck
	{
		get { return lastCheck;}
		set { lastCheck = value;}
	}

	public float Speed
	{
		get { return speed;}
	}

	public float JumpForce
	{
		get { return jumpForce;}
	}

	public int Pressure
	{
		get { return pressure; }
		set 
		{ 
			pressure = value;
			ChangePressure(0);
		}
	}

	private PlayerScript player;
	public void SetPlayer(PlayerScript player) {
		this.player = player;
	}

	public void Load() {
		player.transform.position = lastCheck;
	}
		
	public void ChangePressure(int change) {
		pressure += change;
		if (pressure > maxPressure) {
			pressure = maxPressure;
		} else if (pressure < minPressure) {
			pressure = minPressure;
		}
		if (pressure > highBorder) {
			curPressureState = PressureState.High;
			speed = 4;
			jumpForce = 400;
		} else if (pressure < lowBorder) {
			curPressureState = PressureState.Low;
			speed = 2;
			jumpForce = 250;
		} else {
			curPressureState = PressureState.Medium;
			speed = 3;
			jumpForce = 300;
		}
		pressUIScript.SetPressure (curPressureState);
	}
}

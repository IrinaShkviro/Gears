using UnityEngine;
using System.Collections;

public sealed class PlayerSettings {

	private PlayerSettings(){}

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
}

using UnityEngine;
using System.Collections;

public sealed class PlayerSettings {

	private static readonly PlayerSettings instance = new PlayerSettings();

	private PlayerSettings(){}

	public static PlayerSettings Instance
	{
		get { return instance; }
	}
}

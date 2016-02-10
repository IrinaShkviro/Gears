using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PressureUIScript : MonoBehaviour {

	private Text testShow;

	void Start ()
	{
		testShow = gameObject.GetComponent<Text>();
	}
	public void SetPressure(PlayerSettings.PressureState newPressureState)
	{
		switch (newPressureState) {
			case PlayerSettings.PressureState.High:
				testShow.text = "High";
				break;
			case PlayerSettings.PressureState.Low:
				testShow.text = "Low";
				break;
			default:
				testShow.text = "Normal";
				break;
		}
	}
}

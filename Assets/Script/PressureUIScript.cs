using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PressureUIScript : MonoBehaviour {

	private Text testShow;

	void Start ()
	{
		testShow = gameObject.GetComponent<Text>();
	}
	public void SetPressure(int newPressure)
	{
		if (newPressure > 65)
		{
			testShow.text = "High";
		}
		else if (newPressure < 35)
		{
			testShow.text = "Low";
		}
		else
		{
			testShow.text = "Normal";
		}
	}
}

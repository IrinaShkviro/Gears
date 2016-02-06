using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RecourcesUIScript : MonoBehaviour {

	public Text simpleText;
	public Text rareText;

	public void Change()
	{
		simpleText.text = GameInfo.simpleRec.ToString();
		rareText.text = "Rare: "+ GameInfo.rareRec.ToString();
	}
}

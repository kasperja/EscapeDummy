using UnityEngine;
using System.Collections;

public class ComboManager : MonoBehaviour {

	private KeyCombo falconPunch= new KeyCombo(new string[] {"W", "E","R"});
	private KeyCombo falconKick= new KeyCombo(new string[] {"down", "right","Fire1"});
	public bool falconPunchBool = false;
	public float falconPunchDmg = 200.0f;

	public GameObject mainCamObj;

	void Update () {
		if (falconPunch.Check())
		{
			falconPunchBool = true;
			iTween.ShakePosition (mainCamObj, new Vector3 (0.2f,0.2f,0), 0.5f);

			// do the falcon punch
			//Debug.Log("FALCONPUNCH"); 
		}	



		if (falconKick.Check())
		{
			// do the falcon punch
			Debug.Log("FALCONKICK"); 
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SunFade : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.GetComponent<SunShafts> ().sunShaftIntensity -= 4.5f * Time.deltaTime;
	
	}
}

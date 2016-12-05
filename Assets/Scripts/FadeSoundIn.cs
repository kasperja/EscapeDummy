using UnityEngine;
using System.Collections;

public class FadeSoundIn : MonoBehaviour {

	private AudioSource currentSound;
	private float maxVol;
	private bool hasMaxed = false;
	public float fadeInSpeed = 0.2f;
	public PlayIntro playIntroScript;
	// Use this for initialization
	void Start () {
	
		currentSound = gameObject.GetComponent<AudioSource> ();
		maxVol = currentSound.volume;
		currentSound.volume = 0f;

	}
	
	// Update is called once per frame
	void Update () {


		if (playIntroScript.introSkipped) {
			fadeInSpeed = 0.5f;
		}



		if (currentSound.volume < maxVol && !hasMaxed) {

			currentSound.volume += fadeInSpeed * Time.deltaTime;

		} else {
		
			hasMaxed = true;
		
		}


	
	}
}

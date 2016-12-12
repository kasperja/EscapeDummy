using UnityEngine;
using System.Collections;

public class FadeSoundIn : MonoBehaviour {

	private AudioSource currentSound;
	public float maxVol = 1f;
	private bool hasMaxed = false;
	public float fadeInSpeed = 0.2f;
	public PlayIntro playIntroScript;
	// Use this for initialization
	void Start () {
	
		currentSound = gameObject.GetComponent<AudioSource> ();
		//maxVol = 1f;
		currentSound.volume = 0f;

	}
	
	// Update is called once per frame
	void Update () {


		if (playIntroScript.fadeSawSounds) {
			fadeInSpeed = 0.5f;
		}



		if (currentSound.volume < maxVol && !hasMaxed && playIntroScript.fadeSawSounds) {

			currentSound.volume += fadeInSpeed * Time.deltaTime;

		} else {
		
			hasMaxed = true;
		
		}


	
	}
}

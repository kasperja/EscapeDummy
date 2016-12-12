using UnityEngine;
using System.Collections;

public class FadeSawSoundsIn : MonoBehaviour {
	private AudioSource currentSound;
	private float maxVol;
	private bool hasMaxed = false;
	public float fadeInSpeed = 0.2f;
	public bool hasBeenInside = false;
	public DoorAbattoir doorScript;
	public bool insideTrigger = false;
	public PlayIntro introScript;
	// Use this for initialization
	void Start () {

		currentSound = gameObject.GetComponent<AudioSource> ();
		maxVol = currentSound.volume;
		currentSound.volume = 0f;



	}

	// Update is called once per frame
	void Update () {

		if (introScript.fadeSawSounds && !insideTrigger && currentSound.volume < (maxVol / 3f)) {
			
			currentSound.volume += fadeInSpeed * Time.deltaTime;
		
		}


		if(currentSound.volume < maxVol && insideTrigger && introScript.introSkipped) {

			currentSound.volume += fadeInSpeed * Time.deltaTime;
			hasBeenInside = true;

		}else if(!insideTrigger && currentSound.volume > 0f && doorScript.doorOpen){
			
			currentSound.volume -= fadeInSpeed * 2f * Time.deltaTime;

		} else if (!insideTrigger && currentSound.volume > (maxVol / 3f)) {

			currentSound.volume -= fadeInSpeed * Time.deltaTime;

		}



	}

	/*void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "SoundTriggerSaw") {

			insideTrigger = true;

		}
		
	}
	void OnTriggerExit2D(Collider2D other)
	{

		if (other.gameObject.tag == "SoundTriggerSaw") {
			
			insideTrigger = false;
		}
		
	}*/

}
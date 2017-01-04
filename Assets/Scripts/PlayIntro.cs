using UnityEngine;
using System.Collections;

public class PlayIntro : MonoBehaviour {

	public MovieTexture introMovieTex;
	public AudioSource introAudio;

	public GameObject character;
	public Animator fadeAnim;
	public ForceOnStart hookForce;
	public AudioSource landingStartSound;
	public AudioSource pickupHookSound;
	public AudioSource landingFemaleGruntSound;

	public bool introSkipped = false;

	public bool fadeSawSounds = false;

	public Platformer2DUserControl userControlScript;

	public Breath breath;
	//public ParticleSystem breath;
	// Use this for initialization
	void Start () {
	

		character.SetActive (false);
		//Handheld.PlayFullScreenMovie ("intro02", Color.black, FullScreenMovieControlMode.Hidden);
		introMovieTex.Play ();
		introAudio.Play ();

		StartCoroutine (waitIntro ());
		StartCoroutine (waitSawSound ());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.O)){



			landingStartSound.Play ();
			landingFemaleGruntSound.Play ();
			pickupHookSound.Play ();
			fadeAnim.SetBool ("Fade", true);


			introMovieTex.Stop ();
			userControlScript.initiateStanding = true;
			character.SetActive (true);
			breath.StartCoroutine(breath.waitBreath(breath.breathSpeed));
			//breath.Play ();
			hookForce.gameStarted = true;
			gameObject.SetActive (false);
			introSkipped = true;
			fadeSawSounds = true;

		}
	
	}

	IEnumerator waitSawSound(){

		yield return new WaitForSeconds (21f);
		fadeSawSounds = true;
		yield return new WaitForSeconds (9.9f);
		landingStartSound.Play ();
		landingFemaleGruntSound.Play ();
		pickupHookSound.Play ();


	}

	IEnumerator waitIntro(){
	
		yield return new WaitForSeconds (introMovieTex.duration +1f);
	

		fadeAnim.SetBool ("Fade", true);
		introMovieTex.Stop ();
		userControlScript.initiateStanding = true;
		character.SetActive (true);
		breath.StartCoroutine(breath.waitBreath(breath.breathSpeed));
		//breath.Play ();
		hookForce.gameStarted = true;
		gameObject.SetActive (false);
		introSkipped = true;


	}

}

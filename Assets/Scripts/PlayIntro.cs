using UnityEngine;
using System.Collections;

public class PlayIntro : MonoBehaviour {

	public MovieTexture introMovieTex;
	public AudioSource introAudio;

	public GameObject character;
	public Animator fadeAnim;
	public ForceOnStart hookForce;

	public bool introSkipped = false;

	public bool fadeSawSounds = false;

	public Platformer2DUserControl userControlScript;
	// Use this for initialization
	void Start () {
	

		character.SetActive (false);
		introMovieTex.Play ();
		introAudio.Play ();

		StartCoroutine (waitIntro ());
		StartCoroutine (waitSawSound ());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.O)){
			
			fadeAnim.SetBool ("Fade", true);
			introMovieTex.Stop ();
			userControlScript.initiateStanding = true;
			character.SetActive (true);
			hookForce.gameStarted = true;
			gameObject.SetActive (false);
			introSkipped = true;
			fadeSawSounds = true;

		}
	
	}

	IEnumerator waitSawSound(){

		yield return new WaitForSeconds (21f);
		fadeSawSounds = true;


	}

	IEnumerator waitIntro(){
	
		yield return new WaitForSeconds (introMovieTex.duration);
	
		fadeAnim.SetBool ("Fade", true);
		introMovieTex.Stop ();
		userControlScript.initiateStanding = true;
		character.SetActive (true);
		hookForce.gameStarted = true;
		gameObject.SetActive (false);
		introSkipped = true;


	}

}

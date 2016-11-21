using UnityEngine;
using System.Collections;

public class PlayIntro : MonoBehaviour {

	public MovieTexture introMovieTex;
	public AudioSource introAudio;

	public GameObject character;
	public Animator fadeAnim;
	// Use this for initialization
	void Start () {
	
		character.SetActive (false);
		introMovieTex.Play ();
		introAudio.Play ();

		StartCoroutine (waitIntro ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator waitIntro(){
	
		yield return new WaitForSeconds (introMovieTex.duration);
	
		fadeAnim.SetBool ("Fade", true);
		introMovieTex.Stop ();
		gameObject.SetActive (false);
		character.SetActive (true);

	}
}

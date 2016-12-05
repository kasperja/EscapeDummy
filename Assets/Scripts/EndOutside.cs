﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndOutside : MonoBehaviour {

	public PlatformerCharacter2D pc2D;
	public Animator m_Anim;
	public Animator endFadeAnim;
	public GameObject mainCam;
	public GameObject endCamTarget;
	public float waitBeforeEnd = 60f;
	public GameObject endFadeLayer;
	public GameObject titleObj;
	private bool hasEnded = false;
	public AudioSource footStepsSoundGravel;
	// Use this for initialization
	void Start () {
	



	}
	
	// Update is called once per frame
	void Update () {
	
		if (pc2D.isEndOutside) {
		
			pc2D.sideArrowsBool = true;
			m_Anim.SetBool ("SideArrows", true);
			footStepsSoundGravel.volume -= 0.2f * Time.deltaTime;
		
		}

		if (Input.GetKeyDown (KeyCode.Space) && hasEnded) {
		
			StartCoroutine (waitEndTwo());
		
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
	
		if (other.gameObject.tag == "Player") {
		
			pc2D.isEndOutside = true;
			pc2D.endCamBool = true;

			StartCoroutine (waitEnd());

		
		}
	
	
	}

	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player") {

			pc2D.isEndOutside = true;
			pc2D.endCamBool = true;
			StartCoroutine (waitEnd());


		}


	}

	IEnumerator waitEnd(){


		yield return new WaitForSeconds (1f);
		titleObj.SetActive (true);

		yield return new WaitForSeconds (6f);
		hasEnded = true;
		yield return new WaitForSeconds (waitBeforeEnd);

		endFadeAnim.SetBool ("FadeEnd", true);

		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (0);
	
	}
	IEnumerator waitEndTwo(){

		endFadeAnim.SetBool ("FadeEnd", true);

		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (0);

	}
}

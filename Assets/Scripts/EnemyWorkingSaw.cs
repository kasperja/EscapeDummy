using UnityEngine;
using System.Collections;

public class EnemyWorkingSaw : MonoBehaviour {

	public SawMover sawMoverScript;
	public Animator enemyAnim;

	private bool workOnce = true;

	private bool workActive = true;

	public AudioSource activeControllerSound;
	private bool soundOnce = true;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		if (enemyAnim.GetCurrentAnimatorStateInfo (0).IsName ("EnemyWorkingSaw")) {
		
			if (soundOnce) {

				StartCoroutine (waitSound ());
				soundOnce = false;

			}
		
		}

		if ((sawMoverScript.hookDetected || sawMoverScript.meatDetected) && !enemyAnim.GetCurrentAnimatorStateInfo (0).IsName ("EnemyWorkingSaw")) {
		
			if (workActive) {
				if (workOnce) {
					enemyAnim.SetBool ("Work", true);
					StartCoroutine (waitForAnim (0.5f));
					workOnce = false;


				}

				StartCoroutine (waitForActive (2f));
			}
		
		} else {
		
			enemyAnim.SetBool ("Work", false);
		
		}
	
	}

	IEnumerator waitForAnim(float waitTime){
	
		yield return new WaitForSeconds (waitTime);

		enemyAnim.SetBool ("Work", false);
		workOnce = true;
	
	
	}

	IEnumerator waitSound(){
		yield return new WaitForSeconds (0.6f);
		if (!activeControllerSound.isPlaying) {
			activeControllerSound.Play ();
		}

		yield return new WaitForSeconds (1.2f);
		soundOnce = true;
	
	}

	IEnumerator waitForActive(float waitTime){

		yield return new WaitForSeconds (waitTime);

		workActive = false;
		enemyAnim.SetBool ("Work", false);



		yield return new WaitForSeconds (5f);

		workActive = true;

	}

}

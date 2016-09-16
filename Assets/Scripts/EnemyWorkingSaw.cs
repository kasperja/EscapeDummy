using UnityEngine;
using System.Collections;

public class EnemyWorkingSaw : MonoBehaviour {

	public SawMover sawMoverScript;
	public Animator enemyAnim;

	private bool workOnce = true;

	private bool workActive = true;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		if (sawMoverScript.hookDetected || sawMoverScript.meatDetected) {
		
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

	IEnumerator waitForActive(float waitTime){

		yield return new WaitForSeconds (waitTime);

		workActive = false;
		enemyAnim.SetBool ("Work", false);

		yield return new WaitForSeconds (5f);

		workActive = true;

	}

}

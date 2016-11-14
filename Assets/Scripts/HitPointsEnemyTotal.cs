﻿using UnityEngine;
using System.Collections;

public class HitPointsEnemyTotal : MonoBehaviour {


	public float hitpoints = 5000.0f;
	public float maxHitpoints;
	public GameObject healthBar;
	public RectTransform healthBarRect;

	public GameObject mainCamObj;

	public GameObject enemyGraphics;
	public GameObject particlePosObj;
	public GameObject particleHitPosObj;
	public GameObject EnemyParentObj;

	public GameObject tarHigh;
	public GameObject tarMid;
	public GameObject tarLow;
	public GameObject enemyObj;
	public GameObject detectTrigger;
	public GameObject stopTrigger;
	public GameObject attackHigh;
	public GameObject attackMid;
	public GameObject attackLow;
	public EnemyMovement enemyMoveScript;

	public GameObject slashHigh;
	public GameObject slashMiddle;


	public ParticleSystem deathParticle;
	public ParticleSystem hitLargeParticle;
	public AudioSource deathSound;
	public Animator enemy_Animator;
	public bool isDeadEnemy = false;

	private bool dieOnce = true;

	// Use this for initialization
	void Start () {
	
		maxHitpoints = hitpoints;

	}
	
	// Update is called once per frame
	void Update () {

		if (hitpoints <= 0.0f) {

			if(dieOnce){

				enemyMoveScript.isFollowing = false;
				enemyMoveScript.isInRange = false;


			Instantiate (deathParticle, particlePosObj.transform.position, Quaternion.identity);
			Instantiate (hitLargeParticle, particleHitPosObj.transform.position, Quaternion.identity);

			deathSound.Play ();

			isDeadEnemy = true;
			enemy_Animator.SetBool ("DeadBool", true);
			enemy_Animator.SetBool ("Attack2Bool", false);
			enemy_Animator.SetBool ("Attack1Bool", false);
			enemy_Animator.SetBool ("Attack3Bool", false);

			
			tarHigh.SetActive (false);
			tarMid.SetActive (false);
			tarLow.SetActive (false);
			attackHigh.SetActive (false);
			attackMid.SetActive (false);
			attackLow.SetActive (false);
			detectTrigger.SetActive (false);
			stopTrigger.SetActive (false);
			enemyObj.GetComponent<BoxCollider2D> ().enabled = false;
			//enemyObj.SetActive (false);
			//Destroy (EnemyParentObj);

				slashHigh.GetComponent<FollowPath> ().slashActive = false;
				slashHigh.GetComponent<FollowPath> ().slashActive = false;
				slashHigh.SetActive(false);
				slashMiddle.SetActive(false);

			
				iTween.MoveBy (enemyObj, new Vector3 (0f, -2f, 0f), 0.3f);
			//	iTween.PunchScale (enemyObj, new Vector3 (-1f, 2f, 0f), 0.7f);
				iTween.PunchPosition (mainCamObj, new Vector3 (2f, 2f, 0f), 1f);

				StartCoroutine (waitEnableSlash ());

				enemyGraphics.GetComponent<SpriteRenderer> ().sortingOrder = 151;

				dieOnce = false;
			}

		}
		if (hitpoints > 0.0f && hitpoints < maxHitpoints) {
		
			healthBar.SetActive (true);
			healthBarRect.sizeDelta = new Vector2((hitpoints / maxHitpoints) * 150, 50);


		
		} else {
		
			healthBar.SetActive (false);
		
		}


	}

	IEnumerator waitEnableSlash(){

		yield return new WaitForSeconds (4f);
		slashHigh.SetActive(true);
		slashMiddle.SetActive(true);
	
	}
	

}

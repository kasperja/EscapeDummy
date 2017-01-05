﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float speed = 10.0f;
	public float followSpeed = 20f;
	float useSpeed;
	float origX;
	public float moveDistance = 15.0f;
	private float moveDistMax;
	private float moveDistMin;
	public bool isFollowing = true;
	public bool isInRange = false;
	public GameObject player;
	public HitpointsPlayerTotal hptScript;
	//private Vector3 flipH = new Vector3(-1f,0f,0f);
	private Vector3 flipHVector;
	private Vector3 flipHVectorL;
	private bool hitL = false;
	private bool hitR = true;
	private bool flipDirOnce;

	public PlatformerCharacter2D mainCharScript;

	public Animator enemy_Animator;
	public HitPointsEnemyTotal hpEnemyTotal;

	public bool walkWhenDead = false;

	public bool killOnce = true;

	public bool waitForWalk = true;

	private bool breakOnceMusic = true;


	//private float flipHFloat;

	void Start(){

		//flipHFloat = gameObject.transform.localScale.x;

		flipHVector = new Vector3(-gameObject.transform.localScale.x * -1f, gameObject.transform.localScale.y,gameObject.transform.localScale.z);
		flipHVectorL = new Vector3(gameObject.transform.localScale.x * -1f, gameObject.transform.localScale.y,gameObject.transform.localScale.z);

		origX = transform.position.x;
		moveDistMax = transform.position.x - moveDistance;
		moveDistMin = transform.position.x + moveDistance;

		useSpeed = -speed;
		gameObject.transform.localScale = flipHVector;
		//this.transform.Translate (new Vector3 (-15.0f, transform.position.y, 19.0f));
	
	}

	// Update is called once per frame
	void Update () {
	
		enemy_Animator.SetBool ("StopTrigger", isInRange);
		enemy_Animator.SetBool ("DetectTrigger", isFollowing);


		if (Input.GetKeyDown (KeyCode.Return) && hptScript.isDead) {
			
			StartCoroutine (Waiter ());
		
		}

		if (hptScript.isDead && killOnce) {
			

			StartCoroutine (Waiter ());

			killOnce = false;



		} 
			
		if (hpEnemyTotal.isDeadEnemy) {
			
			useSpeed = 0.0f;
		
		}
		else if (((isFollowing == false && isInRange == false) || walkWhenDead || mainCharScript.onStairsBool) && !hpEnemyTotal.isDeadEnemy ) {


			mainCharScript.FadeOutMusic (mainCharScript.musicScript.breakdownMusic, mainCharScript.musicVolumeBreakDown);
			mainCharScript.FadeOutMusic (mainCharScript.musicScript.fightMusic, mainCharScript.musicVolumeFight);
			mainCharScript.FadeInMusic (mainCharScript.musicScript.introMusic, mainCharScript.musicVolumeIntro);



			if (transform.position.x <= moveDistMax) {
				
				hitR = false;
				hitL = true;

				//useSpeed = speed;

				//gameObject.transform.localScale = flipHVectorL;
		
			} 

			if (transform.position.x >= moveDistMin) {
		
				//useSpeed = -speed;

				hitR = true;
				hitL = false;

				//gameObject.transform.localScale = flipHVector;

			}
				
			if (hitR) {
				//Debug.Log ("hej");
				useSpeed = -speed;
				gameObject.transform.localScale = flipHVectorL;

			} 
			if (hitL) {
				//Debug.Log ("hej2");
				useSpeed = speed;
				gameObject.transform.localScale = flipHVector;
				
			}


			

			
			transform.Translate (useSpeed * Time.deltaTime, 0, 0);

		} else if (isFollowing == true && isInRange == false && !hptScript.isDead && !hpEnemyTotal.isDeadEnemy /*&& waitForWalk*/) {



			mainCharScript.FadeInMusic (mainCharScript.musicScript.breakdownMusic, mainCharScript.musicVolumeBreakDown);
			mainCharScript.FadeInMusic (mainCharScript.musicScript.introMusic, mainCharScript.musicVolumeIntro);

			if (transform.position.x > player.transform.position.x) {

				useSpeed = -followSpeed;

				gameObject.transform.localScale = flipHVectorL;


			} else if (transform.position.x < player.transform.position.x) {

				useSpeed = followSpeed;

				gameObject.transform.localScale = flipHVector;

			} else {

				useSpeed = -followSpeed;

				if (flipDirOnce) {
					
					flipDirOnce = false;
					gameObject.transform.localScale = flipHVector;

				}

			}
			transform.Translate (useSpeed * Time.deltaTime, 0, 0);

		} else if (isInRange && !hptScript.isDead) {

			if (breakOnceMusic) {

				mainCharScript.musicScript.breakdownMusic.Play ();
				breakOnceMusic = false;

			}
			
			mainCharScript.musicIntro = false;

			if (!mainCharScript.musicScript.fightMusic.isPlaying) {
			
				mainCharScript.musicScript.fightMusic.Play ();

			}
			mainCharScript.FadeInMusic (mainCharScript.musicScript.fightMusic, mainCharScript.musicVolumeFight);
			mainCharScript.FadeOutMusic (mainCharScript.musicScript.introMusic, mainCharScript.musicVolumeIntro);
			useSpeed = 0.0f;
		
		} 
	}

	IEnumerator Waiter(){

		yield return new WaitForSeconds (2f);
		walkWhenDead = true;

		flipDirOnce = true;
		isFollowing = false;
		isInRange = false;

		yield return new WaitForSeconds (0.1f);
		walkWhenDead = false;
		killOnce = true;
	}





}

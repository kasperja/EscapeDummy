using UnityEngine;
using System.Collections;

public class HitPointsEnemyTotal : MonoBehaviour {


	public float hitpoints = 5000.0f;
	public float maxHitpoints;
	public GameObject healthBar;
	public RectTransform healthBarRect;

	public GameObject mainCamObj;
	public PlatformerCharacter2D mainCharScript;

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

	public AttackEnemyManager attackManagerEnemyScript;

	public AttackManager highDmg;
	public AttackManager middleDmg;
	public AttackManager lowDmg;

	public ParticleSystem blockParticle;
	public ParticleSystem blockParticleMetal;
	public GameObject blockParticlePos;

	public GameObject slashHigh;
	public GameObject slashMiddle;


	public ParticleSystem deathParticle;
	public ParticleSystem hitLargeParticle;
	public AudioSource deathSound;
	public AudioSource deathSoundVoice;
	public Animator enemy_Animator;
	public bool isDeadEnemy = false;
	public Animator mainCharAnimator;

	public bool enemyBlock = false;
	public bool blockOnce = true;
	public bool blockOnceTwo = true;

	private bool dieOnce = true;

	public GameObject blockColObj;

	public DoorAbattoir doorScript;

	public GameObject slashParticle;
	public GameObject kneeParticle;

	public AudioSource blockSound;
	public AudioSource blockSoundMetal;

	// Use this for initialization
	void Start () {

		maxHitpoints = hitpoints;
		blockColObj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if ((highDmg.blockColEnemyEnable || middleDmg.blockColEnemyEnable || lowDmg.blockColEnemyEnable) && blockOnceTwo) {

			if (mainCharAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack3")) {
				
				blockSoundMetal.Play ();
				blockParticleMetal.Play ();

			} else {
				
				blockSound.Play ();
				blockParticle.Play ();

			}
			//Instantiate (blockParticle, blockParticlePos.transform.position, Quaternion.identity);
			//iTween.MoveBy (enemyObj, new Vector3 (-20f, 0f, 0f), 0.3f);
			//Debug.Log ("blockeedd");
			StartCoroutine (waitBlockTwo ());
			blockOnceTwo = false;


		}


		if (enemyBlock && blockOnce) {
			
			if (mainCharAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack3")) {

				blockSoundMetal.Play ();
				blockParticleMetal.Play ();

			} else {

				blockSound.Play ();
				blockParticle.Play ();

			};
			StartCoroutine (waitBlock ());
			enemyBlock = false;
			blockOnce = false;
		}

		if (hitpoints <= 0.0f) {

			mainCharScript.musicIntro = true;
			mainCharScript.FadeOutMusic (mainCharScript.musicScript.fightMusic, mainCharScript.musicVolumeFight);



			if(!doorScript.doorOpen) mainCharScript.FadeInMusic (mainCharScript.musicScript.introMusic, mainCharScript.musicVolumeIntro);


			if(dieOnce){
				
				blockParticle.gameObject.SetActive (false);
				blockParticleMetal.gameObject.SetActive (false);

				mainCharScript.musicScript.introMusic.Play ();
				mainCharScript.musicScript.deathMusic.Play ();
				enemyMoveScript.isFollowing = false;
				enemyMoveScript.isInRange = false;

				StartCoroutine (waitDeathParticle ());
			//Instantiate (deathParticle, particlePosObj.transform.position, Quaternion.identity);
			Instantiate (hitLargeParticle, particleHitPosObj.transform.position, Quaternion.identity);

			deathSound.Play ();
			deathSoundVoice.Play ();

			mainCharScript.enemyIsDead = true;
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

	IEnumerator waitBlock(){

		blockColObj.SetActive (true);
		slashParticle.SetActive (false);
		kneeParticle.SetActive (false);
		tarHigh.GetComponent<CircleCollider2D>().enabled = false;
		tarMid.GetComponent<CircleCollider2D>().enabled = false;
		tarLow.GetComponent<CircleCollider2D>().enabled = false;

		enemy_Animator.SetBool ("Block", true);
		//iTween.MoveBy (enemyObj, new Vector3 (5f, 0f, 0f), 0.3f);
		yield return new WaitForSeconds (0.1f);
		enemy_Animator.SetBool ("Block", false);




		yield return new WaitForSeconds (0.5f);
		slashParticle.SetActive (true);
		kneeParticle.SetActive (true);
		blockColObj.SetActive (false);
		tarHigh.GetComponent<CircleCollider2D>().enabled = true;
		tarMid.GetComponent<CircleCollider2D>().enabled = true;
		tarLow.GetComponent<CircleCollider2D>().enabled = true;
		attackManagerEnemyScript.blockOnce = true;


	}

	IEnumerator waitBlockTwo(){

		yield return new WaitForSeconds (0.5f);
		blockOnceTwo = true;

	}

	IEnumerator waitDeathParticle(){
	
		yield return new WaitForSeconds (0.5f);
		Instantiate (deathParticle, particlePosObj.transform.position, Quaternion.identity);


	
	}
	

}

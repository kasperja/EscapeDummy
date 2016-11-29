using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour {
    
	public GameObject UpperHitObj;
	public GameObject MiddleHitObj;
	public GameObject LowerHitObj;
	public GameObject hitPos;
	public GameObject beginPos;
	private bool playOnce = true;
	public bool activate = false;
	public bool isJumpKick = false;
	//private bool isLerping = false;
//	private float timer = 0.0f;
//	private float timerMax = 0.5f;
	public KeyCode AttackKey = KeyCode.F;
	public KeyCode AttackKey2 = KeyCode.F;
	public KeyCode AttackKey3 = KeyCode.F;
	public PlatformerCharacter2D mainCharScript;
	public GameObject charObj;
	public float attackDamage = 100.0f;
	//public AudioSource whooshSound;
	public CircleCollider2D hitCollider;

	public HitpointsPlayerTotal hpT;
	public AudioSource attackSound;
	private bool attackSoundOnce = true;
	private bool playOnceMoveObj = true;
	public float attackDelay = 0.5f;
	public float attackSpeed = 0.4f;

	//public HitPointsEnemyTotal enemyHpOne;
	public HitPointsEnemyTotal enemyHpTwo;
	//private bool enemyOneDieOnce = true;
	private bool enemyTwoDieOnce = true;

//	public EnemyMovement emOne;
	public EnemyMovement emTwo;

	public Animator charAnimator;

	public bool blockColEnemyEnable = false;
	private bool blockEnemyOnce = true;


	public bool attackHit = false;

	private bool attackDone = true;

	// Use this for initialization
	void Start () {

		hitCollider.enabled = false;



	}
	
	// Update is called once per frame
	void Update () {

		mainCharScript.attackDone = attackDone;

		if (charAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack3") || charAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack5")) {
		
			attackDone = false;
		
		} else {
		
			attackDone = true;
			mainCharScript.playOnce5 = true;
			mainCharScript.playOnce3 = true;

		}

		if (hpT.isDead) {

			attackHit = false;
			hitCollider.enabled = false;

		} else {
		
			//hitCollider.enabled = false;
		}
		/*if (enemyHpOne.isDeadEnemy && enemyOneDieOnce) {
		
			attackHit = false;
			enemyOneDieOnce = false;
		}*/
		if (enemyHpTwo.isDeadEnemy && enemyTwoDieOnce) {
		
			attackHit = false;
			enemyTwoDieOnce = false;
		}

		/*if (emOne.isInRange && mainCharScript.m_FacingRight) {
		
			attackHit = true;
		
		} else*/ if (emTwo.isInRange && !mainCharScript.m_FacingRight) {

			attackHit = true;

		} else {
		
			attackHit = false;
		}
			
		if (Input.GetKey (AttackKey) && /* playOnce && */ !mainCharScript.sideArrowsBool && !hpT.isDead && (charAnimator.GetBool("Attack3Bool") || charAnimator.GetBool("Attack5Bool") ) ) {

			if (attackDone) {
				iTween.MoveBy (charObj, new Vector3 (10f, 0f, 0f), 0.8f);
			}
			activate = true;
			//StopCoroutine ("MoveObjTwo");
			playOnce = false;


			//transform.position = Vector3.Lerp(transform.position, hitPos.transform.position, Time.fixedDeltaTime);








		} /*if ((Input.GetKeyDown (AttackKey) || Input.GetKeyDown (AttackKey2) || Input.GetKeyDown (AttackKey3)) && playOnce && isJumpKick && !hpT.isDead && (charAnimator.GetBool("Attack3Bool") || charAnimator.GetBool("Attack5Bool") ) && attackDone) {
		
			activate = true;
			playOnce = false;
		
		
		}*/

		if (Input.GetKeyUp (AttackKey) && !hpT.isDead) {
			//StartCoroutine (MoveObj (this.transform, this.transform.position, beginPos.transform.position, 0.06f));
			//StopCoroutine ("MoveObj");
			activate = false;
			playOnce = false;

			StartCoroutine (Waiter());

			//StopCoroutine ("MoveObj");



			//transform.position = Vector3.Lerp(transform.position, beginPos.transform.position, Time.fixedDeltaTime);


		} else if (isJumpKick && (Input.GetKeyUp (AttackKey) || Input.GetKeyUp (AttackKey2) || Input.GetKeyUp (AttackKey3)) && !hpT.isDead) {
		
			//activate = false;

			activate = false;
			playOnce = false;

			StartCoroutine (Waiter());
		
		}
		if (activate && !hpT.isDead && playOnceMoveObj) {
		

			StartCoroutine (MoveObj (transform, transform.position, hitPos.transform.position, attackSpeed));

			playOnceMoveObj = false;

		} else {



		}


	}

	IEnumerator MoveObj(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){


		yield return new WaitForSeconds (attackDelay);

		float elapsedTime = 0.0f;
		hitCollider.enabled = true;
		//while (elapsedTime < time) {
		
		//	elapsedTime += Time.deltaTime * 50f;

			iTween.MoveTo (gameObject, endPos, time);
			//thisTransform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / time));

		//	yield return null;

			//elapsedTime = 0.0f;
		
	//	}


		//thisTransform.position = endPos;
		//thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);
		yield return new WaitForSeconds (attackSpeed);

		activate = false;

		StartCoroutine (MoveObjTwo (this.transform, this.transform.position, beginPos.transform.position, 0.06f));

		// StopCoroutine ("MoveObj");
		//yield return null;


	}

	IEnumerator Waiter(){



		yield return new WaitForSeconds (0.0f);
		playOnce = true;


	}
		



	IEnumerator MoveObjTwo(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){

		//yield return new WaitForSeconds(0.06f);

		//thisTransform.position = startPos;
		float elapsedTime = 0.0f;

		//while (elapsedTime < time) {

			//elapsedTime += Time.deltaTime * 100f;

			iTween.MoveTo (gameObject, endPos, time);
			//thisTransform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / time));

			//yield return null;
			//elapsedTime = 0.0f;

		//}

		//thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);

		yield return new WaitForSeconds (0.06f);
		hitCollider.enabled = false;
		//attackDone = true;
		//yield return new WaitForSeconds (0.5f);
		//yield return new WaitForSeconds (0.2f);
		// StopCoroutine ("MoveObj");
		playOnceMoveObj = true;
		yield return null;


	}

	void OnTriggerEnter2D(Collider2D coll){
	
		if (coll.tag == "highCol" || coll.tag == "middleCol" || coll.tag == "lowCol") {


			attackSound.Play ();
		}

		if (coll.tag == "blockCol" && blockEnemyOnce){


			StartCoroutine (enemyBlock ());
			blockEnemyOnce = false;

		} 
	}

	IEnumerator enemyBlock(){
		blockColEnemyEnable = true;
		yield return new WaitForSeconds (0.1f);
		blockColEnemyEnable = false;
		yield return new WaitForSeconds (0.3f);
		blockEnemyOnce = true;


	}



}

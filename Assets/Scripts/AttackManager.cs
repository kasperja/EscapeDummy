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
	public float attackDamage = 100.0f;
	//public AudioSource whooshSound;
	public CircleCollider2D hitCollider;

	public HitpointsPlayerTotal hpT;
	public AudioSource attackSound;
	private bool attackSoundOnce = true;
	private bool playOnceMoveObj = true;
	public float attackDelay = 0.5f;



	// Use this for initialization
	void Start () {




	}
	
	// Update is called once per frame
	void Update () {
		


			
		if (Input.GetKeyDown (AttackKey) && playOnce && !mainCharScript.sideArrowsBool && !hpT.isDead) {


			activate = true;
			//StopCoroutine ("MoveObjTwo");
			playOnce = false;


			//transform.position = Vector3.Lerp(transform.position, hitPos.transform.position, Time.fixedDeltaTime);








		} if ((Input.GetKeyDown (AttackKey) || Input.GetKeyDown (AttackKey2) || Input.GetKeyDown (AttackKey3)) && playOnce && isJumpKick && !hpT.isDead) {
		
			activate = true;
			playOnce = false;
		
		
		}

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
		

			StartCoroutine (MoveObj (transform, transform.position, hitPos.transform.position, 1f));
			playOnceMoveObj = false;

		} else {



		}


	}

	IEnumerator MoveObj(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){



		yield return new WaitForSeconds (attackDelay);

		float elapsedTime = 0.0f;
		hitCollider.enabled = true;
		while (elapsedTime < time) {
		
			elapsedTime += Time.deltaTime * 50f;

			thisTransform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / time));

			yield return null;

			//elapsedTime = 0.0f;
		
		}


		//thisTransform.position = endPos;
		//thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);
		yield return new WaitForSeconds (0.05f);

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

		while (elapsedTime < time) {

			elapsedTime += Time.deltaTime * 50f;

			thisTransform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / time));

			yield return null;
			//elapsedTime = 0.0f;

		}

		//thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);

		yield return new WaitForSeconds (0.06f);
		hitCollider.enabled = false;
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
	}



}

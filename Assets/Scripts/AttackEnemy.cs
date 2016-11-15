using UnityEngine;
using System.Collections;

public class AttackEnemy : MonoBehaviour {

	public EnemyMovement Em;
	private bool playOnce = true;
	public bool activate = false;
	public bool playOnceWaiter = true;
	//public bool playOnceSound = true;
	public CircleCollider2D attackCollider;
	 
	public float attackFrequency = 4.09f;

	public float attackDamage = 100.0f;

	public float attackSpeed = 0.4f;
	public float attackDelay = 0.2f;

	public AudioSource attackSound;

	public AttackEnemyManager attackEnemyManager;
	public GameObject hitPos;
	public GameObject beginPos;
	public bool activeAttack = true;

	public GameObject playerObj;
	public HitpointsPlayerTotal hptPlayerScript;

	private bool attackOnce = true;
	public bool playerBlockEnabled = false;
	// Use this for initialization


	void Start () {
		

		attackCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Em.isInRange && playOnce && activeAttack && !hptPlayerScript.isDead) {
		
			activate = true;
			playOnce = false;
		
		} else {
		
			//dont know if activate should be false here
			//activate = false;
			playOnce = false;

			if (playOnceWaiter) {
				
				StartCoroutine (Waiter ());
				playOnceWaiter = false;
			}
		
		}

		if (activate && activeAttack && attackOnce) {


		
			StartCoroutine (MoveObj (this.transform, this.transform.position, hitPos.transform.position, attackSpeed));

			attackOnce = false;
		} else {
			

		}
	
	}

	IEnumerator MoveObj(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){

		yield return new WaitForSeconds (attackDelay);

		attackCollider.enabled = true;

		float elapsedTime = 0.0f;


		iTween.MoveTo (gameObject, endPos, time);

		/*while (elapsedTime < time) {

			elapsedTime += Time.deltaTime * 30f;

			thisTransform.position = Vector3.Lerp(startPos, endPos, elapsedTime / time);

			yield return null;

			//elapsedTime = 0.0f;

		}*/

		//thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);
		yield return new WaitForSeconds (attackSpeed);
		activate = false;

		StartCoroutine (MoveObjTwo (this.transform, this.transform.position, beginPos.transform.position, 1f));



	}



	IEnumerator Waiter(){



		yield return new WaitForSeconds (attackFrequency);
		playOnceWaiter = true;
		playOnce = true;
		attackOnce = true;
	

	}

	IEnumerator MoveObjTwo(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){

		float elapsedTime = 0.0f;


		iTween.MoveTo (gameObject, endPos, time);
		/*while (elapsedTime < time) {

			elapsedTime += Time.deltaTime * 30f;

			thisTransform.position = Vector3.Lerp(startPos, endPos, elapsedTime / time);

			yield return null;

			//elapsedTime = 0.0f;

		}*/

	
		//thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);
		attackCollider.enabled = false;

		yield return null;


	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.tag == "middlePlayerCol") {
			attackSound.Play ();
		}
		if (coll.tag == "blockColPlayer") {
			StartCoroutine (waitBlockPlayer ());
		}
	
	}

	IEnumerator waitBlockPlayer(){
		playerBlockEnabled = true;
		yield return new WaitForSeconds (1f);
		playerBlockEnabled = false;
	
	}


}

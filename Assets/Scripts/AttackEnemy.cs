using UnityEngine;
using System.Collections;

public class AttackEnemy : MonoBehaviour {

	public EnemyMovement Em;
	private bool playOnce = true;
	public bool activate = false;
	public bool playOnceWaiter = true;
	//public bool playOnceSound = true;
	public CircleCollider2D attackCollider;
	 
	public float attackFrequency = 0.7f;

	public float attackDamage = 100.0f;

	public AudioSource attackSound;

	public AttackEnemyManager attackEnemyManager;
	public GameObject hitPos;
	public GameObject beginPos;
	public bool activeAttack = false;

	public GameObject playerObj;
	// Use this for initialization


	void Start () {
		

	
	}
	
	// Update is called once per frame
	void Update () {

		if (Em.isInRange && playOnce && activeAttack && playerObj.activeSelf == true) {
		
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

		if (activate && activeAttack) {


		
			StartCoroutine (MoveObj (this.transform, this.transform.position, hitPos.transform.position, 0.06f));


		} else {
			
			StartCoroutine (MoveObjTwo (this.transform, this.transform.position, beginPos.transform.position, 0.06f));
		}
	
	}

	IEnumerator MoveObj(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){


		attackCollider.enabled = true;
		thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);
		yield return new WaitForSeconds (0.2f);
		activate = false;



		yield return null;


	}



	IEnumerator Waiter(){



		yield return new WaitForSeconds (attackFrequency);
		playOnceWaiter = true;
		playOnce = true;
	

	}

	IEnumerator MoveObjTwo(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){



		thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);
		attackCollider.enabled = false;

		yield return null;


	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.tag == "middlePlayerCol") {
			attackSound.Play ();
		}
	
	}


}

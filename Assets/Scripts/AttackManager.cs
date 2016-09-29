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
	//private bool isLerping = false;
//	private float timer = 0.0f;
//	private float timerMax = 0.5f;
	public KeyCode AttackKey = KeyCode.F;
	public PlatformerCharacter2D mainCharScript;
	public float attackDamage = 100.0f;
	//public AudioSource whooshSound;
	public CircleCollider2D hitCollider;

	public AudioSource attackSound;
	private bool attackSoundOnce = true;


	// Use this for initialization
	void Start () {




	}
	
	// Update is called once per frame
	void Update () {
		






		/*timer += Time.deltaTime;

		if (timer >= timerMax) {


			activate = true;
			//timer = timerMax;
			//


		} else {


			activate = false;


		}*/

	

			
		if (Input.GetKeyDown (AttackKey) && playOnce && !mainCharScript.sideArrowsBool) {


			activate = true;
			//StopCoroutine ("MoveObjTwo");
			playOnce = false;


			//transform.position = Vector3.Lerp(transform.position, hitPos.transform.position, Time.fixedDeltaTime);








		} 
			

		if (Input.GetKeyUp (AttackKey)) {
			//StartCoroutine (MoveObj (this.transform, this.transform.position, beginPos.transform.position, 0.06f));
			//StopCoroutine ("MoveObj");
			activate = false;
			playOnce = false;

			StartCoroutine (Waiter());

			//StopCoroutine ("MoveObj");



			//transform.position = Vector3.Lerp(transform.position, beginPos.transform.position, Time.fixedDeltaTime);


		} else {
		
			//activate = false;
		
		}
		if (activate) {
		

			StartCoroutine (MoveObj (this.transform, this.transform.position, hitPos.transform.position, 0.005f));

		} else {
			
			StartCoroutine (MoveObjTwo (this.transform, this.transform.position, beginPos.transform.position, 0.06f));
		}


	}

	IEnumerator MoveObj(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){



		yield return new WaitForSeconds (0.3f);
		hitCollider.enabled = true;
		thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);
		yield return new WaitForSeconds (0.1f);

		activate = false;


		// StopCoroutine ("MoveObj");
		yield return null;


	}

	IEnumerator Waiter(){



		yield return new WaitForSeconds (1f);
		playOnce = true;


	}
		



	IEnumerator MoveObjTwo(Transform thisTransform, Vector3  startPos, Vector3 endPos, float time){

		//yield return new WaitForSeconds(0.06f);
		thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime / time);

		yield return new WaitForSeconds (0.4f);
		hitCollider.enabled = false;
		//yield return new WaitForSeconds (0.5f);
		//yield return new WaitForSeconds (0.2f);
		// StopCoroutine ("MoveObj");
		yield return null;


	}

	void OnTriggerEnter2D(Collider2D coll){
	
		if (coll.tag == "highCol" || coll.tag == "middleCol" || coll.tag == "lowCol") {
			attackSound.Play ();
		}
	}



}

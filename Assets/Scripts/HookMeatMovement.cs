using UnityEngine;
using System.Collections;

public class HookMeatMovement : MonoBehaviour {

	public GameObject StartTrigger;
	public GameObject EndTrigger;
	public float meatSpeed;
	public float meatSpeedUp;
	private Vector2 meatSpeedVector;
	public float waitTime;
	public float moveTime;
	private bool isMoving = true;
	private bool playOnce = true;
	private bool playOnceWait = true;
	public Rigidbody2D HingeRb;
	public GameObject meat;
	private float meatStartPosY;
	public float moveUpAngle;
	private float moveUpRadians;
	public Transform startPosY;
	public GameObject MeatBackObj;
	public MeatCutScript MeatCutScript;
	public float speedUpStart = 0f;
	public bool hookWaitOnce = true;
	public bool hookWaitOnceMoving = true;

	public SawMover SawMoverScript;

	public bool waitDetectOnce = true;

	public bool readyToStopMoving = false;
	// Use this for initialization
	void Start () {

		MeatCutScript.bloodDripParticle.Stop ();
		moveUpRadians = moveUpAngle * Mathf.Deg2Rad;
		meatSpeedUp = (meatSpeed / Mathf.Sin ((90f* Mathf.Deg2Rad) - moveUpRadians)) * Mathf.Sin(moveUpRadians);
		meatStartPosY = startPosY.position.y;
		meatSpeedVector = new Vector2(meatSpeed, speedUpStart);


	}

	// Update is called once per frame
	void FixedUpdate () {



		if (isMoving)
		{

			HingeRb.MovePosition(HingeRb.position + Time.deltaTime * meatSpeedVector);
			//this.transform.Translate(Vector3.left * Time.deltaTime * meatSpeed);
			if (playOnce)
			{if (SawMoverScript.hookDetected && hookWaitOnceMoving) {

					StartCoroutine (MoveTime (0f));
					StartCoroutine (WaitNumerator (1.5f));
					StartCoroutine (HookWaitTrue (5f));
					playOnce = false;
					hookWaitOnceMoving = false;

				} else {
					StartCoroutine (MoveTime (moveTime));
					playOnce = false;
				}
			}


		}
		else{

			if (playOnceWait)
			{
				if (SawMoverScript.hookDetected && hookWaitOnce) {

					StartCoroutine (WaitNumerator (1.5f));
					StartCoroutine (HookWaitTrue (5f));
					playOnceWait = false;
					hookWaitOnce = false;

				} else {
					StartCoroutine (WaitNumerator (waitTime));
					playOnceWait = false;
				}
			}
		}

	}

	void Update(){

		if (SawMoverScript.hookDetected && waitDetectOnce) {

			//readyToStopMoving = false;

			StartCoroutine (waitIfDetected (2f));
			waitDetectOnce = false;

		}

		if (SawMoverScript.hookDetected) {


			if(waitDetectOnce)waitTime = 1.5f;
			StartCoroutine (waitForMove ());

		} else {


			waitTime = 0f;

		}


	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		/*if (other.gameObject.tag == "SawCol") {
		
			Debug.Log ("jj");
			MeatBackObj.SetActive(true);
		
		}*/

		if (other.gameObject.tag == "MeatEndCol") {

			this.gameObject.transform.position = new Vector3(StartTrigger.transform.position.x, meatStartPosY, transform.position.z);

			meatSpeedVector = new Vector2(meatSpeed, -meatSpeedUp);

			MeatCutScript.MeatBackObj.SetActive (false);
			MeatCutScript.MeatBackUncutObj.SetActive (true);
			MeatCutScript.cutMeatOnce = true;
			MeatCutScript.meatPassedBool = false;

			MeatCutScript.resetPosBool = true;

			MeatCutScript.bloodDripParticle.Stop ();

		}


		if (other.gameObject.tag == "MeatUpCol") {


			meatSpeedVector = new Vector2(meatSpeed, meatSpeedUp);

		}


		if (other.gameObject.tag == "HookStraightCol") {

			meatSpeedVector = new Vector2(meatSpeed, 0f);

		} 




		if (other.gameObject.tag == "DetectHook") {

			SawMoverScript.meatDetected = true;

		}

		else if (other.gameObject.tag == "MeatPassedCol") {

			SawMoverScript.meatDetected = false;
			//SawMoverScript.meatDetectSpeed = -SawMoverScript.startSpeed;

		}



	}
	void OnTriggerStay2D(Collider2D other)
	{


		if (other.gameObject.tag == "DetectHook") {

			SawMoverScript.meatDetected = true;

		}
		else if (other.gameObject.tag == "MeatPassedCol") {

			SawMoverScript.meatDetected = false;
			//SawMoverScript.meatDetectSpeed = -SawMoverScript.startSpeed;

		}






	}
	IEnumerator WaitNumerator(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		isMoving = true;
		playOnce = true;
	}
	IEnumerator MoveTime(float moveTime)
	{
		yield return new WaitForSeconds(moveTime);
		isMoving = false;
		playOnceWait = true;
	}
	IEnumerator HookWaitTrue(float moveTime)
	{
		yield return new WaitForSeconds(moveTime);
		hookWaitOnce = true;
		hookWaitOnceMoving = true;
		waitDetectOnce = true;
		waitTime = 1.5f;
	}
	IEnumerator waitIfDetected(float moveTime)
	{
		yield return new WaitForSeconds(0.5f);
		isMoving = false;
		yield return new WaitForSeconds(moveTime);
		isMoving = true;
	}
	IEnumerator waitForMove(){
	//	yield return new WaitForSeconds (1f);
		readyToStopMoving = false;
		yield return new WaitForSeconds (1f);
		readyToStopMoving = true;

	}
}

using UnityEngine;
using System.Collections;

public class HookHingeMovement : MonoBehaviour {

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
	public Rigidbody2D hookRb;
	private bool hookForceOnce = true;
	private bool hookForceWaitBool = false;
	public PlatformerCharacter2D mainCharScript;
	public bool hookWaitOnce = true;
	public bool hookWaitOnceMoving = true;


	public float speedUpStart = 0f;

	public bool waitDetectOnce = true;

	public SawMover SawMoverScript;

	// Use this for initialization
	void Start () {

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
			{

				if (SawMoverScript.hookDetected && hookWaitOnceMoving) {

					mainCharScript.hookStandingStill = true;
					StartCoroutine (MoveTime (0f));
					StartCoroutine (WaitNumerator (2f));
					StartCoroutine(HookWaitTrue(5f));
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
					mainCharScript.hookStandingStill = true;
					StartCoroutine (WaitNumerator (2f));
					StartCoroutine(HookWaitTrue(5f));
					playOnceWait = false;
					hookWaitOnce = false;
				
				} else {

					StartCoroutine (WaitNumerator (waitTime));
					playOnceWait = false;

				}
			}
		}

		if (mainCharScript.hookJumpActive && hookForceOnce) {

			StartCoroutine(HookForceWait(0.7f));
			if (hookForceWaitBool) {
				SawMoverScript.hookDetected = false;
				//SawMoverScript.hookDetectSpeed = -30f;
				hookRb.AddForce (new Vector2 (100f, 0), ForceMode2D.Impulse);
				hookForceOnce = false;


			}
		}

	}

	void Update(){
	
		if (SawMoverScript.hookDetected && waitDetectOnce) {


			StartCoroutine (waitIfDetected (2f));
			waitDetectOnce = false;

		}

		if (SawMoverScript.hookDetected) {
		
		
			waitTime = 0f;

		
		} else {
		
		
			waitTime = 0f;
		
		}

	
	}

	void OnTriggerEnter2D(Collider2D other)
	{


		if (other.gameObject.tag == "MeatEndCol") {

			this.gameObject.transform.position = new Vector3(StartTrigger.transform.position.x, meatStartPosY, transform.position.z);

			meatSpeedVector = new Vector2(meatSpeed, -meatSpeedUp);

		}
			

		if (other.gameObject.tag == "MeatUpCol") {


			meatSpeedVector = new Vector2(meatSpeed, meatSpeedUp);

		}
		if (other.gameObject.tag == "HookStraightCol") {
		
			meatSpeedVector = new Vector2(meatSpeed, 0f);
		
		}
		if (other.gameObject.tag == "DetectHook") {
	
			SawMoverScript.hookDetected = true;
		
		}
		if (other.gameObject.tag == "MeatPassedCol") {

			SawMoverScript.hookDetected = false;
			//SawMoverScript.hookDetectSpeed = -30f;
		
		}

	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "DetectHook") {

			SawMoverScript.hookDetected = true;

		}else if (other.gameObject.tag == "MeatPassedCol") {

			SawMoverScript.hookDetected = false;
			//SawMoverScript.hookDetectSpeed = -30f;

		}

	
	}
	IEnumerator WaitNumerator(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		mainCharScript.hookStandingStill = false;
		isMoving = true;
		playOnce = true;
	}
	IEnumerator MoveTime(float moveTime)
	{
		yield return new WaitForSeconds(moveTime);
		isMoving = false;
		playOnceWait = true;
	}

	IEnumerator HookForceWait(float moveTime)
	{
		yield return new WaitForSeconds(moveTime);
		hookForceWaitBool = true;
	}
	IEnumerator HookWaitTrue(float moveTime)
	{
		yield return new WaitForSeconds(moveTime);
		hookWaitOnce = true;
		hookWaitOnceMoving = true;
		waitDetectOnce = true;
	}
	IEnumerator waitIfDetected(float moveTime)
	{
		mainCharScript.hookStandingStill = true;
		yield return new WaitForSeconds(0.5f);
		isMoving = false;
		yield return new WaitForSeconds(moveTime);
		isMoving = true;
	}


}

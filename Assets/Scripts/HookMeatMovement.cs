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

	// Use this for initialization
	void Start () {

		moveUpRadians = moveUpAngle * Mathf.Deg2Rad;
		meatSpeedUp = (meatSpeed / Mathf.Sin ((90f* Mathf.Deg2Rad) - moveUpRadians)) * Mathf.Sin(moveUpRadians);
		meatStartPosY = startPosY.position.y;
		meatSpeedVector = new Vector2(meatSpeed, 0f);


	}

	// Update is called once per frame
	void FixedUpdate () {


		if (isMoving)
		{

			HingeRb.MovePosition(HingeRb.position + Time.deltaTime * meatSpeedVector);
			//this.transform.Translate(Vector3.left * Time.deltaTime * meatSpeed);
			if (playOnce)
			{
				StartCoroutine(MoveTime(moveTime));
				playOnce = false;
			}


		}
		else{

			if (playOnceWait)
			{
				StartCoroutine(WaitNumerator(waitTime));
				playOnceWait = false;
			}
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

		}


		if (other.gameObject.tag == "MeatUpCol") {


			meatSpeedVector = new Vector2(meatSpeed, meatSpeedUp);

		}


		if (other.gameObject.tag == "HookStraightCol") {

			meatSpeedVector = new Vector2(meatSpeed, 0f);

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
}

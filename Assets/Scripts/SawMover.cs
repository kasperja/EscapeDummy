using UnityEngine;
using System.Collections;

public class SawMover : MonoBehaviour {

	public float useSpeed = 30f;
	public float startSpeed = 60f;
	public float damage = 1400f;
	public bool hookDetected = false;
	public bool meatDetected = false;
	public float hookDetectSpeed = -30f;
	public float meatDetectSpeed = 30f;

	// Use this for initialization
	void Start () {

		useSpeed = startSpeed;
	
	}
	
	// Update is called once per frame
	void Update () {
	



		if (hookDetected) {


			transform.Translate (0, hookDetectSpeed * Time.deltaTime, 0);
		

		} else if (meatDetected){


			transform.Translate (0, meatDetectSpeed * Time.deltaTime, 0);


		} else {

			transform.Translate (0, useSpeed * Time.deltaTime, 0);

		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "SawMoveTop") {


			useSpeed = -startSpeed;

			hookDetectSpeed = -30f;






		}

		if (hookDetected && other.gameObject.tag == "SawMoveBottom") {


			hookDetectSpeed = 0f;


		} else if (meatDetected && other.gameObject.tag == "SawMoveBottom") {


			meatDetectSpeed = 30f;

		}

		if (hookDetected && other.gameObject.tag == "SawMoveTop") {


			hookDetectSpeed = -30f;


		} else if (meatDetected && other.gameObject.tag == "SawMoveTop") {


			meatDetectSpeed = 0f;

		}
			


		if (other.gameObject.tag == "SawMoveBottomNorm") {


			useSpeed = startSpeed;

			meatDetectSpeed = 30f;




		}



	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "SawMoveTop") {


			useSpeed = -startSpeed;
			hookDetectSpeed = -30f;




		}

		if (hookDetected && other.gameObject.tag == "SawMoveBottom") {


			hookDetectSpeed = 0f;


		} else if (meatDetected && other.gameObject.tag == "SawMoveBottom") {


			meatDetectSpeed = 0f;

		}

		if (hookDetected && other.gameObject.tag == "SawMoveTop") {


			hookDetectSpeed = -30f;


		} else if (meatDetected && other.gameObject.tag == "SawMoveTop") {


			meatDetectSpeed = 0f;

		}





		if (other.gameObject.tag == "SawMoveBottomNorm") {


			useSpeed = startSpeed;
			meatDetectSpeed = 30f;




		}



	}

}

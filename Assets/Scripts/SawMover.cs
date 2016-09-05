using UnityEngine;
using System.Collections;

public class SawMover : MonoBehaviour {

	private float useSpeed = 20f;
	public float startSpeed = 20f;
	public float damage = 1400f;
	public bool hookDetected = false;
	public float hookDetectSpeed = -10f;

	// Use this for initialization
	void Start () {

		useSpeed = startSpeed;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (hookDetected) {


			transform.Translate (0, hookDetectSpeed * Time.deltaTime, 0);
		

		} else {

			transform.Translate (0, useSpeed * Time.deltaTime, 0);

		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		//Debug.Log ("col");

		if (hookDetected && other.gameObject.tag == "SawMoveBottom") {
		

			hookDetectSpeed = 0f;

		
		} 

		if (other.gameObject.tag == "SawMoveTop") {


			useSpeed = -startSpeed;

		}
		if (other.gameObject.tag == "SawMoveBottomNorm") {


			useSpeed = startSpeed;

		}



	}

}

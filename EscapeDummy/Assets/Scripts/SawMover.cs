using UnityEngine;
using System.Collections;

public class SawMover : MonoBehaviour {

	public float useSpeed = 10f;
	public float damage = 1400f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		transform.Translate (useSpeed * Time.deltaTime, 0, 0);

	}

	void OnTriggerEnter2D(Collider2D other)
	{

		//Debug.Log ("col");

		if (other.gameObject.tag == "SawMoveTriggers") {


			useSpeed = -useSpeed;

		}



	}

}

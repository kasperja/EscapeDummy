using UnityEngine;
using System.Collections;

public class StopTrigger : MonoBehaviour {

	public EnemyMovement Em;

	void OnTriggerStay2D(Collider2D collider){

		if (collider.tag == "Player") {

			Em.isInRange = true;

		}
	}
	void OnTriggerEnter2D(Collider2D collider){

		if (collider.tag == "Player") {

			Em.isInRange = true;

		}
	}
	void OnTriggerExit2D(Collider2D collider){

		if (collider.tag == "Player") {

			Em.isInRange = false;

		}
	}
}

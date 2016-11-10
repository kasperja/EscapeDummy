using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	private BoxCollider2D boxCol;
	private bool dropOnce = true;

	public HitPointsEnemyTotal hpEnemyTotal;

	public PickUp pickUpScript;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		boxCol = GetComponent<BoxCollider2D> ();

		if (!hpEnemyTotal.isDeadEnemy) {

			boxCol.enabled = false;

		} else if (dropOnce) {
		
			StartCoroutine(pickupWait(1f));
			dropOnce = false;
		}


	}

	IEnumerator pickupWait(float waitTime){
	
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		gameObject.GetComponent<HingeJoint2D> ().enabled = false;
		yield return new WaitForSeconds (waitTime);
		boxCol.enabled = true;
		pickUpScript.KeyReadyForPickup = true;

	
	}
}

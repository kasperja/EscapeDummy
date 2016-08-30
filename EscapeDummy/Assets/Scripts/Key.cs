using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	private BoxCollider2D boxCol;
	private bool dropOnce = true;
	public PickUp pickUpScript;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		boxCol = GetComponent<BoxCollider2D> ();

		if (GameObject.Find("Enemy1ParentKey") !=null) {

			boxCol.enabled = false;

		} else if (dropOnce) {
		
			StartCoroutine(pickupWait(1f));
			dropOnce = false;
		}


	}

	IEnumerator pickupWait(float waitTime){
	
		yield return new WaitForSeconds (waitTime);
		boxCol.enabled = true;
		pickUpScript.KeyReadyForPickup = true;

	
	}
}

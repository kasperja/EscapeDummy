using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

	public float destoyAfterSeconds = 3.0f;
	// Use this for initialization
	void Start () {

		StartCoroutine (DestroyWaiter (destoyAfterSeconds));
	
	}
	
	IEnumerator DestroyWaiter(float destroyer){


		yield return new WaitForSeconds (destroyer);
		Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class ForceOnStart : MonoBehaviour {

	public bool gameStarted = false;
	private bool gameStartedOnce = true;
	// Use this for initialization
	void Start () {
	
		//gameObject.GetComponent<Rigidbody2D>().AddForce (new Vector2 (-60f, 0), ForceMode2D.Impulse);

	}
	
	// Update is called once per frame
	void Update () {

		if (gameStarted && gameStartedOnce) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (80f, 0), ForceMode2D.Impulse);
			gameStartedOnce = false;
		}
	}
}

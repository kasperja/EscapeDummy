using UnityEngine;
using System.Collections;

public class BoxPunch : MonoBehaviour {
	//public GameObject box;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Player") {

			//iTween.PunchScale (box, new Vector3 (2f, 2f, 0f), 1f);

		}

	}
}

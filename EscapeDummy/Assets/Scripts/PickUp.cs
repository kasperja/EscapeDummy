using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public GameObject keyObj;
	public bool hasKey = false;
	public AudioSource keySound;
	public bool KeyReadyForPickup = false;

	void OnTriggerEnter2D(Collider2D other)
	{

		//Debug.Log("COL!");

		if (other.gameObject.tag == "Key" && KeyReadyForPickup) {

			keySound.Play ();
			hasKey = true;
			Destroy (keyObj);

		}



	}
}

using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public GameObject keyObj;
	public bool hasKey = false;
	public AudioSource keySound;
	public bool KeyReadyForPickup = false;

	public GameObject keyUI;

	void OnTriggerEnter2D(Collider2D other)
	{

		//Debug.Log("COL!");

		if (other.gameObject.tag == "Key" && KeyReadyForPickup) {

			keyUI.SetActive (true);
			keySound.Play ();
			hasKey = true;
			Destroy (keyObj);

		}



	}
}

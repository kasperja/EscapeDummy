using UnityEngine;
using System.Collections;

public class DoorAbattoir : MonoBehaviour {

	public PickUp pickUpScript;
	public bool doorOpen = false;
	public Sprite doorClosedSprite;
	public Sprite doorOpenSprite;
	public AudioSource doorOpenSound;
	public bool doorSoundPlayOnce = true;
	// Use this for initialization
	void Start () {

	
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	void OnTriggerEnter2D(Collider2D other)
	{



		if (other.gameObject.tag == "Player") {

			if (pickUpScript.hasKey == true) {

				doorOpen = true;

				if (doorSoundPlayOnce) {
					doorOpenSound.Play ();
					StartCoroutine (waitForSound (2f));


				}
				gameObject.GetComponent<SpriteRenderer> ().sprite = doorOpenSprite;

			}

		}



	}
	IEnumerator waitForSound(float waitTime){
	
		yield return new WaitForSeconds (waitTime);
		doorSoundPlayOnce = false;
	}
}

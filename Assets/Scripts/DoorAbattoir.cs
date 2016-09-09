using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class DoorAbattoir : MonoBehaviour {

	public PickUp pickUpScript;
	public bool doorOpen = false;
	public Sprite doorClosedSprite;
	public Sprite doorOpenSprite;
	public AudioSource doorOpenSound;
	public bool doorSoundPlayOnce = true;

	public AudioSource ambientSound;

	public GameObject whiteScreen;
	public GameObject whiteScreenTwo;

	private Color whiteScreenColor = new Color (1f, 1f, 1f, 0f);
	private Color whiteScreenColorTwo = new Color (1f, 1f, 1f, 0f);

	public GameObject MainCamObj;

	public Animator m_AnimDoorAbattoir;

	private bool doorFullyOpenBool = false;
	// Use this for initialization
	void Start () {

	
	
	}
	
	// Update is called once per frame
	void Update () {

		whiteScreen.GetComponent<SpriteRenderer> ().color = whiteScreenColor;

		whiteScreenTwo.GetComponent<SpriteRenderer> ().color = whiteScreenColorTwo;

		if (doorOpen) {
		
			MainCamObj.GetComponent<SunShafts> ().sunShaftIntensity += 3f * Time.deltaTime;

			ambientSound.volume -= 0.1f;

			if (doorFullyOpenBool) {
				whiteScreenColor.a += 0.4f * Time.deltaTime;
				whiteScreenColorTwo.a += 0.3f * Time.deltaTime;

			}
		
		}
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{



		if (other.gameObject.tag == "Player") {

			if (pickUpScript.hasKey == true) {

				doorOpen = true;

				m_AnimDoorAbattoir.SetBool ("DoorOpenAnim", true);

				StartCoroutine (waitForDoor (0.1f));


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
	IEnumerator waitForDoor(float waitTime){

		yield return new WaitForSeconds (waitTime);
		doorFullyOpenBool = true;
	}
}

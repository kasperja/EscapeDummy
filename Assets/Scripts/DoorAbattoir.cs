using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class DoorAbattoir : MonoBehaviour {

	public PickUp pickUpScript;
	public bool doorOpen = false;
	public Sprite doorClosedSprite;
	public Sprite doorOpenSprite;
	public AudioSource doorOpenSound;
	public bool doorSoundPlayOnce = true;

	public GameObject enemyWorkingSaw;
	public GameObject controller;

	public AudioSource ambientSound;
	public AudioSource sawIdleSound;
	public AudioSource sawCutSound;

	public GameObject whiteScreen;
	public GameObject whiteScreenTwo;

	private Color whiteScreenColor = new Color (1f, 1f, 1f, 0f);
	private Color whiteScreenColorTwo = new Color (1f, 1f, 1f, 0f);

	public GameObject MainCamObj;

	public Animator m_AnimDoorAbattoir;

	public GameObject enemyWithKey;
	public GameObject key;

	private bool doorFullyOpenBool = false;

	// Use this for initialization
	void Start () {

	
	
	}
	
	// Update is called once per frame
	void Update () {

		whiteScreen.GetComponent<SpriteRenderer> ().color = whiteScreenColor;

		whiteScreenTwo.GetComponent<SpriteRenderer> ().color = whiteScreenColorTwo;

		if (doorOpen) {
		
			StartCoroutine (waitForSceneLoad (3f));

			MainCamObj.GetComponent<SunShafts> ().sunShaftIntensity += 3f * Time.deltaTime;
		

			ambientSound.volume -= 0.5f * Time.deltaTime;
			sawCutSound.volume -= 0.5f * Time.deltaTime;
			sawIdleSound.volume -= 0.5f * Time.deltaTime;

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

			} else {
			
				enemyWorkingSaw.SetActive (false);
				controller.SetActive (true);
				enemyWithKey.SetActive (true);
				key.SetActive (true);
			
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
	IEnumerator waitForSceneLoad(float waitTime){

		yield return new WaitForSeconds (waitTime);
		SceneManager.LoadScene (2);
	}
}

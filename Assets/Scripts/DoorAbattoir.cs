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
	public AudioSource doorLockedSound;
	public bool doorSoundPlayOnce = true;

	public AudioSource iGotKeySound;
	private bool iGotKeySoundOnce = true;

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

	public PlatformerCharacter2D mainCharScript;

	public HitPointsEnemyTotal hpET;

	private bool doorFullyOpenBool = false;

	public bool enemyActive = false;

	public AudioSource ambientOutside;
	// Use this for initialization
	void Start () {

	
	
	}

	// Update is called once per frame
	void Update () {

		if (hpET.isDeadEnemy && doorOpen == false) {
			
			mainCharScript.FadeInMusic (mainCharScript.musicScript.introMusic, mainCharScript.musicVolumeIntro);

		}

		whiteScreen.GetComponent<SpriteRenderer> ().color = whiteScreenColor;

		whiteScreenTwo.GetComponent<SpriteRenderer> ().color = whiteScreenColorTwo;

		if (doorOpen) {
		

			mainCharScript.FadeOutMusic (mainCharScript.musicScript.introMusic, mainCharScript.musicVolumeIntro);

			StartCoroutine (waitForSceneLoad (3f));

			MainCamObj.GetComponent<SunShafts> ().sunShaftIntensity += 3f * Time.deltaTime;
		
			ambientOutside.volume += 0.5f * Time.deltaTime;
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
			
				doorLockedSound.Play ();

				if (iGotKeySoundOnce && !mainCharScript.enemyIsDead) {
					
					iGotKeySound.Play ();
					iGotKeySoundOnce = false;
				}

				enemyActive = true;


			
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
		yield return new WaitForSeconds (2f);
		//ambientOutside.volume -= 1f * Time.deltaTime;
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (2);
	}
}

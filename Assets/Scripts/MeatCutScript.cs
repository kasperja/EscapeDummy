using UnityEngine;
using System.Collections;

public class MeatCutScript : MonoBehaviour {

	public GameObject MeatBackObj;
	public GameObject MeatBackUncutObj;
	public ParticleSystem BloodSawParticle;
	public bool cutMeatOnce = true;
	private float punchAmmount = 0.4f;
	public bool meatPassedBool = false;
	public AudioSource sawSound;
	public bool waitForSoundBool = true; 

	public bool resetPosBool = false;

	private Transform startPos;
	private Transform startPosBack;

	public AudioSource cutSound;
	public AudioSource idleSound;
	private bool idleSoundOnce = true;

	public ParticleSystem bloodDripParticle;
	// Use this for initialization
	void Start () {
	
		startPos = transform;
		startPosBack = MeatBackObj.transform;

		startPos.localPosition = gameObject.transform.localPosition;
		startPosBack.localPosition = MeatBackObj.transform.localPosition;


		BloodSawParticle.Stop ();

	}
	
	// Update is called once per frame
	void Update () {

		if (resetPosBool) {
		
			transform.localPosition = startPos.localPosition;
			MeatBackObj.transform.localPosition = startPosBack.localPosition;
			resetPosBool = false;
		
		}
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.tag == "SawCol" && !meatPassedBool) {
			BloodSawParticle.Play ();
			bloodDripParticle.Play ();


			if (waitForSoundBool) {
				cutSound.Play ();
				idleSound.Stop ();
				sawSound.Play ();
				waitForSoundBool = false;
				idleSoundOnce = true;

			}


			iTween.PunchScale (gameObject, new Vector3 (punchAmmount, -punchAmmount, 0f), 1f);
			iTween.PunchScale (MeatBackUncutObj, new Vector3 (punchAmmount, -punchAmmount, 0f), 1f);

			StartCoroutine (waitAndCut (0.5f));


		} else {
		
			if(idleSoundOnce){
				idleSound.Play ();
				idleSoundOnce = false;
			}
		
		}


		if (other.gameObject.tag == "MeatPassedCol") {

			meatPassedBool = true;

		}
			

	}
		
	void OnTriggerExit2D(Collider2D other){

		if (other.gameObject.tag == "SawCol") {

			BloodSawParticle.Stop ();

		}
			
	}
	IEnumerator waitAndCut(float waitTime){

		yield return new WaitForSeconds (waitTime);

		if (cutMeatOnce) {

			MeatBackUncutObj.SetActive (false);
			MeatBackObj.SetActive(true);

			iTween.PunchScale (gameObject, new Vector3 (punchAmmount, -punchAmmount, 0f), 1f);
			iTween.PunchScale (MeatBackObj, new Vector3 (punchAmmount, -punchAmmount, 0f), 1f);

			MeatBackObj.transform.rotation = transform.rotation;
			MeatBackObj.transform.position = transform.position;
			MeatBackObj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			cutMeatOnce = false;

		}
		yield return new WaitForSeconds (0.5F);
		BloodSawParticle.Stop ();

		yield return new WaitForSeconds (0.2f);
		BloodSawParticle.Stop ();

		gameObject.GetComponent<CircleCollider2D> ().enabled = false;

		waitForSoundBool = true;

		yield return new WaitForSeconds (5f);
		gameObject.GetComponent<CircleCollider2D> ().enabled = true;


	
	}

}
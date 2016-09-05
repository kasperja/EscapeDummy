using UnityEngine;
using System.Collections;

public class MeatCutScript : MonoBehaviour {

	public GameObject MeatBackObj;
	public GameObject MeatBackUncutObj;
	public ParticleSystem BloodSawParticle;
	public bool cutMeatOnce = true;
	private float punchAmmount = 0.4f;
	public bool meatPassedBool = false;
	// Use this for initialization
	void Start () {
	
		BloodSawParticle.Stop ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.tag == "SawCol" && !meatPassedBool) {
			BloodSawParticle.Play ();
			iTween.PunchScale (gameObject, new Vector3 (punchAmmount, -punchAmmount, 0f), 1f);
			iTween.PunchScale (MeatBackUncutObj, new Vector3 (punchAmmount, -punchAmmount, 0f), 1f);

			StartCoroutine (waitAndCut (0.5f));


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

		yield return new WaitForSeconds (5f);
		gameObject.GetComponent<CircleCollider2D> ().enabled = true;

	
	}
}
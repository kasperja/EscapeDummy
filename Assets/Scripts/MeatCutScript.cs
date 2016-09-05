using UnityEngine;
using System.Collections;

public class MeatCutScript : MonoBehaviour {

	public GameObject MeatBackObj;
	public GameObject MeatBackUncutObj;
	public ParticleSystem BloodSawParticle;
	// Use this for initialization
	void Start () {
	
		BloodSawParticle.Stop ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "SawCol") {

			MeatBackUncutObj.SetActive (false);
			MeatBackObj.SetActive(true);
			MeatBackObj.transform.rotation = transform.rotation;
			MeatBackObj.transform.position = transform.position;
			BloodSawParticle.Play ();

		}

	}
	void OnTriggerExit2D(Collider2D other){

		if (other.gameObject.tag == "SawCol") {

			BloodSawParticle.Stop ();

		}
	}
}
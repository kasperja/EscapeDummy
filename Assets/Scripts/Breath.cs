using UnityEngine;
using System.Collections;

public class Breath : MonoBehaviour {

	public float breathSpeed = 0.8f;
	public ParticleSystem breathParticle;
	public Animator m_Anim;
	// Use this for initialization
	void Start () {

		if (m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle Lookup")) {
			

		} else {

			StartCoroutine (waitBreath (breathSpeed));
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator waitBreath(float waitTime){
	
		yield return new WaitForSeconds (waitTime);

		if (m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle Lookup")) {
			
			breathParticle.Stop ();

		} else {
		
			breathParticle.Play ();
		
		}
		yield return new WaitForSeconds (waitTime);

		breathParticle.Stop ();

		yield return new WaitForSeconds (waitTime);

		StartCoroutine (waitBreath (waitTime));
	
	}
}

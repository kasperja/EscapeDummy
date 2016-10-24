using UnityEngine;
using System.Collections;

public class HitpointsPlayerTotal : MonoBehaviour {

	public float hitpoints = 10000.0f;
	public AudioSource deathSound;
	public ParticleSystem deathParticle;
	public GameObject particlePosObj;
	public float maxHitpoints;
	public GameObject healthBar;
	public GameObject healthBarBG;
	public RectTransform hpPos;
	public RectTransform healthBarRect;
	public EnemyMovement em;
	public GameObject MainCharacterObj;

	public GameObject UpperTarget;
	public GameObject MiddleTarget;
	public GameObject LowerTarget;

	public PlatformerCharacter2D pc2D;

	public Animator m_Anim;
	public bool isDead = false;
	public bool dieOnce = true;

	public bool blockBool;
	public Animation blockAnim;

	// Use this for initialization
	void Start () {
	
		maxHitpoints = hitpoints;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (hitpoints <= 0.0f) {

			//em.isFollowing = false;
			//em.isInRange = false;
			if(dieOnce){
				Instantiate (deathParticle, particlePosObj.transform.position, Quaternion.identity);

				deathSound.Play ();

				healthBar.SetActive (false);
				healthBarBG.SetActive (false);
				healthBarRect.sizeDelta = new Vector2(100, 10);



				UpperTarget.SetActive (false);
				MiddleTarget.SetActive (false);
				LowerTarget.SetActive (false);

				dieOnce = false;



			// death
			}

			m_Anim.SetBool ("Dead", true);
			isDead = true;
		

		


		} else if (hitpoints > 0.0f && hitpoints < maxHitpoints) {

			healthBar.SetActive (true);
			healthBarRect.sizeDelta = new Vector2(((hitpoints / maxHitpoints)* 100), 10);

			if (Input.GetKey (KeyCode.Q)) {

				m_Anim.SetBool ("Block", true);
				blockBool = true;
				StartCoroutine (waitBlock (blockAnim.clip.length));
				UpperTarget.SetActive (false);
				MiddleTarget.SetActive (false);
				LowerTarget.SetActive (false);
			
			}


		} else {

			healthBar.SetActive (true);


			if (Input.GetKey (KeyCode.Q)) {

				m_Anim.SetBool ("Block", true);
				blockBool = true;
				StartCoroutine (waitBlock (blockAnim.clip.length));
				UpperTarget.SetActive (false);
				MiddleTarget.SetActive (false);
				LowerTarget.SetActive (false);

			}

		}



	}

	IEnumerator waitBlock(float waitTime){

		yield return new WaitForSeconds (waitTime);

		m_Anim.SetBool ("Block", false);
		blockBool = false;
		UpperTarget.SetActive (true);
		MiddleTarget.SetActive (true);
		LowerTarget.SetActive (true);


	}

}

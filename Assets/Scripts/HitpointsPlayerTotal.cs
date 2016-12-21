using UnityEngine;
using System.Collections;

public class HitpointsPlayerTotal : MonoBehaviour {

	public float hitpoints = 10000.0f;
	public AudioSource deathSound;
	public ParticleSystem deathParticle;
	public ParticleSystem deathBloodParticle;
	public ParticleSystem sawDeathBloodParticle;
	public Transform sawDeathBloodParticlePos;
	public GameObject graphics;
	public bool deathSaw = false;
	public GameObject particlePosObj;
	public GameObject particlePosObjDeathBlood;
	public float maxHitpoints;
	public GameObject healthBar;
	public GameObject healthBarBG;
	public RectTransform hpPos;
	public RectTransform healthBarRect;
	public EnemyMovement em;
	public GameObject MainCharacterObj;
	public GameObject MainCharObj;


	public GameObject UpperTarget;
	public GameObject MiddleTarget;
	public GameObject LowerTarget;

	public AttackEnemy enemyMiddleAttack;
	public bool blockOncePlayer = true;
	public GameObject blockCollider;
	public GameObject blockParticlePosObj;
	public ParticleSystem blockPlayerParticle;

	public AudioSource blockSound;

	public PlatformerCharacter2D pc2D;

	public Animator m_Anim;
	public bool isDead = false;
	public bool dieOnce = true;

	public bool blockBool;
	//public Animation blockAnim;

	// Use this for initialization
	void Start () {
	
		maxHitpoints = hitpoints;

	}
	
	// Update is called once per frame
	void Update () {

		if (hitpoints <= 0.0f) {

			m_Anim.SetBool ("Hit", false);
			//em.isFollowing = false;
			//em.isInRange = false;

			if (deathSaw) {
				
				if (dieOnce) {
					
					Instantiate (sawDeathBloodParticle, sawDeathBloodParticlePos.position, Quaternion.identity);


					deathSound.Play ();

					healthBar.SetActive (false);
					healthBarBG.SetActive (false);
					healthBarRect.sizeDelta = new Vector2 (100, 10);



					UpperTarget.SetActive (false);
					MiddleTarget.SetActive (false);
					LowerTarget.SetActive (false);
					MainCharObj.GetComponent<BoxCollider2D> ().enabled = false;

					dieOnce = false;

					deathSaw = false;


					// death
				}
				m_Anim.SetBool ("Hit", false);
				graphics.SetActive (false);
				//m_Anim.SetBool ("Dead", true);
				isDead = true;


			} else {
				
				if (dieOnce) {
					Instantiate (deathParticle, particlePosObj.transform.position, Quaternion.identity);

					Instantiate (deathBloodParticle, particlePosObjDeathBlood.transform.position, Quaternion.identity);

					deathSound.Play ();

					healthBar.SetActive (false);
					healthBarBG.SetActive (false);
					healthBarRect.sizeDelta = new Vector2 (100, 10);



					UpperTarget.SetActive (false);
					MiddleTarget.SetActive (false);
					LowerTarget.SetActive (false);
					MainCharObj.GetComponent<BoxCollider2D> ().enabled = false;
					MainCharObj.GetComponent<CircleCollider2D> ().enabled = false;

					dieOnce = false;



					// death
				}
				m_Anim.SetBool ("Hit", false);
				m_Anim.SetBool ("Dead", true);
				isDead = true;
		
			}
		


		} else if (hitpoints > 0.0f && hitpoints <= maxHitpoints) {

			if (enemyMiddleAttack.playerBlockEnabled && blockOncePlayer) {
				
				if (!pc2D.m_FacingRight) {
					iTween.MoveBy (MainCharObj, new Vector3 (10f, 0f, 0f), 0.6f);
				} else {
				
					iTween.MoveBy (MainCharObj, new Vector3 (-10f, 0f, 0f), 0.6f);

				}
				blockSound.Play ();
				blockPlayerParticle.Play ();
				//Instantiate (blockPlayerParticle, blockParticlePosObj.transform.position, Quaternion.identity);
				StartCoroutine (waitBlockEnable ());
				blockOncePlayer = false;
			
			}

			healthBar.SetActive (true);
			healthBarRect.sizeDelta = new Vector2(((hitpoints / maxHitpoints)* 100), 10);

			if (Input.GetKey (KeyCode.Q)) {
				//iTween.MoveBy (MainCharObj, new Vector3 (-10f,0f,0f), 0.6f);
				m_Anim.SetBool ("Block", true);
				m_Anim.SetBool ("Hit", false);
				blockBool = true;
				StartCoroutine (waitBlock (0.5f));

				blockCollider.GetComponent<BoxCollider2D> ().enabled = true;
				UpperTarget.SetActive (false);
				MiddleTarget.SetActive (false);
				LowerTarget.SetActive (false);
			
			}


		} else {

			healthBar.SetActive (true);


			if (Input.GetKey (KeyCode.Q)) {

				m_Anim.SetBool ("Block", true);
				m_Anim.SetBool ("Hit", false);
				blockBool = true;
				StartCoroutine (waitBlock (0.5f));
				UpperTarget.GetComponent<CircleCollider2D> ().enabled = false;
				MiddleTarget.GetComponent<CircleCollider2D> ().enabled = false;
				LowerTarget.GetComponent<CircleCollider2D> ().enabled = false;

			}

		}



	}

	IEnumerator waitBlock(float waitTime){

		yield return new WaitForSeconds (waitTime);

		m_Anim.SetBool ("Block", false);
		blockBool = false;
		UpperTarget.GetComponent<CircleCollider2D> ().enabled = true;
		MiddleTarget.GetComponent<CircleCollider2D> ().enabled = true;
		LowerTarget.GetComponent<CircleCollider2D> ().enabled = true;
		blockCollider.GetComponent<BoxCollider2D> ().enabled = false;



	}
	IEnumerator waitBlockEnable(){
		
		yield return new WaitForSeconds (1f);
		blockOncePlayer = true;
	}

}

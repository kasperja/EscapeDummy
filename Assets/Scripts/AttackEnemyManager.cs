using UnityEngine;
using System.Collections;

public class AttackEnemyManager : MonoBehaviour {

	private float randomAttackNumber = 0.5f;

	public AttackEnemy attackEnemyHigh;
	public AttackEnemy attackEnemyMiddle;
	public AttackEnemy attackEnemyLow;
	private bool playOnceWaiter = true;

	public AudioSource woosh;
	public bool wooshPlayOnce = true;

	private Animator m_Anim;
	public HitPointsEnemyTotal hpt;
	public bool blockOnce = true;
	public bool blockActive = true;

	public EnemyMovement em;

	// Use this for initialization
	private void Awake(){

		m_Anim = GetComponent<Animator>();

	}


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



		if (playOnceWaiter) {
			playOnceWaiter = false;
			StartCoroutine (Waiter ());

		}


		if (randomAttackNumber >= 0.0f && randomAttackNumber < 0.33f && blockOnce && em.isInRange && blockActive) {
		

			hpt.enemyBlock = true;

			blockOnce = false;

			StartCoroutine (waitBlock ());

		
		} else if (randomAttackNumber >= 0.33f && randomAttackNumber <= 0.66f) {
		
			attackEnemyMiddle.activeAttack = true;
			attackEnemyHigh.activeAttack = false;
			attackEnemyLow.activeAttack = false;
		
		
		} else if (randomAttackNumber > 0.66f && randomAttackNumber <= 1.0f) {
		
			attackEnemyLow.activeAttack = true;
			attackEnemyHigh.activeAttack = false;
			attackEnemyMiddle.activeAttack = false;
		
		}

		if (attackEnemyHigh.activate == true && !m_Anim.GetBool("Block")) {

			m_Anim.SetBool ("Attack1Bool", true);

			if (wooshPlayOnce) {
				woosh.Play ();
				wooshPlayOnce = false;
			}

		
		} else if (attackEnemyMiddle.activate == true && !m_Anim.GetBool("Block")) {
			
			m_Anim.SetBool ("Attack2Bool", true);

			if (wooshPlayOnce) {
				woosh.Play ();
				wooshPlayOnce = false;
			}

		} else if (attackEnemyLow.activate == true && !m_Anim.GetBool("Block")) {

			m_Anim.SetBool ("Attack3Bool", true);

			if (wooshPlayOnce) {
				woosh.Play ();
				wooshPlayOnce = false;
			}

		} else {
		
			m_Anim.SetBool ("Attack1Bool", false);
			m_Anim.SetBool ("Attack2Bool", false);
			m_Anim.SetBool ("Attack3Bool", false);
			wooshPlayOnce = true;
		
		
		}
	
	}

	IEnumerator Waiter(){



		yield return new WaitForSeconds (0.0f);

		randomAttackNumber = Random.Range (0.15f, 0.66f);
		playOnceWaiter = true;

	}

	IEnumerator waitBlock(){
		blockActive = false;
		yield return new WaitForSeconds (4f);
		blockActive = true;
		hpt.blockOnce = true;
	
	}

}

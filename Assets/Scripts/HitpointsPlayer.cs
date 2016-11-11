using UnityEngine;
using System.Collections;

public class HitpointsPlayer : MonoBehaviour {

	private bool playOnceHigh = true;
	private bool playOnceMiddle = true;
	private bool playOnceLow = true;
	public HitpointsPlayerTotal hitPointsTotalScript;
	public AttackEnemy enemyAttackDmgHigh;
	public AttackEnemy enemyAttackDmgMiddle;
	public AttackEnemy enemyAttackDmgLow;
	public SawMover SawTrapDamage;
	public float dmgMultiplier = 1.0f;
	public GameObject hitParticle;
	public PlatformerCharacter2D MainCharScript;

	public Animator m_Anim;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "EnemyAttackHigh") {

			StartCoroutine (waitHitAnim ());

			hitPointsTotalScript.hitpoints -= enemyAttackDmgHigh.attackDamage * dmgMultiplier;
			Instantiate (hitParticle, this.gameObject.transform.position, Quaternion.identity);
			playOnceHigh = false;

		}else if (collider.tag == "EnemyAttackMiddle") {

			StartCoroutine (waitHitAnim ());
			hitPointsTotalScript.hitpoints -= enemyAttackDmgMiddle.attackDamage * dmgMultiplier;
			Instantiate (hitParticle, this.gameObject.transform.position, Quaternion.identity);
			playOnceMiddle = false;

		}else if (collider.tag == "EnemyAttackLow") {

			StartCoroutine (waitHitAnim ());
			hitPointsTotalScript.hitpoints -= enemyAttackDmgLow.attackDamage * dmgMultiplier;
			Instantiate (hitParticle, this.gameObject.transform.position, Quaternion.identity);
			playOnceLow = false;
		}
		if (collider.tag == "SawCol" && !MainCharScript.hookJumpActive && !hitPointsTotalScript.isDead) {
			
			//StartCoroutine (waitHitAnim ());
			hitPointsTotalScript.hitpoints -= SawTrapDamage.damage * Time.deltaTime;
			hitPointsTotalScript.deathSaw = true;

		}


		}

	void OnTriggerStay2D(Collider2D collider){
	

		if (collider.tag == "SawCol" && !MainCharScript.hookJumpActive && !hitPointsTotalScript.isDead) {

			hitPointsTotalScript.hitpoints -= SawTrapDamage.damage * Time.deltaTime;
			hitPointsTotalScript.deathSaw = true;

		}
	
	}
	void OnTriggerExit2D(Collider2D collider){
	
		playOnceHigh = true;
		playOnceMiddle = true;
		playOnceLow = true;


		if (collider.tag == "SawCol" && !MainCharScript.hookJumpActive && !hitPointsTotalScript.isDead) {


			hitPointsTotalScript.deathSaw = false;

		}
	
	}
	IEnumerator waitHitAnim(){

		m_Anim.SetBool ("Hit", true);
		yield return new WaitForSeconds (0.1f);
		m_Anim.SetBool ("Hit", false);

	


	}

			
}

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


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "EnemyAttackHigh" && playOnceHigh) {


			hitPointsTotalScript.hitpoints -= enemyAttackDmgHigh.attackDamage * dmgMultiplier;
			Instantiate (hitParticle, this.gameObject.transform.position, Quaternion.identity);
			playOnceHigh = false;

		}else if (collider.tag == "EnemyAttackMiddle" && playOnceMiddle) {


			hitPointsTotalScript.hitpoints -= enemyAttackDmgMiddle.attackDamage * dmgMultiplier;
			Instantiate (hitParticle, this.gameObject.transform.position, Quaternion.identity);
			playOnceMiddle = false;

		}else if (collider.tag == "EnemyAttackLow" && playOnceLow) {


			hitPointsTotalScript.hitpoints -= enemyAttackDmgLow.attackDamage * dmgMultiplier;
			Instantiate (hitParticle, this.gameObject.transform.position, Quaternion.identity);
			playOnceLow = false;
		}



		}

	void OnTriggerStay2D(Collider2D collider){
	

		if (collider.tag == "SawCol" && !MainCharScript.hooked) {

			hitPointsTotalScript.hitpoints -= SawTrapDamage.damage * Time.deltaTime;

		}
	
	}
	void OnTriggerExit2D(Collider2D collider){
	
		playOnceHigh = true;
		playOnceMiddle = true;
		playOnceLow = true;

	
	}
			
}

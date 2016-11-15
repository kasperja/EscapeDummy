using UnityEngine;
using System.Collections;

public class StopTrigger : MonoBehaviour {

	public EnemyMovement Em;
	public HitpointsPlayerTotal hptScript;
	public Animator m_AnimEnemy;


	void Update(){

	
	
	}
	void OnTriggerStay2D(Collider2D collider){

		if (collider.tag == "Player") {

			Em.isInRange = true;

		}
	}
	void OnTriggerEnter2D(Collider2D collider){

		if (collider.tag == "Player") {
			
			Em.isInRange = true;

		}
	}
	void OnTriggerExit2D(Collider2D collider){

		if (collider.tag == "Player") {
			
			Em.isInRange = false;

		}
	}
	IEnumerator waitWalk(){
		m_AnimEnemy.SetBool ("StopTrigger", true);
		Em.waitForWalk = false;
		yield return new WaitForSeconds (1f);
		Em.waitForWalk = true;
		m_AnimEnemy.SetBool ("StopTrigger", false);

	}
}

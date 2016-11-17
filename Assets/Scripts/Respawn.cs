using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	public GameObject Player;
	public HitpointsPlayerTotal hp;
	public PlatformerCharacter2D pc2DScript;
	public EnemyMovement em;
	public GameObject MainCharObj;
	public GameObject graphics;
	public Transform respawnPos;

	public Animator m_Anim;

	public GameObject UpperTarget;
	public GameObject MiddleTarget;
	public GameObject LowerTarget;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Return) && hp.isDead) {

			MainCharObj.transform.position = respawnPos.position;
			graphics.SetActive (true);

			pc2DScript.hookJumpActive = false;
			pc2DScript.hookJumpActiveOnce = true;
			pc2DScript.hookJumpActiveOnceTween = true;


			m_Anim.SetBool ("Dead", false);
			pc2DScript.playOnce1 = true;
			pc2DScript.playOnce2 = true;
			pc2DScript.playOnce3 = true;
			pc2DScript.playOnce4 = true;

			pc2DScript.m_Attack1 = false;
			pc2DScript.m_Attack2 = false;
			pc2DScript.m_Attack3 = false;
			pc2DScript.m_Attack4 = false;

			Player.SetActive (true);
			hp.healthBar.SetActive (true);
			hp.healthBarBG.SetActive (true);

			hp.hitpoints = hp.maxHitpoints;

			UpperTarget.SetActive (true);
			MiddleTarget.SetActive (true);
			LowerTarget.SetActive (true);
			MainCharObj.GetComponent<BoxCollider2D> ().enabled = true;
			MainCharObj.GetComponent<CircleCollider2D> ().enabled = true;
			em.walkWhenDead = false;
			em.killOnce = true;

			m_Anim.SetBool ("Hit", false);
			hp.isDead = false;
			hp.dieOnce = true;





		}
	}
}

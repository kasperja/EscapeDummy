using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {

	public ParticleSystem slashParticle;
	public Transform[] wayPointArrayTwo;
	public float attackTime;
	public float attackDelay;
	float percentsPerSecond = 5f;
	float currentPathPercent = 0.0f;
	public bool slashActive = true;
	private bool slashOnce = true;
	private bool slashStart = true;
	//public KeyCode attackKey;
	public string attackAnimName;
	public string attackStateName;
	public Animator animatorAttack;
	public bool rotateEnabled = false;
	public Vector3 rotation = new Vector3();
	public Quaternion startRotation;
	public Animator m_anim;
	public PlatformerCharacter2D pc2D;

	public bool isKneeAttack = false;

	public PlatformerCharacter2D pc2Dscript;
	public AttackManager am;


	private bool playOnce = true;


	// Use this for initialization
	void Start () {

		transform.position = wayPointArrayTwo [0].position;
		startRotation = transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {



		if (animatorAttack.GetBool(attackAnimName) && animatorAttack.GetCurrentAnimatorStateInfo(0).IsName(attackStateName) && slashOnce && am.attackHit) {
		
			slashActive = true;
			slashOnce = false;

		
		}

		if (slashActive) {
		
			StartCoroutine (slashDelay (attackDelay));
		
		}

		if (slashStart) {
			//currentPathPercent += percentsPerSecond * Time.deltaTime;
			//iTween.PutOnPath (gameObject, wayPointArrayTwo, currentPathPercent);
			if(playOnce){ 

				slashParticle.Play ();
				playOnce = false;
			}
			if (rotateEnabled) {
			
				if (pc2Dscript.m_FacingRight) {
					
					transform.Rotate (rotation);
				
				} else {
					
					transform.Rotate (-rotation);

				}

			
			}
			iTween.MoveTo (gameObject, wayPointArrayTwo [1].position, attackTime);
			StartCoroutine (waitSlash ());
		} else {
			//iTween.PutOnPath (gameObject, wayPointArrayTwo, 0);
			transform.position = wayPointArrayTwo [0].position;
			transform.rotation = startRotation;
		
		}

	}
	IEnumerator waitSlash(){
		

		yield return new WaitForSeconds (attackTime);

		slashParticle.Stop ();
		slashStart = false;
		slashOnce = true;
		playOnce = true;

	
	}
	IEnumerator slashDelay(float attackD){
		



		yield return new WaitForSeconds (attackD);

		slashActive = false;
		if(isKneeAttack)slashStart = true;
		if(!isKneeAttack && !m_anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack3") && !pc2D.m_FacingRight)slashStart = true;

	}
}

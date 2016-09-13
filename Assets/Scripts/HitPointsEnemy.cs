﻿using UnityEngine;
using System.Collections;

public class HitPointsEnemy : MonoBehaviour {


	private bool playOnceHigh = true;
	private bool playOnceMiddle = true;
	private bool playOnceLow = true;
	/*public GameObject highAttackPrefab;
	public GameObject middleAttackPrefab;
	public GameObject lowAttackPrefab;*/
	public GameObject hitParticle;

	public GameObject mainCamObj;

	//public ParticleSystem falconParticle;

	public HitPointsEnemyTotal hitPointsTotalScript;

	public AttackManager highDmg;
	public AttackManager middleDmg;
	public AttackManager lowDmg;

	public float dmgMultiplier = 2.0f;

	public AudioSource punchSound;
	public AudioSource falconPunchSound;

	public GameObject falconParticle;

	private KeyCombo falconPunch= new KeyCombo(new string[] {"W", "E","R"});

	public ComboManager comboManager;
	public bool falconPunchBool = false;
	private bool falconPunchPlayOnce = true;

	public GameObject particlePosObj;



	// Use this for initialization
	/*public Collider2D highCol;
	public Collider2D middleCol;
	public Collider2D lowCol;*/
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

			if (falconPunch.Check())
			{



				// do the falcon punch
			falconPunchBool = true;
			falconPunchPlayOnce = true;
			comboManager.falconPunchBool = true;
			StartCoroutine(ComboWaiter());
				//Debug.Log("PUNCH"); 
			}		
			


	
	}

	IEnumerator ComboWaiter(){
	
		yield return new WaitForSeconds (0.7f);
		comboManager.falconPunchBool = false;
		falconPunchBool = false;


	
	}
		
	void OnTriggerEnter2D(Collider2D collider){




		if (collider.tag == "highCol" && playOnceHigh) {
			//Debug.Log ("HIT High Attack");

			if (falconPunchBool && falconPunchPlayOnce) {

				hitPointsTotalScript.hitpoints -= comboManager.falconPunchDmg * dmgMultiplier;

				playOnceHigh = false;
				falconPunchSound.Play ();
				//falconParticle.Play ();


				Instantiate (falconParticle, particlePosObj.transform.position, Quaternion.identity);




				falconPunchPlayOnce = false;


			} else {
				
				hitPointsTotalScript.hitpoints -= highDmg.attackDamage * dmgMultiplier;
				Instantiate (hitParticle, gameObject.transform.position, Quaternion.identity);
				playOnceHigh = false;

			}



		}
		if (collider.tag == "middleCol" && playOnceMiddle) {
			//Debug.Log ("HIT Middle Attack");

			if (falconPunchBool && falconPunchPlayOnce) {

				hitPointsTotalScript.hitpoints -= comboManager.falconPunchDmg * dmgMultiplier;

				playOnceMiddle = false;
				falconPunchSound.Play ();
				//falconParticle.Play ();

				Instantiate (falconParticle, particlePosObj.transform.position, Quaternion.identity);


				falconPunchPlayOnce = false;


			} else {
				
				hitPointsTotalScript.hitpoints -= middleDmg.attackDamage * dmgMultiplier;
				Instantiate (hitParticle, gameObject.transform.position, Quaternion.identity);
				playOnceMiddle = false;

			}


		}

		if (collider.tag == "lowCol" && playOnceLow) {
			//Debug.Log ("HIT Low Attack");
			if (falconPunchBool && falconPunchPlayOnce) {

				hitPointsTotalScript.hitpoints -= comboManager.falconPunchDmg * dmgMultiplier;

				playOnceLow = false;
				falconPunchSound.Play ();
				//falconParticle.Play ();

				Instantiate (falconParticle, particlePosObj.transform.position, Quaternion.identity);

				falconPunchPlayOnce = false;


			} else {
				hitPointsTotalScript.hitpoints -= lowDmg.attackDamage * dmgMultiplier;
				Instantiate (hitParticle, gameObject.transform.position, Quaternion.identity);
				playOnceLow = false;

			}
		}


	}

	void OnTriggerExit2D(Collider2D collider){

		falconPunchBool = false;
		playOnceHigh = true;
		playOnceMiddle = true;
		playOnceLow = true;

	}


}
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


	// Use this for initialization
	void Start () {
	
		maxHitpoints = hitpoints;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (hitpoints <= 0.0f) {

			//em.isFollowing = false;
			//em.isInRange = false;
			Instantiate (deathParticle, particlePosObj.transform.position, Quaternion.identity);

			deathSound.Play ();
			// death


			healthBar.SetActive (false);
			healthBarBG.SetActive (false);
			healthBarRect.sizeDelta = new Vector2(100, 10);
			MainCharacterObj.SetActive (false);



		} else if (hitpoints > 0.0f && hitpoints < maxHitpoints) {

			healthBar.SetActive (true);
			healthBarRect.sizeDelta = new Vector2(((hitpoints / maxHitpoints)* 100), 10);




		} else {

			healthBar.SetActive (true);

		}



	}
}

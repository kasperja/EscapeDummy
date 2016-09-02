using UnityEngine;
using System.Collections;

public class HitPointsEnemyTotal : MonoBehaviour {


	public float hitpoints = 5000.0f;
	public float maxHitpoints;
	public GameObject healthBar;
	public RectTransform healthBarRect;

	public GameObject particlePosObj;
	public GameObject EnemyParentObj;

	public ParticleSystem deathParticle;
	public AudioSource deathSound;

	// Use this for initialization
	void Start () {
	
		maxHitpoints = hitpoints;

	}
	
	// Update is called once per frame
	void Update () {

		if (hitpoints <= 0.0f) {

			Instantiate (deathParticle, particlePosObj.transform.position, Quaternion.identity);

			deathSound.Play ();
			Destroy (EnemyParentObj);


		}
		if (hitpoints > 0.0f && hitpoints < maxHitpoints) {
		
			healthBar.SetActive (true);
			healthBarRect.sizeDelta = new Vector2((hitpoints / maxHitpoints) * 150, 50);


		
		} else {
		
			healthBar.SetActive (false);
		
		}


	}


	

}

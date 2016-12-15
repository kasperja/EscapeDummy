using UnityEngine;
using System.Collections;

public class EnemyActive : MonoBehaviour {

	public GameObject enemyWorkingSaw;
	public GameObject controller;
	public GameObject enemyWithKey;
	public GameObject key;

	private bool setActiveOnce = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		

	
	}

	void OnTriggerEnter2D(Collider2D other){
	
		if (other.gameObject.tag == "Player" && setActiveOnce) {
			enemyWorkingSaw.SetActive (false);
			controller.SetActive (true);
			enemyWithKey.SetActive (true);
			key.SetActive (true);
			setActiveOnce = false;
		}
	
	
	}
}

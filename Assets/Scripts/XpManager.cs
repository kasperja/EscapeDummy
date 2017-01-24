using UnityEngine;
using System.Collections;

public class XpManager : MonoBehaviour {

	public HitPointsEnemyTotal hpET;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (hpET.isDeadEnemy || Input.GetKeyDown(KeyCode.U)) {
		
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (255 ,255 ,255 ,255);
		
		}
	
	}
}

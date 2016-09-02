using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	public GameObject Player;
	public HitpointsPlayerTotal hp;
	public PlatformerCharacter2D pc2DScript;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Return)) {

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

		}
	}
}

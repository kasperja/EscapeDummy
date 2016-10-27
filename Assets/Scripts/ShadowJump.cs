using UnityEngine;
using System.Collections;

public class ShadowJump : MonoBehaviour {

	public PlatformerCharacter2D pf2Dscript;
	private Vector3 minScale;
	private Vector3 maxScale;
	// Use this for initialization
	void Start () {
	
		maxScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
		minScale = maxScale / 2f;

	}
	
	// Update is called once per frame
	void Update () {

		if (!pf2Dscript.m_Grounded) {



			if (transform.localScale.x <= minScale.x) {
			
				iTween.ScaleTo (gameObject, maxScale, 2f);
			
			} else {
			
				iTween.ScaleTo (gameObject, minScale, 2f);
			
			}
		
		} else {
		
			iTween.ScaleTo (gameObject, maxScale, 1f);
		
		}
	
	}
}

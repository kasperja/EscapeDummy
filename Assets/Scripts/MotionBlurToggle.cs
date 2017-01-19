using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MotionBlurToggle : MonoBehaviour {

	public Animator m_Anim;
	private MotionBlur mBlur;

	// Use this for initialization
	void Start () {

		mBlur = gameObject.GetComponent<MotionBlur> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack3")) {
			
			mBlur.enabled = true;

		} else {
			
			mBlur.enabled = false;
		
		}
	}
}

using UnityEngine;
using System.Collections;

public class CameraHandheldStop : MonoBehaviour {

	public Animator handheldAnim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.RightArrow))
			handheldAnim.SetBool ("xAxis", true);
			

	}
}

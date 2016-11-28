using UnityEngine;
using System.Collections;

public class StartScriptCam : MonoBehaviour {

	public PlatformerCharacter2D pc2D;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerExit2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player") {


			pc2D.startCamBool = false;


		}


	}
}

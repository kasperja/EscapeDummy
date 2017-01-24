using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroyUI : MonoBehaviour {

	public float hpPlayer = 600f;
	//public float xpPlayer = 9f;

	// Use this for initialization
	void Awake () {

		if (SceneManager.GetActiveScene ().name == "StartMenu") {
			Destroy (this);
		} else {
			DontDestroyOnLoad (this);
		}
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}

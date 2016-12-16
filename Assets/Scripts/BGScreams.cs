using UnityEngine;
using System.Collections;

public class BGScreams : MonoBehaviour {

	public AudioSource scream1;
	public AudioSource scream2;
	public AudioSource scream3;
	public AudioSource scream4;
	public AudioSource scream5;

	private float randomScreamFloat;

	// Use this for initialization
	void Start () {

		StartCoroutine (waitScream ());
	
	}
	
	// Update is called once per frame
	void Update () {

		randomScreamFloat = Random.Range (0f, 5f);
	
	}

	IEnumerator waitScream(){
	
		yield return new WaitForSeconds (Random.Range(2f, 14f));

		if (randomScreamFloat < 1f) {

			scream1.Play ();
			
		} else if (randomScreamFloat >= 1f && randomScreamFloat < 2f) {

			scream2.Play ();

		} else if (randomScreamFloat >= 2f && randomScreamFloat < 3f) {
			scream3.Play ();

		} else if (randomScreamFloat >= 3f && randomScreamFloat < 4f) {

			scream4.Play ();
		} else {
		
			scream5.Play ();
		
		}

		StartCoroutine (waitScream ());
	
	}
}

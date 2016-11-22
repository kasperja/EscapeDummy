using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Animator spaceTextAnim;
	public AudioSource startGameSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
		
			spaceTextAnim.SetBool ("SpaceBool", true);
			startGameSound.Play ();
			StartCoroutine (waitAndLoad ());
		
		}
	
	}
	IEnumerator waitAndLoad(){
	
		yield return new WaitForSeconds (0.7f);

		SceneManager.LoadScene (1);
	
	}
}

using UnityEngine;
using System.Collections;

public class PlayerDetect : MonoBehaviour {

	public AudioSource DangerSound;
	private bool dangerSoundPlayOnce = true;
	public EnemyMovement Em;

	void OnTriggerStay2D(Collider2D collider){

		if (collider.tag == "Player") {
			
			Em.isFollowing = true;
			dangerSoundPlayOnce = false;

		}
	}
		void OnTriggerEnter2D(Collider2D collider){

		if (collider.tag == "Player") {

			if (dangerSoundPlayOnce) {
				DangerSound.Play ();
				dangerSoundPlayOnce = false;
				StartCoroutine (WaitForSound (10.0f));
			}
			Em.isFollowing = true;

		}
	}
			void OnTriggerExit2D(Collider2D collider){

		if (collider.tag == "Player") {

			Em.isFollowing = false;
			dangerSoundPlayOnce = false;
			StartCoroutine (WaitForSound (10.0f));

		}
	}

	IEnumerator WaitForSound(float waitTime){
	
		yield return new WaitForSeconds (waitTime);
		dangerSoundPlayOnce = true;

	
	}

}

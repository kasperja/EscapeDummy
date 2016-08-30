using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float speed = 10.0f;
	public float followSpeed = 20f;
	float useSpeed;
	float origX;
	public float moveDistance = 15.0f;
	private float moveDistMax;
	private float moveDistMin;
	public bool isFollowing = false;
	public bool isInRange = false;
	public GameObject player;
	//private Vector3 flipH = new Vector3(-1f,0f,0f);
	private Vector3 flipHVector;
	private Vector3 flipHVectorL;
	private bool hitL = false;
	private bool hitR = true;
	private bool flipDirOnce;
	//private float flipHFloat;

	void Start(){

		//flipHFloat = gameObject.transform.localScale.x;

		flipHVector = new Vector3(-gameObject.transform.localScale.x * -1f, gameObject.transform.localScale.y,gameObject.transform.localScale.z);
		flipHVectorL = new Vector3(gameObject.transform.localScale.x * -1f, gameObject.transform.localScale.y,gameObject.transform.localScale.z);

		origX = transform.position.x;
		moveDistMax = transform.position.x - moveDistance;
		moveDistMin = transform.position.x + moveDistance;

		useSpeed = -speed;
		gameObject.transform.localScale = flipHVectorL;
		//this.transform.Translate (new Vector3 (-15.0f, transform.position.y, 19.0f));
	
	}

	// Update is called once per frame
	void Update () {
	
		if (player.gameObject.activeInHierarchy == false) {
			
			flipDirOnce = true;
			StartCoroutine (Waiter ());



		} 
			

		if (isFollowing == false && isInRange == false) {
			
			if (transform.position.x <= moveDistMax) {
				
				hitR = false;
				hitL = true;

				//useSpeed = speed;

				//gameObject.transform.localScale = flipHVectorL;
		
			} 

			if (transform.position.x >= moveDistMin) {
		
				//useSpeed = -speed;

				hitR = true;
				hitL = false;

				//gameObject.transform.localScale = flipHVector;

			}
				
			if (hitR) {
					//Debug.Log ("hej");
					useSpeed = -speed;
					gameObject.transform.localScale = flipHVectorL;

			} 
			if (hitL) {
					//Debug.Log ("hej2");
					useSpeed = -speed;
					gameObject.transform.localScale = flipHVector;
				
			}


			

			
			transform.Translate (useSpeed * Time.deltaTime, 0, 0);

		} else if (isFollowing == true && isInRange == false && player.gameObject.activeInHierarchy) {

			if (transform.position.x > player.transform.position.x) {

				useSpeed = -followSpeed;

				gameObject.transform.localScale = flipHVectorL;


			} else if (transform.position.x < player.transform.position.x) {

				useSpeed = -followSpeed;

				gameObject.transform.localScale = flipHVector;

			} else {

				useSpeed = -followSpeed;

				if (flipDirOnce) {
					Debug.Log ("wee");
					flipDirOnce = false;
					gameObject.transform.localScale = flipHVector;

				}

			}
			transform.Translate (useSpeed * Time.deltaTime, 0, 0);

		} else if (isInRange && player.gameObject.activeInHierarchy) {
		
			useSpeed = 0.0f;
		
		}
	}

	IEnumerator Waiter(){

		yield return new WaitForSeconds (0.0f);
		isFollowing = false;
		isInRange = false;

	}





}

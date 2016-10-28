using UnityEngine;
using System.Collections;

public class FollowXShadow : MonoBehaviour {

	public GameObject target;
	public GameObject stairYPosObj;
	public float XOffset = 0.0f;
	public float YOffset= 10.0f;
	public PlatformerCharacter2D pc2dScript;

	public bool isJumping = false;
	public bool jumpOnce = true;

	public float origY;
	public float stairY;
	//public string objName;
	// Use this for initialization
	void Start () {

		origY = transform.position.y;
		// objName = target.ToString();
		stairY = stairYPosObj.transform.position.y;

	}

	// Update is called once per frame
	void Update () {


		/*if (GameObject.Find(objName) == null) {
		
			gameObject.SetActive (true);*/
		if (pc2dScript.scaleCharBool) {
			
			if (pc2dScript.sideArrowsBool) {
				iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, Mathf.Lerp (gameObject.transform.position.y, origY, 0.1f), transform.position.z), 0.2f);
			} else {
			
				iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, gameObject.transform.position.y, transform.position.z), 0.2f);


			}

				//transform.position = new Vector3 (target.transform.position.x + XOffset, origY, transform.position.z);
		} else {



			if (!pc2dScript.m_Grounded) {

				if (pc2dScript.sideArrowsBool) {

					iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, Mathf.Lerp (gameObject.transform.position.y, target.transform.position.y + YOffset, 0.2f), transform.position.z), 0.2f);

				} else {

					iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, stairY, transform.position.z), 0.2f);

				}

				isJumping = true;


			
			}else{

				if (pc2dScript.sideArrowsBool) {
					iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, Mathf.Lerp (gameObject.transform.position.y, target.transform.position.y + YOffset, 0.6f), transform.position.z), 0.3f);
				} else {
				
					iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, gameObject.transform.position.y, transform.position.z), 0.3f);
				}

					//transform.position = new Vector3 (target.transform.position.x + XOffset, target.transform.position.y + YOffset, transform.position.z);
			}
		}
		/*} else {
		
			gameObject.SetActive (false);
		}*/

	}
	IEnumerator waitJump(){
	


		yield return new WaitForSeconds (1f);

		isJumping = false;
		jumpOnce = true;

	
	}
}

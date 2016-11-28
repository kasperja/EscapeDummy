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

	private Color origColor;
	private Color fadeOutColor;
	private Color tempColor;

	public SpriteRenderer targetSpriteRenderer;

	public float origY;
	public float stairY;

	public float maximum = 1f;
	public float minimum = 0f;
	static float t = 0.0f;
	//public string objName;
	// Use this for initialization
	void Start () {

		origY = transform.position.y;
		// objName = target.ToString();
		stairY = stairYPosObj.transform.position.y;
		origColor = gameObject.GetComponent<SpriteRenderer> ().color;
		fadeOutColor = new Color (gameObject.GetComponent<SpriteRenderer> ().color.r, gameObject.GetComponent<SpriteRenderer> ().color.g, gameObject.GetComponent<SpriteRenderer> ().color.b, 0.0f);

	}

	// Update is called once per frame
	void Update () {

		gameObject.GetComponent<SpriteRenderer> ().color = tempColor;

		/*if (GameObject.Find(objName) == null) {
		
			gameObject.SetActive (true);*/
		if (pc2dScript.scaleCharBool) {


			if (tempColor.a >= 0f && tempColor.a < origColor.a) {
				tempColor.a += 2f * Time.deltaTime;
			} else {
			
				tempColor.a = origColor.a;
			
			}


			if (pc2dScript.sideArrowsBool) {
				iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, Mathf.Lerp (gameObject.transform.position.y, origY, 0.2f), transform.position.z), 0.2f);
			} else {
			
				iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, gameObject.transform.position.y, transform.position.z), 0.2f);


			}


				//transform.position = new Vector3 (target.transform.position.x + XOffset, origY, transform.position.z);
		} else {

			if (tempColor.a > 0f && tempColor.a <= origColor.a) {
				tempColor.a -= 2f * Time.deltaTime;
			} else {
			
				tempColor.a = 0f;
			
			}

			if (!pc2dScript.m_Grounded) {

				if (pc2dScript.sideArrowsBool) {

					iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, Mathf.Lerp (gameObject.transform.position.y, origY, 0.2f), transform.position.z), 0.2f);

				} else {

					iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, origY, transform.position.z), 0.2f);

				}

				isJumping = true;


			
			}else{
				

				if (pc2dScript.sideArrowsBool) {
					iTween.MoveUpdate (gameObject, new Vector3 (target.transform.position.x + XOffset, Mathf.Lerp (gameObject.transform.position.y, origY, 0.6f), transform.position.z), 0.3f);
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

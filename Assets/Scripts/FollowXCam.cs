using UnityEngine;
using System.Collections;

public class FollowXCam : MonoBehaviour {

	public GameObject target;
	public float XOffset = 0.0f;

	//public string objName;
	// Use this for initialization
	void Start () {

		// objName = target.ToString();

	}

	// Update is called once per frame
	void Update () {


		/*if (GameObject.Find(objName) == null) {
		
			gameObject.SetActive (true);*/
		transform.position = new Vector3 (target.transform.position.x + XOffset, transform.position.y, transform.position.z);






		/*} else {
		
			gameObject.SetActive (false);
		}*/

	}
}
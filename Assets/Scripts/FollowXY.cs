using UnityEngine;
using System.Collections;

public class FollowXY : MonoBehaviour {

	public GameObject target;
	public float XOffset = 0.0f;
	public float YOffset = 0.0f;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (target.transform.position.x +XOffset, target.transform.position.y + YOffset, transform.position.z);
	
	}
}

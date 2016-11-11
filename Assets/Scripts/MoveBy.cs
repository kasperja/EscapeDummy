using UnityEngine;
using System.Collections;

public class MoveBy : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
		iTween.MoveBy (gameObject, new Vector3 (0f, -5f, 0f), 1f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

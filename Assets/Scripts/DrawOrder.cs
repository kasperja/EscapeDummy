using UnityEngine;
using System.Collections;

public class DrawOrder : MonoBehaviour {

	public int sortingLayer = 10050;
	// Use this for initialization
	void Start () {
	
		gameObject.GetComponent<MeshRenderer> ().sortingOrder = sortingLayer;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

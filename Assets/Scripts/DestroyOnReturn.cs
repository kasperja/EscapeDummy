﻿using UnityEngine;
using System.Collections;

public class DestroyOnReturn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Return)) {

			Destroy(gameObject);


		}
	
	}
}

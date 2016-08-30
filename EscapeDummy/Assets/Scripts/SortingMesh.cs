using UnityEngine;
using System.Collections;

public class SortingMesh : MonoBehaviour {

    public int sortOrder = 0;

	// Use this for initialization
	void Start () {

        this.GetComponent<MeshRenderer>().sortingLayerName = "Default";
        this.GetComponent<MeshRenderer>().sortingOrder = sortOrder;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

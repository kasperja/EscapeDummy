using UnityEngine;
using System.Collections;

public class SortingSkinnedMesh : MonoBehaviour {

    public int sortOrder = 0;

    // Use this for initialization
    void Start () {

        this.GetComponent<SkinnedMeshRenderer>().sortingLayerName = "Default";
        this.GetComponent<SkinnedMeshRenderer>().sortingOrder = sortOrder;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

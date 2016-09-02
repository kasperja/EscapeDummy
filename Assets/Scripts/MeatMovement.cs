using UnityEngine;
using System.Collections;

public class MeatMovement : MonoBehaviour
{

    public GameObject StartTrigger;
    public GameObject EndTrigger;
    public float meatSpeed;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

       // this.transform.Translate(Vector3.left * Time.deltaTime * meatSpeed);

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("COL!");


			
			this.gameObject.transform.position = new Vector3 (StartTrigger.transform.position.x, transform.position.y, transform.position.z);

    }


}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndOutside : MonoBehaviour {

	public PlatformerCharacter2D pc2D;
	public Animator m_Anim;
	public Animator endFadeAnim;
	public GameObject mainCam;
	public GameObject endCamTarget;
	public float waitBeforeEnd = 2f;
	public GameObject endFadeLayer;
	// Use this for initialization
	void Start () {
	



	}
	
	// Update is called once per frame
	void Update () {
	
		if (pc2D.isEndOutside) {
		
			pc2D.sideArrowsBool = true;
			m_Anim.SetBool ("SideArrows", true);
		
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
	
		if (other.gameObject.tag == "Player") {
		
			pc2D.isEndOutside = true;
			pc2D.endCamBool = true;
			StartCoroutine (waitEnd());

		
		}
	
	
	}

	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player") {

			pc2D.isEndOutside = true;
			pc2D.endCamBool = true;
			StartCoroutine (waitEnd());


		}


	}

	IEnumerator waitEnd(){
		
		yield return new WaitForSeconds (waitBeforeEnd);

		endFadeAnim.SetBool ("FadeEnd", true);

		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (0);
	
	}
}

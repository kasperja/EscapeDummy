using UnityEngine;
using System.Collections;

public class StartOutside : MonoBehaviour {

	public PlatformerCharacter2D pc2D;
	public Animator m_Anim;

	public GameObject mainCam;
	public GameObject boxColStart;
	public AudioSource footStepGravel;
	public AudioSource runBreath;
	private bool hasReachedMax = false;
	private bool hasReachedMaxBreath = false;
	private float breathMax;


	// Use this for initialization
	void Start () {

		breathMax = runBreath.volume;

		footStepGravel.volume = 0f;
		runBreath.volume = 0f;

		pc2D.isStartOutside = true;
		pc2D.startCamBool = true;

	}

	// Update is called once per frame
	void Update () {

		if (footStepGravel.volume < 0.2f && !hasReachedMax) {
		
			footStepGravel.volume += 0.1f * Time.deltaTime;

		
		} else {
		
			hasReachedMax = true;

		}

		if (runBreath.volume < breathMax && !hasReachedMaxBreath) {


			runBreath.volume += 0.1f * Time.deltaTime;

		} else {

			hasReachedMaxBreath = true;

		}

		if (pc2D.isStartOutside) {

			pc2D.sideArrowsBool = true;
			m_Anim.SetBool ("SideArrows", true);

		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player") {

			pc2D.isStartOutside = true;
			pc2D.startCamBool = true;
		}


	}

	void OnTriggerExit2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player") {

			pc2D.isStartOutside = false;

			boxColStart.SetActive (true);
		}


	}

	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player") {


			pc2D.isStartOutside = true;
			pc2D.startCamBool = true;

		}


	}


}

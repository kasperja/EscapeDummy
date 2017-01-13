using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour {

	private static DontDestroyOnLoad instance = null;
	public AudioSource ambientOutside;
	public bool isEndOutside = false;
	private float volumeBirds;
	public static DontDestroyOnLoad Instance{

		get { return instance; }

	}
	void Awake () {

		if (instance != null && instance != this) {
		
			ambientOutside.Stop ();
		
			if (instance.ambientOutside.clip != ambientOutside.clip) {
			
				instance.ambientOutside.clip = ambientOutside.clip;
				instance.ambientOutside.volume = ambientOutside.volume;

				instance.ambientOutside.Play ();
			
			}

			Destroy (this.gameObject);
			return;
		
		}

		instance = this;
		ambientOutside.Play ();

		//if(SceneManager.GetActiveScene().name == "KasperChanges")
			DontDestroyOnLoad (this.gameObject);
	//	if(SceneManager.GetActiveScene().name == "StartMenu")DontDestroyOnLoad (this.gameObject);
		//if(SceneManager.GetActiveScene().name == "DummyOutside")DontDestroyOnLoad (this.gameObject);



	}
	// Use this for initialization
	void Start () {

		volumeBirds = ambientOutside.volume;

		if (SceneManager.GetActiveScene ().name == "KasperChanges") {

			GameObject door = GameObject.Find("DoorAbattoir");
			door.GetComponent<DoorAbattoir> ().ambientOutside = ambientOutside;

		}


	
	}
	
	// Update is called once per frame
	void Update () {

		if (SceneManager.GetActiveScene ().name == "StartMenu") {

			ambientOutside.volume -= 1f * Time.deltaTime;


		}


	
	}
}

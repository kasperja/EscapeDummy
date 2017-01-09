using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	private static DontDestroyOnLoad instance = null;
	public AudioSource ambientOutside;
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

		DontDestroyOnLoad (this.gameObject);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

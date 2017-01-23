using System;
using UnityEngine;
using System.Collections;

//namespace UnityStandardAssets._2D
//{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] public float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
	public Animator m_AnimHooked;
        private Rigidbody2D m_Rigidbody2D;
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.
		public ComboManager cm;
		public HitpointsPlayerTotal hpPlayerTotal;
		public bool m_Attack1 = false;
		public bool m_Attack2 = false;
		public bool m_Attack3 = false;
		public bool m_Attack4 = false;
		public bool m_Attack5 = false;

	public float vSpeed = 0;

		public bool playOnce1 = true;
		public bool playOnce2 = true;
		public bool playOnce3 = true;
		public bool playOnce4 = true;
		public bool playOnce5 = true;

		private float animWaitTime = 0.1f;

	public HingeJoint2D tempHinge;
	public GameObject hook;
	public GameObject hookParent;
	public bool hooked = false;
	private bool hookOnce = true;
	public GameObject graphicsHooked;
	public GameObject graphicsNorm;
	public GameObject MainCharObj;
	public Transform charPos;
	public Transform camTarget;
	public GameObject anchor;

	public bool sideArrowsBool = false;
	private float timer = 0.0f;
	public float friction = 0.9f;
	private bool jumpOnce = true;
	private bool jumpOnceTwo = true;
	public GameObject WaitForHookedCol;
	public GameObject WaitForHookedCol2;

	private bool spaceBoolTwo = false;

	private bool anticiHook = true;

	public Vector2 direction;

	public bool climbingStairsBool = false;

	public bool endCamBool = false;
	public bool endCamBackBool = false;

	public bool startCamBool = false;
	public bool startCamBackBool = false;

	private float scale = 5f;
	private float minScale = 4.6f;
	private float maxScale = 5f;
	private float scaleSpeed = -40f;
	public bool scaleCharBool = false;
	public bool scaleCharBackBool = false;


	public AudioSource sawSound;
	private Vector3 posMoveBack;
	private Vector3 posMoveForward;

	public float moveBackAmmount = 0.25f;
	//public GameObject groundColObj;

	public FollowXCam fx;


	public GameObject stairColsObj;
	public Transform stairPosUp;
	public Transform stairPosDown;

	private bool moveColUpBool = false;
	private bool moveColDownBool = false;

	private bool climbingStairsBoolCol = false;

	public Transform[] wayPointArray;
	private float percentsPerSecond = 1.2f;
	private float currentPathPercent = 0.0f;

	public bool hookJumpActive = false;
	public bool hookJumpActiveOnce = true;
	public bool hookJumpActiveOnceTween = true;

	public SawMover sawMoverScript;

	public bool hookStandingStill = false;

	public AudioSource attackSound1;
	public AudioSource attackGrunt1;
	public AudioSource attackGrunt2;
	public AudioSource attackGrunt3;
	public float randomGruntFloat;

	private bool attackSoundOnce = true;

	private bool lookUpOnce = true;
	private bool hookedLookUp = false;

	public bool blinkOnce = true;

	public bool blink2Once = true;


	private bool runLookOnce = true;

	private bool lookUpBool = false;

	private float t = 0.0f;
	private float maxLerp = 400f;
	private float minLerp = 0f;

	private float waitJumpTime = 0.0f;

	public float velocity = 0f;

	public bool attackDone = true;

	public ParticleSystem landingParticle;

	public bool isEndOutside = false;
	public bool isStartOutside = false;

	public ParticleSystem runParticle;

	public AudioSource idleSawSound;
	public AudioSource cutSawSound;
	public Transform sawPos;

	public bool isFootstepIndoor = true;
	public AudioSource footstepIndoor;
	public AudioSource footstepGravel;

	public AudioSource runBreath;

	public FadeSawSoundsIn fadeSoundInSawScript;
	public FadeSawSoundsIn fadeSoundInSawScriptIdle;
	public FadeSawSoundsIn fadeSoundInSawScriptControllerIdle;
	public FadeSawSoundsIn fadeSoundInSawScriptControllerActive;

	public bool musicIntro = false;
	public bool musicBreakDown = false;
	public bool musicFight = false;
	public bool musicDeath = false;

	public MusicController musicScript;
	public float musicVolumeIntro;
	public float musicVolumeBreakDown;
	public float musicVolumeFight;
	public float musicVolumeDeath;

	public bool camSaw = false;

	public bool canHook = false;

	public DoorAbattoir doorScript;

	public Animator animWorkingButcher;
	public AudioSource wtfSound;

	public bool onStairsBool = false;
	public bool onStairsSoundOnce = true;
	public AudioSource longStopSound;
	public bool enemyIsDead = false;

	private bool breakOnce = true;

	public AudioSource landingSound;
	private bool landOnce = true;
	//public AudioSource pickupHook;

	private bool isJumpingFalseOnce = true;

	public GameObject box1;
	public GameObject box2;
	public GameObject box3;
	public AudioSource boxBumpSound;

	private float gravOrig;

	public GameObject runTrigger;

	public GameObject groundTrigger;

	private float gravSmall = 38f;

	public GameObject boxWall;
        private void Awake()
        {
			Application.targetFrameRate = 900;
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

		//tempHinge = gameObject.GetComponent<FixedJoint2D> ();
        }

	private void Start()
	{

		gravOrig = gameObject.GetComponent<Rigidbody2D> ().gravityScale;
		
		musicVolumeIntro = musicScript.introMusic.volume;
		musicVolumeBreakDown = musicScript.introMusic.volume;
		musicVolumeFight = musicScript.introMusic.volume;
		musicVolumeDeath = musicScript.introMusic.volume;

		musicScript.introMusic.volume = 0f;
		//musicScript.breakdownMusic.volume = 0f;
		musicScript.fightMusic.volume = 0f;
		//musicScript.deathMusic.volume = 0f;


		if (isStartOutside) {
		
			m_Anim.SetBool ("outsideStart", true);
		
		}
		posMoveBack = new Vector3 (graphicsNorm.transform.localPosition.x, 
			graphicsNorm.transform.localPosition.y + moveBackAmmount, graphicsNorm.transform.localPosition.z);

		posMoveForward = new Vector3 (graphicsNorm.transform.localPosition.x, 
			graphicsNorm.transform.localPosition.y, graphicsNorm.transform.localPosition.z);




	}
		private void Update(){

		if (m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Falling") || m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Landing") || m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("PlayerJump")) {

			groundTrigger.SetActive (true);

		} else {
		
			groundTrigger.SetActive (false);
		
		
		}


		if (vSpeed < -0.0001f) {
			m_AirControl = false;
		} else {
		
			m_AirControl = true;
		
		}

		if (sideArrowsBool && !climbingStairsBool) {
		
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = gravSmall;
		
		}else if(!sideArrowsBool && !climbingStairsBool){

			gameObject.GetComponent<Rigidbody2D> ().gravityScale = gravSmall;

		}

		if (hookJumpActive && !m_FacingRight)
			Flip ();

		if (climbingStairsBool) {
			m_Anim.SetBool ("Ground", true);
			m_Grounded = true;
			m_Anim.SetBool ("Climb", true);
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 50f;


		} else {
			m_Anim.SetBool ("Climb", false);

			if (sideArrowsBool) {

				gameObject.GetComponent<Rigidbody2D> ().gravityScale = gravOrig;
			} else {
			
				gameObject.GetComponent<Rigidbody2D> ().gravityScale = gravSmall;
			
			}
		}

		if (vSpeed > -0.0001f && vSpeed < 0.0001f && !spaceBoolTwo && !sideArrowsBool) {

			//StartCoroutine (waitAndGround(1f));
			//m_Grounded = true;
		}

		vSpeed = m_Rigidbody2D.velocity.y;

		if (m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Falling") || m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Landing")) {
		
			m_Anim.SetBool ("isFalling", true);
		
		
		} else {
		
			m_Anim.SetBool ("isFalling", false);

		}

		if (m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("StartJump") ||
		    m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("PlayerJump") ||
		    m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Falling") ||
		    m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("Landing") || 
			m_Anim.GetCurrentAnimatorStateInfo (0).IsName ("PlayerJumpGrab")
		
		) {
		
			m_Anim.SetBool ("isJumping", true);
		
		} else if(isJumpingFalseOnce && !endCamBool){
		
			if(!endCamBool)StartCoroutine (waitBeforeFallingActive (0.02f));

			isJumpingFalseOnce = false;
		
		} else{

			m_Anim.SetBool ("isJumping", true);


		}



		randomGruntFloat = UnityEngine.Random.Range (0f, 5f);

		if (doorScript.doorOpen) {
			
			musicIntro = false;
		
		}

		if (musicIntro && !doorScript.doorOpen) {

			FadeInMusic (musicScript.introMusic, musicVolumeIntro);

		} else if(!musicIntro){
		
			FadeOutMusic (musicScript.introMusic, musicVolumeIntro);
		
		}

		if (sawPos.position.x >= transform.position.x) {
		
			if(idleSawSound.panStereo < 1f){
				idleSawSound.panStereo += 0.3f * Time.deltaTime;
				cutSawSound.panStereo += 0.3f * Time.deltaTime;
			}

		} else {
			if (idleSawSound.panStereo > -1f) {
				
				idleSawSound.panStereo -= 0.3f * Time.deltaTime;
				cutSawSound.panStereo -= 0.3f * Time.deltaTime;

			}
		}
		
		if (Input.GetKeyDown (KeyCode.Space)  && !hpPlayerTotal.isDead && m_Grounded && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack5") 
			&& !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("StartJump") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Falling") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Landing")) {
			landOnce = true;


			StartCoroutine (waitSpaceBool(0.2f));
		
		} else {
		

		
		}

		if(Input.GetKey(KeyCode.Space)){

			spaceBoolTwo = true;

		}else{

			spaceBoolTwo = false;

		}
			





		if (hookedLookUp && lookUpOnce && !sideArrowsBool  && !hpPlayerTotal.isDead) {
		
			m_Anim.SetBool ("LookUpBool", true);

			StartCoroutine (waitLookUp(4f));


		
		} else {
		
			m_Anim.SetBool ("LookUpBool", false);
		
		
		}

		if (m_Grounded && sideArrowsBool && runLookOnce  && !hpPlayerTotal.isDead) {

			StartCoroutine (waitRunLookBack ());

			runLookOnce = false;


		}

		if (m_Grounded && !sideArrowsBool && blinkOnce  && !hpPlayerTotal.isDead) {
		

			float rdm = UnityEngine.Random.Range (1f, 2f);

			if (rdm < 1.5f) {
				
				StartCoroutine (waitBlink ());

			} else {
			
				StartCoroutine (waitBlinkNorm ());
			
			}

			blinkOnce = false;
		
		}


		if (canHook && hooked && Input.GetKey(KeyCode.Space) && sawMoverScript.hookDetected && hookJumpActiveOnce && hookStandingStill && !hpPlayerTotal.isDead && m_Grounded) {

			if(!m_FacingRight) Flip();
			StartCoroutine (waitActiveHook ());


		} 

		if (hookJumpActive && !hpPlayerTotal.isDead) {





			if (hookJumpActiveOnce) {

				StartCoroutine (stopOnHook (3f));
				StartCoroutine (waitHook (3f));

				hookJumpActiveOnce = false;

			}
			//currentPathPercent += percentsPerSecond * Time.deltaTime;

			//sawMoverScript.gameObject.GetComponent<CircleCollider2D> ().enabled = false;


			if (anticiHook) {


			
			} else {

				//percentsPerSecond += 0f * Time.deltaTime;

				currentPathPercent += percentsPerSecond * Time.deltaTime;

				iTween.PutOnPath (gameObject, wayPointArray, currentPathPercent);

			}


			
			//iTween.PutOnPath (gameObject, wayPointArray, currentPathPercent);




			//Debug.Log ("hi");
			WaitForHookedCol.SetActive (false);
			WaitForHookedCol2.SetActive (false);





		
		}
			

		if (scaleCharBool) {

			//stairColsObj.transform.position = stairPosUp.position;
			scale -= scaleSpeed * Time.deltaTime;



			if (scale >= maxScale) {
			
				scale = maxScale;
			
			}

			if (scale <= minScale) {

				scale = minScale;
			
			}

			if (sideArrowsBool) {
				graphicsNorm.transform.localScale = new Vector3 (scale, scale, 1f);

				graphicsNorm.transform.localPosition = Vector3.Lerp (graphicsNorm.transform.localPosition, posMoveForward, 2f * Time.deltaTime);


			} else {
				

			
			}
			//friction = 1f;
		
		}

		if (climbingStairsBoolCol && Input.GetKey(KeyCode.RightArrow)) {
			
			// if (sideArrowsBool) {
			m_Grounded = true;
				stairColsObj.transform.localPosition = Vector3.Lerp (stairColsObj.transform.localPosition, stairPosUp.localPosition, 5f * Time.deltaTime);
			// }
		}

		else if (climbingStairsBoolCol && Input.GetKey(KeyCode.LeftArrow)) {
			
				//if (sideArrowsBool) {
			m_Grounded = true;
					stairColsObj.transform.localPosition = Vector3.Lerp (stairColsObj.transform.localPosition, stairPosDown.localPosition, 5f * Time.deltaTime);
				//}
		}

		if (scaleCharBackBool) {

			//stairColsObj.transform.position = stairPosDown.position;
			scale += scaleSpeed * Time.deltaTime;

			if (scale >= maxScale) {

				scale = maxScale;

			}

			if (scale <= minScale) {

				scale = minScale;

			}

			if (sideArrowsBool) {
				graphicsNorm.transform.localScale = new Vector3 (scale, scale, 1f);

				graphicsNorm.transform.localPosition = Vector3.Lerp (graphicsNorm.transform.localPosition, posMoveBack, 2f * Time.deltaTime);
			
				//stairColsObj.transform.localPosition = Vector3.Lerp (stairColsObj.transform.localPosition, stairPosDown.localPosition, 2f * Time.deltaTime);
			
			}
			//friction = 1f;
		


		}
		if (m_Grounded) {
			
			m_Anim.SetBool ("JumpKickToJump", false);

		}
				
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow) || isStartOutside || isEndOutside) {

			sideArrowsBool = true;
			m_Anim.SetBool ("SideArrows", true);


		} else {


			if(!isEndOutside)sideArrowsBool = false;
			if(!isEndOutside)m_Anim.SetBool ("SideArrows", false);
			if(!isStartOutside)sideArrowsBool = false;
			if(!isStartOutside)m_Anim.SetBool ("SideArrows", false);

		}

		/*if (cm.falconPunchBool && playOnce4 && !sideArrowsBool && m_Grounded && !hpPlayerTotal.isDead) {

			if(attackSoundOnce){

				attackSound1.Play ();
				attackSoundOnce = false;
			}
			m_Attack4 = true;
			playOnce4 = false;
			StartCoroutine (WaitForAnim4(0.5f));

		}*/ /*else if (Input.GetKeyDown (KeyCode.E) && playOnce1 && cm.falconPunchBool == false && !sideArrowsBool && m_Grounded && !hpPlayerTotal.isDead) {

			if(attackSoundOnce){

				attackSound1.Play ();
				attackSoundOnce = false;
			}
				m_Attack1 = true;
				playOnce1 = false;
			StartCoroutine (WaitForAnim1(0.5f));

			}
		else if (Input.GetKeyDown (KeyCode.R) && playOnce2 && cm.falconPunchBool == false && !sideArrowsBool && m_Grounded && !hpPlayerTotal.isDead) {

			if(attackSoundOnce){

				attackSound1.Play ();
				attackSoundOnce = false;
			}
				m_Attack2 = true;
				playOnce2 = false;
			StartCoroutine (WaitForAnim2(0.5f));

			}*/
	if ((m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle Lookup") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleBlink3-4") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleBlinkLookback") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) && !m_Anim.GetBool("SpaceBool") && Input.GetKeyDown (KeyCode.W) && playOnce3 && cm.falconPunchBool == false && !sideArrowsBool && m_Grounded && !hpPlayerTotal.isDead && attackDone) {

			if(attackSoundOnce){

				attackSound1.Play ();
				
				if (randomGruntFloat >= 0f && randomGruntFloat < 1f) {

				attackGrunt1.Play ();

				} else if (randomGruntFloat >= 1f && randomGruntFloat < 2f) {

				attackGrunt2.Play ();

				} else if (randomGruntFloat >= 2f && randomGruntFloat < 3f) {

				attackGrunt3.Play ();

				} else if (randomGruntFloat >= 3f && randomGruntFloat < 4f) {



				} else {

			
				}



				//attackGrunt1.Play ();


				attackSoundOnce = false;
			}
				m_Attack3 = true;
				playOnce3 = false;
			StartCoroutine (WaitForAnim3(0.1f));

		}
	else if ((m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle Lookup") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleBlink3-4") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleBlinkLookback") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) && !m_Anim.GetBool("SpaceBool") && Input.GetKeyDown(KeyCode.E) && playOnce5 && cm.falconPunchBool == false && m_Grounded && !hpPlayerTotal.isDead && attackDone) {

			if(attackSoundOnce){
			
				attackGrunt1.Play ();
				attackSound1.Play ();
				attackSoundOnce = false;
			}
			m_Attack5 = true;
			playOnce5 = false;
			StartCoroutine (WaitForAnim5(0.1f));

		}

		/*else if ((Input.GetKeyDown (KeyCode.R) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown (KeyCode.W)) && playOnce5 && cm.falconPunchBool == false && !m_Grounded && !hpPlayerTotal.isDead) {

			if(attackSoundOnce){

				attackSound1.Play ();
				attackSoundOnce = false;
			}
			m_Attack5 = true;
			playOnce5 = false;
			StartCoroutine (WaitForAnim5(0.7f));

		}*/
			
		/*else if (!Input.GetKey (KeyCode.E) && !Input.GetKey (KeyCode.R) && !Input.GetKey (KeyCode.W) 
			&& cm.falconPunchBool == false) {
		
			m_Attack1 = false;
			m_Attack2 = false;
			m_Attack3 = false;
			m_Attack4 = false;
			m_Attack5 = false;
			attackSoundOnce = true;
		
		
		} */


		}

	IEnumerator WaitForAnim1(float animWaitTime){
			yield return new WaitForSeconds (animWaitTime);
				m_Attack1 = false;
			playOnce1 = true;
	attackSoundOnce = true;
				

		}
	IEnumerator WaitForAnim2(float animWaitTime){
			yield return new WaitForSeconds (animWaitTime);
			m_Attack2 = false;
			playOnce2 = true;
	attackSoundOnce = true;
		}

	IEnumerator WaitForAnim3(float animWaitTime){
			yield return new WaitForSeconds (animWaitTime);
			m_Attack3 = false;
		attackSoundOnce = true;
			//playOnce3 = true;
			//m_Grounded = true;
		}

	IEnumerator WaitForAnim4(float animWaitTime){
			yield return new WaitForSeconds (animWaitTime);
			cm.falconPunchBool = false;	
			m_Attack4 = false;
			playOnce4 = true;
	attackSoundOnce = true;
		}

	IEnumerator WaitForAnim5(float animWaitTime){
		yield return new WaitForSeconds (animWaitTime);
		m_Attack5 = false;
		//playOnce5 = true;
		//m_Grounded = true;
		m_Anim.SetBool ("JumpKickToJump", false);
	attackSoundOnce = true;

	}



        private void FixedUpdate()
        {
	

		/*if (!climbingStairsBool) {
			m_Grounded = false;
		}*/


            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
				if (colliders [i].gameObject != gameObject) {
					m_Grounded = true;
			landingParticle.Play ();
				if (!landingSound.isPlaying && landOnce) {
					landingSound.Play ();
					landOnce = false;
				}


				}

            }
			m_Anim.SetBool ("Attack1Bool", m_Attack1);
			m_Anim.SetBool ("Attack2Bool", m_Attack2);
			m_Anim.SetBool ("Attack3Bool", m_Attack3);
			m_Anim.SetBool ("Attack4Bool", m_Attack4);
			m_Anim.SetBool ("Attack5Bool", m_Attack5);
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
			m_Anim.SetFloat("Speed", Mathf.Abs(m_Rigidbody2D.velocity.x));


        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
	if (m_Grounded || m_AirControl || isEndOutside || isStartOutside)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                
			if (isFootstepIndoor) {

			if(!footstepIndoor.isPlaying && m_Grounded) footstepIndoor.Play();

			} else {

			if(!footstepGravel.isPlaying && m_Grounded) footstepGravel.Play();

			}

		if(!runBreath.isPlaying && m_Grounded) runBreath.Play();

		if (isEndOutside || isStartOutside) {

				move = 1f;

			} else {

			if (!scaleCharBool || climbingStairsBool) {
				runParticle.Stop ();
				} else {
				
				runParticle.Play ();
					
			
			}
				move = (crouch ? move * m_CrouchSpeed : move);
			}
                // The Speed animator parameter is set to the absolute value of the horizontal input.
               // m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character

			//if (m_Rigidbody2D.velocity.x < 60f && m_Rigidbody2D.velocity.x > -60f) {
				
				timer = 0.0f;



			if (climbingStairsBool) {
				
				m_Grounded = true;



				if(Input.GetKey(KeyCode.LeftArrow)){

					//down Stairs
				//m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed * 1f, move * m_MaxSpeed * 0.8f);
				m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
				m_Rigidbody2D.isKinematic = false;
				
				}else{

				if (isEndOutside || isStartOutside) {

						m_Rigidbody2D.velocity = new Vector2 (m_MaxSpeed * 1f, 0f);
						m_Rigidbody2D.isKinematic = false;
				
					} else {
						//up Stairs

						//m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed * 1f, move * m_MaxSpeed * 0.2f);
						m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
					m_Rigidbody2D.isKinematic = false;
					}
				}
			
			} else {
			
				m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.isKinematic = false;
			
			}
				//m_Rigidbody2D.AddForce (new Vector2 (0f, m_Rigidbody2D.velocity.y));
			//}
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
	if ((m_Grounded || (vSpeed < 0.0001f && vSpeed > -0.0001f)) && jump && m_Anim.GetBool("Ground") && !climbingStairsBool && !hookJumpActive  && !hpPlayerTotal.isDead && jumpOnce && !m_Attack3 && !m_Attack5)
            {
			if (!sideArrowsBool) {
			if (jumpOnce && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack5") && /*!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("StartJump") && */ !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Falling") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Landing")) {
					if (!isEndOutside) {
						if (randomGruntFloat >= 0f && randomGruntFloat < 1f) {

							attackGrunt1.Play ();

						} else if (randomGruntFloat >= 1f && randomGruntFloat < 2f) {

							attackGrunt2.Play ();

						} else if (randomGruntFloat >= 2f && randomGruntFloat < 3f) {

							attackGrunt3.Play ();

						} else if (randomGruntFloat >= 3f && randomGruntFloat < 4f) {

							attackGrunt2.Play ();


						} else {

							attackGrunt3.Play ();

						}
					}
				m_Anim.SetBool ("SpaceBool", true);
				StartCoroutine (waitSpaceBool(0.2f));
					// Add a vertical force to the player.
					m_Anim.SetBool ("StartJump", true);
					StartCoroutine (waitJump (waitJumpTime));
					jumpOnce = false;
				}
			} else {

			if (!climbingStairsBool && jumpOnce && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack5") && /*!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("StartJump") &&*/ !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Falling") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Landing")) {
					//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

					if (!isEndOutside) {
						if (randomGruntFloat >= 0f && randomGruntFloat < 1f) {

							attackGrunt1.Play ();

						} else if (randomGruntFloat >= 1f && randomGruntFloat < 2f) {

							attackGrunt2.Play ();

						} else if (randomGruntFloat >= 2f && randomGruntFloat < 3f) {

							attackGrunt2.Play ();

						} else if (randomGruntFloat >= 3f && randomGruntFloat < 4f) {

							attackGrunt1.Play ();

						} else {

							attackGrunt2.Play ();
						}
					}
					m_Anim.SetBool ("SpaceBool", true);
					m_Anim.SetBool ("StartJump", true);

				

					m_Grounded = false;

					m_Anim.SetBool ("Ground", false);

					//m_Anim.SetBool("StartJump", false);

					StartCoroutine (waitJump (0.0f));

					jumpOnce = false;

				}

			}
				

            }
        }

	public void Stop(float move, bool crouch, bool jump){

		//runBreath.Stop ();
		footstepIndoor.Stop ();
		footstepGravel.Stop ();

		if (!crouch && m_Anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		// Set whether or not the character is crouching in the animator
		m_Anim.SetBool("Crouch", crouch);

		//only control the player if grounded or airControl is turned on
	if (m_Grounded || m_AirControl || isEndOutside || isStartOutside)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
		if (isEndOutside || isStartOutside) {

				move = 1f;

			} else {
				runParticle.Stop ();
				move = (crouch ? move * m_CrouchSpeed : move);

			}
			// The Speed animator parameter is set to the absolute value of the horizontal input.
			// m_Anim.SetFloat("Speed", Mathf.Abs(move));

			// Move the character

			//if (m_Rigidbody2D.velocity.x < 60f && m_Rigidbody2D.velocity.x > -60f) {





			if (climbingStairsBool) {
				
				//m_Rigidbody2D.velocity = new Vector2 (20.4f, -21.5f);

			/*if(m_FacingRight)move = 1f;
			if(!m_FacingRight)move = -1f;
			m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed * 1f, m_Rigidbody2D.velocity.y);
			m_Anim.SetBool ("Climb", true);*/

			//m_Rigidbody2D.velocity = new Vector2 (Mathf.Lerp (m_Rigidbody2D.velocity.x, 0f, timer), m_Rigidbody2D.velocity.y);
			//m_Rigidbody2D.velocity = new Vector2 (m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
				m_Rigidbody2D.isKinematic = true;
			} else {


			if (isEndOutside || isStartOutside) {
					m_Rigidbody2D.isKinematic = false;
					m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed * 1f, 0f);

				} else {
				
					m_Rigidbody2D.velocity = new Vector2 (Mathf.Lerp (m_Rigidbody2D.velocity.x, 0f, timer), m_Rigidbody2D.velocity.y);
				//m_Rigidbody2D.velocity = new Vector2 (m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
					//m_Rigidbody2D.isKinematic = true;
				}
			}

			//m_Rigidbody2D.position = new Vector2 (Mathf.Lerp( m_Rigidbody2D.position.x , m_Rigidbody2D.position.x , timer), m_Rigidbody2D.position.y);
			timer += friction * Time.deltaTime; 


			//m_Rigidbody2D.AddForce (new Vector2 (0f, m_Rigidbody2D.velocity.y));
			//}
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
	if ((m_Grounded || (vSpeed < 0.0001f && vSpeed > -0.0001f)) && jump && m_Anim.GetBool("Ground") && !climbingStairsBool && !hookJumpActive && !hpPlayerTotal.isDead && jumpOnce && !m_Attack3 && !m_Attack5)
		{
			if (!sideArrowsBool) {
			if (jumpOnce && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack5") /* && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("StartJump") */ && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Falling") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Landing")) {

					if (!isEndOutside) {
						if (randomGruntFloat >= 0f && randomGruntFloat < 1f) {

							attackGrunt1.Play ();

						} else if (randomGruntFloat >= 1f && randomGruntFloat < 2f) {

							attackGrunt2.Play ();

						} else if (randomGruntFloat >= 2f && randomGruntFloat < 3f) {

							attackGrunt3.Play ();

						} else if (randomGruntFloat >= 3f && randomGruntFloat < 4f) {

							attackGrunt2.Play ();

						} else {

							attackGrunt1.Play ();

						}
					}
					m_Anim.SetBool ("SpaceBool", true);
					StartCoroutine (waitSpaceBool(0.2f));
						// Add a vertical force to the player.
					m_Anim.SetBool ("StartJump", false);
					m_Anim.SetBool("Ground", false);
					StartCoroutine (waitJump (waitJumpTime));
					jumpOnce = false;
				}
			} else {
			

				//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

			if (!climbingStairsBool && jumpOnce && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack5") /* && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("StartJump") */ && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Falling") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Landing")) {

					if (!isEndOutside) {
						if (randomGruntFloat >= 0f && randomGruntFloat < 1f) {

							attackGrunt1.Play ();

						} else if (randomGruntFloat >= 1f && randomGruntFloat < 2f) {

							attackGrunt2.Play ();

						} else if (randomGruntFloat >= 2f && randomGruntFloat < 3f) {

							attackGrunt2.Play ();

						} else if (randomGruntFloat >= 3f && randomGruntFloat < 4f) {

							attackGrunt3.Play ();

						} else {

							attackGrunt2.Play ();

						}
					}
					m_Anim.SetBool ("SpaceBool", true);
					m_Anim.SetBool ("StartJump", true);

					m_Grounded = false;

					m_Anim.SetBool ("Ground", false);

					m_Anim.SetBool ("StartJump", false);

					StartCoroutine (waitJump (0.0f));
					jumpOnce = false;


				}
					
			
			}

		}

	
	
	
	}



		void OnTriggerEnter2D(Collider2D other)
		{



	if (other.gameObject.tag == "RunTrigger") {

		//iTween.PunchScale (box1, new Vector3 (1f, -1f, 0f), 0.5f);

			isEndOutside = true;

	}


	if (other.gameObject.tag == "BoxTrigger") {

		//iTween.PunchScale (box1, new Vector3 (1f, -1f, 0f), 0.5f);

			boxWall.SetActive (true);

	}

	if (other.gameObject.tag == "BoxOne") {

		//iTween.PunchScale (box1, new Vector3 (1f, -1f, 0f), 0.5f);

			boxBumpSound.Play ();

	}
	if (other.gameObject.tag == "BoxTwo") {

		//iTween.PunchScale (box2, new Vector3 (1f, -1f, 0f), 0.5f);
		//iTween.PunchScale (box3, new Vector3 (0.5f, -0.5f, 0f), 0.5f);
		boxBumpSound.Play ();

	}

	if (other.gameObject.tag == "SoundTriggerSaw") {

			fadeSoundInSawScript.insideTrigger = true;
			fadeSoundInSawScriptIdle.insideTrigger = true;
		fadeSoundInSawScriptControllerIdle.insideTrigger = true;
		fadeSoundInSawScriptControllerActive.insideTrigger = true;
	}

	if (other.gameObject.tag == "WtfTrigger") {

			animWorkingButcher.SetBool ("Surprised", true);
			
			wtfSound.Play ();

	}
	if (other.gameObject.tag == "OnStairs") {

			onStairsBool = true;
		if (onStairsSoundOnce && !enemyIsDead){
				longStopSound.Play ();
			onStairsSoundOnce = false;
		}

	}

		/*if (other.gameObject.tag == "HookJoint") {

			if (hookOnce) {
				hooked = true;
				//tempHinge.connectedAnchor = anchor.gameObject.GetComponent<Rigidbody2D> ().position;
				graphicsNorm.SetActive (false);
				graphicsHooked.SetActive (true);
				other.GetComponent<CircleCollider2D> ().enabled = true;
				tempHinge.enabled = true;
				tempHinge.connectedAnchor = new Vector2 (-0f, -0.2f);
				tempHinge.connectedBody = other.GetComponent<Rigidbody2D> ();// hook.GetComponent<Rigidbody2D>();



				hookOnce = false;
			
			}





			}*/

		/*if (other.gameObject.tag == "WayPoint1Trigger") {
		
		
			wayPointBool1 = true;

		
		}*/
	if (other.gameObject.tag == "StopGrabTrigger") {
		hookJumpActiveOnce = false;
		hookJumpActive = false;
		m_Anim.SetBool ("Grab", false);
		WaitForHookedCol.SetActive (true);
		WaitForHookedCol2.SetActive (true);
		//m_Grounded = true;

	}
		if (other.gameObject.tag == "HookTrigger") {

			
		
			if (hookOnce) {
			
				hooked = true;
				hookOnce = false;
			
			}
		
		
		}

		if (other.gameObject.tag == "LookUpTrigger") {
		
			hookedLookUp = true;


		}
		if (other.gameObject.tag == "StairsTrigger") {

			m_Grounded = true;

			climbingStairsBool = true;

		}
		if (other.gameObject.tag == "TopStairTrigger") {

			

		}
		if (other.gameObject.tag == "StairsTriggerColScale") {

			//m_Grounded = true;
			//Debug.Log("HI");

			climbingStairsBoolCol = true;

		}
		if (other.gameObject.tag == "CamTargetEndTrigger") {




			endCamBool = true;

		}
if (other.gameObject.tag == "CamSawTrigger") {




	camSaw = true;

}
if (other.gameObject.tag == "IntroMusicTrigger") {

			if (!musicScript.introMusic.isPlaying)
				musicScript.introMusic.Play ();

	if(breakOnce) musicScript.breakdownMusic.Play ();
			musicIntro = true;
			breakOnce = false;

	}
		if (other.gameObject.tag == "CamTargetStartTrigger") {

			startCamBool = true;

		}

		if (other.gameObject.tag == "SawSound") {

			sawSound.volume = 0.4f;

		}
		if (other.gameObject.tag == "ScaleCharTrigger") {

			scaleCharBool = true;
			scaleCharBackBool = false;


		}

	
		}

	void OnTriggerExit2D(Collider2D other)
	{


	if (other.gameObject.tag == "BoxTriggerJump") {

		//iTween.PunchScale (box1, new Vector3 (1f, -1f, 0f), 0.5f);
		m_Anim.SetBool("BoxesBool",false);

	}

	if (other.gameObject.tag == "RunTrigger") {

		//iTween.PunchScale (box1, new Vector3 (1f, -1f, 0f), 0.5f);
		runTrigger.SetActive(false);

		isEndOutside = false;

	}

	if (other.gameObject.tag == "OnStairs") {

		onStairsBool = false;

	}

	if (other.gameObject.tag == "SoundTriggerSaw") {

		fadeSoundInSawScript.insideTrigger = false;
		fadeSoundInSawScriptIdle.insideTrigger = false;
		fadeSoundInSawScriptControllerIdle.insideTrigger = false;
		fadeSoundInSawScriptControllerActive.insideTrigger = false;
	}


	if (other.gameObject.tag == "HookTrigger") {

			hooked = false;
			hookJumpActive = false;
			hookedLookUp = false;

			//hookOnce = true;


	}
	if (other.gameObject.tag == "LookUpTrigger") {


		hookedLookUp = false;

		//hookOnce = true;


	}




		if (other.gameObject.tag == "StairsTrigger") {

			climbingStairsBool = false;

		}
		if (other.gameObject.tag == "TopStairTrigger") {

			

		}
		if (other.gameObject.tag == "StairsTriggerColScale") {

			//m_Grounded = true;


			climbingStairsBoolCol = false;

		}
		if (other.gameObject.tag == "CamTargetEndTrigger") {


			endCamBackBool = true;

			endCamBool = false;

			StartCoroutine (waitCamEnd (0.5f));

		}

		if (other.gameObject.tag == "CamTargetStartTrigger") {

		startCamBackBool = true;

		startCamBool = false;

		StartCoroutine (waitCamStart (0.5f));

		}

	if (other.gameObject.tag == "CamSawTrigger") {

			

		camSaw = false;

	}

		if (other.gameObject.tag == "IntroMusicTrigger") {
			//musicIntro = false;
		}


		if (other.gameObject.tag == "SawSound") {

			sawSound.volume = 0f;

		}
		if (other.gameObject.tag == "ScaleCharTrigger") {

			scaleCharBool = true;
			scaleCharBackBool = false;

		}

		/*if (other.gameObject.tag == "moveColTrigger") {

			moveColUpBool = false;
			scaleCharBackBool = true;

		}*/

	}

	void OnTriggerStay2D(Collider2D other)
	{
	if (other.gameObject.tag == "BoxTriggerJump") {

		//iTween.PunchScale (box1, new Vector3 (1f, -1f, 0f), 0.5f);
		m_Anim.SetBool ("BoxesBool", true);

	}

		if (other.gameObject.tag == "StairsTrigger") {

			m_Grounded = true;


			climbingStairsBool = true;

		}
		
	if (other.gameObject.tag == "HookTrigger") {

			hooked = true;



	}

	if (other.gameObject.tag == "LookUpTrigger") {

		hookedLookUp = true;



	}
		if (other.gameObject.tag == "StairsTriggerColScale") {

			//m_Grounded = true;


			climbingStairsBoolCol = true;

		}

		if (other.gameObject.tag == "CamTargetEndTrigger") {

		


			endCamBool = true;

		}

	if (other.gameObject.tag == "CamTargetStartTrigger") {




		startCamBool = true;

	}
		if (other.gameObject.tag == "ScaleCharTrigger") {
		
			scaleCharBool = false;
			scaleCharBackBool = true;
		
		}

		if (other.gameObject.tag == "TopStairTrigger") {

			

		}

	}

public void FadeInMusic(AudioSource musicSource, float musicVolume){

		// musicSource.Play ();

	if (musicSource.volume >= 0f && musicSource.volume < musicVolume) {

		musicSource.volume += 5f * Time.deltaTime;
		}

}
public void FadeOutMusic(AudioSource musicSource, float musicVolume){

	if (musicSource.volume > 0f) {
			
		//if(musicSource.volume > 1f) musicSource.volume = 1f;

		musicSource.volume -= 0.4f * Time.deltaTime;

		if (musicVolume <= 0.001f) {
			musicSource.Stop ();
		}

	}


}





        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
		fx.XOffset *= -1;
        }

	IEnumerator waitHook (float waitTime){
		


	yield return new WaitForSeconds (waitTime);
		
		hookJumpActiveOnce = false;
		hookJumpActive = false;
		m_Anim.SetBool ("Grab", false);
		WaitForHookedCol.SetActive (true);
		WaitForHookedCol2.SetActive (true);
		//m_Grounded = true;

	
	}

	IEnumerator waitLookUp (float waitTime){
	
		yield return new WaitForSeconds (2f);

		lookUpOnce = false;

		yield return new WaitForSeconds (waitTime);

		lookUpOnce = true;


	}
		

	IEnumerator waitJump (float waitTime){


		yield return new WaitForSeconds (waitTime);
		//hookParent.GetComponent<BoxCollider2D> ().enabled = true;
		//tempHinge.enabled = true;
		if (!climbingStairsBool) {
			m_Grounded = false;
		
			m_Anim.SetBool ("Ground", false);

		}

			m_Anim.SetBool ("StartJump", false);


	float jumpXforce = m_JumpForce;
		if (!m_FacingRight) {
			jumpXforce = -m_JumpForce;
		} else {

			jumpXforce = m_JumpForce;
	
	}

	m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);

		

	//m_Rigidbody2D.AddForce(new Vector2(jumpXforce /  1f ,0f));

		yield return new WaitForSeconds (0.1f);
		
		

		jumpOnce = true;

	}

	IEnumerator waitCamEnd(float waitTime){

		yield return new WaitForSeconds (waitTime);

		endCamBackBool = false;

	}
IEnumerator waitCamStart(float waitTime){

	yield return new WaitForSeconds (waitTime);

	startCamBackBool = false;

}

IEnumerator stopOnHook(float waitTime){

	currentPathPercent = 0.0f;

		anticiHook = true;

	yield return new WaitForSeconds (0.1f);
		if (!landingSound.isPlaying && landOnce) {
			landingSound.Play ();
			landOnce = false;
		}
	landingParticle.Play ();
		anticiHook = false;

	yield return new WaitForSeconds (waitTime);

	/*percentsPerSecond = 1f;
	yield return new WaitForSeconds (0.2f);
	percentsPerSecond = 0.1f;
	yield return new WaitForSeconds (0.1f);
	percentsPerSecond = 1f;
	yield return new WaitForSeconds (0.7f);
*/
	hookJumpActiveOnce = false;
	hookJumpActive = false;
	m_Anim.SetBool ("Grab", false);

	//sawMoverScript.gameObject.GetComponent<CircleCollider2D> ().enabled = true;

}

IEnumerator waitActiveHook(){

		yield return new WaitForSeconds (0.0f);
	m_Anim.SetBool ("Grab", true);
	yield return new WaitForSeconds (0.00f);
	hookJumpActive = true;

}

IEnumerator waitBlink(){

	yield return new WaitForSeconds (2f);

	m_Anim.SetBool ("Blink", true);

	yield return new WaitForSeconds (2f);

	m_Anim.SetBool ("Blink", false);

	yield return new WaitForSeconds (3.2f);
		
	m_Anim.SetBool ("Blink", false);

	m_Anim.SetBool ("Blink3-4", true);

	yield return new WaitForSeconds (4f);


	m_Anim.SetBool ("Blink3-4", false);


	blinkOnce = true;


}

IEnumerator waitBlinkNorm(){

	yield return new WaitForSeconds (1f);

	m_Anim.SetBool ("Blink", true);

	yield return new WaitForSeconds (2f);

	m_Anim.SetBool ("Blink", false);

	blinkOnce = true;


}

IEnumerator waitRunLookBack(){

		yield return new WaitForSeconds (0f);

		m_Anim.SetBool ("Run3-4", true);

		yield return new WaitForSeconds (1.9f);

		m_Anim.SetBool ("Run3-4", false);


		yield return new WaitForSeconds (6f);

		runLookOnce = true;


	}

IEnumerator waitSpaceBool(float waitTime){


	yield return new WaitForSeconds (waitTime);
	m_Anim.SetBool ("SpaceBool", false);


}

IEnumerator waitBeforeFallingActive(float waitTime){
	
	yield return new WaitForSeconds (waitTime);
		if (!endCamBool) {
		
			m_Anim.SetBool ("isJumping", false);

		} else {
		
		m_Anim.SetBool ("isJumping", true);
	
	}
		isJumpingFalseOnce = true;

}

IEnumerator waitAndGround(float waitTime){

	yield return new WaitForSeconds (waitTime);
	//if(vSpeed > -0.0001f && vSpeed < 0.0001f)m_Grounded = true;

}
	


void OnDrawGizmos(){

		iTween.DrawPath (wayPointArray);

}

    }
//}

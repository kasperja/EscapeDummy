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
	public GameObject WaitForHookedCol;
	public GameObject WaitForHookedCol2;

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

	public FollowX fx;


	public GameObject stairColsObj;
	public Transform stairPosUp;
	public Transform stairPosDown;

	private bool moveColUpBool = false;
	private bool moveColDownBool = false;

	private bool climbingStairsBoolCol = false;

	public Transform[] wayPointArray;
	private float percentsPerSecond = 1.4f;
	private float currentPathPercent = 0.0f;

	public bool hookJumpActive = false;
	public bool hookJumpActiveOnce = true;
	public bool hookJumpActiveOnceTween = true;

	public SawMover sawMoverScript;

	public bool hookStandingStill = false;

	public AudioSource attackSound1;
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

	private float waitJumpTime = 0.2f;

	public float velocity = 0f;

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
		posMoveBack = new Vector3 (graphicsNorm.transform.localPosition.x, 
			graphicsNorm.transform.localPosition.y + moveBackAmmount, graphicsNorm.transform.localPosition.z);

		posMoveForward = new Vector3 (graphicsNorm.transform.localPosition.x, 
			graphicsNorm.transform.localPosition.y, graphicsNorm.transform.localPosition.z);




	}
		private void Update(){
		
		if (Input.GetKeyDown (KeyCode.Space)  && !hpPlayerTotal.isDead) {
		
			m_Anim.SetBool ("SpaceBool", true);
			StartCoroutine (waitSpaceBool(0.2f));
		
		} else {
		

		
		}


			vSpeed = m_Rigidbody2D.velocity.y;





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


		if (hooked && Input.GetKey(KeyCode.Space) && sawMoverScript.hookDetected && hookJumpActiveOnce && hookStandingStill && !hpPlayerTotal.isDead) {

			
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
				
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) {

			sideArrowsBool = true;
			m_Anim.SetBool ("SideArrows", true);

		} else {

			sideArrowsBool = false;
			m_Anim.SetBool ("SideArrows", false);

		}

		if (cm.falconPunchBool && playOnce4 && !sideArrowsBool && m_Grounded && !hpPlayerTotal.isDead) {

			if(attackSoundOnce){

				attackSound1.Play ();
				attackSoundOnce = false;
			}
			m_Attack4 = true;
			playOnce4 = false;
			StartCoroutine (WaitForAnim4(0.5f));

		} /*else if (Input.GetKeyDown (KeyCode.E) && playOnce1 && cm.falconPunchBool == false && !sideArrowsBool && m_Grounded && !hpPlayerTotal.isDead) {

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
		else if (Input.GetKeyDown (KeyCode.W) && playOnce3 && cm.falconPunchBool == false && !sideArrowsBool && m_Grounded && !hpPlayerTotal.isDead) {

			if(attackSoundOnce){

				attackSound1.Play ();
				attackSoundOnce = false;
			}
				m_Attack3 = true;
				playOnce3 = false;
			StartCoroutine (WaitForAnim3(0.5f));

		}
		else if (Input.GetKeyDown(KeyCode.E) && playOnce5 && cm.falconPunchBool == false && !hpPlayerTotal.isDead) {

			if(attackSoundOnce){

				attackSound1.Play ();
				attackSoundOnce = false;
			}
			m_Attack5 = true;
			playOnce5 = false;
			StartCoroutine (WaitForAnim5(0.7f));

		}

		else if ((Input.GetKeyDown (KeyCode.R) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown (KeyCode.W)) && playOnce5 && cm.falconPunchBool == false && !m_Grounded && !hpPlayerTotal.isDead) {

			if(attackSoundOnce){

				attackSound1.Play ();
				attackSoundOnce = false;
			}
			m_Attack5 = true;
			playOnce5 = false;
			StartCoroutine (WaitForAnim5(0.7f));

		}
			
		else if (!Input.GetKey (KeyCode.E) && !Input.GetKey (KeyCode.R) && !Input.GetKey (KeyCode.W) 
			&& cm.falconPunchBool == false) {
		
			m_Attack1 = false;
			m_Attack2 = false;
			m_Attack3 = false;
			m_Attack4 = false;
			m_Attack5 = false;
			attackSoundOnce = true;
		
		
		} 


		}

	IEnumerator WaitForAnim1(float animWaitTime){
			yield return new WaitForSeconds (animWaitTime);
				m_Attack1 = false;
			playOnce1 = true;
				

		}
	IEnumerator WaitForAnim2(float animWaitTime){
			yield return new WaitForSeconds (animWaitTime);
			m_Attack2 = false;
			playOnce2 = true;
		}

	IEnumerator WaitForAnim3(float animWaitTime){
			yield return new WaitForSeconds (animWaitTime);
			m_Attack3 = false;
			playOnce3 = true;
		}

	IEnumerator WaitForAnim4(float animWaitTime){
			yield return new WaitForSeconds (animWaitTime);
			cm.falconPunchBool = false;	
			m_Attack4 = false;
			playOnce4 = true;
		}

	IEnumerator WaitForAnim5(float animWaitTime){
		yield return new WaitForSeconds (animWaitTime);
		m_Attack5 = false;
		playOnce5 = true;
		m_Anim.SetBool ("JumpKickToJump", true);
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
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
               // m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character

			//if (m_Rigidbody2D.velocity.x < 60f && m_Rigidbody2D.velocity.x > -60f) {
				
				timer = 0.0f;



			if (climbingStairsBool) {
				
				m_Grounded = true;



				if(Input.GetKey(KeyCode.LeftArrow)){

					//down Stairs
				m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed * 1f, move * m_MaxSpeed * 0.8f);
				
				
				}else{


					//up Stairs
					m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed * 1f, move * m_MaxSpeed * 0.2f);

				}
			
			} else {
			
				m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
			
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
		if (m_Grounded && jump && m_Anim.GetBool("Ground") && !climbingStairsBool && !hookJumpActive  && !hpPlayerTotal.isDead && jumpOnce)
            {
			if (!sideArrowsBool) {
				if (jumpOnce) {
					// Add a vertical force to the player.
					m_Anim.SetBool ("StartJump", true);
					StartCoroutine (waitJump (waitJumpTime));
					jumpOnce = false;
				}
			} else {


				//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				m_Anim.SetBool ("StartJump", true);



				m_Grounded = false;

				m_Anim.SetBool("Ground", false);

				//m_Anim.SetBool("StartJump", false);

				StartCoroutine (waitJump (0.0f));

				jumpOnce = false;



			}
				

            }
        }

	public void Stop(float move, bool crouch, bool jump){
	

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
		if (m_Grounded || m_AirControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move*m_CrouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			// m_Anim.SetFloat("Speed", Mathf.Abs(move));

			// Move the character

			//if (m_Rigidbody2D.velocity.x < 60f && m_Rigidbody2D.velocity.x > -60f) {





			if (climbingStairsBool) {
				
				m_Rigidbody2D.velocity = new Vector2 (20.4f, -21.5f);
			} else {
				
				m_Rigidbody2D.velocity = new Vector2 (Mathf.Lerp(m_Rigidbody2D.velocity.x,  0f , timer), m_Rigidbody2D.velocity.y);
			
			
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
		if (m_Grounded && jump && m_Anim.GetBool("Ground") && !climbingStairsBool && !hookJumpActive && !hpPlayerTotal.isDead && jumpOnce)
		{
			if (!sideArrowsBool) {
				if (jumpOnce) {
					// Add a vertical force to the player.
					m_Anim.SetBool ("StartJump", false);
					m_Anim.SetBool("Ground", false);
					StartCoroutine (waitJump (waitJumpTime));
					jumpOnce = false;
				}
			} else {
			

				//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));



				m_Anim.SetBool ("StartJump", true);

				m_Grounded = false;

				m_Anim.SetBool("Ground", false);

				m_Anim.SetBool("StartJump", false);

				StartCoroutine (waitJump (0.0f));
				jumpOnce = false;


					
					
			
			}

		}

	
	
	
	}
		void OnTriggerEnter2D(Collider2D other)
		{


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

			friction = 4f;

		}
		if (other.gameObject.tag == "StairsTriggerColScale") {

			//m_Grounded = true;
			//Debug.Log("HI");

			climbingStairsBoolCol = true;

		}
		if (other.gameObject.tag == "CamTargetEndTrigger") {




			endCamBool = true;

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

			friction = 0.9f;

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

			friction = 4f;

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
		m_Grounded = true;

	
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
		m_Grounded = false;

		m_Anim.SetBool("Ground", false);

		m_Anim.SetBool("StartJump", false);

		m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

		

		yield return new WaitForSeconds (0.0f);
		
		

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

	
void OnDrawGizmos(){

		iTween.DrawPath (wayPointArray);

}

    }
//}

using System;
using UnityEngine;
using System.Collections;

//namespace UnityStandardAssets._2D
//{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
	public Animator m_AnimHooked;
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
		public ComboManager cm;
		public HitpointsPlayerTotal hpPlayerTotal;
		public bool m_Attack1 = false;
		public bool m_Attack2 = false;
		public bool m_Attack3 = false;
		public bool m_Attack4 = false;

	public float vSpeed = 0;

		public bool playOnce1 = true;
		public bool playOnce2 = true;
		public bool playOnce3 = true;
		public bool playOnce4 = true;
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
	public float friction = 0.7f;
	private bool jumpOnce = true;
	public GameObject WaitForHookedCol;

	public Vector2 direction;

	public bool climbingStairsBool = false;

	public bool endCamBool = false;
	public bool endCamBackBool = false;

	private float scale = 5f;
	private float minScale = 4.5f;
	private float maxScale = 5f;
	private float scaleSpeed = 10f;




        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

		//tempHinge = gameObject.GetComponent<FixedJoint2D> ();
        }

		private void Update(){

		/*
		 
		 RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up);

		if (true) {
		
		
			float distanceToGround = hit.distance;

			transform.position = new Vector2(transform.position.x, hit.distance - transform.GetComponent<BoxCollider2D> ().bounds.extents.y);
		

		
		}*/

		vSpeed = m_Rigidbody2D.velocity.y;

			/*if (hpPlayerTotal.hitpoints <= 0.0f) {
				m_Attack1 = false;
				m_Attack2 = false;
				m_Attack3 = false;
				m_Attack4 = false;

				playOnce1 = true;
				playOnce2 = true;
				playOnce3 = true;
				playOnce4 = true;
			
			}*/

		/*RaycastHit2D hit = Physics2D.Raycast (this.gameObject.transform.position, direction);



		if (hit.collider != null) {
		
			float distanceToGround = hit.distance;

			transform.position = new Vector3 (this.gameObject.transform.position.x, hit.distance - transform.GetComponent<BoxCollider2D> ().bounds.extents.y, transform.position.z);
		
		}*/

		if (hooked) {
			

			transform.position = new Vector3( graphicsHooked.transform.position.x, graphicsHooked.transform.position.y + 20f, transform.position.z);
			//tempHinge.connectedAnchor = anchor.transform.position;
			//MainCharObj.SetActive(false);
			m_AnimHooked.SetBool("Grab", true);
			m_AnimHooked.SetBool ("Hold", true);
			WaitForHookedCol.SetActive (false);
			gameObject.GetComponent<BoxCollider2D>().enabled = false;

		}

		if (!hooked) {


			WaitForHookedCol.SetActive (true);
			//MainCharObj.SetActive(true);
			graphicsHooked.transform.position = graphicsNorm.transform.position;
			graphicsHooked.transform.rotation = graphicsNorm.transform.rotation;
			//m_AnimHooked.SetBool("Grab", false);
			//m_AnimHooked.SetBool ("Hold", false);
			//m_AnimHooked.SetBool ("Release", true);
			graphicsHooked.SetActive (false);
			gameObject.GetComponent<BoxCollider2D>().enabled = true;

		
		}

		if(hooked && Input.GetKeyDown(KeyCode.Space)){

			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
			//gameObject.GetComponent<Rigidbody2D> ().gravityScale = 7;
			//transform.position = new Vector3( graphicsHooked.transform.position.x, graphicsHooked.transform.position.y + 20f, transform.position.z);
			hooked = false;
			tempHinge.enabled = false;
			graphicsNorm.SetActive (true);
			graphicsHooked.SetActive (false);
			//hookParent.GetComponent<BoxCollider2D> ().enabled = false;
			//tempHinge.connectedBody = gameObject.GetComponent<Rigidbody2D> ();
			StartCoroutine (waitHook (1f));


		}

		if (endCamBool) {

			scale -= scaleSpeed * Time.deltaTime;

			if (scale > maxScale) {
			
				scale = maxScale;
			
			}

			if (scale < minScale) {
			
				scale = minScale;
			
			}

			graphicsNorm.transform.localScale = new Vector3 (scale, scale, 1f);
		
			friction = 4f;
		
		}

		if (endCamBackBool) {

			scale += scaleSpeed * Time.deltaTime;

			if (scale > maxScale) {

				scale = maxScale;

			}

			if (scale < minScale) {

				scale = minScale;

			}

			graphicsNorm.transform.localScale = new Vector3 (scale, scale, 1f);

			friction = 1f;
		
		}
		/*if (m_Grounded) {
			
			tempHinge.enabled = true;
			hookParent.GetComponent<BoxCollider2D> ().enabled = true;
			hookOnce = true;
			hooked = false;

		}*/
				
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) {

			sideArrowsBool = true;
			m_Anim.SetBool ("SideArrows", true);

		} else {

			sideArrowsBool = false;
			m_Anim.SetBool ("SideArrows", false);

		}

		if (cm.falconPunchBool && playOnce4) {


			m_Attack4 = true;
			playOnce4 = false;
			StartCoroutine (WaitForAnim4());

		} else if (Input.GetKeyDown (KeyCode.E) && playOnce1 && cm.falconPunchBool == false) {

				m_Attack1 = true;
				playOnce1 = false;
				StartCoroutine (WaitForAnim1());

			}
		else if (Input.GetKeyDown (KeyCode.R) && playOnce2 && cm.falconPunchBool == false) {

				m_Attack2 = true;
				playOnce2 = false;
				StartCoroutine (WaitForAnim2());

			}
		else if (Input.GetKeyDown (KeyCode.W) && playOnce3 && cm.falconPunchBool == false) {

				m_Attack3 = true;
				playOnce3 = false;
				StartCoroutine (WaitForAnim3());

			}
			
		else if (!Input.GetKey (KeyCode.E) && !Input.GetKey (KeyCode.R) && !Input.GetKey (KeyCode.W) 
			&& cm.falconPunchBool == false) {
		
			m_Attack1 = false;
			m_Attack2 = false;
			m_Attack3 = false;
			m_Attack4 = false;
		
		
			}


		}

		IEnumerator WaitForAnim1(){
			yield return new WaitForSeconds (animWaitTime);
				m_Attack1 = false;
			playOnce1 = true;
				

		}
		IEnumerator WaitForAnim2(){
			yield return new WaitForSeconds (animWaitTime);
			m_Attack2 = false;
			playOnce2 = true;
		}

		IEnumerator WaitForAnim3(){
			yield return new WaitForSeconds (animWaitTime);
			m_Attack3 = false;
			playOnce3 = true;
		}

		IEnumerator WaitForAnim4(){
			yield return new WaitForSeconds (animWaitTime);
			cm.falconPunchBool = false;	
			m_Attack4 = false;
			playOnce4 = true;
		}

        private void FixedUpdate()
        {

		if (!climbingStairsBool) {
			m_Grounded = false;
		}


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
					m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed * 1f, move * m_MaxSpeed * 0.1f);

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
		if (m_Grounded && jump && m_Anim.GetBool("Ground") && !hooked && !climbingStairsBool)
            {
			if (!sideArrowsBool) {
				if (jumpOnce) {
					// Add a vertical force to the player.
					m_Anim.SetBool ("StartJump", true);
					StartCoroutine (waitJump (0.5f));
					jumpOnce = false;
				}
			} else {


				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				m_Anim.SetBool ("StartJump", true);

				m_Grounded = false;

				m_Anim.SetBool("Ground", false);

				m_Anim.SetBool("StartJump", false);





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
				
				m_Rigidbody2D.velocity = new Vector2 (20.4f, -20f);
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
		if (m_Grounded && jump && m_Anim.GetBool("Ground") && !climbingStairsBool)
		{
			if (!sideArrowsBool) {
				if (jumpOnce) {
					// Add a vertical force to the player.
					m_Anim.SetBool ("StartJump", true);
					StartCoroutine (waitJump (0.5f));
					jumpOnce = false;
				}
			} else {
			

				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				m_Anim.SetBool ("StartJump", true);

				m_Grounded = false;

				m_Anim.SetBool("Ground", false);

				m_Anim.SetBool("StartJump", false);


					
					
			
			}

		}

	
	
	
	}
		void OnTriggerEnter2D(Collider2D other)
		{
		if (other.gameObject.tag == "HookJoint") {

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





			}

		if (other.gameObject.tag == "StairsTrigger") {

			m_Grounded = true;

			climbingStairsBool = true;

		}
		if (other.gameObject.tag == "CamTargetEndTrigger") {




			endCamBool = true;

		}


	
		}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "StairsTrigger") {

			climbingStairsBool = false;

		}
		if (other.gameObject.tag == "CamTargetEndTrigger") {


			endCamBackBool = true;

			endCamBool = false;

			StartCoroutine (waitCamEnd (0.5f));

		}

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "StairsTrigger") {

			m_Grounded = true;


			climbingStairsBool = true;

		}

		if (other.gameObject.tag == "CamTargetEndTrigger") {

		


			endCamBool = true;

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
        }

	IEnumerator waitHook (float waitTime){
		


		yield return new WaitForSeconds (waitTime);
		//hookParent.GetComponent<BoxCollider2D> ().enabled = true;
		//tempHinge.enabled = true;

			tempHinge.enabled = true;
			//hookParent.GetComponent<BoxCollider2D> ().enabled = true;
			hookOnce = true;
			hooked = false;

	
	}
		

	IEnumerator waitJump (float waitTime){


		yield return new WaitForSeconds (waitTime);
		//hookParent.GetComponent<BoxCollider2D> ().enabled = true;
		//tempHinge.enabled = true;
		m_Grounded = false;

		m_Anim.SetBool("Ground", false);

		m_Anim.SetBool("StartJump", false);

		m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

		yield return new WaitForSeconds (waitTime + 1f);
		jumpOnce = true;

	}

	IEnumerator waitCamEnd(float waitTime){

		yield return new WaitForSeconds (waitTime);

		endCamBackBool = false;
	
	}

    }
//}

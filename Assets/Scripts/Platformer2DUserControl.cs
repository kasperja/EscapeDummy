using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

//namespace UnityStandardAssets._2D
//{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

	public HitpointsPlayerTotal hp;

		private bool isStopped = true;
		private bool playOnce = true;
	public bool isStandingStart = false;
	public bool initiateStanding = false;
	private bool initiateOnce = true;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}


		if (initiateStanding && initiateOnce) {
		
			StartCoroutine (waitForStanding ());
			initiateOnce = false;

		}
			// Read the inputs.

			// Pass all parameters to the character control script.


        }


        private void FixedUpdate()
        {

		if (!hp.isDead) {
			bool crouch = Input.GetKey (KeyCode.LeftControl);
			float h = CrossPlatformInputManager.GetAxis ("Horizontal");
			
			if (m_Character.sideArrowsBool == true && isStandingStart) {

				isStopped = false;
				playOnce = true;
				m_Character.Move (h, crouch, m_Jump);


			} else if (m_Character.sideArrowsBool == false || !isStandingStart) {

				if (isStopped) {

					m_Character.Stop (h, crouch, m_Jump);
					playOnce = true;

				} else if (playOnce) {

					StartCoroutine (waitAndStop (0.00f));
					playOnce = false;

				} else {

					m_Character.Move (h, crouch, m_Jump);

				}

			}

			m_Jump = false;

		} else {
		
			bool crouch = false;
			float h = 0f;
			m_Character.Stop (h, crouch, false);
		
		}
        }

		IEnumerator waitAndStop(float waitTime){

			yield return new WaitForSeconds (waitTime);
		
			playOnce = false;
			isStopped = true;
		}
	IEnumerator waitForStanding(){

		yield return new WaitForSeconds (0.8f);
		isStandingStart = true;

	}


    }



//}

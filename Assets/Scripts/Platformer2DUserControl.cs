using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

		private bool isStopped = true;
		private bool playOnce = true;

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


			// Read the inputs.

			// Pass all parameters to the character control script.


        }


        private void FixedUpdate()
        {
			bool crouch = Input.GetKey(KeyCode.LeftControl);
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			
			if (m_Character.sideArrowsBool == true) {

				isStopped = false;
				playOnce = true;
				m_Character.Move(h, crouch, m_Jump);


			}

			else if (m_Character.sideArrowsBool == false) {

				if (isStopped) {

					m_Character.Move (h, crouch, m_Jump);
					playOnce = true;

				} else if (playOnce) {

					StartCoroutine (waitAndStop (0.00f));
					playOnce = false;

				} else {

					m_Character.Move(h, crouch, m_Jump);

				}

			}

			m_Jump = false;
        }

		IEnumerator waitAndStop(float waitTime){

			yield return new WaitForSeconds (waitTime);
		
			playOnce = false;
			isStopped = true;
		}

    }



}

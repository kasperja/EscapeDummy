using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
		public Transform targetEnd;
		public Transform targetStart;
		public Transform targetSaw;
		public Transform targetHooked;
		public Transform targetNotHooked;
		public PlatformerCharacter2D MainCharScript;
        public float damping = 0.1f;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float yValue = 1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
		public bool startOutsideBool = false;

        // Use this for initialization
        private void Start()
        {
			//RenderSettings.fog = false;
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
			if (MainCharScript.hookJumpActive) {
			
				damping = .4f;
				target = targetSaw;
			
			} else if (MainCharScript.endCamBool) {
			
				damping = 0.4f;
				target = targetEnd;
			
			
			} else if (MainCharScript.startCamBool) {


				if (startOutsideBool) {
					damping = 0.5f;
				} else {
					damping = 0.4f;
				}
				target = targetStart;


			} else if (MainCharScript.startCamBackBool){

				if (startOutsideBool) {
					damping = 0.5f;
				} else {
					damping = 0.4f;
				}
				target = targetNotHooked;

			}else if (MainCharScript.endCamBackBool){
				
				damping = 0.1f;
				target = targetNotHooked;
			
			}else {
			
				if (startOutsideBool) {

					if(damping > 0.1f){

						damping -= 0.7f * Time.deltaTime;
					}

				} else {
					
					damping = 0.1f;

				}
				target = targetNotHooked;
			
			}
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }


    }

}

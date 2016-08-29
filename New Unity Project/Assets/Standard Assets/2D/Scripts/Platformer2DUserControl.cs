using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		private float nextSlide;
		public float slideRate;
		bool crouch;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
			crouch = false;
//			nextSlide = Time.time + slideRate;
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
		{
//			Debug.Log (m_Character.GetGameOver());
            // Read the inputs.
			bool crouch = Input.GetKey (KeyCode.LeftControl); // If left control pressed, crouch = true
//			if (Time.time > nextSlide) {
//				nextSlide = Time.time + slideRate;
//				crouch = true;
//			} else {
//				crouch = false;
//			}
			bool dodge = Input.GetKey (KeyCode.LeftAlt); 	 // If left alt pressed, dodge = true
//            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
			if (m_Character.GetGameOver ()) {
				m_Character.Move (0f, false, false, false);
			} else {
				m_Character.Move (1f, crouch, m_Jump, dodge);
			}
            m_Jump = false;
        }
    }
}

// pass crouch = true only for a couple of second

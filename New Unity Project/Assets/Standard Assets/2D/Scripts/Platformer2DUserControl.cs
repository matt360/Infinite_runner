using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		bool crouch;
		bool slide;
		[SerializeField] private float slideTime = 1.5f;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
			crouch = false;
			slide = false;
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
			bool slide = Input.GetButton ("Fire1");
			if (slide && m_Character.GetGrounded()) {
				StartCoroutine (Slide ());
			}
			if (!m_Character.GetGrounded()) {
				crouch = false;
			}

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

		IEnumerator Slide() {
			crouch = true;
			yield return new WaitForSeconds(slideTime);
			crouch = false;
		}
    }
}

// pass crouch = true only for a couple of second

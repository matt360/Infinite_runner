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
        }


        private void FixedUpdate()
		{
			Debug.Log (m_Character.GetGameOver());
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl); // If left control pressed, crouch = true
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

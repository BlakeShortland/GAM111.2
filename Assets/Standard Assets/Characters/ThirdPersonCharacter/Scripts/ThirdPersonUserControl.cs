// Changes I've made to this premade script:
//		Changes instances of "forward" to "up" to accomidate my camera orientation.
//		Created a target transform variable.
//		Created the MoveToTarget function.
//		Added the UnityEngine.AI namespace.
//
//
//

using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamUp;				  // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

		GameObject target; // Stores the target postion for the player to move to.

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

			MoveToTarget(); // Runs my MoveToTarget function.
		}


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamUp = Vector3.Scale(m_Cam.up, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamUp + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.up + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }

		void MoveToTarget()
		{
			if (GameObject.Find("PlayerTarget(Clone)") != null)
			{
				target = GameObject.Find("PlayerTarget(Clone)");
				NavMeshAgent agent = GetComponent<NavMeshAgent>();
				agent.destination = target.transform.position;
			}
		}

		void OnCollisionEnter (Collision collision)
		{
			if(collision.gameObject.name == "PlayerTarget(Clone)")
			{
				Debug.Log("hit the target");
				Destroy(target);
			}
		}
	}
}

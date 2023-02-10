using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("ground"))
        {
            InputManager.isGrounded = true;
            InputManager.bonusJumps = 1;
        }

    }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == ("ground"))
            {
                InputManager.isGrounded = false;
            }
        }
}

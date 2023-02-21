using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public MeshRenderer bottomColor;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("ground"))
        {
            // sets charged color if aplicable
            if(InputManager.charged == true) bottomColor.material.color = new Color32(0, 0, 200, 143);

            //sets grounded to true and resets bouns jumps
            InputManager.isGrounded = true;
            InputManager.bonusJumps = 1;
        }

    }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == ("ground"))
            {
            // disables grounded when leaving ground
                InputManager.isGrounded = false;
            }
        }
}

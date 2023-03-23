using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private InputManager inputManagerScript;
    public MeshRenderer bottomColor;

    void Start()
    {
        inputManagerScript = this.transform.root.GetComponentInChildren<InputManager>();
    }
    private void OnTriggerStay(Collider other)
    {

        

        if (other.tag == ("ground"))
        {
            // sets charged color if aplicable
            if(inputManagerScript.charged == true) bottomColor.material.color = new Color32(0, 0, 200, 143);

            //sets grounded to true and resets bouns jumps
            inputManagerScript.isGrounded = true;
            inputManagerScript.bonusJumps = 1;
            inputManagerScript.moveMultiplier = 1f;
        }

    }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == ("ground"))
            {
            // disables grounded when leaving ground
                inputManagerScript.isGrounded = false;
            
            }
        }
}

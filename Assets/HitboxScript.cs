using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    public MeshRenderer thisColor;
    private Color originalColor;



    private void Awake()
    {
        //sets the mesh and original color
        thisColor = this.gameObject.GetComponent<MeshRenderer>();
        originalColor = thisColor.material.color;
        
    }
    private void OnTriggerStay(Collider attack)
    {
        //Debug.Log(this.gameObject.name + " _collided with_ " + attack.name);
        // ^^^ collision detection debug


        //sets the collor to a less transarent when hitting something
        thisColor.material.color = new Color(thisColor.material.color.r, thisColor.material.color.g, thisColor.material.color.b, thisColor.material.color.a + 50);


        //sets the bools for the combat script
        if (this.gameObject.name == "HighHitbox") CombatManager.highCheck = true;
        if (this.gameObject.name == "LowHitbox") CombatManager.lowCheck = true;
        if (this.gameObject.name == "FloorCollider") CombatManager.floorCheck = true;

        
    }

    private void OnTriggerExit(Collider attack)
    {
        //returns the color to original
        thisColor.material.color = originalColor;

        //unsets the bools
        if (this.gameObject.name == "HighHitbox") CombatManager.highCheck = false;
        if (this.gameObject.name == "LowHitbox") CombatManager.lowCheck = false;
        if (this.gameObject.name == "FloorCollider") CombatManager.floorCheck = false;
    }
}

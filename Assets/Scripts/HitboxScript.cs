using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    private MeshRenderer thisColor;
    private Color originalColor;
    public Collider collidingAttack;

    private ContactPoint contactPoint;
    private InputManager inputManager;
    private CombatManager combatManager;
    



    private void Awake()
    {
        //sets the mesh and original color
        thisColor = this.gameObject.GetComponent<MeshRenderer>();
        originalColor = thisColor.material.color;

        combatManager = this.transform.root.GetComponentInChildren<CombatManager>();
        inputManager = this.transform.root.GetComponentInChildren<InputManager>();

    }
    private void OnTriggerStay(Collider attack)
    {
        //Debug.Log(this.gameObject.name + " _collided with_ " + attack.name);
        // ^^^ collision detection debug


        //sets the collor to a less transarent when hitting something
        thisColor.material.color = new Color(thisColor.material.color.r, thisColor.material.color.g, thisColor.material.color.b, thisColor.material.color.a + 50);


        //sets the Colliders for the combat script
        if (attack.tag != "ground")
        {
            if (this.gameObject.name == "HighHitbox") combatManager.highCheck = attack;
            if (this.gameObject.name == "LowHitbox") combatManager.lowCheck = attack;
            if (this.gameObject.name == "FloorCollider") combatManager.floorCheck = attack;
        }
        

        

        combatManager.isBeingHit();

        
    }

    private void OnTriggerExit(Collider attack)
    {
        //returns the color to original
        thisColor.material.color = originalColor;

        //unsets the Colliders in the Combat Script
        if (this.gameObject.name == "HighHitbox") combatManager.highCheck = null;
        if (this.gameObject.name == "LowHitbox") combatManager.lowCheck = null;
        if (this.gameObject.name == "FloorCollider") combatManager.floorCheck = null;
    }
}

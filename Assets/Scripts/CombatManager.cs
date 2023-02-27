using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class CombatManager : MonoBehaviour
{

    private SFBUNEWSYSTEM playerControls;
    [SerializeField]
    private PlayerInput Player;
    [SerializeField]
    
    //the hitboxes
    public GameObject highHitbox;
    public GameObject medHitbox;
    public GameObject lowHitbox;



    //the bools to see what is hit
    public static bool floorCheck;
    public static bool lowCheck;
    public static bool highCheck;
    public static Collider collidingAttack;


    private void Awake()
    {
        playerControls = new SFBUNEWSYSTEM();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        
    }

    //function called from the hitbox script when being hit
    public static void isBeingHit()
    {
        if (highCheck == true) Debug.Log("Highbox Colliding with " + collidingAttack.name);
        if (lowCheck == true) Debug.Log("LowBox Colliding with " + collidingAttack.name);
        if (floorCheck == true) Debug.Log("FloorBox Colliding with " + collidingAttack.name);

    }

    private void Update()
    {
       /* Debug.Log(floorCheck);
        Debug.Log(lowCheck);
        Debug.Log(highCheck);
       */
    }
}


// Left off... complete the connection between the hitbox scripts and the combat manger by linking the trigger stay to the isBeingHit() function
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
    private HitboxScript hitboxScript;
    
    
    //the hitboxes
    private GameObject highHitbox;
    private GameObject medHitbox;
    private GameObject lowHitbox;



    //the obj colliding with the hitboxes
    public Collider floorCheck;
    public Collider lowCheck;
    public Collider highCheck;

    private GameObject jab1;
    private GameObject highJab2;
    private GameObject highJab3;
    private GameObject lowJab2;
    private GameObject lowJab3;
    private GameObject upRoot1;
    private GameObject upRoot2;

    private bool isInHitStun;
    [SerializeField]
    private float jabLength;
    [SerializeField]
    private float jabFrames;



    private void Awake()
    {
        playerControls = new SFBUNEWSYSTEM();
        hitboxScript = this.transform.root.GetComponentInChildren<HitboxScript>();

        //finds all the attacks and sets them to vars
        jab1 = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("Jab1").gameObject;
        highJab2 = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("HighJab2").gameObject;
        highJab3 = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("HighJab3").gameObject;
        lowJab2 = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("LowJab2").gameObject;
        lowJab3 = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("LowJab3").gameObject;
        upRoot1 = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("UpRoot1").gameObject;
        upRoot2 = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("UpRoot2").gameObject;




    }

    private void OnEnable()
    {
        playerControls.Enable();
        
    }

    //function called from the hitbox script when being hit
   

    private void Update()
    {
        /* Debug.Log(floorCheck);
         Debug.Log(lowCheck);
         Debug.Log(highCheck);
        */
    }

    public void isBeingHit()
    {
        
        if(highCheck != null) Debug.Log("Highbox Colliding with " + highCheck.name);
        if (lowCheck != null) Debug.Log("LowBox Colliding with " + lowCheck.name);
        if (floorCheck != null) Debug.Log("FloorBox Colliding with " + floorCheck.name);

    }

    public IEnumerator Jab()                //// working on jab
    {
        Debug.Log("Jab");
        jab1.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        highJab2.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        highJab3.SetActive(true);
        yield return new WaitForSeconds(jabLength);
        jab1.SetActive(false);
        highJab2.SetActive(false);
        highJab3.SetActive(false);

        yield return null;
    }
}


// Left off... complete the connection between the hitbox scripts and the combat manger by linking the trigger stay to the isBeingHit() function
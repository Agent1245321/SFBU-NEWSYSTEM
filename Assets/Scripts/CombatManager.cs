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
    public GameObject highHitbox;
    public GameObject medHitbox;
    public GameObject lowHitbox;



    //the obj colliding with the hitboxes
    public Collider floorCheck;
    public Collider lowCheck;
    public Collider highCheck;

    public GameObject jab1;
    public GameObject highJab2;
    public GameObject highJab3;
    public GameObject lowJab2;
    public GameObject lowJab3;
    public GameObject upRoot1;
    public GameObject upRoot2;

    public bool isInHitStun;



    private void Awake()
    {
        playerControls = new SFBUNEWSYSTEM();
        hitboxScript = this.transform.root.GetComponentInChildren<HitboxScript>();

        //finds all the attacks and sets them to vars
        jab1 = this.gameObject.transform.root.Find("Combat").Find("Jab1").gameObject;
        highJab2 = this.gameObject.transform.root.Find("Combat").Find("HighJab2").gameObject;
        highJab3 = this.gameObject.transform.root.Find("Combat").Find("HighJab3").gameObject;
        lowJab2 = this.gameObject.transform.root.Find("Combat").Find("LowJab2").gameObject;
        lowJab3 = this.gameObject.transform.root.Find("Combat").Find("LowJab3").gameObject;
        upRoot1 = this.gameObject.transform.root.Find("Combat").Find("UpRoot1").gameObject;
        upRoot2 = this.gameObject.transform.root.Find("Combat").Find("UpRoot2").gameObject;




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
        jab1.SetActive(true);
        yield return new WaitForSeconds(.1f);
        highJab2.SetActive(true);
        yield return new WaitForSeconds(.1f);
        highJab3.SetActive(true);
        jab1.SetActive(false);
        yield return new WaitForSeconds(.1f);
        highJab2.SetActive(false);
        yield return new WaitForSeconds(.1f);
        highJab3.SetActive(false);

        yield return null;
    }
}


// Left off... complete the connection between the hitbox scripts and the combat manger by linking the trigger stay to the isBeingHit() function
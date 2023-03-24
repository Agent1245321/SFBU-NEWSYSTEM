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

    private GameObject jab1r;
    private GameObject highJab1_2r;
    private GameObject highJab1_3r;
    private GameObject lowJab1_2r;
    private GameObject lowJab1_3r;
    private GameObject upRoot1r;
    private GameObject upRoot2r;
    private GameObject jab1l;
    private GameObject highJab1_2l;
    private GameObject highJab1_3l;
    private GameObject lowJab1_2l;
    private GameObject lowJab1_3l;
    private GameObject upRoot1l;
    private GameObject upRoot2l;

    [SerializeField]
    private float jabLength;
    [SerializeField]
    private float jabFrames;

    private InputManager inputManager;


    




    private void Awake()
    {
        playerControls = new SFBUNEWSYSTEM();
        hitboxScript = this.transform.root.GetComponentInChildren<HitboxScript>();
        inputManager = this.gameObject.GetComponent<InputManager>();

        //finds all the attacks and sets them to vars
        jab1r = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("RCM").gameObject;
        highJab1_2r = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("RMHM").gameObject;
        highJab1_3r = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("RFHB").gameObject;
        lowJab1_2r = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("RMLM").gameObject;
        lowJab1_3r = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("RFLMB").gameObject;
        upRoot1r = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("RCL").gameObject;
        upRoot2r = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("RMLB").gameObject;
        //left equivilents
        /*
        jab1l = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("Jab1 L").gameObject;
        highJab1_2l = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("HighJab2 L").gameObject;
        highJab1_3l = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("HighJab3 L").gameObject;
        lowJab1_2l = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("LowJab2 L").gameObject;
        lowJab1_3l = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("LowJab3 L").gameObject;
        upRoot1l = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("UpRoot1 L").gameObject;
        upRoot2l = this.gameObject.transform.root.Find("Cube").Find("Combat").Find("UpRoot2 L").gameObject;

        */




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

    public IEnumerator JabR()                //// working on jab
    {
        
        //sets up the lag for the move
        inputManager.attackLag = 2 * jabFrames + jabLength + .05f;
        StartCoroutine(inputManager.AttackLag());
         
        Debug.Log("Jab");
        jab1r.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        highJab1_2r.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        highJab1_3r.SetActive(true);
        yield return new WaitForSeconds(jabLength);
        jab1r.SetActive(false);
        highJab1_2r.SetActive(false);
        highJab1_3r.SetActive(false);

        yield return null;
    }

/*
    public IEnumerator JabL()                //// working on jab
    {

        //sets up the lag for the move
        inputManager.attackLag = 2 * jabFrames + jabLength + .05f;
        StartCoroutine(inputManager.AttackLag());

        Debug.Log("Jab");
        jab1l.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        highJab1_2l.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        highJab1_3l.SetActive(true);
        yield return new WaitForSeconds(jabLength);
        jab1l.SetActive(false);
        highJab1_2l.SetActive(false);
        highJab1_3l.SetActive(false);

        yield return null;
    }
*/

    public IEnumerator JabLowR()                //// working on jab
    {
        //sets up the lag for the move
        inputManager.attackLag = 2 * jabFrames + jabLength + .05f;
        StartCoroutine(inputManager.AttackLag());

        Debug.Log("Jab Low");
        jab1r.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        lowJab1_2r.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        lowJab1_3r.SetActive(true);
        yield return new WaitForSeconds(jabLength);
        jab1r.SetActive(false);
        lowJab1_2r.SetActive(false);
        lowJab1_3r.SetActive(false);

        yield return null;
    }

    /*
    public IEnumerator JabLowL()                //// working on jab
    {
        //sets up the lag for the move
        inputManager.attackLag = 2 * jabFrames + jabLength + .05f;
        StartCoroutine(inputManager.AttackLag());

        Debug.Log("Jab Low");
        jab1l.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        lowJab1_2l.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        lowJab1_3l.SetActive(true);
        yield return new WaitForSeconds(jabLength);
        jab1l.SetActive(false);
        lowJab1_2l.SetActive(false);
        lowJab1_3l.SetActive(false);

        yield return null;
    }

    */

    public IEnumerator UpRootR()                //// working on jab
    {
        //sets up the lag for the move
        inputManager.attackLag = jabFrames + jabLength + .4f;
        StartCoroutine(inputManager.AttackLag());

        //performs the move
        Debug.Log("Up Root");
        upRoot1r.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        upRoot2r.SetActive(true);;
        yield return new WaitForSeconds(jabLength);
        upRoot1r.SetActive(false);
        upRoot2r.SetActive(false);


        yield return null;
    }

    /*
    public IEnumerator UpRootL()                //// working on jab
    {
        //sets up the lag for the move
        inputManager.attackLag = jabFrames + jabLength + .4f;
        StartCoroutine(inputManager.AttackLag());

        //performs the move
        Debug.Log("Up Root");
        upRoot1l.SetActive(true);
        yield return new WaitForSeconds(jabFrames);
        upRoot2l.SetActive(true); ;
        yield return new WaitForSeconds(jabLength);
        upRoot1l.SetActive(false);
        upRoot2l.SetActive(false);


        yield return null;
    }
    */
}


// Left off... complete the connection between the hitbox scripts and the combat manger by linking the trigger stay to the isBeingHit() function
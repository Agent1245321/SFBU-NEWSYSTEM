using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    
    private SFBUNEWSYSTEM playerControls;
    [SerializeField]
    private PlayerInput Player;
    [SerializeField]
    private InputActionReference actionReference;

    private Transform pl1;
    public Rigidbody playerRB;

    [SerializeField]
    private Vector2 move;
    private Vector2 aim;
    public float speed;
    public float moveMultiplier;

    public MeshRenderer bottomColor;
    public GameObject groundCollider;
    public int bonusJumps;
    public bool isGrounded;
    public float jumpHeight;
    public float launchHeight;
    public float launchSpeed;
    public bool charged;

    

    private InputActionAsset inputAsset;
    private InputActionMap playerMap;

    private CombatManager combatManager;


    //Stick Input Stuff
    private Vector2 lStickSaved;
    private string stickAction;
    private bool flicking;

    // UI Stuff
    private GameObject controllerUI;
    private Image uArrow;
    private Image dArrow;
    private Image lArrow;
    private Image rArrow;
    private Image ulArrow;
    private Image urArrow;
    private Image dlArrow;
    private Image drArrow;


    //frame data
    [SerializeField]
    public float hitStun;
    [SerializeField]
    public float attackLag;


    public void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log(player.playerIndex);
    }
    
    private void Awake()
    {
        combatManager = this.gameObject.GetComponent<CombatManager>();
        playerControls = new SFBUNEWSYSTEM();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        playerMap = inputAsset.FindActionMap("Player");
        controllerUI = this.gameObject.transform.parent.Find("Controller").gameObject;
        uArrow = controllerUI.transform.Find("Up Arrow").GetComponent<Image>();
        dArrow = controllerUI.transform.Find("Down Arrow").GetComponent<Image>();
        lArrow = controllerUI.transform.Find("Left Arrow").GetComponent<Image>();
        rArrow = controllerUI.transform.Find("Right Arrow").GetComponent<Image>();
        ulArrow = controllerUI.transform.Find("Up Left Arrow").GetComponent<Image>();
        urArrow = controllerUI.transform.Find("Up Right Arrow").GetComponent<Image>();
        dlArrow = controllerUI.transform.Find("Down Left Arrow").GetComponent<Image>();
        drArrow = controllerUI.transform.Find("Down Right Arrow").GetComponent<Image>();





    }

    private void OnEnable()
    {
        playerControls.Enable();
        //actionReference.action.Enable();




        // left stick inputs for flick
        playerMap.FindAction("Left Stick").started += context => {
            StartCoroutine(stickSaver());
        }; 
        playerMap.FindAction("Left Stick").performed += context => {
            
            if(context.interaction is TapInteraction) StartCoroutine(stickFlicker());

        };

        //normal interaction left stick
        playerMap.FindAction("Left Stick").canceled += context => {
            if (context.interaction is HoldInteraction) stickAction = null;

        };




        // reset button
        playerMap.FindAction("North Button").performed += _ => Restet();





        //Jab Button Switch
        playerMap.FindAction("East Button").performed += _ =>
        {
            if (attackLag == 0 && hitStun == 0)
            {
                switch (stickAction)
                {
                    case "U":
                        break;

                    case "D":
                        
                        break;

                    case "L":
                        //StartCoroutine(combatManager.JabL());
                        break;

                    case "R":
                        StartCoroutine(combatManager.JabR());
                        break;

                    case "UL":
                        break;

                    case "UR":
                        break;

                    case "DL":
                        //StartCoroutine(combatManager.JabLowL());
                        break;

                    case "DR":
                        StartCoroutine(combatManager.JabLowR());
                        break;

                    case "FU":
                        break;

                    case "FD":
                        
                        break;

                    case "FL":
                        break;

                    case "FR":
                        break;

                    case "FUL":
                        break;

                    case "FUR":
                        break;

                    case "FDL":
                        //StartCoroutine(combatManager.UpRootL());
                        break;


                    case "FDR":
                        StartCoroutine(combatManager.UpRootR());
                        break;

                    default:
                        StartCoroutine(combatManager.JabR());
                        Debug.Log("Default");
                        break;

                };
            }

        };


            //jump button start
        playerMap.FindAction("South Button").started += context => 
        {
            //jump button started to press
            if (context.interaction is TapInteraction) StartedToCharge();
        };

            //jump button tap canceld , moved to charge
        playerMap.FindAction("South Button").canceled += context =>
        {
            //jump button held long enough for slow tap
            if (context.interaction is TapInteraction) ChargedJumpUI();
        };

            //released the jump button
        playerMap.FindAction("South Button").performed +=
            context =>
            {
                // call the jumps depending on interaction
                if (context.interaction is SlowTapInteraction)
                    TallJump();
                else
                    Jump();
            };





            // Launches
        playerMap.FindAction("Left Bumper").performed += context =>
        {
            //checks the left triggers interaction and calls apropriate function
            if (context.interaction is SlowTapInteraction) BigLeftLaunch();
            else LeftLaunch();

        };

        playerMap.FindAction("Right Bumper").performed += context =>
        {

            //checks the right triggers interaction and calls apropriate function
            if (context.interaction is SlowTapInteraction) BigRightLaunch();
            else RightLaunch();
        };

        playerMap.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        actionReference.action.Disable();
        playerMap.Disable();
    }

    private void Start()
    {
     
    }

    private void Update()
    {
        ///set left and right stick values
       move = playerMap.FindAction("Left Stick").ReadValue<Vector2>();
        playerMap.FindAction("Right Stick").ReadValue<Vector2>();
        if(flicking == false)
        {
            if (move.y <= -.8) stickAction = ("D");
            else if (move.y >= .8) stickAction = ("U");
            else if (move.x <= -.8) stickAction = ("L");
            else if (move.x >= .8) stickAction = ("R");
            else if (move.y <= -.5 && move.x <= -.5) stickAction = ("DL");
            else if (move.y >= .5 && move.x >= .5) stickAction = ("UR");
            else if (move.y <= -.5 && move.x >= .5) stickAction = ("DR");
            else if (move.y >= .5 && move.x <= -.5) stickAction = ("UL");
            else stickAction = null;
            
        }

        uArrow.color = new Color32(255, 255, 255, 100);
        lArrow.color = new Color32(255, 255, 255, 100);
        dArrow.color = new Color32(255, 255, 255, 100);
        rArrow.color = new Color32(255, 255, 255, 100);
        ulArrow.color = new Color32(255, 255, 255, 100);
        urArrow.color = new Color32(255, 255, 255, 100);
        dlArrow.color = new Color32(255, 255, 255, 100);
        drArrow.color = new Color32(255, 255, 255, 100);

        ControllerUI();
        
    }

    private void Jump()
    {
       
        
        //sets jump height
        Vector3 jumpV = new Vector3(0, jumpHeight, 0);

        //checks if you can jump -------------------------------------- this sets the color to red if not
        if (isGrounded == false && bonusJumps < 1) { bottomColor.material.color = new Color32(245, 255, 0, 143); return; }

        //cancels out downward movement
        playerRB.AddForce(new Vector3 (0f, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        //applys new jump force
        playerRB.AddForce(jumpV, ForceMode.VelocityChange);

        //subtracts bonus jump if applicable
        if (isGrounded == false) bonusJumps -= 1;

        //slows movement
        moveMultiplier = .85f;

        //returns color to yellow
        bottomColor.material.color = new Color32(245, 255, 0, 143);

    }

    private void TallJump()
    {
        
        
        //sets jump height
        Vector3 jumpV = new Vector3(0, 2*jumpHeight, 0);

        //sets the charged check to false
        charged = false;

        //checks if you can jump -------------------------------------- this sets the color to red if not
        if (isGrounded == false && bonusJumps < 1) { bottomColor.material.color = new Color32(245, 255, 0, 143); return; }

        //cancels out downward movement
        playerRB.AddForce(new Vector3(0f, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        //applys new jump force
        playerRB.AddForce(jumpV, ForceMode.VelocityChange);

        //subtracts bonus jump if applicable
        if (isGrounded == false) bonusJumps -= 1;

        //slows movement
        moveMultiplier = .55f;
        bottomColor.material.color = new Color32(245, 255, 0, 143);



    }

   private void ChargedJumpUI()
    {//set the charged bool for changing color if started in air without jumps
        charged = true;


        if (isGrounded == true || bonusJumps > 0)
        {
            //change color to blue if available
            bottomColor.material.color = new Color32(0, 0, 200, 143);
        }
    }

    private void StartedToCharge()
    {
        
        //check if can jump then set color to green
        if(isGrounded || bonusJumps > 0) bottomColor.material.color = new Color32(50, 255, 50, 143);
        else
            //sets color to red indicating no jump available
            bottomColor.material.color = new Color32(255, 0, 0, 143);

    }


    private void BigLeftLaunch()
    {

        //checks if you can jump
        if (isGrounded == false && bonusJumps < 1) return;

        //cancel out the downward velocity
        playerRB.AddForce(new Vector3(-playerRB.velocity.x, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        //add the launch force
        playerRB.AddForce(new Vector3(-launchSpeed * 1.5f, launchHeight * 2f, 0), ForceMode.VelocityChange);

        //subtacts bonus jump if in air
        if (isGrounded == false) bonusJumps -= 1;
    }

    private void LeftLaunch()
    {
        //checks if you can jump
        if (isGrounded == false && bonusJumps < 1) return;

        //canccel out current velocity
        playerRB.AddForce(new Vector3(-playerRB.velocity.x, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        //add launch force
        playerRB.AddForce(new Vector3(-launchSpeed, launchHeight, 0), ForceMode.VelocityChange);

        //subtracts bonus jump if aplicable
        if (isGrounded == false) bonusJumps -= 1;
    }


    private void BigRightLaunch()
    {
        //refer to left launch for explanation
        if (isGrounded == false && bonusJumps < 1) return;

        playerRB.AddForce(new Vector3(-playerRB.velocity.x, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        playerRB.AddForce(new Vector3(launchSpeed * 1.5f, launchHeight * 2f, 0), ForceMode.VelocityChange);
        if (isGrounded == false) bonusJumps -= 1;
    }
    private void RightLaunch()
    {
        //refer to left launch for explanation
        if (isGrounded == false && bonusJumps < 1) return;

        //cancel out the downward velocity
        playerRB.AddForce(new Vector3(-playerRB.velocity.x, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

       playerRB.AddForce(new Vector3(launchSpeed, launchHeight, 0), ForceMode.VelocityChange);
        if (isGrounded == false) bonusJumps -= 1;
    }
    private void FixedUpdate()
    {
        //call move
        Move();
    }

    private void Move()
    {
        //cancel out movement if changing direction
        if (move.x < 0f && playerRB.velocity.x > 0f && isGrounded == true) playerRB.AddForce(new Vector3(-playerRB.velocity.x, 0, 0), ForceMode.VelocityChange);
        if (move.x > 0f && playerRB.velocity.x < 0f && isGrounded == true) playerRB.AddForce(new Vector3(-playerRB.velocity.x, 0, 0), ForceMode.VelocityChange);

        //applys movement force
        playerRB.AddForce(new Vector3(move.x, 0, 0) * speed * moveMultiplier * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void Restet()
    {
        //resets velocity and position
        Debug.Log("reset");

        pl1 = this.transform.parent.Find("Cube");
        playerRB.velocity = new Vector3(0, 0, 0);
        pl1.position = new Vector3(0, 5, 0);
        pl1.rotation = new Quaternion(0, 0, 0, 0);
    }



    /// Input Command Stuff
    /// 

    private IEnumerator stickSaver()
    {
        // saves the vector value of the lefts stick right before a flick would be performed
        
        yield return new WaitForSeconds(0.005f);
        lStickSaved = playerMap.FindAction("Left Stick").ReadValue<Vector2>();
        yield return null;
    }

    private IEnumerator stickFlicker()
    {
        flicking = true;
        // sets a bool to change moves to their flicked counterpart 
        if (lStickSaved.y <= -.8) stickAction = ("FD");
        if (lStickSaved.y >= .8) stickAction = ("FU");
        if (lStickSaved.x <= -.8) stickAction = ("FL");
        if (lStickSaved.x >= .8) stickAction = ("FR");
        if (lStickSaved.y <= -.5 && lStickSaved.x <= -.5) stickAction = ("FDL");
        if (lStickSaved.y >= .5 && lStickSaved.x >= .5) stickAction = ("FUR");
        if (lStickSaved.y <= -.5 && lStickSaved.x >= .5) stickAction = ("FDR");
        if (lStickSaved.y >= .5 && lStickSaved.x <= -.5) stickAction = ("FUL");
        Debug.Log(stickAction);

        yield return new WaitForSeconds(0.14f);
        
        flicking = false;
        stickAction = null;
        lStickSaved = new Vector2(0.0f ,0.0f);
        
        yield return null;
    }

    void ControllerUI()
    {
        switch(stickAction)
        {   
            case "U":
                uArrow.color = new Color32(255, 255, 255, 220);
                break;
            case "D":
                dArrow.color = new Color32(255, 255, 255, 220);
                break;
            case "L":
                lArrow.color = new Color32(255, 255, 255, 220);
                break;
            case "R":
                rArrow.color = new Color32(255, 255, 255, 220);
                break;
            case "UL":
                ulArrow.color = new Color32(255, 255, 255, 220);
                break;
            case "UR":
                urArrow.color = new Color32(255, 255, 255, 220);
                break;
            case "DL":
                dlArrow.color = new Color32(255, 255, 255, 220);
                break;
            case "DR":
                drArrow.color = new Color32(255, 255, 255, 220);
                break;
            case "FU":
                uArrow.color = new Color32(255, 100, 100, 220);
                break;
            case "FD":
                dArrow.color = new Color32(255, 100, 100, 220);
                break;
            case "FL":
                lArrow.color = new Color32(255, 100, 100, 220);
                break;
            case "FR":
                rArrow.color = new Color32(255, 100, 100, 220);
                break;
            case "FUL":
                ulArrow.color = new Color32(255, 100, 100, 220);
                break;
            case "FUR":
                urArrow.color = new Color32(255, 100, 100, 220);
                break;
            case "FDL":
                dlArrow.color = new Color32(255, 100, 100, 220);
                break;
            case "FDR":
                drArrow.color = new Color32(255, 100, 100, 220);
                break;
            default:
                uArrow.color = new Color32(255, 255, 255, 100);
                lArrow.color = new Color32(255, 255, 255, 100);
                dArrow.color = new Color32(255, 255, 255, 100);
                rArrow.color = new Color32(255, 255, 255, 100);
                ulArrow.color = new Color32(255, 255, 255, 100);
                urArrow.color = new Color32(255, 255, 255, 100);
                dlArrow.color = new Color32(255, 255, 255, 100);
                drArrow.color = new Color32(255, 255, 255, 100);
                break;
        }
    }

    public  IEnumerator AttackLag()
    {
        yield return new WaitForSeconds(attackLag);
        attackLag = 0;
        yield return null;
    }
}

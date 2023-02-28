using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    
    private SFBUNEWSYSTEM playerControls;
    [SerializeField]
    private PlayerInput Player;
    [SerializeField]
    private InputActionReference actionReference;

    public GameObject pl1;
    public Rigidbody playerRB;

    private Vector2 move;
    private Vector2 aim;
    public float speed;
    public static float moveMultiplier;

    public MeshRenderer bottomColor;
    public GameObject groundCollider;
    public static int bonusJumps;
    public static bool isGrounded;
    public float jumpHeight;
    public float launchHeight;
    public float launchSpeed;
    public static bool charged;

    private int playerNum;
    private string actionMap;

    private InputActionAsset inputAsset;
    private InputActionMap playerMap;
    


    public void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log(player.playerIndex);
    }
    
    private void Awake()
    {
        playerControls = new SFBUNEWSYSTEM();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        playerMap = inputAsset.FindActionMap("Player");

        
        

    }

    private void OnEnable()
    {
        playerControls.Enable();
        //actionReference.action.Enable();
        
        

        playerControls.Player.NorthButton.performed += _ => Restet();

        playerMap.FindAction("South Button").started += context =>  // DO THIS TO THE REST BIIIIIIIAATTTTTCCHHHHHHHHHHHHHHHHs
        {
            Debug.Log("StartedToJump");
            //jump button started to press
            if (context.interaction is TapInteraction) StartedToCharge();
        };
        playerControls.Player.SouthButton.canceled += context =>
        {
            //jump button held long enough for slow tap
            if (context.interaction is TapInteraction) ChargedJumpUI();
        };

        playerControls.Player.SouthButton.performed +=
            context =>
            {
                // call the jumps depending on interaction
                if (context.interaction is SlowTapInteraction)
                    TallJump();
                else
                    Jump();
            };

        playerControls.Player.LeftBumper.performed += context =>
        {
            //checks the left triggers interaction and calls apropriate function
            if (context.interaction is SlowTapInteraction) BigLeftLaunch();
            else LeftLaunch();

        };

        playerControls.Player.RightBumper.performed += context =>
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
        Debug.Log(inputAsset);
        Debug.Log(playerMap);
    }

    private void Update()
    {
        ///set left and right stick values
       move = playerControls.Player.LeftStick.ReadValue<Vector2>();
        aim = playerControls.Player.RightStick.ReadValue<Vector2>();
        
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
        playerRB.velocity = new Vector3(0, 0, 0);
        pl1.transform.position = new Vector3(0, 5, 0);
        pl1.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

}

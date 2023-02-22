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

    
    public Rigidbody playerRB;

    public Vector2 move;
    public Vector2 aim;
    public float speed;

    public MeshRenderer bottomColor;
    public GameObject groundCollider;
    public static int bonusJumps;
    public static bool isGrounded;
    public float jumpHeight;
    public float launchHeight;
    public float launchSpeed;
    public static bool charged;

    private void Awake()
    {
        playerControls = new SFBUNEWSYSTEM();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        actionReference.action.Enable();

        
        playerControls.Player.SouthButton.started += context =>
        {
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
                // call the jumps
                if (context.interaction is SlowTapInteraction)
                    TallJump();
                else
                    Jump();
            };

        playerControls.Player.LeftBumper.performed += context =>
        {
            if (context.interaction is SlowTapInteraction) BigLeftLaunch();
            else LeftLaunch();

        };

        playerControls.Player.RightBumper.performed += context =>
        {
            if (context.interaction is SlowTapInteraction) BigRightLaunch();
            else RightLaunch();
        };


    }

    private void OnDisable()
    {
        playerControls.Disable();
        actionReference.action.Disable();
    }

    private void Start()
    {
        
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

        //checks if you can jump
        if (isGrounded == false && bonusJumps < 1) { bottomColor.material.color = new Color32(245, 255, 0, 143); return; }

        //cancels out downward movement
        playerRB.AddForce(new Vector3 (0f, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        //applys new jump force
        playerRB.AddForce(jumpV, ForceMode.VelocityChange);

        //subtracts bonus jump if applicable
        if (isGrounded == false) bonusJumps -= 1;
        bottomColor.material.color = new Color32(245, 255, 0, 143);

    }

    private void TallJump()
    {
        
        Debug.Log("Attempting to Talljump");
        //sets jump height
        Vector3 jumpV = new Vector3(0, 2*jumpHeight, 0);

        //sets the charged check to false
        charged = false;

        //checks if you can jump
        if (isGrounded == false && bonusJumps < 1) { bottomColor.material.color = new Color32(245, 255, 0, 143); return; }

        //cancels out downward movement
        playerRB.AddForce(new Vector3(0f, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        //applys new jump force
        playerRB.AddForce(jumpV, ForceMode.VelocityChange);

        //subtracts bonus jump if applicable
        if (isGrounded == false) bonusJumps -= 1;
        bottomColor.material.color = new Color32(245, 255, 0, 143);



    }

   private void ChargedJumpUI()
    {//set the charged bool for changing color if started in air without jumps
        charged = true;
        if (isGrounded == true || bonusJumps > 0)
        {
            //change color to blue
            Debug.Log("FullyCharged");
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
        if (isGrounded == false && bonusJumps < 1) return;

            Debug.Log("ChargedLeftLaunch");
        //cancel out the downward velocity
        playerRB.AddForce(new Vector3(-playerRB.velocity.x, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        playerRB.AddForce(new Vector3(-launchSpeed * 1.5f, launchHeight * 2f, 0), ForceMode.VelocityChange);
        if (isGrounded == false) bonusJumps -= 1;
    }

    private void LeftLaunch()
    {

        if (isGrounded == false && bonusJumps < 1) return;


        Debug.Log("LeftLaunch");
        playerRB.AddForce(new Vector3(-playerRB.velocity.x, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        playerRB.AddForce(new Vector3(-launchSpeed, launchHeight, 0), ForceMode.VelocityChange);
        if (isGrounded == false) bonusJumps -= 1;
    }


    private void BigRightLaunch()
    {

        if (isGrounded == false && bonusJumps < 1) return;


        Debug.Log("ChargedRightLaunch");
        //cancel out the downward and horizontal velocity
        playerRB.AddForce(new Vector3(-playerRB.velocity.x, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        playerRB.AddForce(new Vector3(launchSpeed * 1.5f, launchHeight * 2f, 0), ForceMode.VelocityChange);
        if (isGrounded == false) bonusJumps -= 1;
    }
    private void RightLaunch()
    {

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

        playerRB.AddForce(new Vector3(move.x, 0, 0) * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

}

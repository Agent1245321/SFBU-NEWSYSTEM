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

    public MeshRenderer bottomColor;
    public Rigidbody playerRB;

    public Vector2 move;
    public Vector2 aim;
    public float speed;

    public GameObject groundCollider;
    public static int bonusJumps;
    public static bool isGrounded;
    public float jumpHeight;

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
            if (context.interaction is TapInteraction) StartedToCharge();
        };
        playerControls.Player.SouthButton.canceled += context =>
        {
            if (context.interaction is TapInteraction) ChargedJump();
        };

        playerControls.Player.SouthButton.performed +=
            context =>
            {
                if (context.interaction is SlowTapInteraction)
                    TallJump();
                else
                    Jump();
            };
            

    }

    private void OnDisable()
    {
        playerControls.Disable();
        actionReference.action.Disable();
    }

    private void Start()
    {
        if (playerControls.Player.SouthButton.interactions.Contains("Hold")) Debug.Log("Hold Exists");
    }

    private void Update()
    {
       move = playerControls.Player.LeftStick.ReadValue<Vector2>();
        aim = playerControls.Player.RightStick.ReadValue<Vector2>();
        
    }

    private void Jump()
    {
        Debug.Log("Attempting to jump");
        //sets jump height
        Vector3 jumpV = new Vector3(0, jumpHeight, 0);
        
        //checks if you can jump
        if(isGrounded == false && bonusJumps < 1) return;

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
        Debug.Log("Attempting to jump");
        //sets jump height
        Vector3 jumpV = new Vector3(0, 2*jumpHeight, 0);

        //checks if you can jump
        if (isGrounded == false && bonusJumps < 1) return;

        //cancels out downward movement
        playerRB.AddForce(new Vector3(0f, -playerRB.velocity.y, 0f), ForceMode.VelocityChange);

        //applys new jump force
        playerRB.AddForce(jumpV, ForceMode.VelocityChange);

        //subtracts bonus jump if applicable
        if (isGrounded == false) bonusJumps -= 1;

        bottomColor.material.color = new Color32(245, 255, 0, 143);

    }

   private void ChargedJump()
    {
        Debug.Log("FullyCharged");
        bottomColor.material.color = new Color32(0, 0, 200, 143);
    }

    private void StartedToCharge()
    {
        Debug.Log("StartingToJump");
        bottomColor.material.color = new Color32(50, 255, 50, 143);
    }
    private void FixedUpdate()
    {
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

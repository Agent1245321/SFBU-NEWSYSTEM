using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private SFBUNEWSYSTEM playerControls;
    [SerializeField]
    private PlayerInput Player;
    private InputActionReference LeftStick, RightStick, LeftBumper, RightBumper, NorthButton, EastButton, SouthButton, WestButton;

    private void Awake()
    {
        playerControls = new SFBUNEWSYSTEM();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector2 move = playerControls.Player.LeftStick.ReadValue<Vector2>();
        Vector2 aim = playerControls.Player.RightStick.ReadValue<Vector2>();
        
        
        /*
        playerControls.Player.SouthButton.ReadValue<float>();
        if (playerControls.Player.SouthButton.ReadValue<float>() == 1) Debug.Log("South Button");

        playerControls.Player.NorthButton.ReadValue<float>();
        if (playerControls.Player.NorthButton.ReadValue<float>() == 1) Debug.Log("North Button");

        playerControls.Player.EastButton.ReadValue<float>();
        if (playerControls.Player.EastButton.ReadValue<float>() == 1) Debug.Log("East Button");

        playerControls.Player.WestButton.ReadValue<float>();
        if (playerControls.Player.WestButton.ReadValue<float>() == 1) Debug.Log("West Button");
        */
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    
    private PlayerMotor motor;
    private PlayerLook look;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        // Hold to crouch
    onFoot.Crouch.performed += ctx => motor.Crouch(true);
    onFoot.Crouch.canceled += ctx => motor.Crouch(false);

    // Hold to sprint
    onFoot.Sprint.performed += ctx => motor.Sprint(true);
    onFoot.Sprint.canceled += ctx => motor.Sprint(false);

    onFoot.Jump.performed += ctx => motor.Jump();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.processMove(onFoot.Movement.ReadValue<Vector2>());
    }
    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());   
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();        
    }
} 

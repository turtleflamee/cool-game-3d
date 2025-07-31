using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private UnityEngine.Vector3 PlayerVelocity;
    private bool IsGrounded;
    public float gravity = -9.81f;
    public float speed = 5.0f;

    public float CurrentJumps;


    public float JumpNumber = 1f;
    public float jumpHeight = 3.0f;

    private bool crouching = false;
    private bool sprinting = false;
    private float crouchTimer = 0f;
    private bool lerpCrouch = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        CurrentJumps = JumpNumber;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / .5f;
            p *= p; // ease in
            if (crouching)

                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }


        }
        if (IsGrounded)
        {
            CurrentJumps = JumpNumber; // Reset jumps when grounded
        }

    }
    public void Crouch(bool isCrouching)
    {
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }

    public void Sprint(bool isSprinting)
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8f;
        else
            speed = 5f;
    }
    public void processMove(UnityEngine.Vector2 input)
    {
        UnityEngine.Vector3 moveDirection = UnityEngine.Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        PlayerVelocity.y += gravity * Time.deltaTime;
        if (IsGrounded && PlayerVelocity.y < 0)
            PlayerVelocity.y = -2f;
        controller.Move(PlayerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (IsGrounded)
        {
            // Ground jump: reset air jumps and jump
            CurrentJumps = JumpNumber - 2; // Use one jump, leave the rest for air
            IsGrounded = false;
            PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (CurrentJumps > 0)
        {
            // Air jump: use one air jump
            CurrentJumps -= 1;
            PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}

using UnityEngine;

public class JumpButton : Interactable
{
    private PlayerMotor motor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        motor = FindFirstObjectByType<PlayerMotor>();
    }

    // Update is called once per frame
    protected override void Interact()
    {
        
            motor.JumpNumber += 1.0f; // Optionally increase allowed jumps 
            Debug.Log("JumpNumber is now: " + motor.JumpNumber);
        
    }
}

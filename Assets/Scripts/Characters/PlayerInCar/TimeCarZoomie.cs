using UnityEngine;

public class TimeCarZoomie : MonoBehaviour
{
    public float MinimumSpeed = 150f;
    public float DefaultSpeed = 300f;
    public float MaximumSpeed = 600f;

    public WheelJoint2D frontWheel;
    public WheelJoint2D backWheel;

    public Rigidbody2D rb;
    public float jumpForce = 200f;


    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            // forward boost
            AdjustWheelSpeed(backWheel, MaximumSpeed);
            AdjustWheelSpeed(frontWheel, MaximumSpeed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            // brakes!
            AdjustWheelSpeed(backWheel, MinimumSpeed);
            AdjustWheelSpeed(frontWheel, MinimumSpeed);
        }
        else
        {
            // just cruisin
            AdjustWheelSpeed(backWheel, DefaultSpeed);
            AdjustWheelSpeed(frontWheel, DefaultSpeed);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }


    private void AdjustWheelSpeed(WheelJoint2D targetWheelJoint, float targetSpeed)
    {
        JointMotor2D motor = targetWheelJoint.motor; // Get the current motor settings
        motor.motorSpeed = targetSpeed; // Modify the speed
        targetWheelJoint.motor = motor; // Assign the updated motor back
    }
    
}

using UnityEngine;

public class TimeCarZoomie : MonoBehaviour
{
    public float MaximumSpeedRight = 200f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Clamp the horizontal speed.
        if (rb.linearVelocityX > MaximumSpeedRight)
        {
            rb.linearVelocityX = MaximumSpeedRight;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // move forward automagically

        // jump and stuff

    }
}

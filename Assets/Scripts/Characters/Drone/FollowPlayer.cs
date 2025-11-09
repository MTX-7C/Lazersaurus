using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float damping = 0.3f;
    float t = 0;
    Cooldown moveCooldown;
    Player player;

    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 cuteOffset;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        moveCooldown = new Cooldown(3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.deltaTime;
        Vector3 targetPosition = target.position + offset + cuteOffset;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);

        offset = new Vector3(0, BoundedSin(-0.2f, 0.2f, t, 2 * Mathf.PI));

        if (player.rb.linearVelocity.magnitude <= 0.5f)
        {
            if (!moveCooldown.isCoolingDown)
            {
                moveCooldown.StartCooldown();
                cuteOffset = new Vector3(Random.Range(-1f, 4f), Random.Range(-1f, 2f), 0);
                if (transform.position.x - target.position.x > cuteOffset.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                } else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        } else
        {
            cuteOffset = Vector3.zero;
            if (transform.position.x - target.position.x > cuteOffset.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    public float BoundedSin(float min, float max, float t, float speed)
    {
        float difference = max - min;
        return (difference * Mathf.Sin(t * speed)) / 2 + max - difference / 2;
    }
}

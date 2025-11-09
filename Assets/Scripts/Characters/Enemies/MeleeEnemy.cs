using UnityEngine;

public class MeleeEnemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float maxHealth { get; set; } = 100;
    public float currentHealth { get; set; }
    [field: SerializeField] public bool invincible { get; set; } = false;

    [SerializeField] Animator animator;

    [SerializeField] GameObject graphicsLayer;

    [SerializeField] CircleCollider2D aggroRadius;

    [SerializeField] float moveSpeed;

    Rigidbody2D rb;

    bool inAttackRange = false;

    int lookDirection = 1;

    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        animator = graphicsLayer.GetComponent<Animator>();
        player = FindFirstObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inAttackRange)
        {
            animator.SetBool("attackRange", true);
            rb.linearVelocityX = 0;
        } else
        {
            animator.SetBool("attackRange", false);
            SetLookDirection();

            graphicsLayer.transform.localScale = new Vector3(lookDirection, 1, 1);

            rb.linearVelocityX = -lookDirection * moveSpeed;
        }
    }

    public void SetLookDirection()
    {
        if (player.gameObject.transform.position.x > transform.position.x)
        {
            lookDirection = -1;
        }
        else
        {
            lookDirection = 1;
        }
    }

    public void Damage(float damage)
    {
        if (!invincible)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                TriggerDeath();
            }
            Debug.Log(currentHealth);
        }
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("die");
        player.currentHealth += 30;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inAttackRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inAttackRange = false;
        }
    }
}

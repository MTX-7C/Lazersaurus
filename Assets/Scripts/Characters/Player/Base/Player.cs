using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public TextMeshProUGUI demoText;

    #region IDamagable Variables
    [field : SerializeField] public float maxHealth { get; set; } = 100;
    public float currentHealth { get; set; }
    [field : SerializeField] public bool invincible { get; set; } = false;
    #endregion

    public Rigidbody2D rb;


    #region Modifiers
    public float facedDirection = 1;
    public float speed = 5;
    public float glideFallSpeed = 2;
    public float jumpHeight = 5;
    public float defaultGravityScale = 5;
    public float dashTime = 0.25f;
    public float dashSpeedMultiplier = 2;
    public int dashCount = 1;
    public float cayoteTime = 0.15f;
    #endregion

    #region Cooldowns
    [HideInInspector] public Cooldown dashCooldownTracker = new Cooldown(0.5f);
    [HideInInspector] public Cooldown jumpBuffer = new Cooldown(0.15f);

    #endregion

    public Vector2 moveDirection = Vector2.zero;

    #region player state bools
    public bool isGrounded;
    public bool isGliding = false;
    #endregion

    #region State Machine Instances
    public PlayerStateMachine stateMachine { get; set; }
    public PlayerGroundedState groundedState { get; set; }
    public PlayerJumpingState jumpingState { get; set; }
    public PlayerGlidingState glidingState { get; set; }
    public PlayerSwimmingState swimmingState { get; set; }

    public PlayerDashState dashState { get; set; }
    #endregion

    #region Animation trigger handlers
    public enum AnimationTriggerType
    {
        PlayerDamaged,
        PlayFootstepSound
    }

    public void AnimationTriggerEvent(AnimationTriggerType type)
    {
        stateMachine.currentPlayerState.AnimationTriggerEvent(type);
    }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        
        groundedState = new PlayerGroundedState(this, stateMachine);
        jumpingState = new PlayerJumpingState(this, stateMachine);
        glidingState = new PlayerGlidingState(this, stateMachine);
        swimmingState = new PlayerSwimmingState(this, stateMachine);
        dashState = new PlayerDashState(this, stateMachine);
    }

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(groundedState);
    }

    void Update()
    {
        stateMachine.currentPlayerState.FrameUpdate();

        demoText.text = "Current State: " + stateMachine.currentPlayerState.GetType().Name;
    }

    private void FixedUpdate()
    {
        stateMachine.currentPlayerState.PhysicsUpdate();
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
        }
    }

    public void TriggerDeath()
    {
        Debug.Log("YOU DIED");
    }

    public float CalculateJumpVelocity()
    {
        float v = Mathf.Sqrt(20 * rb.gravityScale * jumpHeight);
        return v;
    }
}

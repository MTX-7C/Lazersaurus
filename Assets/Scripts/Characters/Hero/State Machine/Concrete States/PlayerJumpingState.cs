using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState
{
    Cooldown cayoteTimeCooldown;
    public PlayerJumpingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();
        player.rb.gravityScale = player.defaultGravityScale;
        cayoteTimeCooldown = new Cooldown(player.cayoteTime);
        cayoteTimeCooldown.StartCooldown();
    }

    public override void ExitState()
    {
        base.ExitState();
        player.rb.gravityScale = player.defaultGravityScale;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (player.isGliding)
        {
            player.stateMachine.ChangeState(player.glidingState);
        }
        if (player.isGrounded)
        {
            player.stateMachine.ChangeState(player.groundedState);
        }
        if (player.rb.linearVelocity.y < 0)
        {
            player.rb.gravityScale = player.defaultGravityScale * 1.5f;
        }
        Move();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void OnAttack()
    {
        base.OnAttack();
    }
    public override void Move()
    {
        player.rb.linearVelocity = new Vector2(player.moveDirection.x * player.speed, player.rb.linearVelocity.y);
    }

    public override void Jump(bool performed)
    {
        base.Jump(performed);
        float cutVelocity = player.CalculateJumpVelocity() / 3;
        if (!performed)
        {
            if (player.rb.linearVelocity.y >= cutVelocity)
            {
                player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, cutVelocity);
            }
        }
        if (performed)
        {
            if (cayoteTimeCooldown.isCoolingDown)
            {
                player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.CalculateJumpVelocity());
            } else
            {
                player.jumpBuffer.StartCooldown();
            }
        }
    }
}

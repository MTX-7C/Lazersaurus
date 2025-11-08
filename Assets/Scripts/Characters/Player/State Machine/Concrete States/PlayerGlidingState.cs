using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlidingState : PlayerState
{
    public PlayerGlidingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();
        player.rb.gravityScale = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.rb.gravityScale = player.defaultGravityScale;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (!player.isGliding)
        {
            player.stateMachine.ChangeState(player.jumpingState);
        }
        if (player.isGrounded)
        {
            player.stateMachine.ChangeState(player.groundedState);
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
        player.rb.linearVelocity = new Vector2(player.moveDirection.x * player.speed, -player.glideFallSpeed);
    }
}

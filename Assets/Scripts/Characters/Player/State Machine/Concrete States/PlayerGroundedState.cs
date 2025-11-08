using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();
        player.isGliding = false;
        if (player.jumpBuffer.isCoolingDown)
        {
            Jump(true);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Move();
        if (!player.isGrounded)
        {
            player.stateMachine.ChangeState(player.jumpingState);
        }
        player.dashCount = 1;
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
        if (performed)
        {
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.CalculateJumpVelocity());
        }
    }
}

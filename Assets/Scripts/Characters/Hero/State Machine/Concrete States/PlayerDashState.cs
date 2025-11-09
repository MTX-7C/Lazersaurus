using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    float timer;
    float direction;
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Dash");
        timer = player.dashTime;
        direction = player.facedDirection;
        player.invincible = true;
        player.dashCount -= 1;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.invincible = false;
        player.dashCooldownTracker.StartCooldown();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer -= Time.deltaTime;
        Move();
        if (timer <= 0)
        {
            player.stateMachine.ChangeState(player.jumpingState);
        }
    }

    public override void Jump(bool performed)
    {
        base.Jump(performed);
    }

    public override void Move()
    {
        base.Move();
        player.rb.linearVelocity = new Vector2(player.dashSpeedMultiplier * player.speed * direction, 0);
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

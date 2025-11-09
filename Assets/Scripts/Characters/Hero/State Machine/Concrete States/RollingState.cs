using UnityEngine;

public class RollingState : PlayerState
{
    float initialRollSpeed = 0;
    public RollingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
        
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();
        initialRollSpeed = player.speed * player.facedDirection;
        player.animator.SetFloat("rollSpeed", 1);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.animator.SetBool("rolling", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (player.rolling != true)
        {
            player.stateMachine.ChangeState(player.groundedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.rb.linearVelocityX += player.rollAcceleration * Time.fixedDeltaTime * player.facedDirection;
        player.animator.SetFloat("rollSpeed", player.animator.GetFloat("rollSpeed") + player.rollAcceleration * Time.fixedDeltaTime * 0.2f);
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }
}

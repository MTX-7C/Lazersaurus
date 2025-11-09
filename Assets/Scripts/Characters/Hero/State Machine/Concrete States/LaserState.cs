using UnityEngine;

public class LaserState : PlayerState
{
    Laser laser;
    public LaserState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();
        laser = player.gameObject.GetComponent<Laser>();
        laser.active = true;
    }

    public override void ExitState()
    {
        base.ExitState();
        laser.active = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.rb.linearVelocityX = -0.2f * player.facedDirection;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }
}

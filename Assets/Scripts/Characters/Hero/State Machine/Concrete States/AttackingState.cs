using UnityEngine;

public class AttackingState : PlayerState
{
    AttackManager attackManager;
    public AttackingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
        attackManager = player.gameObject.GetComponent<AttackManager>();
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("ATTACKING!");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("(no longer) ATTACKING!");
        
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (!attackManager.attacking)
        {
            player.stateMachine.ChangeState(player.groundedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.rb.linearVelocityX = 0f;

    }

    public override void OnAttack()
    {
        base.OnAttack();
    }
}

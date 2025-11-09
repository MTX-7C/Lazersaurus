using UnityEngine;

public class heavy_melee : StateMachineBehaviour
{
    AttackManager attackManager;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackManager = animator.gameObject.GetComponent<AttackScriptAccessor>().attackManager;
        attackManager.attacking = true;
        attackManager.attackQueued = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackManager.attacking = false;
        if (attackManager.comboFinished)
        {
            attackManager.comboCooldown.StartCooldown();
        }
        attackManager.TriggerNextAttack();
        attackManager.comboTime.StartCooldown();
    }

}

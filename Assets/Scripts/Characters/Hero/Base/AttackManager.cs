using UnityEngine;
using System.Collections.Generic;
public class AttackManager : MonoBehaviour
{
    public Cooldown comboTime;
    public Cooldown comboCooldown;
    public List<string> attacks = new List<string>();
    public bool attacking = false;
    public bool attackQueued = false;
    public int currentAttackIndex = 0;
    public bool comboFinished = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        comboTime = new Cooldown(0.1f);
        comboCooldown = new Cooldown(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking && !comboTime.isCoolingDown)
        {
            currentAttackIndex = 0;
        }
        if (!(currentAttackIndex < attacks.Count))
        {
            currentAttackIndex = 0;
            comboCooldown.StartCooldown();
        }
    }

    public void Attack()
    {
        if (!comboCooldown.isCoolingDown)
        {
            if (!attackQueued && attacking)
            {
                attackQueued = true;
                if (currentAttackIndex < attacks.Count)
                {
                    currentAttackIndex++;
                }
            }
            if (!attacking)
            {
                GetComponent<Player>().animator.SetTrigger(attacks[currentAttackIndex]);
            }
            
        }
    }

    public void TriggerNextAttack()
    {
        if (attackQueued)
        {
            GetComponent<Player>().animator.SetTrigger(attacks[currentAttackIndex]);
        }
    }
}

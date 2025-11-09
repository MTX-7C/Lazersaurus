using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    Controls controls;
    public Player player;
    AttackManager attackManager;
    // Start is called before the first frame update
    void Start()
    {
        attackManager = GetComponent<AttackManager>();
        player = GetComponent<Player>();

        controls = new Controls();
        controls.Gameplay.Enable();

        controls.Gameplay.Movement.performed += ctx => On_Move(ctx);
        controls.Gameplay.Movement.canceled += ctx => On_Move(ctx);

        controls.Gameplay.Jump.performed += ctx => On_Jump(ctx);
        controls.Gameplay.Jump.canceled += ctx => On_Jump(ctx);

        controls.Gameplay.Glide.performed += ctx => On_Glide(ctx);
        controls.Gameplay.Glide.canceled += ctx => On_Glide(ctx);

        controls.Gameplay.Dash.performed += ctx => On_Dash(ctx);
        controls.Gameplay.Dash.canceled += ctx => On_Dash(ctx);

        controls.Gameplay.MeleeAttack.performed += ctx => On_Attack(ctx);
        controls.Gameplay.MeleeAttack.canceled += ctx => On_Attack(ctx);

        controls.Gameplay.Roll.performed += ctx => On_Roll(ctx);
        controls.Gameplay.Roll.canceled += ctx => On_Roll(ctx);

        controls.Gameplay.Laser.performed += ctx => On_Laser(ctx);
        controls.Gameplay.Laser.canceled += ctx => On_Laser(ctx);
    }

    public void On_Move(InputAction.CallbackContext value)
    {
        player.moveDirection = value.ReadValue<Vector2>();
        if (value.ReadValue<Vector2>().x != 0 && (attackManager.attacking != true && !player.rolling && !player.laser))
        {
            player.facedDirection = value.ReadValue<Vector2>().x;
            player.animator.SetBool("moving", true);
        } else
        {
            player.animator.SetBool("moving", false);
        }
    }

    public void On_Jump(InputAction.CallbackContext value)
    {

        
        player.stateMachine.currentPlayerState.Jump(value.performed);
        
    }

    public void On_Glide(InputAction.CallbackContext value)
    {
        //player.isGliding = value.performed;
    }

    public void On_Dash(InputAction.CallbackContext value)
    {
        if (player.dashCooldownTracker.isCoolingDown) { return; }

        if (player.dashCount > 0)
        {
            if (value.performed)
            {
                player.stateMachine.ChangeState(player.dashState);
            }
        }
        Debug.Log("Dashes Remaining: " + player.dashCount);
    }

    public void On_Attack(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            attackManager.Attack();
        }
    }

    public void On_Roll(InputAction.CallbackContext value)
    {
        if (value.performed && !attackManager.attacking)
        {
            player.rolling = true;
            player.stateMachine.ChangeState(player.rollingState);
            player.animator.SetBool("rolling", player.rolling);
        } else
        {
            player.rolling = false;
        }
    }

    public void On_Laser(InputAction.CallbackContext value)
    {
        if (!attackManager.attacking && !player.rolling && value.performed) 
        { 
            player.laser = true;
            player.animator.SetBool("laser", true);
            player.stateMachine.ChangeState(player.laserState);
        } else
        {
            player.laser = false;
            player.animator.SetBool("laser", false);
            player.stateMachine.ChangeState(player.groundedState);
        }
    }
}

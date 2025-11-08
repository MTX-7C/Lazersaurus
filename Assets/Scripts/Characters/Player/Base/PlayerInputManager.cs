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
    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void On_Move(InputAction.CallbackContext value)
    {
        player.moveDirection = value.ReadValue<Vector2>();
        if (value.ReadValue<Vector2>().x != 0)
        {
            player.facedDirection = value.ReadValue<Vector2>().x;
        }
    }

    public void On_Jump(InputAction.CallbackContext value)
    {

        
        player.stateMachine.currentPlayerState.Jump(value.performed);
        
    }

    public void On_Glide(InputAction.CallbackContext value)
    {
        player.isGliding = value.performed;
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
}

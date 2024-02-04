using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MovementController
{
    [SerializeField]
    private UIManager _uiManager;
    public void Move(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            CallOnMove(context.ReadValue<Vector2>());
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            CallOnMove(Vector2.zero);
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        CallOnLook(context.ReadValue<Vector2>());
    }

    public void Jump()
    {
        CallOnJump();
    }

    public void Attack()
    {
        CallOnAttack();
    }

    public void OnInventory()
    {
        var inven = _uiManager.GetUI<Inventory>();
        if(inven.gameObject.activeSelf)
        {
            inven.Disable();
        }else
        {
            inven.Active();
        }
    }
}

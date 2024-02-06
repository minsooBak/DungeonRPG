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
        if (inven.prefab.activeSelf)
        {
            inven.prefab.SetActive(false);
        }
        else
        {
            inven.prefab.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Item"))
        {
            _uiManager.GetUI<Inventory>().AddItem(other.GetComponent<Item>().item);
            Destroy(other.gameObject);
        }    
    }
}

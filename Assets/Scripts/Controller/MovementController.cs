using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action OnAttack;
    public event Action<Vector2> OnLook;
    public event Action OnJump;

    protected void CallOnMove(Vector2 dir)
    {
        OnMove?.Invoke(dir);
    }

    protected void CallOnAttack()
    {
        OnAttack?.Invoke();
    }

    protected void CallOnLook(Vector2 delta)
    {
        OnLook?.Invoke(delta);
    }

    protected void CallOnJump()
    {
        OnJump?.Invoke();
    }
}

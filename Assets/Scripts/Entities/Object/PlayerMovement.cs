using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovementController _controller;
    public MovementObjectStat stat;
    private Rigidbody2D _rigidbody;
    private Camera _camera;
    private SpriteRenderer _spriteRenderer;

    Vector3 dir;


    private void FixedUpdate()
    {
        UpdateMove();
    }

    private void Awake()
    {
        _controller = GetComponent<MovementController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _camera = Camera.main;

        _controller.OnMove += Move;
        _controller.OnLook += Look;
    }

    private void Move(Vector2 dir)
    {
        this.dir = dir.normalized;
    }

    private void Look(Vector2 data)
    {
        Vector2 wolrdPos = _camera.ScreenToViewportPoint(data - (Vector2)transform.position);
        _spriteRenderer.flipX = wolrdPos.x > 0.5 ? false : true;
    }

    private void UpdateMove()
    {
        _rigidbody.velocity = dir * (stat.speed * Time.fixedDeltaTime);
    }
}

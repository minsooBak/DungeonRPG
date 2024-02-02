using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Camera _camera;
    public float speed = 5f;
    [SerializeField]private Vector3 cameraPos;
    public Vector2 mapSize;
    public Vector2 center;
    private float height;
    private float width;

    protected void Awake()
    {
        _camera = GetComponent<Camera>();
        cameraPos = _camera.transform.position;
        height = _camera.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    protected void FixedUpdate()
    {
        CameraLimitMove();
    }

    void CameraLimitMove()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + cameraPos, Time.fixedDeltaTime * speed);

        float x = mapSize.x - width;
        float y = mapSize.y - height;
        float cX = Mathf.Clamp(transform.position.x, -x + center.x, x + center.x);
        float cY = Mathf.Clamp(transform.position.y, -y + center.y, y + center.y);

        transform.position = new Vector3(cX, cY, cameraPos.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}

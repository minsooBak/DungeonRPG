using Structs;
using UnityEngine;

public class Item : MonoBehaviour
{
    //[SerializeField] private float jumpPower = 50f;
    [SerializeField] private ItemDataManager _itemManager;
    [SerializeField] private float startY;
    [SerializeField] private string _name;
    [HideInInspector] public ItemBase item;

    private float time;

    private void Awake()
    {
        item = _itemManager.GetItem(_name);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < startY)
        {
            transform.position = new Vector3(transform.position.x, startY, 0);
        }
        else
        {
            time += 0.1f * Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startY, 0), time);
        }

    }

    private void Start()
    {
        startY = transform.position.y - 2f;
    }
}

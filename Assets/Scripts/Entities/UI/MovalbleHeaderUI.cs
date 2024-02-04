using UnityEngine;
using UnityEngine.EventSystems;

public class MovalbleHeaderUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector2 _startPoint;
    private Vector2 _endPoint;

    private void Awake()
    {
        if(_target == null)
            _target = transform.parent;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _startPoint = _target.position;
        _endPoint = eventData.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _target.position = _startPoint + (eventData.position - _endPoint);
    }
}

using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [HideInInspector] public ItemBase item = null;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI _amountText;
    private Inventory _inventory;
    [HideInInspector] public Transform _startParent;
    private Rect baseRect;
    public int _amount;

    public bool HasItem => item != null;
    public bool IsAddItem => _amount < item.maxAmount;
    public void SetAmount() 
    {
        _amountText.text = _amount.ToString(); 
        if(_amount > 1)
        {
            _amountText.gameObject.SetActive(true);
        }else
        {
            _amountText.gameObject.SetActive(false);
        }
    }
    public void SetSprite(Sprite img) { icon.sprite = img; }

    public void Awake()
    {
        _inventory = GetComponentInParent<Inventory>();
        baseRect = transform.parent.GetComponent<RectTransform>().rect;
    }

    public void Init()
    {
        icon.transform.SetParent(_startParent);
        icon.transform.SetAsFirstSibling();
        icon.transform.localPosition = Vector3.zero;
        icon.rectTransform.sizeDelta = new Vector2(90f, 90);
        SetAmount();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!HasItem) return;
        _startParent = icon.transform.parent;
        icon.transform.SetParent(_startParent.parent.parent);
        _inventory.beginSlot = this;
        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!HasItem) return;
        icon.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.raycastTarget = true;
        if (_inventory.beginSlot != null)
        {
            if (icon.transform.localPosition.x < baseRect.xMin
            || icon.transform.localPosition.x > baseRect.xMax
            || icon.transform.localPosition.y < baseRect.yMin
            || icon.transform.localPosition.y > baseRect.yMax)
            {
                _amount -= 1;
                SetAmount();
                
                Instantiate(item.DropItem).transform.position = _inventory.dropPivot.position + new Vector3(0, 2f, 0);
                if(_amount == 0)
                {
                    _amount = 1;
                    SetAmount();
                    icon.sprite = null;
                    item = null;
                }
            }
            icon.transform.SetParent(_startParent);
            icon.transform.SetAsFirstSibling();
            icon.transform.localPosition = Vector3.zero;
            icon.rectTransform.sizeDelta = new Vector2(90f, 90);

            _inventory.beginSlot = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (_inventory.beginSlot == null) return;
        _inventory.PostData(item, out item);
        icon.sprite = item.icon;

        SetAmount();
    }
}

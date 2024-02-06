using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemBase item;
    [SerializeField] private Image icon;
    public TextMeshProUGUI amount;
    private Inventory _inventory;
    [HideInInspector]public Transform _startParent;
    private Rect baseRect;
    public bool HasItem => icon.sprite != null;

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
        if (amount.text != "1")
            amount.gameObject.SetActive(true);
        else
            amount.gameObject.SetActive(false);
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
                amount.text = (int.Parse(amount.text) - 1).ToString();
                if (amount.text == "1")
                {
                    amount.gameObject.SetActive(false);
                }
                else if(amount.text == "0")
                {
                    amount.text = "1";
                    icon.sprite = null;
                    item.Name = string.Empty;
                }
                
                Instantiate(item.DropItem).transform.position = _inventory.dropPivot.position + new Vector3(0, 2f, 0);

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
        string str;
        _inventory.PostData(item, amount.text, out item, out str);
        icon.sprite = item.icon;
        amount.text = str;
        if (str == "1")
            amount.gameObject.SetActive(false);
        else
            amount.gameObject.SetActive(true);
    }
}

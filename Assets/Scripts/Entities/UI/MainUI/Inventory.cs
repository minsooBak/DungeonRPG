using Structs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : UIBase
{
    [Header("Slot")]
    [SerializeField] private Transform _slotPivot;
    [SerializeField] private GameObject _slotObj;
    private List<ItemSlot> slots = new List<ItemSlot>(MAX_SIZE);
    private const int MAX_SIZE = 35;
    public Transform dropPivot;
    [Header("Gold")]
    [SerializeField] private MovementObjectStat _stat;
    [SerializeField] private TextMeshProUGUI _goldText;

    //[HideInInspector]
    public ItemSlot beginSlot;

    public void Init()
    {
        _goldText.text = _stat.Gold.ToString("N0");
        dropPivot = GameObject.Find("Player").GetComponent<Transform>();
        for(int i = 0; i <  MAX_SIZE; i++)
        {
            GameObject slot = Instantiate(_slotObj, _slotPivot);
            slots.Add(slot.GetComponent<ItemSlot>());
            slot.transform.localScale = Vector3.one;
        }
    }

    public void AddItem(ItemBase item)
    {
        var i = slots.Find((x) => x.item.Name == item.Name);
        if (i == null)
        {
            if(item.Name == "Gold")
            {
                _stat.Gold += 1000;
                _goldText.text = _stat.Gold.ToString("N0");
                return;
            }

            foreach(var slot in slots)
            {
                if(!slot.HasItem)
                {
                    slot.item = item;
                    slot.SetSprite(item.icon);
                    return;
                }
            }
        }else
        {
            if (i.amount.text == "1")
                i.amount.gameObject.SetActive(true);
            i.amount.text = (int.Parse(i.amount.text) + 1).ToString();
        }
    }

    public void PostData(ItemBase item, string amount, out ItemBase data, out string outAmount)
    {
        data = beginSlot.item;
        outAmount = beginSlot.amount.text;
        beginSlot.item = item;
        beginSlot.SetSprite(item.icon);
        beginSlot.amount.text = amount;
        beginSlot.Init();
        beginSlot = null;
    }
}

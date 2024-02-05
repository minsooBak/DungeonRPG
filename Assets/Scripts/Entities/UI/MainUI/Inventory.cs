using Structs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : UIBase
{
    [Header("Slot")]
    [SerializeField] private Transform _slotPivot;
    [SerializeField] private GameObject _slotObj;
    //TODO SlotClassList
    private const int MAX_SIZE = 35;
    private List<ItemSlot> slots = new List<ItemSlot>(MAX_SIZE);
    [Header("Gold")]
    [SerializeField] private MovementObjectStat _stat;
    [SerializeField] private TextMeshProUGUI _goldText;

    public void Init()
    {
        _goldText.text = _stat.Gold.ToString("N0");
        for(int i = 0; i <  MAX_SIZE; i++)
        {
            GameObject slot = Instantiate(_slotObj, _slotPivot);
            slots.Add(slot.GetComponent<ItemSlot>());
            slots[0].SetIndex(i);
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
                int gold = int.Parse(_goldText.text) + 1000;
                _goldText.text = gold.ToString("N0");
                _stat.Gold = gold;
                return;
            }

            foreach(var slot in slots)
            {
                if(slot.prefab == null)
                {
                    slot.item = item;
                    slot.prefab = item.DropItem;
                    slot.icon.sprite = item.icon;
                    return;
                }
            }
        }else
        {
            i.amount.gameObject.SetActive(true);
            i.amount.text = (int.Parse(i.amount.text) + 1).ToString();
        }
    }
}

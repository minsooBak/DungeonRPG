using Structs;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : UIBase
{
    [Header("Slot")]
    [SerializeField] private Transform _slotPivot;
    [SerializeField] private GameObject _slotObj;
    private ItemSlot[] slotArray = new ItemSlot[MAX_SIZE];
    private Dictionary<int, int> slots = new Dictionary<int, int>();//아이템고유ID, 인덱스리스트
    private Dictionary<string, List<int>> itemNames = new Dictionary<string, List<int>>();//아이템이름, 아이템ID
    private const int MAX_SIZE = 35;
    public Transform dropPivot;
    [Header("Gold")]
    [SerializeField] private MovementObjectStat _stat;
    [SerializeField] private TextMeshProUGUI _goldText;

    [SerializeField] private ItemDataManager _itemDataManager;

    [HideInInspector]
    public ItemSlot beginSlot;

    public void Init()
    {
        _itemDataManager.Init();
        _goldText.text = _stat.Gold.ToString("N0");
        dropPivot = GameObject.Find("Player").GetComponent<Transform>();
        for(int i = 0; i <  MAX_SIZE; i++)
        {
            GameObject slot = Instantiate(_slotObj, _slotPivot);
            slotArray[i] = slot.GetComponent<ItemSlot>();
            slotArray[i].item = null;
            slot.transform.localScale = Vector3.one;
        }
    }

    public void AddItem(string itemName)
    {
        if (itemName == "Gold")
        {
            _stat.Gold += 1000;
            _goldText.text = _stat.Gold.ToString("N0");
            return;
        }
        else if (itemNames.ContainsKey(itemName))
        {
            List<int> list = itemNames[itemName];
            foreach(var id in list)
            {
                int slotIndex = slots[id];
                ItemSlot slot = slotArray[slotIndex];
                if (slot.IsAddItem)
                {
                    slot._amount++;
                    slot.SetAmount();
                    return;
                }
            }
            AddItemToEmptySlot(itemName);
        }
        else
        {
            AddItemToEmptySlot(itemName);
        }
    }

    private void AddItemToEmptySlot(string itemName)
    {
        for(int i = 0; i < slotArray.Length; i++)
        {
            if (!slotArray[i].HasItem)
            {
                ItemBase newItem = _itemDataManager.GetItem(itemName);
                slotArray[i].item = newItem;
                slotArray[i]._amount = 1;
                slotArray[i].SetSprite(newItem.icon);
                if (itemNames.TryGetValue(itemName, out List<int> list))
                {
                    list.Add(newItem.ID);
                }
                else
                {
                    itemNames.Add(itemName, new List<int>());
                    itemNames[itemName].Add(newItem.ID);
                }
                slots.Add(newItem.ID, i);
                break;
            }
            if(i == slotArray.Length - 1)
            {
                Debug.Log("Inventory is Full");
            }
        }
    }

    public void PostData(ItemBase item, out ItemBase data)
    {
        data = beginSlot.item;
        if (item == null)
        {
            beginSlot.item = null;
            beginSlot._amount = 0;
            beginSlot.SetSprite(null);
            beginSlot.Init();
            beginSlot = null;
        }
        beginSlot.item = item;
        beginSlot.SetSprite(item.icon);
        beginSlot.SetAmount();
        beginSlot.Init();
        beginSlot = null;
    }
}

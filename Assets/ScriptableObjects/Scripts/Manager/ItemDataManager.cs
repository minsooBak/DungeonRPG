using UnityEngine;
using System.Collections.Generic;
using Structs;
using EnumTypes;

[CreateAssetMenu(fileName ="ItemManager", menuName ="Data/DataManager/ItemData")]
public class ItemDataManager : ScriptableObject
{
    public List<ItemBase> datas = new();
    public Dictionary<int, ItemBase> items = new();

    public void Init()
    {
        foreach(var item in datas)
        {
            items.Add(item.Name.GetHashCode(), item);
        }
    }

    public ItemBase GetItem(string itemName)
    {
        return new ItemBase(items[itemName.GetHashCode()]);
    }
}

[System.Serializable]
public class ItemBase
{
    public string Name;
    public string Description;
    public ItemType Type;
    public Sprite icon;
    public int maxAmount;
    public int ID { get; set; }

    private static int _nextID = 0;

    public GameObject DropItem;
    public ItemBase(ItemBase item)
    {
        Name = item.Name;
        Description = item.Description;
        Type = item.Type;
        icon = item.icon;
        maxAmount = item.maxAmount;
        ID = _nextID++;
    }
}
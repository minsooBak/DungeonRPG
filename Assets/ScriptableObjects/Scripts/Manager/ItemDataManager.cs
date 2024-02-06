using UnityEngine;
using System.Collections.Generic;
using Structs;

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
        return items[itemName.GetHashCode()];
    }
}


using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemSlot(ItemSlot itemSlot)
    {
        prefab = itemSlot.prefab;
        item = itemSlot.item;
        icon.sprite = itemSlot.icon.sprite;
    }
    public GameObject prefab;
    public GameObject highlight;
    public ItemBase item;
    public Image icon;
    public TextMeshProUGUI amount;
    public int Index { get; private set; }
    public bool HasItem => prefab != null;

    public void SetIndex(int index) => Index = index;
}

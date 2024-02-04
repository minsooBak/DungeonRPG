using TMPro;
using UnityEngine;

public class Inventory : UIBase
{
    [Header("Slot")]
    [SerializeField] private Transform _slotPivot;
    [SerializeField] private GameObject _slotObj;
    //TODO SlotClassList
    private const int MAX_SIZE = 35;
    private ItemSlot[] slots = new ItemSlot[MAX_SIZE];
    [Header("Gold")]
    [SerializeField] private MovementObjectStat _stat;
    [SerializeField] private TextMeshProUGUI _goldText;
    

    private void Start()
    {
        if (prefab != null)
            return;

        _goldText.text = $"{_stat.Gold}G";
        for(int i = 0; i <  MAX_SIZE; i++)
        {
            GameObject slot = Instantiate(_slotObj, _slotPivot);
            slots[i] = slot.GetComponent<ItemSlot>();
            slot.transform.localScale = Vector3.one;
        }
    }
}

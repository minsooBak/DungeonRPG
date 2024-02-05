using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private BankDataManager _bankDataManager;
    [SerializeField] private ItemDataManager _itemDataManager;

    public void Awake()
    {
        _bankDataManager.Init();
        _itemDataManager.Init();
        var inven = _uiManager.GetUI<Inventory>();
        inven.Init();
        inven.prefab.SetActive(false);
    }

    public void OnBank()
    {
        if(_uiManager.TryGetUI(out Bank ui) == false)
        {
            ui = _uiManager.GetUI<Bank>();
            ui.exitButton.onClick.AddListener(Disable);
        }
        ui.Active();
    }

    public void AddItem()
    {
        _uiManager.GetUI<Inventory>().AddItem(_itemDataManager.GetCopyItem("µ¹ °Ë"));
    }

    public void AddGold()
    {
        _uiManager.GetUI<Inventory>().AddItem(_itemDataManager.GetCopyItem("Gold"));
    }

    private void Disable()
    {
        mainUI.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        _uiManager.Clear();
        _bankDataManager.Save();
    }
}

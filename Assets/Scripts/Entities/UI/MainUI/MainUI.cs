using UnityEngine;
using UnityEngine.UI;

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
        float r = Random.Range(0f, 1f);
        switch(r)
        {
            case < 0.2f:
                _uiManager.GetUI<Inventory>().AddItem(_itemDataManager.GetItem("�� ��"));
                break;
            case < 0.4f:
                _uiManager.GetUI<Inventory>().AddItem(_itemDataManager.GetItem("ö ��"));
                break;
            case < 0.6f:
                _uiManager.GetUI<Inventory>().AddItem(_itemDataManager.GetItem("�ϱ� ü������"));
                break;
            case < 0.8f:
                _uiManager.GetUI<Inventory>().AddItem(_itemDataManager.GetItem("�ϱ� ��������"));
                break;
        }
    }

    public void AddGold()
    {
        _uiManager.GetUI<Inventory>().AddItem(_itemDataManager.GetItem("Gold"));
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

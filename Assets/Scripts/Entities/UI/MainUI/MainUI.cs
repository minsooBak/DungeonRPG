using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private BankDataManager _bankDataManager;

    public void Awake()
    {
        _bankDataManager.Init();
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
                _uiManager.GetUI<Inventory>().AddItem("�� ��");
                break;
            case < 0.4f:
                _uiManager.GetUI<Inventory>().AddItem("ö ��");
                break;
            case < 0.6f:
                _uiManager.GetUI<Inventory>().AddItem("�ϱ� ü������");
                break;
            case < 0.8f:
                _uiManager.GetUI<Inventory>().AddItem("�ϱ� ��������");
                break;
        }
    }

    public void AddGold()
    {
        _uiManager.GetUI<Inventory>().AddItem("Gold");
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

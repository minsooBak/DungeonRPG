using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private UIManager _uiManager;

    public void OnBank()
    {
        if(_uiManager.TryGetUI(out Bank ui) == false)
        {
            ui = _uiManager.GetUI<Bank>();
            ui.exitButton.onClick.AddListener(Disable);
        }
        ui.Active();
    }

    private void Active()
    {
        mainUI.SetActive(false);
    }

    private void Disable()
    {
        mainUI.SetActive(true);
    }
}

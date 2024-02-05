using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : UIBase
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI mainText;
    public Button confirm;

    public void Active(string header, string main)
    {
        headerText.text = header;
        mainText.text = main;
        gameObject.SetActive(true);
        confirm.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
        confirm.onClick.RemoveAllListeners();
    }
}

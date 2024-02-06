using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

public class Bank : UIBase
{
    [Header("Bank")]
    public Button exitButton;
    [SerializeField] private UIManager _manager;
    [SerializeField] private GameObject _signInObj;
    [SerializeField] private GameObject _mainObj;
    [SerializeField] private BankDataManager _bankDataManager;

    [Header("SignIn")]
    [SerializeField] private TMP_InputField _inIDText;
    [SerializeField] private TMP_InputField _inPSText;
    private BankData _curData;

    [Header("SignUp")]
    [SerializeField] private TMP_InputField _upIDText;
    [SerializeField] private TMP_InputField _upNameText;
    [SerializeField] private TMP_InputField _upPSText;
    [SerializeField] private TMP_InputField _upPSConfirmText;

    private const int MinID = 3;
    private const int MinPS = 5;
    private const int MinName = 2;

    [Header("Bank Processing")]
    [SerializeField] private GameObject _bankObj;
    [SerializeField] private GameObject _bankMainObj;
    [SerializeField] private GameObject _depoisitObj;
    [SerializeField] private GameObject _withdrawalObj;
    [SerializeField] private GameObject _transferObj;
    [SerializeField] private GameObject highlight;

    [SerializeField] private MovementObjectStat _playerGold;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _curName;
    [SerializeField] private TextMeshProUGUI _curGold;

    [Header("Bank Deposit")]
    [SerializeField] private TMP_InputField _depoisitGold;

    [Header("Bank Withdrawal")]
    [SerializeField] private TMP_InputField _withdrawalGold;

    [Header("Bank Transfer")]
    [SerializeField] private TMP_InputField _targetID;
    [SerializeField] private TMP_InputField _transferGold;


    private void Start()
    {
        if (prefab != null)
            return;
        //signIn
        _inIDText.onValueChanged.AddListener((text) => _inIDText.text = Regex.Replace(text, @"[^a-zA-Z0-9]", ""));
        _inPSText.onValueChanged.AddListener((text) => _inPSText.text = Regex.Replace(text, @"[^a-zA-Z0-9]", ""));
        //signUp
        _upIDText.onValueChanged.AddListener((text) => _upIDText.text = Regex.Replace(text, @"[^a-zA-Z0-9]", ""));
        _upNameText.onValueChanged.AddListener((text) => _upNameText.text = Regex.Replace(text, @"[^a-zA-Z]", ""));
        _upPSText.onValueChanged.AddListener((text) => _upPSText.text = Regex.Replace(text, @"[^a-zA-Z0-9]", ""));
        _upPSConfirmText.onValueChanged.AddListener((text) => _upPSConfirmText.text = Regex.Replace(text, @"[^a-zA-Z0-9]", ""));
        //Transfer
        _targetID.onValueChanged.AddListener((text) => _targetID.text = Regex.Replace(text, @"[^a-zA-Z0-9]", ""));

    }

    public void Login()
    {
        if (CheckFieldEmpty(_inIDText) || CheckFieldEmpty(_inPSText))
        {
            if (CheckFieldEmpty(_inIDText))
            {
                TextEmpty("Bank SignIn", "ID");
            }
            else
            {
                TextEmpty("Bank SingIn", "Password");
            }
            return;
        }
        else if (_inIDText.text.Length < MinID || _inPSText.text.Length < MinPS)
        {
            if (_inIDText.text.Length < MinID)
            {
                CheckTextLength("Bank LogIn", MinID, "ID");
            }
            else
            {
                CheckTextLength("Bank LogIn", MinPS, "PS");
            }
            return;
        }
        _bankDataManager.GetBankData(_inIDText.text, out _curData);
        UIPopup uI = _manager.GetUI<UIPopup>();
        if (_curData == null || _curData.PS != _inPSText.text)
        {
            uI.Active("Bank LogIn", "Account does not exist, or entered the wrong ID or password");
        }
        else
        {
            _inIDText.text = string.Empty;
            _inPSText.text = string.Empty;
            uI.confirm.onClick.AddListener(() => _signInObj.SetActive(false));
            uI.confirm.onClick.AddListener(() => _bankObj.SetActive(true));
            uI.confirm.onClick.AddListener(() => _goldText.text = _playerGold.Gold.ToString("N0"));
            uI.confirm.onClick.AddListener(() => _curName.text = _curData.Name);
            uI.confirm.onClick.AddListener(() => _curGold.text = _curData.Gold.ToString("N0"));
            uI.Active("Bank LogIn", $"WelCome {_curData.Name}");
        }
    }

    public void Logout()
    {
        _bankObj.SetActive(false);
        _mainObj.SetActive(false);
        _signInObj.SetActive(true);
    }

    public void SignUp()
    {
        if (CheckFieldEmpty(_upIDText) || CheckFieldEmpty(_upNameText) || _upPSText.text == "" || _upPSConfirmText.text == "")
        {
            if (CheckFieldEmpty(_upIDText) || CheckFieldEmpty(_upNameText))
            {
                TextEmpty("Bank SingUp", "ID or Name");
            }
            else
            {
                TextEmpty("Bank SingUp", "PS or PS Confirm");
            }
        }
        else if (_upIDText.text.Length < MinID || _upNameText.text.Length < MinName || _upPSText.text.Length < MinPS)
        {
            if (_upIDText.text.Length < MinID)
            {
                CheckTextLength("Bank SingUp", MinID, "ID");
            }
            else if (_upNameText.text.Length < MinName)
            {
                CheckTextLength("Bank SingUp", MinName, "Name");
            }
            else
            {
                CheckTextLength("Bank SingUp", MinPS, "PS");
            }
        }
        CheckPasswordMatch();
    }

    public void Depoisit()
    {
        UIPopup ui = _manager.GetUI<UIPopup>();
        highlight.SetActive(true);
        ui.confirm.onClick.AddListener(() => highlight.SetActive(false));

        if(CheckFieldEmpty(_depoisitGold))
        {
            _depoisitGold.text = string.Empty;
            TextEmpty("Bank Depoisit", " Depoisit Gold");
            return;
        }

        int gold = int.Parse(_depoisitGold.text);
        if (gold <= _playerGold.Gold)
        {
            _playerGold.Gold -= gold;
            _curData.Gold += gold;
            _curGold.text = _curData.Gold.ToString("N0");
            _goldText.text = _playerGold.Gold.ToString("N0");
            _depoisitGold.text = string.Empty;
            ui.confirm.onClick.AddListener(()=> _bankMainObj.SetActive(true));
            ui.confirm.onClick.AddListener(() => _depoisitObj.SetActive(false));
            ui.Active("Bank Depoisit", $"{gold} Depoisit");
            return;
        }
        ui.Active("Bank Depoisit", $"Insufficient funds : {_playerGold.Gold - gold}G");

    }

    public void Withdrawal()
    {
        UIPopup ui = _manager.GetUI<UIPopup>();
        highlight.SetActive(true);
        ui.confirm.onClick.AddListener(() => highlight.SetActive(false));

        if (CheckFieldEmpty(_withdrawalGold))
        {
            _withdrawalGold.text = string.Empty;
            TextEmpty("Bank Withdrawal", "Withdrawal Gold");
            return;
        }

        int gold = int.Parse(_withdrawalGold.text);
        if (gold <= _curData.Gold)
        {
            _curData.Gold -= gold;
            _playerGold.Gold += gold;
            _curGold.text = _curData.Gold.ToString("N0");
            _goldText.text = _playerGold.Gold.ToString("N0");
            ui.confirm.onClick.AddListener(() => _bankMainObj.SetActive(true));
            ui.confirm.onClick.AddListener(() => _withdrawalObj.SetActive(false));
            _withdrawalGold.text = string.Empty;
            ui.Active("Bank Withdrawal", $"{gold} Withdrawal");
            return;
        }
        ui.Active("Bank Withdrawal", $"Insufficient funds : {_curData.Gold - gold}");
    }

    public void Transfer()
    {
        UIPopup ui = _manager.GetUI<UIPopup>();
        highlight.SetActive(true);
        ui.confirm.onClick.AddListener(() => highlight.SetActive(false));

        if (CheckFieldEmpty(_transferGold))
        {
            TextEmpty("Bank Transfer", "Transfer Gold");
            return;
        }

        int gold = int.Parse(_transferGold.text);
        _bankDataManager.GetBankData(_targetID.text, out BankData bank);
        if (bank.ID == null)
        {
            ui.Active("Bank Transfer", "Bank ID is Not Match");
            return;
        }else if(gold > _curData.Gold)
        {
            ui.Active("Bank Transfer", $"Insufficient funds : {_curData.Gold - gold}");
            return;
        }
        _curData.Gold -= gold;
        bank.Gold += gold;
        _curGold.text = _curData.Gold.ToString("N0");
        _targetID.text = string.Empty;
        _transferGold.text = string.Empty;
        ui.confirm.onClick.AddListener(() => _bankMainObj.SetActive(true));
        ui.confirm.onClick.AddListener(() => _transferObj.SetActive(false));
        ui.Active("Bank Transfer", $"Send {gold}G to {bank.Name}");
    }

    private bool CheckFieldEmpty(TMP_InputField inputField)
    {
        return inputField.text == "" ? true : false;
    }

    private void TextEmpty(string header, string InputText)
    {
        UIPopup uI = _manager.GetUI<UIPopup>();
        uI.Active(header, $"Please enter your {InputText}");
    }

    private void CheckTextLength(string header, int min, string InputText = "")
    {
        UIPopup uI = _manager.GetUI<UIPopup>();
        uI.Active(header, $"Enter at least {min} characters for {InputText} in English, case letters, numbers");
    }

    private void CheckPasswordMatch()
    {
        UIPopup uI = _manager.GetUI<UIPopup>();
        if(_upPSText.text != _upPSConfirmText.text)
        {
            uI.Active("Bank SignUp", "Password does not match");
        }else
        {

            if(_bankDataManager.AddBankData(_upIDText.text, _upPSText.text, _upNameText.text, 0))
            {
                _upIDText.text = string.Empty;
                _upPSText.text = string.Empty;
                _upPSConfirmText.text = string.Empty;
                uI.confirm.onClick.AddListener(() => _mainObj.SetActive(false));
                uI.confirm.onClick.AddListener(() => _signInObj.SetActive(true));
                uI.Active("Bank SignUp", $"WellCome {_upNameText.text}");
                _upNameText.text = string.Empty;
            }else
            {
                uI.Active("Bank SignUp", "This ID is already in use");
            }
        }
    }
}

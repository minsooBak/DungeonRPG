using UnityEngine;
using UnityEngine.UI;

public class Bank : UIBase
{
    [SerializeField] private GameObject signUI;
    [SerializeField] private GameObject bankUI;
    public Button exitButton;

    private void Start()
    {
        if (prefab != null)
            return;

        //TODO �α��� ���� üũ
        //ȸ������[���� �輳] -> �Աݿ��� ���� �ݾ׺�ó�� -> ��� �� ��й�ȣ
    }
}

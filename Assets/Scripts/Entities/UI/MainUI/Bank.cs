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

        //TODO 로그인 여부 체크
        //회원가입[계좌 계설] -> 입금에서 현재 금액비교처리 -> 출금 시 비밀번호
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class NewBehaviourScript : MonoBehaviour
{
    GameManager gm;
    private void Awake()
    {
        gm = GameObject.Find("GameManager").AddComponent<GameManager>();
    }

    //자동 로그인
    public void AutoLogin()
    {
        BackendReturnObject BRO = Backend.BMember.LoginWithTheBackendToken();

        if (BRO.IsSuccess())
        {
            Debug.Log("[동기방식] 자동로그인 완료");
        }
        else
        {
            Debug.Log("자동 로그인 실패");
        }
    }
}

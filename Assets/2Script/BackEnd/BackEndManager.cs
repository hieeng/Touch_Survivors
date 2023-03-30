using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndManager : MonoBehaviour
{
    private static BackEndManager instance = null;
    public static BackEndManager MyInstance { get => instance; set => instance = value; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        InitBackEnd();
        AutoLogin();
    }

    //뒤끝 초기화
    private void InitBackEnd()
    {
        Backend.Initialize(BRO =>
        {
            Debug.Log("뒤끝 초기화 진행" + BRO);

            //성공
            if (BRO.IsSuccess())
            {
                Debug.Log(Backend.Utils.GetGoogleHash());
            }

            //실패
            else
            {

            }
        });
    }

    //자동로그인
    public void AutoLogin()
    {
        //BackendReturnObject BRO = Backend.BMember.LoginWithTheBackendToken();
        BackendReturnObject BRO = Backend.BMember.GuestLogin("게스트 로그인으로 로그인함");

        if (BRO.IsSuccess())
        {
            Debug.Log("[동기방식] 자동로그인 완료");

            switch (BRO.GetStatusCode())
            {
                case "201":
                    Debug.Log("회원가입 성공");
                    break;
                case "200":
                    Debug.Log("로그인 성공");
                    break;
            }
        }
        else
        {
            Debug.Log("자동 로그인 실패");

            switch (BRO.GetStatusCode())
            {
                case "401":
                    Debug.Log("존재하지 않는 아이디 or 게스트계정을 페더레이션");
                    break;
                case "403":
                    Debug.Log("차단 or au가 10을 초과");
                    break;
            }
        }
    }
}

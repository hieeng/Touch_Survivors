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

    //�ڵ� �α���
    public void AutoLogin()
    {
        BackendReturnObject BRO = Backend.BMember.LoginWithTheBackendToken();

        if (BRO.IsSuccess())
        {
            Debug.Log("[������] �ڵ��α��� �Ϸ�");
        }
        else
        {
            Debug.Log("�ڵ� �α��� ����");
        }
    }
}

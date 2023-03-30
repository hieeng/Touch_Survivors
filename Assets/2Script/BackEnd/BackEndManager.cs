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

    //�ڳ� �ʱ�ȭ
    private void InitBackEnd()
    {
        Backend.Initialize(BRO =>
        {
            Debug.Log("�ڳ� �ʱ�ȭ ����" + BRO);

            //����
            if (BRO.IsSuccess())
            {
                Debug.Log(Backend.Utils.GetGoogleHash());
            }

            //����
            else
            {

            }
        });
    }

    //�ڵ��α���
    public void AutoLogin()
    {
        //BackendReturnObject BRO = Backend.BMember.LoginWithTheBackendToken();
        BackendReturnObject BRO = Backend.BMember.GuestLogin("�Խ�Ʈ �α������� �α�����");

        if (BRO.IsSuccess())
        {
            Debug.Log("[������] �ڵ��α��� �Ϸ�");

            switch (BRO.GetStatusCode())
            {
                case "201":
                    Debug.Log("ȸ������ ����");
                    break;
                case "200":
                    Debug.Log("�α��� ����");
                    break;
            }
        }
        else
        {
            Debug.Log("�ڵ� �α��� ����");

            switch (BRO.GetStatusCode())
            {
                case "401":
                    Debug.Log("�������� �ʴ� ���̵� or �Խ�Ʈ������ ������̼�");
                    break;
                case "403":
                    Debug.Log("���� or au�� 10�� �ʰ�");
                    break;
            }
        }
    }
}

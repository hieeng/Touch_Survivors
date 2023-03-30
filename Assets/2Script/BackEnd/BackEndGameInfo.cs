using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class BackEndGameInfo : MonoBehaviour
{
    Player player;
    GameManager gm;
    //������ ����
    public void OnClickSave()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //������ ����� �� �� �Ѱ��ִ� �Ķ���� Ŭ����
        Param param = new Param();

        param.Add("weapon_whip", player.WeaponLevel[0]);
        param.Add("weapon_daager", player.WeaponLevel[1]);
        param.Add("weapon_fireball", player.WeaponLevel[2]);
        param.Add("weapon_circle", player.WeaponLevel[3]);
        param.Add("weapon_laser", player.WeaponLevel[4]);
        param.Add("weapon_rotation", player.WeaponLevel[5]);
        param.Add("time", gm.playTime);

        BackendReturnObject BRO = Backend.GameData.Insert("UserData", param);

        if (BRO.IsSuccess())
        {
            Debug.Log("Success");
        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "404":
                    Debug.Log("�������� �ʴ� ���̺�");
                    break;
                case "412":
                    Debug.Log("��Ȱ��ȭ�� ���̺�");
                    break;
                case "413":
                    Debug.Log("�뷮 �ʰ�");
                    break;
                default:
                    Debug.Log("���� ���� ����");
                    break;
            }
        }
    }
}

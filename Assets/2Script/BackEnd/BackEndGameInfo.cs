using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class BackEndGameInfo : MonoBehaviour
{
    Player player;
    GameManager gm;
    //서버에 저장
    public void OnClickSave()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //서버와 통신을 할 때 넘겨주는 파라미터 클래스
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
                    Debug.Log("존재하지 않는 테이블");
                    break;
                case "412":
                    Debug.Log("비활성화된 테이블");
                    break;
                case "413":
                    Debug.Log("용량 초과");
                    break;
                default:
                    Debug.Log("서버 공통 에러");
                    break;
            }
        }
    }
}

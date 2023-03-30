using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager_ : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    public GameObject chestBoxPrefab;
    public GameObject coin1Prefab;
    public GameObject coin2Prefab;
    public GameObject coin3Prefab;
    public GameObject heartPrefab;

    public GameObject exp1Prefab;

    GameObject[] enemy1;
    GameObject[] enemy2;
    GameObject[] enemy3;

    GameObject[] chestBox;
    GameObject[] coin1;
    GameObject[] coin2;
    GameObject[] coin3;
    GameObject[] heart;

    GameObject[] exp1;


    GameObject[] targetPool;

    private void Awake()
    {
        enemy1 = new GameObject[80];
        enemy2 = new GameObject[50];
        enemy3 = new GameObject[50];

        chestBox = new GameObject[20];
        coin1 = new GameObject[15];
        coin2 = new GameObject[10];
        coin3 = new GameObject[10];
        heart = new GameObject[10];

        exp1 = new GameObject[100];

        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < enemy1.Length; i++)
        {
            enemy1[i] = Instantiate(enemy1Prefab);
            enemy1[i].SetActive(false);
        }
        for (int i = 0; i < enemy2.Length; i++)
        {
            enemy2[i] = Instantiate(enemy2Prefab);
            enemy2[i].SetActive(false);
        }
        for (int i = 0; i < enemy3.Length; i++)
        {
            enemy3[i] = Instantiate(enemy3Prefab);
            enemy3[i].SetActive(false);
        }
        for (int i = 0; i < chestBox.Length; i++)
        {
            chestBox[i] = Instantiate(chestBoxPrefab);
            chestBox[i].SetActive(false);
        }
        for (int i = 0; i < coin1.Length; i++)
        {
            coin1[i] = Instantiate(coin1Prefab);
            coin1[i].SetActive(false);
        }
        for (int i = 0; i < coin2.Length; i++)
        {
            coin2[i] = Instantiate(coin2Prefab);
            coin2[i].SetActive(false);
        }
        for (int i = 0; i < coin3.Length; i++)
        {
            coin3[i] = Instantiate(coin3Prefab);
            coin3[i].SetActive(false);
        }
        for (int i = 0; i < heart.Length; i++)
        {
            heart[i] = Instantiate(heartPrefab);
            heart[i].SetActive(false);
        }
        for (int i = 0; i < exp1.Length; i++)
        {
            exp1[i] = Instantiate(exp1Prefab);
            exp1[i].SetActive(false);
        }

    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            //적
            case "enemy1":
                targetPool = enemy1;
                break;
            case "enemy2":
                targetPool = enemy2;
                break;
            case "enemy3":
                targetPool = enemy3;
                break;

            //아이템
            case "chestBox":
                targetPool = chestBox;
                break;
            case "coin1":
                targetPool = coin1;
                break;
            case "coin2":
                targetPool = coin2;
                break;
            case "coin3":
                targetPool = coin3;
                break;
            case "heart":
                targetPool = heart;
                break;


            //경험치
            case "exp1":
                targetPool = exp1;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}

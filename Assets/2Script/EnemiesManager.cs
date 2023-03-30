using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public ObjectManager_ objManger;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gm;
    float timer;
    float time;

    private void Update()
    {
        time = gm.GetComponent<GameManager>().playTime;
        timer -= Time.deltaTime;

        //난이도 1
        if (time < 30)
        {
            if (timer < 0f)
            {
                SpawnEnmey1();
                timer = spawnTimer;
            }
        }
        //난이도 2
        else if (time < 60 && time >= 30)
        {
            if (timer < 0f)
            {
                spawnTimer = 0.5f;
                SpawnEnmey1();
                timer = spawnTimer;
            }
        }
        //난이도 3
        else if (time < 120 && time >= 60)
        {
            if (timer < 0f)
            {
                spawnTimer = 1f;
                SpawnEnmey1();
                SpawnEnmey2();
                timer = spawnTimer;
            }
        }
        //난이도 4
        else if (time >= 120 && time < 180)
        {
            if(timer < 0f)
            {
                spawnTimer = 0.5f;
                SpawnEnmey1();
                SpawnEnmey1();
                SpawnEnmey1();
                time = spawnTimer;
            }
        }

        else
        {
            if (timer < 0f)
            {
                spawnTimer = 0.8f;
                SpawnEnmey3();
                timer = spawnTimer;
            }
        }
    }

    //적1
    private void SpawnEnmey1()
    {
        Vector3 position = GenerateRandomPositionthod();
        GameObject newEnemy = objManger.MakeObj("enemy1");

        position += player.transform.position;

        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);
        newEnemy.transform.parent = transform;
    }
    //적2
    private void SpawnEnmey2()
    {
        Vector3 position = GenerateRandomPositionthod();
        GameObject newEnemy = objManger.MakeObj("enemy2");

        position += player.transform.position;

        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);
        newEnemy.transform.parent = transform;
    }
    //적3
    private void SpawnEnmey3()
    {
        Vector3 position = GenerateRandomPositionthod();
        GameObject newEnemy = objManger.MakeObj("enemy3"); ;

        position += player.transform.position;

        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);
        newEnemy.transform.parent = transform;
    }

    //랜덤 좌표
    private Vector3 GenerateRandomPositionthod()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;

        if(UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.x = spawnArea.x * f;
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
        }
        position.z = 0f;

        return position;
    }
}

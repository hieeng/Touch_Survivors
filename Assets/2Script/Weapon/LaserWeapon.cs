using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    [SerializeField] GameObject laser;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] public float timetoAttack;
    [SerializeField] float damage;
    [SerializeField] GameObject player;
    float timer;
    [HideInInspector] public int level = 1;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            if (level == 1)
            {
                SpawnLaser();
                timer = timetoAttack;
            }
            else if (level == 2)
            {
                SpawnLaser();
                SpawnLaser();
                timer = timetoAttack;
            }
        }
    }

    private void SpawnLaser()
    {
        Vector3 position = GenerateRandomPosition();
        GameObject newLaser = Instantiate(laser);

        position += player.transform.position;
        newLaser.transform.position = position;
        
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;

        if (UnityEngine.Random.value > 0.5f)
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

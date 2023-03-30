using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerWeapon : MonoBehaviour
{
    [SerializeField] public float timeToAttack;
    [HideInInspector] public int level = 1;
    float timer;
    float x;
    float y;

    public Player player;

    [SerializeField] GameObject dagger;

    private void Update()
    {
        if (timer < timeToAttack)
        {
            timer += Time.deltaTime;
            return;
        }

        x = player.lastX;
        y = player.lastY;

        //Debug.Log(x + y);

        if (x == 0 & y == 0)
        {
            x = player.lastDX;
            y = 0;
        }
        timer = 0;

        SpawnDagger1();
        if(level == 2)
        {
            SpawnDagger2();
        }
        else  if(level == 3)
        {
            SpawnDagger2();
            SpawnDagger3();
        }
    }

    private void SpawnDagger1()
    {
        GameObject thrownDagger = Instantiate(dagger);
        thrownDagger.transform.position = transform.position;
        thrownDagger.GetComponent<ThrowingDagger>().SetDirection(x, y);
    }
    private void SpawnDagger2()
    {
        GameObject thrownDagger = Instantiate(dagger);
        var temp = transform.position;
        temp.y = temp.y + 0.3f;
        thrownDagger.transform.position = temp;
        thrownDagger.GetComponent<ThrowingDagger>().SetDirection(x, y);
    }
    private void SpawnDagger3()
    {
        GameObject thrownDagger = Instantiate(dagger);
        var temp = transform.position;
        temp.y = temp.y - 0.3f;
        thrownDagger.transform.position = temp;
        thrownDagger.GetComponent<ThrowingDagger>().SetDirection(x, y);
    }
}

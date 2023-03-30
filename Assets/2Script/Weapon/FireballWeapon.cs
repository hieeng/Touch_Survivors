using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack;
    float timer;
    float x;
    float y;

    public Player player;

    [SerializeField] GameObject fireball;

    private void Awake()
    {
    }

    private void Update()
    {
        if (timer < timeToAttack)
        {
            timer += Time.deltaTime;
            return;
        }

        x = player.lastX;
        y = player.lastY;

        if (x == 0 & y == 0)
        {
            x = player.lastDX;
            y = 0;
        }
        timer = 0;
        SpawnFireball();
    }

    private void SpawnFireball()
    {
        GameObject thrownFireball = Instantiate(fireball);
        thrownFireball.transform.position = transform.position;
        thrownFireball.GetComponent<ThrowingFireball>().SetDirection(x, y);
    }
}
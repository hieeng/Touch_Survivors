using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject laser;
    [SerializeField] float damage;
    [SerializeField] Vector2 range = new Vector2(1f, 5f);
    float time = 0;

    void Update()
    {
        time += Time.deltaTime;

        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapBoxAll(laser.transform.position, range, 0f);
            foreach (Collider2D c in hit)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    break;
                }
            }
            if (time > 0.13)
            {
                time = 0;
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 0.8f);
    }
}

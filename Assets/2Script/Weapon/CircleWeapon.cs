using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack = 0.5f;
    float timer;

    [SerializeField] GameObject circle;
    [SerializeField] float radius = 2f;
    [SerializeField] float damage;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timer = timeToAttack;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius);
        ApplyDamage(hit);
    }

    private void ApplyDamage(Collider2D[] colliders)
    {

        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable e = colliders[i].GetComponent<IDamageable>();
            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }
    }
}



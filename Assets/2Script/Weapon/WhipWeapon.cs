using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : MonoBehaviour
{
    [SerializeField] public float timeToAttack = 3f;
    float timer;

    [SerializeField] GameObject rightWhipObject;
    [SerializeField] GameObject leftWhipObject;

    public bool upgrade = false;

    [SerializeField] public int whipDamage = 2;
    [SerializeField] Vector2 whipAttackSzie = new Vector2(4f, 2f);

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timer = timeToAttack;
        rightWhipObject.SetActive(true);
        Collider2D[] collidersR = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSzie, 0f);
        ApplyDamage(collidersR);
        if (upgrade)
        {
            leftWhipObject.SetActive(true);
            Collider2D[] collidersL = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSzie, 0f);
            ApplyDamage(collidersL);
        }
        Invoke("SetFalse", 0.5f);
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable e = colliders[i].GetComponent<IDamageable>();
            if (e != null)
            {
                e.TakeDamage(whipDamage);
            }
        }
    }

    private void SetFalse()
    {
        rightWhipObject.SetActive(false);
        leftWhipObject.SetActive(false);
    }
}

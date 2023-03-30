using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    ObjectManager_ objManger;
    Rigidbody2D rb2d;
    Player targetPlayer;
    Transform targetDestination;
    GameObject targetGameObject;
    GameManager gm;
    [SerializeField] float speed;
    [SerializeField] float hp = 4;
    [SerializeField] int damage = 1;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        objManger = GameObject.Find("ObjectManager").GetComponent<ObjectManager_>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;

        //이미지 방향전환
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rb2d.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)    //플레이어
        {
            Attack();
        }

    }

    private void Attack()
    {
        if (targetPlayer == null)
        {
            targetPlayer = targetGameObject.GetComponent<Player>();
        }

        targetPlayer.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        damage = damage * gm.Pdamage;
        hp -= damage;

        if (hp <= 0)
        {
            SpawnExp(transform.position);
            gameObject.SetActive(false);
        }
    }

    private void SpawnExp(Vector3 position)
    {
        GameObject newExp = objManger.MakeObj("exp1");

        newExp.transform.position = position;
    }
}

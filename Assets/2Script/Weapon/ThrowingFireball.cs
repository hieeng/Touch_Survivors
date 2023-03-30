using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThrowingFireball : MonoBehaviour
{
    Player player;
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] int damage;

    float time = 0;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        damage = player.fireballDamage;
    }
    private void Update()
    {
        time += Time.deltaTime;
        transform.position += direction * speed * Time.deltaTime;

        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            foreach (Collider2D c in hit)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    break;
                }
            }
            if (time > 2)
            {
                time = 0;
                Destroy(gameObject);
            }
        }
    }

    public void SetDirection(float dir_x, float dir_y)
    {
        
        direction = new Vector3(dir_x, dir_y, 0);
        direction = direction.normalized;

        //Debug.Log(direction);

        if (dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
        else if (dir_y > 0 && dir_x == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir_y < 0 && dir_x == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
}

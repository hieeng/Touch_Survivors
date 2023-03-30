using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThrowingDagger : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] public int damage;

    bool hitDetected = false;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        //Debug.Log(direction);

        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            foreach (Collider2D c in hit)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    hitDetected = true;
                    break;
                }
            }
            if (hitDetected)
            {
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 0.8f);
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
    }
}

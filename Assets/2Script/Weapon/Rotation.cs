using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] GameObject rotation;
    [SerializeField] float damage = 4;
    [SerializeField] float range = 0.15f;


    private void Update()
    {
        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(rotation.transform.position, range);
            foreach (Collider2D c in hit)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    break;
                }
            }
        }
    }
}

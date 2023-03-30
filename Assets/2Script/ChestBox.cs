using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBox : MonoBehaviour, IDamageable
{
    ObjectManager_ objManger;

    float hp = 1;

    private void Awake()
    {
        objManger = GameObject.Find("ObjectManager").GetComponent<ObjectManager_>();
    }

    private void SpawnItem(string item, Vector3 position)
    {
        GameObject newItem = objManger.MakeObj(item);

        newItem.transform.position = position;

    }
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            int chance = Random.Range(0, 101);

            if (chance < 65)
            {
                SpawnItem("coin1", transform.position);
            }
            else if (chance >= 65 && chance < 85)
            {
                SpawnItem("heart", transform.position);
            }
            else if (chance >= 85 && chance < 95)
            {
                SpawnItem("coin2", transform.position);
            }
            else
            {
                SpawnItem("coin3", transform.position);
            } 
            gameObject.SetActive(false);
        }
    }
}

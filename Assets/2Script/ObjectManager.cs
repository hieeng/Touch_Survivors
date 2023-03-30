using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public ObjectManager_ objManger;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject player;

    float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            SpawnObject();
            timer = spawnTimer;
        }
    }

    private void SpawnObject()
    {
        Vector3 position = GenerateRandomPositionthod();
        GameObject newChest = objManger.MakeObj("chestBox");

        position += player.transform.position;

        newChest.transform.position = position;
        newChest.transform.parent = transform;
    }

    private Vector3 GenerateRandomPositionthod()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;

        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.x = spawnArea.x * f;
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
        }
        position.z = 0f;

        return position;
    }
}

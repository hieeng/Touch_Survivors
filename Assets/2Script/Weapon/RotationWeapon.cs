using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWeapon : MonoBehaviour
{
    public Player player;
    [SerializeField] float rotSpeed = 4f;
    [SerializeField] GameObject U;
    [SerializeField] GameObject D;
    [SerializeField] GameObject L;
    [SerializeField] GameObject R;

    int check = 0;

    private void Update()
    {
        transform.position = player.transform.position;
        transform.Rotate(new Vector3(0, 0, 1) * rotSpeed * Time.deltaTime);
    }

    public void Level()
    {
        check++;
        if (check == 1)
        {
            D.SetActive(true);
        }
        else if (check == 2)
        {
            L.SetActive(true);
        }
        else if (check == 3)
        {
            R.SetActive(true);
        }
    }
}

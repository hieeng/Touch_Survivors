using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public void OpenPanel()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
    }
}

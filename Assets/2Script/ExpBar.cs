using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void UpdateExpSlider(float current, int target)
    {
        slider.maxValue = target;
        slider.value = current;
    }
}

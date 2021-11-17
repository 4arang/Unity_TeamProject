using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RP_Bar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxRP(float maxRP)
    {
        slider.maxValue = maxRP;
        slider.minValue = -maxRP * 0.259f;
        slider.value = maxRP;
    }

    public void SetRP(float rp)
    {
        slider.value = rp;
    }
}

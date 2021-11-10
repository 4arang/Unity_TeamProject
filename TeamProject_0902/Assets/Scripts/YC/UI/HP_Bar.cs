using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHP(float maxHP)
    {
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }

   public void SetHP(float hp)
    {
        slider.value = hp;
    }
}

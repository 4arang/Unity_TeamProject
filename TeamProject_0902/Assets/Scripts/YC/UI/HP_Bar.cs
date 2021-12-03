using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHP(float maxHP, float UIref) // ui º¸Á¤°ª
    {
        slider.maxValue = maxHP;
        slider.minValue = -maxHP * UIref;
        slider.value = maxHP;
    }

   public void SetHP(float hp)
    {
        slider.value = hp;
        Debug.Log("hp " + hp);
    }
}

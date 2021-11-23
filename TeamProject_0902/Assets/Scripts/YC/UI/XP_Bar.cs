using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XP_Bar : MonoBehaviour
{
    public Slider slider;
    public Text level_text;

    private void Start()
    {
        slider = GetComponent<Slider>();
      SetMaxXP(280 );
    }

    private void Update()
    {
        SetXP(UIManager.Instance.Exp);
        SetLevel(UIManager.Instance.Level);
    }
    public void SetXP(float xp)
    {
        slider.value = xp;
    }
    public void SetMaxXP(float max)
    {
        slider.maxValue = max;
    }

    public void SetLevel(int level)
    {
        level_text.text = level.ToString();
    }
}

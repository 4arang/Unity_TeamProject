using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Text : MonoBehaviour
{
    public Text Level_text;

    void Start()
    {
        Level_text = GetComponent<Text>();
    }

  public void SetLevel(int level)
    {
        Level_text.text = level.ToString();
    }
}

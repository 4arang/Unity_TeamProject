using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold_Text : MonoBehaviour
{
    public Text gold;

    void Start()
    {
        gold = GetComponent<Text>();    
    }

    public void SetGold(int Gold)
    {
        gold.text = Gold.ToString();
    }
}

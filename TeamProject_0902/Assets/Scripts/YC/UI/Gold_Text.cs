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

    void Update()
    {
        gold.text = UIManager.Instance.Gold.ToString();
    }
}

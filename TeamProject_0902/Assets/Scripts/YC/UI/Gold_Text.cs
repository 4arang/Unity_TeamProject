using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold_Text : MonoBehaviour
{
    public Text gold;
    [SerializeField] private Text shopGoldText;

    void Start()
    {
       // gold = GetComponent<Text>();
        // shopGoldText = GetComponentInParent<UI_Setup>().GetComponentInChildren<Gold_Text_Shop>().GetComponent<Text>();
        
    }

    public void SetGold(int Gold)
    {
        gold.text = Gold.ToString();
        shopGoldText.text = Gold.ToString();
    }
}

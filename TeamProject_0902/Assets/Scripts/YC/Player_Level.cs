using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level : MonoBehaviour
{ 

    public float Exp { get; set; } // exp to level up : 280 + (level-1)*100
    public int Gold { get; set; }
    public int Level { get; set; } //max 18

    Lvlup UI_levelupBar;
    XP_Bar UI_Level;
    Gold_Text UI_Gold;
    // Start is called before the first frame update
    void Start()
    {
        Exp = 0;
        Gold = 0;
        Level = 1;

        UI_levelupBar = FindObjectOfType<Lvlup>();
        UI_Level = FindObjectOfType<XP_Bar>();
        UI_Gold = FindObjectOfType<Gold_Text>();
        InvokeRepeating("BasicEXP", 0, 5.0f); //5초마다 경험치 획득
        UI_levelupBar.gameObject.SetActive(false);
    }

    private void BasicEXP()
    {
        GetEXP(5.0f);
    }

    public void GetEXP(float gain)
    {
        Exp += gain;
        if (Exp >= 280 + (Level - 1) * 100)
        {      
            Exp -= 280 + (Level - 1) * 100;
             Level++;
            GetComponentInChildren<Level_Text>().SetLevel(Level); //플레이어 ui 레벨
            UI_Level.SetLevel(Level);//인게임 ui 레벨
            UI_Level.SetMaxXP(280 + (Level - 1) * 100);

            UI_levelupBar.gameObject.SetActive(true);
        }
        UI_Level.SetXP(Exp);
    }

    public void GetGold(int gain)
    {
        Gold += gain;
        UI_Gold.SetGold(Gold);
    }
}

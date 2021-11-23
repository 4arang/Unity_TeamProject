using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level : MonoBehaviour
{
    public float Exp { get; set; } // exp to level up : 280 + (level-1)*100
    public int Gold { get; set; }
    public int Level { get; set; } //max 18

    
    // Start is called before the first frame update
    void Start()
    {
        Exp = 0;
        Gold = 0;
        Level = 1;
        UIManager.Instance.Exp = Exp;
        UIManager.Instance.Gold = Gold;
        UIManager.Instance.Level = Level;
        InvokeRepeating("BasicEXP", 0, 5.0f); //5�ʸ��� ����ġ ȹ��
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
            GetComponentInChildren<Level_Text>().SetLevel(Level); //�÷��̾� ui ����
            UIManager.Instance.Level = Level; //�ΰ��� ui ����
        }

        UIManager.Instance.Exp = Exp;
        //XP_Bar.SetXP(Exp);
    }

    public void GetGold(int gain)
    {
        Gold += gain;
        UIManager.Instance.Gold = Gold;
    }
}

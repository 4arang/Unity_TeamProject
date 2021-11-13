using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level : MonoBehaviour
{
    public float Exp { get; set; } // exp to level up : 280 + (level-1)*100
    public int Gold { get; set; }
    public float Level { get; set; } //max 18
    

    // Start is called before the first frame update
    void Start()
    {
        Exp = 0;
        Gold = 0;
        Level = 1;
        InvokeRepeating("BasicEXP", 0, 5.0f); //5ÃÊ¸¶´Ù °æÇèÄ¡ È¹µæ
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
            Level++;
            Exp -= 280 + (Level - 1) * 100;
        }
    }
}

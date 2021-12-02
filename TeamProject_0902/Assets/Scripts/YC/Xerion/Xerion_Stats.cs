using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_Stats : MonoBehaviour
{
    //Player Information
    public byte AttackAbility;
    public byte DefenseAbility;
    public byte MagicAbility;
    public byte Difficulty;

    //Game Stats
    public bool TeamColor; //true = blue, false = red
    public float HP;             //Health Point
    public int HPperLevel;     //HP increasement per Level
    public int MP;             //Mana Point
    public int MPperLevel;
    public int AP;             //Armor Point
    public float APperLevel;
    public int AD;             //Attack Damage
    public float ADperLevel;
    public int MRP;             //Magic Resistance Point
    public float MRPperLevel;
    public float AttackSpeed;
    public float AttackSpeedperLevel;
    public float MoveSpeed;
    public int AttackRange;
    public float HPregen;
    public float HPregenperLevel;
    public int MPregen;
    public float MPregenperLevel;

    public float hp; 
    public float mp;

    public float EXP=0;
    public int Gold=0;
    public int Level = 1;

    //Xerion special
    public int Energy=0;
    private float TimeCheck = 0.0f;
    private float TimeCheck2 = 0.0f;

    


    void Awake()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");


        AttackAbility = byte.Parse(data[3]["infoattack"].ToString());
        DefenseAbility = byte.Parse(data[3]["infodefense"].ToString());
        MagicAbility = byte.Parse(data[3]["infomagic"].ToString());
        Difficulty = byte.Parse(data[3]["infodifficulty"].ToString());

        if (transform.position.x < 0) TeamColor = true;
        else TeamColor = false; //빼도되는거 player return에 공통적으로 구현

        HP = int.Parse(data[3]["statshp"].ToString());  //max hp
        HPperLevel = int.Parse(data[3]["statshpperlevel"].ToString());
        MP = int.Parse(data[3]["statsmp"].ToString());  //max mp
        MPperLevel = int.Parse(data[3]["statsmpperlevel"].ToString());
        AP = int.Parse(data[3]["statsarmor"].ToString());
        APperLevel = float.Parse(data[3]["statsarmorperlevel"].ToString());
        AD = int.Parse(data[3]["statsattackdamage"].ToString());
        ADperLevel = float.Parse(data[3]["statsattackdamageperlevel"].ToString());
        MRP = int.Parse(data[3]["statsspellblock"].ToString());
        MRPperLevel = float.Parse(data[3]["statsspellblockperlevel"].ToString());
        AttackSpeed = float.Parse(data[3]["statsattackspeed"].ToString());
        AttackSpeedperLevel = float.Parse(data[3]["statsattackspeedperlevel"].ToString());
        MoveSpeed = int.Parse(data[3]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[3]["statsattackrange"].ToString());
        HPregen = float.Parse(data[3]["statshpregen"].ToString());
        HPregenperLevel = float.Parse(data[3]["statshpregenperlevel"].ToString());
        MPregen = int.Parse(data[3]["statsmpregen"].ToString());
        MPregenperLevel = float.Parse(data[3]["statsmpregenperlevel"].ToString());

        Xerion_Manager.Instance.Xerion_AD = AD;
        hp = HP;
        mp = MP;
    }
  


}

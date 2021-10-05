using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger_Stats : MonoBehaviour
{
    //Player Information
    public byte AttackAbility;
    public byte DefenseAbility;
    public byte MagicAbility;
    public byte Difficulty;

    //Game Stats
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


    //WT special (Wildness)
    public byte Wildness;
    public byte minWildness = 0;
    public byte maxWildness = 4;
    private float TimeCheck = 0.0f;

    void Start()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");



        //AttackAbility = (byte)data[1]["infoattack"];    //unboxing porblem?
        AttackAbility = byte.Parse(data[1]["infoattack"].ToString());
        DefenseAbility = byte.Parse(data[1]["infodefense"].ToString());
        MagicAbility = byte.Parse(data[1]["infomagic"].ToString());
        Difficulty = byte.Parse(data[1]["infodifficulty"].ToString());

        HP = int.Parse(data[1]["statshp"].ToString());
        HPperLevel = int.Parse(data[1]["statshpperlevel"].ToString());
        MP = int.Parse(data[1]["statsmp"].ToString());
        MPperLevel = int.Parse(data[1]["statsmpperlevel"].ToString());
        AP = int.Parse(data[1]["statsarmor"].ToString());
        APperLevel = float.Parse(data[1]["statsarmorperlevel"].ToString());
        AD = int.Parse(data[1]["statsattackdamage"].ToString());
        ADperLevel = float.Parse(data[1]["statsattackdamageperlevel"].ToString());
        MRP = int.Parse(data[1]["statsspellblock"].ToString());
        MRPperLevel = float.Parse(data[1]["statsspellblockperlevel"].ToString());
        AttackSpeed = float.Parse(data[1]["statsattackspeed"].ToString());
        AttackSpeedperLevel = float.Parse(data[1]["statsattackspeedperlevel"].ToString());
        MoveSpeed = int.Parse(data[1]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[1]["statsattackrange"].ToString());
        HPregen = float.Parse(data[1]["statshpregen"].ToString());
        HPregenperLevel = float.Parse(data[1]["statshpregenperlevel"].ToString());
        MPregen = int.Parse(data[1]["statsmpregen"].ToString());
        MPregenperLevel = float.Parse(data[1]["statsmpregenperlevel"].ToString());

    }


    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_Stats : MonoBehaviour
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

    //Coldy special (Helium)
    [Range(0.0f, 100.0f)]
    public int Helium;
    public int minHelium = 0;
    public int maxHelium = 100;
    public bool Charging; //충전상태
    public bool isDanger; //헬륨 50이하인경우 상태 ->스킬능력강화
    public bool isZero; //헬륨 0인경우 상태->스킬사용x
    private float TimeCheck = 0.0f;

    void Start()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");



        AttackAbility = byte.Parse(data[2]["infoattack"].ToString());
        DefenseAbility = byte.Parse(data[2]["infodefense"].ToString());
        MagicAbility = byte.Parse(data[2]["infomagic"].ToString());
        Difficulty = byte.Parse(data[2]["infodifficulty"].ToString());

        HP = float.Parse(data[2]["statshp"].ToString());
        HPperLevel = int.Parse(data[2]["statshpperlevel"].ToString());
        MP = int.Parse(data[2]["statsmp"].ToString());
        MPperLevel = int.Parse(data[2]["statsmpperlevel"].ToString());
        AP = int.Parse(data[2]["statsarmor"].ToString());
        APperLevel = float.Parse(data[2]["statsarmorperlevel"].ToString());
        AD = int.Parse(data[2]["statsattackdamage"].ToString());
        ADperLevel = float.Parse(data[2]["statsattackdamageperlevel"].ToString());
        MRP = int.Parse(data[2]["statsspellblock"].ToString());
        MRPperLevel = float.Parse(data[2]["statsspellblockperlevel"].ToString());
        AttackSpeed = float.Parse(data[2]["statsattackspeed"].ToString());
        AttackSpeedperLevel = float.Parse(data[2]["statsattackspeedperlevel"].ToString());
        MoveSpeed = float.Parse(data[2]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[2]["statsattackrange"].ToString());
        HPregen = float.Parse(data[2]["statshpregen"].ToString());
        HPregenperLevel = float.Parse(data[2]["statshpregenperlevel"].ToString());
        MPregen = int.Parse(data[2]["statsmpregen"].ToString());
        MPregenperLevel = float.Parse(data[2]["statsmpregenperlevel"].ToString());

        Helium = 100;
        Charging = true;
        isDanger = false;
        isZero = false;
    }

    private void Update()
    {

        if(Charging)        //충전가능한 상태인 경우 1초마다 충전
        {
            TimeCheck += Time.deltaTime;
            if (TimeCheck > 1.0f)
            {
                ChargeHe();
                TimeCheck = 0;
            }
        }

        if(Helium<=50)
        {
            isDanger = true; //->스킬 수정?
        }
        else
        {
            isDanger = false;
        }

        if(Helium==0)
        {
            isZero = true;
        }
        //Debug.Log("Helium " + Helium);
    }

    public void DropHe()
    {
        if(Helium >= 20)
        Helium -= 20;
        StartCoroutine("ConsumeHe");
    }

    IEnumerator ConsumeHe()
    {
        Charging = false;
        yield return new WaitForSeconds(4.0f);
        Charging = true;
    }
     private void ChargeHe()
    {
        if (Helium < maxHelium)
        {
            Helium += 10;
            if (Helium > maxHelium) Helium = maxHelium;
        }
    }
}

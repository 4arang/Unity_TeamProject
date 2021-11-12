using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Monster_Stats : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();

    public float HP;   //Health Point
    public float HPregen; //HP increasemnt per every 90s
    public float HPregenperLevel; //HPregen increasement per every 90s
    public int HPPtime = 90000; //�ٸ� ���ݰ� ����->�ϳ��� ����
    public float hp;

    public float MaxAD;
    public float AD; //Attack Damage
    public float ADperTime; //AD increasement per every 90s


    public float Armor;    //Armor Point
    public float Armorp;   //Armor increasement per every 90s


    public int MRP;             //Magic Resistance Point
    public int MRPp;            //MRP increasement per every 90s

    public float AttackSpeed;
    public float MoveSpeed;
    public float AttackRange;

    public int Minion_Number;

    public int Gold_Normal = 300;
    public int Gold_Advanced = 45;
    public float EXP = 200;

    private bool MonsterSetup = true;

    //for passive
    float AD_;
    float Armor_;
    int MRP_;

    private void Start()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");

        Minion_Number = int.Parse(data[13]["tags"].ToString());

        HP = float.Parse(data[13]["statshp"].ToString());
        HPregen = float.Parse(data[13]["statshpregen"].ToString());
        HPregenperLevel = float.Parse(data[13]["statshpregenperlevel"].ToString());

        MaxAD = 300;
        AD = float.Parse(data[13]["statsattackdamage"].ToString());
        ADperTime = float.Parse(data[13]["statsattackdamageperlevel"].ToString());

        Armor = float.Parse(data[13]["statsarmor"].ToString());
        Armorp = float.Parse(data[13]["statsarmorperlevel"].ToString());

       
        MRP =int.Parse(data[13]["statsspellblock"].ToString());
        MRPp = int.Parse(data[13]["statsspellblockperlevel"].ToString());

        AttackSpeed = float.Parse(data[6]["statsattackspeed"].ToString());
        MoveSpeed = float.Parse(data[6]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[6]["statsattackrange"].ToString());

        GetComponentInChildren<HP_Bar>().SetMaxHP(HP);
        hp = 100; //hp = HP;

        //for passive
        AD_ = AD;
        Armor_ = Armor;
        MRP_ = MRP;

        stopwatch.Start();
    }

    private void FixedUpdate()
    {
        long elapsedTime = stopwatch.ElapsedMilliseconds;
        if (elapsedTime % HPPtime == 0)
        {
            if (hp <= HP)
            {
                hp += HPregen;
                HPregen += HPregenperLevel;
                if (hp > HP) hp = HP;
                GetComponentInChildren<HP_Bar>().SetHP(hp);
            }
            if (AD < MaxAD)
            {
                AD += ADperTime;
                if (AD > MaxAD) AD = MaxAD;
            }
            Armor += Armorp;
            MRP += MRPp;
        }
        if (MonsterSetup)
        {
            if (elapsedTime >= 8000)
            {
                GetComponent<Monster>().Start_();
                MonsterSetup = false;
            }
        }
    }

    public void DropHP(float damage)
    {


        damage *= (1 - Armor / (100 + Armor));

        hp -= damage;

        if (hp <= 0)
        {
            GetComponent<Monster>().Die();
        }
        UnityEngine.Debug.Log("Monster hp " + hp);
        GetComponent<Monster>().Attacked();
        GetComponentInChildren<HP_Bar>().SetHP(hp);

    }

    //IEnumerator Dying()
    //{
    //    yield return new WaitForSeconds(2.5f);
    //    animator.SetBool("Die", false);
    //}

    public void PassiveOn()
    {
        AD = AD_ * 1.5f;
        Armor = Armor_ * 2;
        MRP = MRP_ * 2;
    }

    public void PassiveOff()
    {
        AD = AD_;
        Armor = Armor_;
        MRP = MRP_;
    }
}

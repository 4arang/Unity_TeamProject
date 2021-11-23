using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Monster_Stats : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();

    public float MaxHP;   //Health Point
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

        MaxHP = float.Parse(data[13]["statshp"].ToString());
        HPregen = float.Parse(data[13]["statshpregen"].ToString());
        HPregenperLevel = float.Parse(data[13]["statshpregenperlevel"].ToString());

        MaxAD = 300;
        AD = float.Parse(data[13]["statsattackdamage"].ToString());
        ADperTime = float.Parse(data[13]["statsattackdamageperlevel"].ToString());

        Armor = float.Parse(data[13]["statsarmor"].ToString());
        Armorp = float.Parse(data[13]["statsarmorperlevel"].ToString());


        MRP = int.Parse(data[13]["statsspellblock"].ToString());
        MRPp = int.Parse(data[13]["statsspellblockperlevel"].ToString());

        AttackSpeed = float.Parse(data[6]["statsattackspeed"].ToString());
        MoveSpeed = float.Parse(data[6]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[6]["statsattackrange"].ToString());

        GetComponentInChildren<HP_Bar>().SetMaxHP(MaxHP, 0.06f);
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
            if (hp <= MaxHP)
            {
                hp += HPregen;
                HPregen += HPregenperLevel;
                if (hp > MaxHP) hp = MaxHP;
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

    public void DropHP(float damage, Transform obj)
    {


    damage *= (1 - Armor / (100 + Armor));

        hp -= damage;

        if (hp <= 0)
        {
                //사거리 2000이내의 팀 영웅 이동속도 증가
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, 20.0f);
        foreach (Collider col in colliderArray)
        {
            if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
             && (player.TeamColor == obj.GetComponent<Player_Stats>().TeamColor))
            {
                //이동속도 175% 2초간 줄어들게
            }
        }
        //팀원들 잃은체력 15% 회복
        //경험치 골드 제공
        //90초간 주문력 공격력 16%증가
        //사망시 사라지는 버프
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    //Player Information
    public byte AttackAbility; // coldy 8 wt 3  xerion2
    public byte DefenseAbility;
    public byte MagicAbility;
    public byte Difficulty;


    //Game Stats
    public bool TeamColor;
    public float CurrentHP;
    public float MaxHP;             //Health Point
    public int HPperLevel;     //HP increasement per Level
    public float CurrentMP;
    public float MaxMP;             //Mana Point
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
    private float Recover_MoveSpeed;
    public int AttackRange;
    public float HPregen;
    public float HPregenperLevel;
    public int MPregen;
    public float MPregenperLevel;

    public float hp;
    public float mp;
    

    //Coldy special (Helium)
    [Range(0.0f, 100.0f)]
    public int Helium=100;
    public int minHelium = 0;
    public int maxHelium = 100;
    public bool Charging; //충전상태
    public bool isDanger; //헬륨 50이하인경우 상태 ->스킬능력강화
    public bool isZero; //헬륨 0인경우 상태->스킬사용x
    private float TimeCheck_ColD = 0.0f;

    //Xerion special
    public int Energy = 0;
    private float TimeCheck = 0.0f;
    private float TimeCheck2 = 0.0f;

    //WT special (W 스킬 사용 후 데미지 회복)
    public float DamageStorage = 0;

    //for minion targetsetting
    public bool isAttack_Minion = false;
    public bool isAttack_Player = false;

    public int Level = 1;




    private void Start()
    {
        if (TryGetComponent(out ColD_Stats Champ_Num1))
        {
            TeamColor = GetComponent<ColD_Stats>().TeamColor;
            AttackAbility = GetComponent<ColD_Stats>().AttackAbility;
            DefenseAbility = GetComponent<ColD_Stats>().DefenseAbility;
            MagicAbility = GetComponent<ColD_Stats>().MagicAbility;
            Difficulty = GetComponent<ColD_Stats>().Difficulty;



            MaxHP = GetComponent<ColD_Stats>().HP;
            HPperLevel = GetComponent<ColD_Stats>().HPperLevel;
            MaxMP = GetComponent<ColD_Stats>().MP;
            MPperLevel = GetComponent<ColD_Stats>().MPperLevel;
            AP = GetComponent<ColD_Stats>().AP;
            APperLevel = GetComponent<ColD_Stats>().APperLevel;
            AD = GetComponent<ColD_Stats>().AD;
            ADperLevel = GetComponent<ColD_Stats>().ADperLevel;
            MRP = GetComponent<ColD_Stats>().MRP;
            MRPperLevel = GetComponent<ColD_Stats>().MRPperLevel;
            AttackSpeed = GetComponent<ColD_Stats>().AttackSpeed;
            AttackSpeedperLevel = GetComponent<ColD_Stats>().AttackSpeedperLevel;
            MoveSpeed = GetComponent<ColD_Stats>().MoveSpeed;
            AttackRange = GetComponent<ColD_Stats>().AttackRange;
            HPregen = GetComponent<ColD_Stats>().HPregen;
            HPregenperLevel = GetComponent<ColD_Stats>().HPregenperLevel;
            MPregen = GetComponent<ColD_Stats>().MPregen;
            MPregenperLevel = GetComponent<ColD_Stats>().MPregenperLevel;


            hp = MaxHP;
            mp = MaxMP;

            Helium = 100;
        }
        else if (TryGetComponent(out Xerion_Stats Champ_Num2))
        {
            TeamColor = GetComponent<Xerion_Stats>().TeamColor;
            AttackAbility = GetComponent<Xerion_Stats>().AttackAbility;
            DefenseAbility = GetComponent<Xerion_Stats>().DefenseAbility;
            MagicAbility = GetComponent<Xerion_Stats>().MagicAbility;
            Difficulty = GetComponent<Xerion_Stats>().Difficulty;



            MaxHP = GetComponent<Xerion_Stats>().HP;
            HPperLevel = GetComponent<Xerion_Stats>().HPperLevel;
            MaxMP = GetComponent<Xerion_Stats>().MP;
            MPperLevel = GetComponent<Xerion_Stats>().MPperLevel;
            AP = GetComponent<Xerion_Stats>().AP;
            APperLevel = GetComponent<Xerion_Stats>().APperLevel;
            AD = GetComponent<Xerion_Stats>().AD;
            ADperLevel = GetComponent<Xerion_Stats>().ADperLevel;
            MRP = GetComponent<Xerion_Stats>().MRP;
            MRPperLevel = GetComponent<Xerion_Stats>().MRPperLevel;
            AttackSpeed = GetComponent<Xerion_Stats>().AttackSpeed;
            AttackSpeedperLevel = GetComponent<Xerion_Stats>().AttackSpeedperLevel;
            MoveSpeed = GetComponent<Xerion_Stats>().MoveSpeed;
            AttackRange = GetComponent<Xerion_Stats>().AttackRange;
            HPregen = GetComponent<Xerion_Stats>().HPregen;
            HPregenperLevel = GetComponent<Xerion_Stats>().HPregenperLevel;
            MPregen = GetComponent<Xerion_Stats>().MPregen;
            MPregenperLevel = GetComponent<Xerion_Stats>().MPregenperLevel;

            CurrentHP = MaxHP;
            CurrentMP = MaxMP;
            hp = MaxHP;
            mp = MaxMP;

            Xerion_Manager.Instance.Xerion_AD = AD;
        }
        else if (TryGetComponent(out WhiteTiger_Stats Champ_Num3))
        {
            TeamColor = GetComponent<WhiteTiger_Stats>().TeamColor;
            AttackAbility = GetComponent<WhiteTiger_Stats>().AttackAbility;
            DefenseAbility = GetComponent<WhiteTiger_Stats>().DefenseAbility;
            MagicAbility = GetComponent<WhiteTiger_Stats>().MagicAbility;
            Difficulty = GetComponent<WhiteTiger_Stats>().Difficulty;



            MaxHP = GetComponent<WhiteTiger_Stats>().HP;
            HPperLevel = GetComponent<WhiteTiger_Stats>().HPperLevel;
            MaxMP = GetComponent<WhiteTiger_Stats>().MP;
            MPperLevel = GetComponent<WhiteTiger_Stats>().MPperLevel;
            AP = GetComponent<WhiteTiger_Stats>().AP;
            APperLevel = GetComponent<WhiteTiger_Stats>().APperLevel;
            AD = GetComponent<WhiteTiger_Stats>().AD;
            ADperLevel = GetComponent<WhiteTiger_Stats>().ADperLevel;
            MRP = GetComponent<WhiteTiger_Stats>().MRP;
            MRPperLevel = GetComponent<WhiteTiger_Stats>().MRPperLevel;
            AttackSpeed = GetComponent<WhiteTiger_Stats>().AttackSpeed;
            AttackSpeedperLevel = GetComponent<WhiteTiger_Stats>().AttackSpeedperLevel;
            MoveSpeed = GetComponent<WhiteTiger_Stats>().MoveSpeed;
            AttackRange = GetComponent<WhiteTiger_Stats>().AttackRange;
            HPregen = GetComponent<WhiteTiger_Stats>().HPregen;
            HPregenperLevel = GetComponent<WhiteTiger_Stats>().HPregenperLevel;
            MPregen = GetComponent<WhiteTiger_Stats>().MPregen;
            MPregenperLevel = GetComponent<WhiteTiger_Stats>().MPregenperLevel;


            CurrentHP = MaxHP;
            CurrentMP = MaxMP;
            hp = MaxHP;
            mp = MaxMP;
        }

        Recover_MoveSpeed = MoveSpeed;
        //GetComponentInChildren<HP_Bar>().SetMaxHP(HP);
    }

    private void Update()
    {
        if (AttackAbility == 8) //coldy
        {
            if (Charging)        //충전가능한 상태인 경우 1초마다 충전
            {
                TimeCheck += Time.deltaTime;
                if (TimeCheck > 1.0f)
                {
                    ChargeHe();
                    TimeCheck = 0;
                }
            }

            if (Helium <= 50)
            {
                isDanger = true; //->스킬 수정?
            }
            else
            {
                isDanger = false;
            }

            if (Helium == 0)
            {
                isZero = true;
            }
            //Debug.Log("Helium " + Helium);
        }

        if (AttackAbility == 2) //xerion
        {
            if (Energy >= 100)
            {
                Energy = 100;
                GetComponent<Xerion>().OnPassive();
            }



            Regen();

        }


    }

    void Regen()
    {
        TimeCheck += Time.deltaTime;
        if (TimeCheck % 1.0f == 0)
        {
            if (hp < MaxHP)
            {
                hp += HPregen;
                if (hp > MaxHP) hp = MaxHP;
            }
            if (mp < MaxMP)
            {
                mp += MPregen;
                if (mp > MaxMP) mp = MaxMP;
            }
        }

    }


    public void DropMP(float energy)
    {
        if (mp >= energy)
        {
            mp -= energy;
        }
        if (mp > MaxMP) mp = MaxMP;
    }



    public void DropHP(float Damage)
    {
        Damage *= (1 - AP / (100 + AP));
        hp -= Damage;
        if (AttackAbility == 3) //for whitetiger
            StartCoroutine("StoreDamage", Damage);
        Debug.Log("player HP" + hp);

        GetComponentInChildren<HP_Bar>().SetHP(hp);

        if(hp<=0)
        {

        }

    }

    IEnumerator StoreDamage(float Damage)
    {
        DamageStorage += Damage;
        yield return new WaitForSeconds(1.5f);
        DamageStorage -= Damage;
    }

    public void DropHe()
    {
        if (Helium >= 20)
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

    public void DropSpeed(float damage, float time)
    {
        MoveSpeed *= damage;
        StartCoroutine("Active_SpeedReturn", time);
    }
    IEnumerator Active_SpeedReturn(float time)
    {
        yield return new WaitForSeconds(time); //1초후에 스피드 복구
        MoveSpeed = Recover_MoveSpeed; //렙업시 변경시켜주기
    }

    public void Stun(float StunTime)
    {
        DropSpeed(0, StunTime);
        //stop attack
    }

}
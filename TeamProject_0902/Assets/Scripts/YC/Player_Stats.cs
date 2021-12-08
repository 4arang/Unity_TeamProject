using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Player_Stats : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Sprite lvlupImg;
    //Player Information
    public byte AttackAbility; // coldy 3 wt 8  xerion2
    public byte DefenseAbility;
    public byte MagicAbility;
    public byte Difficulty;

    //UI info
    Stats_Text UI_Stats;
    UI_Bar ActionBar;
    Skill_BarQ Qbar;
    Skill_BarW Wbar;
    Skill_BarE Ebar;
    Skill_BarR Rbar;
    int Qlevel = 1;
    int Wlevel = 1;
    int Elevel = 1;
    int Rlevel = 1;


    //Game Stats
    public bool TeamColor;
    public float MaxHP;             //Health Point
    public int HPperLevel;     //HP increasement per Level
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

    public float hp;    //current HP;
    public float mp;    //current MP;

    public int Level = 1;
    

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


    private Transform[] champs; //처치에 관여한 챔피언들


    private void Awake()
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


           // GetComponentInChildren<RP_Bar>().SetMaxRP(MaxMP);
            MaxMP=100;
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


            //GetComponentInChildren<RP_Bar>().SetMaxRP(MaxMP); //mp들어가나
        }
        hp = MaxHP;
        

        Recover_MoveSpeed = MoveSpeed;

        PlayerStatManager.Instance.Player = this.transform;
      //  GetComponentInChildren<HP_Bar>().SetMaxHP(MaxHP);

    }
    private void Start()
    {
        //stats bar
        UI_Stats = FindObjectOfType<Stats_Text>();

        GetComponentInChildren<RP_Bar>().SetMaxRP(MaxMP);
        GetComponentInChildren<HP_Bar>().SetMaxHP(MaxHP, 0.259f);

        UI_Stats.SetAD(AD);
        //UI_Stats.SetMP();
        UI_Stats.SetArmor(AP);
        UI_Stats.SetMRP(MRP);
        //UI_Stats.SetCoolTime();
        UI_Stats.SetMoveSpeed(MoveSpeed);
        //UI_Stats.SetCriticalRate
        UI_Stats.SetAttackSpeed(AttackSpeed);

        //action bar
        ActionBar = FindObjectOfType<UI_Bar>();

        ActionBar.SetMaxHP(MaxHP);
        ActionBar.SetHP(hp);
        ActionBar.SetMaxRP(MaxMP);
        ActionBar.SetRP(mp);
        if (AttackAbility == 3) ActionBar.SetRP(Helium);

        //skill bar
        Qbar = FindObjectOfType<Skill_BarQ>();
        Wbar = FindObjectOfType<Skill_BarW>();
        Ebar = FindObjectOfType<Skill_BarE>();
        Rbar = FindObjectOfType<Skill_BarR>();
    }

    private void Update()
    {
        if (AttackAbility == 3) //coldy
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

            GetComponentInChildren<RP_Bar>().SetRP(Helium);
            ActionBar.SetRP(Helium);
        }

        if (AttackAbility == 2) //xerion
        {
            if (Energy >= 100)
            {
                Energy = 100;
                GetComponent<Xerion>().OnPassive();
            }
        }
        Regen();
        if (Input.GetKeyDown(KeyCode.L)) //경험치 치트키
        {
            GetComponent<Player_Level>().GetEXP(280);
        }
    }

    void Regen()
    {
        TimeCheck += Time.deltaTime;
      
        if (TimeCheck > 1.0f)
        {
            if (hp < MaxHP)
            {
                hp += HPregen;
                if (hp > MaxHP) hp = MaxHP;
            }
            if (AttackAbility == 2 && mp < MaxMP) //mp regen only goes for xerion
            {
                mp += MPregen;
                if (mp > MaxMP) mp = MaxMP;

                GetComponentInChildren<RP_Bar>().SetRP(mp);
                ActionBar.SetRP(mp);
            }

            TimeCheck = 0;

            GetComponentInChildren<HP_Bar>().SetHP(hp);
            ActionBar.SetHP(hp);

        }
    }


    public void DropMP(float energy)
    {
        if (mp >= energy)
        {
            mp -= energy;
        }
        if (mp > MaxMP) mp = MaxMP;

        GetComponentInChildren<RP_Bar>().SetRP(mp);

        ActionBar.SetRP(mp);
    }



    public void DropHP(float Damage, Transform obj)
    {
        Damage *= (1 - AP / (100 + AP));
        hp -= Damage;
        if (AttackAbility == 3) //for whitetiger
            StartCoroutine("StoreDamage", Damage);

        GetComponentInChildren<HP_Bar>().SetHP(hp);

        ActionBar.SetHP(hp);

        if (hp<=0)
        {
            //죽었을때
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, 16.0f);
            foreach (Collider col in colliderArray)
            {
                if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                 && (player.TeamColor != TeamColor))
                {
                    //사거리 내에 있는 경우 자동으로 획득 처치에 관여한 플레이와 중복 방지
                    if((player.transform != champs[0]) 
                        ||(player.transform !=champs[1]))
                    {
                        player.GetComponent<Player_Level>().GetEXP(90 * (Level - 1) + 42);
                    }
                }
            }
            return;
        }

       
        if(obj.CompareTag("Player"))
        {
            if (true/*/해당 오브젝트가 죽어있는 상태인 경우 10초간 저장*/)
            {
                StartCoroutine("StoreChampion", obj);
            }
        }
    }

    IEnumerator StoreChampion(Transform champion)
    {
        champs[0] = champion;
        yield return new WaitForSeconds(10.0f);
        champs = null;
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
        GetComponentInChildren<RP_Bar>().SetRP(Helium);
        ActionBar.SetRP(Helium);
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
        UI_Stats.SetMoveSpeed(MoveSpeed);
    }
    IEnumerator Active_SpeedReturn(float time)
    {
        yield return new WaitForSeconds(time); //1초후에 스피드 복구
        MoveSpeed = Recover_MoveSpeed; //렙업시 변경시켜주기
        UI_Stats.SetMoveSpeed(MoveSpeed);
    }

    public void Stun(float StunTime)
    {
        DropSpeed(0, StunTime);
        UI_Stats.SetMoveSpeed(MoveSpeed);
        //stop attack
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //동기화되는 변수들 추가
            stream.SendNext(hp);
            stream.SendNext(MaxHP);
            stream.SendNext(mp);
            stream.SendNext(MaxMP);
        }
        else
        {
            //받아오는 변수들 추가
            hp = (float)stream.ReceiveNext();
            MaxHP = (float)stream.ReceiveNext();
            mp = (float)stream.ReceiveNext();
            MaxMP = (float)stream.ReceiveNext();
        }
    }

    public void LevelupQ()
    { 
        if (AttackAbility == 3) //coldy
        {
            GetComponent<ColD_W>().levelupQ();
        }

        else if (AttackAbility == 2) //xerion
        {
            GetComponent<Xerion_Shooting_Skill>().levelupQ();
        }
        else //WT
        {
            GetComponent<WhiteTiger>().levelUpQ();
        }

        Qbar.imgLevelorg[Qlevel-1].sprite = lvlupImg;
        Qlevel++;
    }
    public void LevelupW()
    {
        if (AttackAbility == 3) //coldy
        {
            GetComponent<ColD_W>().levelupW();
        }

        else if (AttackAbility == 2) //xerion
        {
            GetComponent<Xerion_Shooting_Skill>().levelupW();
        }
        else //WT
        {
            GetComponent<WhiteTiger_Skill>().levelUpW();
        }
        Wbar.imgLevelorg[Wlevel - 1].sprite = lvlupImg;
        Wlevel++;

    }
    public void LevelupE()
    {
        if (AttackAbility == 3) //coldy
        {
            GetComponent<ColD_W>().levelupE();
        }

        else if (AttackAbility == 2) //xerion
        {
            GetComponent<Xerion_Shooting_Skill>().levelupE();
        }
        else //WT
        {
            GetComponent<WhiteTiger_Skill>().levelUpE();
        }
        Ebar.imgLevelorg[Elevel - 1].sprite = lvlupImg;
        Elevel++;
    }
    public void LevelupR()
    {
      if (AttackAbility == 3) //coldy
        {
            GetComponent<ColD_W>().levelupR();
        }

        else if (AttackAbility == 2) //xerion
        {
            GetComponent<Xerion_Shooting_Skill>().levelupR();
        }
        else //WT
        {
            GetComponent<WhiteTiger_Skill>().levelUpR();
        }
        Rbar.imgLevelorg[Rlevel - 1].sprite = lvlupImg;
        Rlevel++;
    }

    public void GetHP(float gain)
    {

        hp += gain;
        if (hp >= MaxHP)
            hp = MaxHP;

        GetComponentInChildren<HP_Bar>().SetHP(hp);
        ActionBar.SetHP(hp);

    }

}
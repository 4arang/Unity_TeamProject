using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class Minion_Stats : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();

    public float MaxHP;
    public float HP;   
    public float HPregen; 
    public float HPregenperLevel;
    public int HPPtime;

    public float MaxAD;
    public float AD; 
    public float ADperTime; 
    public int ADPtime;

    public float MaxAP;
    public float AP;  
    public float APp;   
    public int APPtime;

    public float MaxMoveSpeed;
    public float MoveSpeed;
    public float AttackRange;
    public int MoveSpeedp;
    public int MoveSpeedptime;
    private float Recover_MoveSpeed;

    private byte MinionNum;

    [SerializeField] private GameObject DamagedEffect;

    void Start()
    {
       if(TryGetComponent(out Minion1_Stats Minion_Num1))
        {
            MaxHP = GetComponent<Minion1_Stats>().MaxHP;
            HP = GetComponent<Minion1_Stats>().HP;
            HPregen = GetComponent<Minion1_Stats>().HPregen;
            HPregenperLevel = GetComponent<Minion1_Stats>().HPregenperLevel;
            HPPtime = GetComponent<Minion1_Stats>().HPPtime;

            MaxAD = GetComponent<Minion1_Stats>().MaxAD;
            AD = GetComponent<Minion1_Stats>().AD;
            ADperTime = GetComponent<Minion1_Stats>().ADperTime;
            ADPtime = GetComponent<Minion1_Stats>().ADPtime;

            MaxAP = GetComponent<Minion1_Stats>().MaxAP;
            AP = GetComponent<Minion1_Stats>().AP;
            APp = GetComponent<Minion1_Stats>().APp;
            APPtime = GetComponent<Minion1_Stats>().APPtime;

            MaxMoveSpeed = GetComponent<Minion1_Stats>().MaxMoveSpeed;
            MoveSpeed = GetComponent<Minion1_Stats>().MoveSpeed;
            AttackRange = GetComponent<Minion1_Stats>().AttackRange;
            MoveSpeedp = GetComponent<Minion1_Stats>().MoveSpeedp;
            MoveSpeedptime = GetComponent<Minion1_Stats>().MoveSpeedptime;

            MinionNum = GetComponent<Minion1_Stats>().Minion_Number;
        }
        else if (TryGetComponent(out Minion2_Stats Minion_Num2))
        {
            MaxHP = GetComponent<Minion2_Stats>().MaxHP;
            HP = GetComponent<Minion2_Stats>().HP;
            HPregen = GetComponent<Minion2_Stats>().HPregen;
            HPPtime = GetComponent<Minion2_Stats>().HPPtime;

            MaxAD = GetComponent<Minion2_Stats>().MaxAD;
            AD = GetComponent<Minion2_Stats>().AD;
            ADperTime = GetComponent<Minion2_Stats>().ADperTime;
            ADPtime = GetComponent<Minion2_Stats>().ADPtime;

            MaxMoveSpeed = GetComponent<Minion2_Stats>().MaxMoveSpeed;
            MoveSpeed = GetComponent<Minion2_Stats>().MoveSpeed;
            AttackRange = GetComponent<Minion2_Stats>().AttackRange;
            MoveSpeedp = GetComponent<Minion2_Stats>().MoveSpeedp;
            MoveSpeedptime = GetComponent<Minion2_Stats>().MoveSpeedptime;

            MinionNum = GetComponent<Minion2_Stats>().Minion_Number;
        }
        else if (TryGetComponent(out Minion3_Stats Minion_Num3))
        {
            MaxHP = GetComponent<Minion3_Stats>().MaxHP;
            HP = GetComponent<Minion3_Stats>().HP;
            HPregen = GetComponent<Minion3_Stats>().HPregen;
            HPPtime = GetComponent<Minion3_Stats>().HPPtime;

            MaxAD = GetComponent<Minion3_Stats>().MaxAD;
            AD = GetComponent<Minion3_Stats>().AD;
            ADperTime = GetComponent<Minion3_Stats>().ADperTime;
            ADPtime = GetComponent<Minion3_Stats>().ADPtime;

            MaxMoveSpeed = GetComponent<Minion3_Stats>().MaxMoveSpeed;
            MoveSpeed = GetComponent<Minion3_Stats>().MoveSpeed;
            AttackRange = GetComponent<Minion3_Stats>().AttackRange;
            MoveSpeedp = GetComponent<Minion3_Stats>().MoveSpeedp;
            MoveSpeedptime = GetComponent<Minion3_Stats>().MoveSpeedptime;

            MinionNum = GetComponent<Minion3_Stats>().Minion_Number;
        }
        else if (TryGetComponent(out Minion4_Stats Minion_Num4))
        {

            HP = GetComponent<Minion4_Stats>().HP;
            HPregen = GetComponent<Minion4_Stats>().HPregen;
            HPPtime = GetComponent<Minion4_Stats>().HPPtime;

            AD = GetComponent<Minion4_Stats>().AD;
            ADperTime = GetComponent<Minion4_Stats>().ADperTime;
            ADPtime = GetComponent<Minion4_Stats>().ADPtime;

            MaxMoveSpeed = GetComponent<Minion4_Stats>().MaxMoveSpeed;
            MoveSpeed = GetComponent<Minion4_Stats>().MoveSpeed;
            AttackRange = GetComponent<Minion4_Stats>().AttackRange;
            MoveSpeedp = GetComponent<Minion4_Stats>().MoveSpeedp;
            MoveSpeedptime = GetComponent<Minion4_Stats>().MoveSpeedptime;

            MinionNum = GetComponent<Minion4_Stats>().Minion_Number;
        }


        DamagedEffect.SetActive(false);
    }

    private void Awake()
    {
        stopwatch.Start();
    }

    void Update()
    {
        UnityEngine.Debug.Log("Minion." + MinionNum + " HP " + HP);
        UnityEngine.Debug.Log("Minion." + MinionNum + " Speed " + MoveSpeed);
    }




    public void DropHP(float damage)
    {
        HP -= damage;
    }

    public void DropSpeed(float damage, float time)
    {
        MoveSpeed *= damage;
        StartCoroutine("Active_SpeedReturn", time);
    }
    IEnumerator Active_SpeedReturn(float time)
    {
        yield return new WaitForSeconds(time); //1초후에 스피드 복구
        MoveSpeed = Recover_MoveSpeed;
    }
}

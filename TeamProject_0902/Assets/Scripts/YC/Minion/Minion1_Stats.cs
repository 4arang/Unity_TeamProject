using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics; //unity 내장시간함수? 어떻게 처리해야하는지 게임내 멈춰있는동안 처리같은거?

public class Minion1_Stats : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();

    public float MaxHP;
    public float HP;   //Health Point
    public float HPregen; //HP increasemnt per every 90s
    public float HPregenperLevel;
    public int HPPtime = 90000;

    public float MaxAD;
    public float AD; //Attack Damage
    public float ADperTime; //AD increasement per every 90s
    public int ADPtime = 90000;

    public float MaxAP;
    public float AP;    //Armor Point
    public float APp;   //Armor point per level
    public int APPtime = 180000;

    public float MaxMoveSpeed;
    public float MoveSpeed;
    public float AttackRange;
    public int MoveSpeedp = 25; //모든 미니언 고정
    public int MoveSpeedptime = 300000; //300초마다 ms증가
    private float Recover_MoveSpeed;

    public byte Minion_Number;



   

    private void Awake()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");

        Minion_Number = byte.Parse(data[4]["tags"].ToString());

        MaxHP = 1300;
        HP = float.Parse(data[4]["statshp"].ToString());
        HPregen = float.Parse(data[4]["statshpregen"].ToString());
        HPregenperLevel = float.Parse(data[4]["statshpregenperlevel"].ToString());

        MaxAD = 80;
        AD = float.Parse(data[4]["statsattackdamage"].ToString());
        ADperTime = float.Parse(data[4]["statsattackdamageperlevel"].ToString());

        MaxAP = 16;
        AP = float.Parse(data[4]["statsarmor"].ToString());
        APp = float.Parse(data[4]["statsarmorperlevel"].ToString());

        MaxMoveSpeed = 425;
        MoveSpeed = float.Parse(data[4]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[4]["statsattackrange"].ToString());
        Recover_MoveSpeed = MoveSpeed;



        stopwatch.Start();
    }

    //private void FixedUpdate()
    //{
    //    long elapsedTime = stopwatch.ElapsedMilliseconds;
    //    if (elapsedTime % HPPtime == 0)
    //    {
    //        if (HP < MaxHP)
    //        {
    //            HP += HPregen;
    //            HPregen += HPregenperLevel;
    //            if (HP > MaxHP) HP = MaxHP;
    //        }
    //        if (AD < MaxAD)
    //        {
    //            AD += ADperTime;
    //            if (AD > MaxAD) AD = MaxAD;
    //        }
    //    }
    //    if (elapsedTime % APPtime == 0)
    //    {
    //        if (AP < MaxAP)
    //        {
    //            AP += APp;
    //            if (AP > MaxAP) AP = MaxAP;
    //        }
    //    }
    //    if (elapsedTime % MoveSpeedptime == 0)
    //    {
    //        if (MoveSpeed < MaxMoveSpeed)
    //        {
    //            MoveSpeed += MoveSpeedp;
    //            if (MoveSpeed > MaxMoveSpeed) MoveSpeed = MaxMoveSpeed;
    //        }
    //    }

    //}

    //public void DropHP(float damage)
    //{
    //    HP -= damage;
    //}
    //public void DropSpeed(float damage, float time)
    //{
    //    MoveSpeed *= damage;
    //    StartCoroutine("Active_SpeedReturn", time);
    //}
    //IEnumerator Active_SpeedReturn(float time)
    //{
    //    yield return new WaitForSeconds(time); //1초후에 스피드 복구
    //    MoveSpeed = Recover_MoveSpeed;
    //}
}

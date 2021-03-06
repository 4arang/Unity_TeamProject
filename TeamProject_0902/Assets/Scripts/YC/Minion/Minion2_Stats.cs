using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics; //unity 내장시간함수? 어떻게 처리해야하는지 게임내 멈춰있는동안 처리같은거?

public class Minion2_Stats : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();

    public float MaxHP;
    public float HP;   //Health Point
    public float HPregen; //HP increasemnt per every 90s
    public int HPPtime = 90000;

    public float MaxAD;
    public float AD; //Attack Damage
    public float ADperTime; //AD increasement per every 90s
    public int ADPtime = 90000;

    public float AttackSpeed;
    public float MaxMoveSpeed;
    public float MoveSpeed;
    public float AttackRange;
    public int MoveSpeedp = 25; //모든 미니언 고정
    public int MoveSpeedptime = 300000; //300초마다 ms증가
    private float Recover_MoveSpeed;

    public byte Minion_Number;
   // public bool TeamColor; //true = blue, false = red

    public int Gold_Normal = 6;
    public int Gold_Advanced = 17;
    public float EXP = 29.76f;
    public float EXPperTime = 0.92f;

    private void Awake()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");

        Minion_Number = byte.Parse(data[5]["tags"].ToString());

        MaxHP = 485;
        HP = float.Parse(data[5]["statshp"].ToString());
        HPregen = float.Parse(data[5]["statshpregen"].ToString());

        MaxAD = 120;
        AD = float.Parse(data[5]["statsattackdamage"].ToString());
        ADperTime = float.Parse(data[5]["statsattackdamageperlevel"].ToString());

        AttackSpeed = float.Parse(data[5]["statsattackspeed"].ToString());
        MaxMoveSpeed = 425;
        MoveSpeed = float.Parse(data[5]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[5]["statsattackrange"].ToString());
        Recover_MoveSpeed = MoveSpeed;


        //if (transform.position.x < 0) TeamColor = true;
        //else TeamColor = false;


    }

    //private void FixedUpdate()
    //{
    //    long elapsedTime = stopwatch.ElapsedMilliseconds;
    //    if (elapsedTime % HPPtime == 0)
    //    {
    //        if (HP < MaxHP)
    //        {
    //            HP += HPregen;
    //            if (HP > MaxHP) HP = MaxHP;
    //        }
    //        if (AD < MaxAD)
    //        {
    //            AD += ADperTime;
    //            if (AD > MaxAD) AD = MaxAD;
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

    //    public void DropHP(float damage)
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

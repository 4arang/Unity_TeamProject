using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics; //unity ����ð��Լ�? ��� ó���ؾ��ϴ��� ���ӳ� �����ִµ��� ó��������?

public class Minion3_Stats : MonoBehaviour
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


    public float MaxMoveSpeed;
    public float MoveSpeed;
    public float AttackRange;
    public int MoveSpeedp = 25; //��� �̴Ͼ� ����
    public int MoveSpeedptime = 300000; //300�ʸ��� ms����
    private float Recover_MoveSpeed;

    public byte Minion_Number;



    private void Awake()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");

        Minion_Number = byte.Parse(data[6]["tags"].ToString());

        MaxHP = 2900;
        HP = float.Parse(data[6]["statshp"].ToString());
        HPregen = float.Parse(data[6]["statshpregen"].ToString());


        MaxAD = 77;
        AD = float.Parse(data[6]["statsattackdamage"].ToString());
        ADperTime = float.Parse(data[6]["statsattackdamageperlevel"].ToString());



        MaxMoveSpeed = 425;
        MoveSpeed = float.Parse(data[6]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[6]["statsattackrange"].ToString());
        Recover_MoveSpeed = MoveSpeed;



        stopwatch.Start();
    }

    private void FixedUpdate()
    {
        long elapsedTime = stopwatch.ElapsedMilliseconds;
        if (elapsedTime % HPPtime == 0)
        {
            if (HP < MaxHP)
            {
                HP += HPregen;
                if (HP > MaxHP) HP = MaxHP;
            }
            if (AD < MaxAD)
            {
                AD += ADperTime;
                if (AD > MaxAD) AD = MaxAD;
            }
        }

        if (elapsedTime % MoveSpeedptime == 0)
        {
            if (MoveSpeed < MaxMoveSpeed)
            {
                MoveSpeed += MoveSpeedp;
                if (MoveSpeed > MaxMoveSpeed) MoveSpeed = MaxMoveSpeed;
            }
        }
    }

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
    //    yield return new WaitForSeconds(time); //1���Ŀ� ���ǵ� ����
    //    MoveSpeed = Recover_MoveSpeed;
    //}
}

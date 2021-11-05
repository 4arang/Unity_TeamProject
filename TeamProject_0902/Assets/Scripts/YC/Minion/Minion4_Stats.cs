using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics; //unity ����ð��Լ�? ��� ó���ؾ��ϴ��� ���ӳ� �����ִµ��� ó��������?

public class Minion4_Stats : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();


    public float HP;   //Health Point
    public float HPregen; //HP increasemnt per every 90s
    public int HPPtime = 90000;


    public float AD; //Attack Damage
    public float ADperTime; //AD increasement per every 90s
    public int ADPtime = 90000;

    public float AttackSpeed;
    public float MaxMoveSpeed;
    public float MoveSpeed;
    public float AttackRange;
    public int MoveSpeedp = 25; //��� �̴Ͼ� ����
    public int MoveSpeedptime = 300000; //300�ʸ��� ms����
    private float Recover_MoveSpeed;

    public byte Minion_Number;
    public bool TeamColor; //true = blue, false = red

    public int Gold_Normal = 6;
    public int Gold_Advanced = 60;
    public float EXP = 97f;
    public float EXPperTime = 0.0f;

    private void Awake()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");

        Minion_Number = byte.Parse(data[7]["tags"].ToString());

        HP = float.Parse(data[7]["statshp"].ToString());
        HPregen = float.Parse(data[7]["statshpregen"].ToString());



        AD = float.Parse(data[7]["statsattackdamage"].ToString());
        ADperTime = float.Parse(data[7]["statsattackdamageperlevel"].ToString());


        AttackSpeed = float.Parse(data[7]["statsattackspeed"].ToString());
        MaxMoveSpeed = 425;
        MoveSpeed = float.Parse(data[7]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[7]["statsattackrange"].ToString());
        Recover_MoveSpeed = MoveSpeed;


        if (transform.position.x < 0) TeamColor = true;
        else TeamColor = false;


    }

    //private void FixedUpdate()
    //{
    //    long elapsedTime = stopwatch.ElapsedMilliseconds;
    //    if (elapsedTime % HPPtime == 0)
    //    {
      
    //            HP += HPregen;
       

    //            AD += ADperTime;


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
    //    yield return new WaitForSeconds(time); //1���Ŀ� ���ǵ� ����
    //    MoveSpeed = Recover_MoveSpeed;
    //}
}

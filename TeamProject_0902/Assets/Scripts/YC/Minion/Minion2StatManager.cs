using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics; //unity ����ð��Լ�? ��� ó���ؾ��ϴ��� ���ӳ� �����ִµ��� ó��������?

public class Minion2StatManager : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();
    [Range(0, 485)]
    public float HP = 296;    //Health Point
    public float HPP = 6.0f; //90�ʸ��� HP������
    public float HPPP = 0.3f; //increase per every HPp
    public int HPPtime = 90000;

    [Range(24, 120)]
    public float AD = 24; //Attack Damage
    public float ADP = 3.0f; //90�ʸ��� AD������
    public int ADPtime = 90000;


    public float AP = 0;    //Armor Point


    [Range(100, 425)]
    public int MoveSpeed = 100;
    public int MoveSpeedp = 25; //300�ʸ��� MS ������
    public int MoveSpeedptime = 300000;

    public float AttackSpeed = 0.667f;
    public float AttackRange = 550;

    private static Minion2StatManager sInstance;
    public static Minion2StatManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("Minion2StatManager");
                sInstance = newGameObj.AddComponent<Minion2StatManager>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        stopwatch.Start();
        DontDestroyOnLoad(this.gameObject);
    }
    private void FixedUpdate()
    {
        long elapsedTime = stopwatch.ElapsedMilliseconds;
        if(elapsedTime%HPPtime==0)
        {
            HP += HPP;
            HPP += HPPP;

            AD += ADP;
        }
        if(elapsedTime%MoveSpeedptime==0)
        {
            MoveSpeed += MoveSpeedp;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics; //unity ����ð��Լ�? ��� ó���ؾ��ϴ��� ���ӳ� �����ִµ��� ó��������?

public class Minion1StatManager : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();
    [Range(0, 1300)]
    public float HP = 477;    //Health Point
    public float HPP = 22; //90�ʸ��� HP������
    public float HPPP = 0.3f; //increase per every HPp
    public int HPPtime = 90000;

    [Range(12, 80)]
    public float AD = 12; //Attack Damage
    public float ADP = 3.4f; //90�ʸ��� AD������
    public int ADPtime = 90000;

    [Range(0, 16)]
    public float AP = 0;    //Armor Point
    public float APp = 1.5f; //180�ʸ��� AP������
    public int APPtime = 180000;

    [Range(325, 425)]
    public int MoveSpeed = 325;
    public int MoveSpeedp = 25; //300�ʸ��� MS ������
    public int MoveSpeedptime = 300000;

    private static Minion1StatManager sInstance;
    public static Minion1StatManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("Minion1StatManager");
                sInstance = newGameObj.AddComponent<Minion1StatManager>();
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
        if(elapsedTime%APPtime==0)
        {
            AP += APp;
        }
        if(elapsedTime%MoveSpeedptime==0)
        {
            MoveSpeed += MoveSpeedp;
        }

    }
}

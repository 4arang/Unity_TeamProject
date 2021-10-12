using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics; //unity ����ð��Լ�? ��� ó���ؾ��ϴ��� ���ӳ� �����ִµ��� ó��������?

public class Minion2_Stats : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();

    public float HP;   //Health Point
    public float HPregen; //HP increasemnt per every 90s
    public float HPregenperLevel;
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

    [SerializeField] private GameObject DamagedEffect;

    void Start()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");

    
        HP = float.Parse(data[5]["statshp"].ToString());
        HPregen = float.Parse(data[5]["statshpregen"].ToString());
        HPregenperLevel = float.Parse(data[5]["statshpregenperlevel"].ToString());

        AD = float.Parse(data[5]["statsattackdamage"].ToString());
        ADperTime = float.Parse(data[5]["statsattackdamageperlevel"].ToString());



        MaxMoveSpeed = 425;
        MoveSpeed = float.Parse(data[5]["statsmovespeed"].ToString());
        AttackRange = int.Parse(data[5]["statsattackrange"].ToString());
        Recover_MoveSpeed = MoveSpeed;

        DamagedEffect.SetActive(false);
    }

    void Update()
    {
        UnityEngine.Debug.Log("HP " + HP);
        UnityEngine.Debug.Log("Speed " + MoveSpeed);
    }

    private void Awake()
    {
        stopwatch.Start();
    }

    private void FixedUpdate()
    {
        long elapsedTime = stopwatch.ElapsedMilliseconds;
        if (elapsedTime % HPPtime == 0)
        {

                HP += HPregen;
                HPregen += HPregenperLevel;       

                AD += ADperTime;

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
        yield return new WaitForSeconds(time); //1���Ŀ� ���ǵ� ����
        MoveSpeed = Recover_MoveSpeed;
    }
}

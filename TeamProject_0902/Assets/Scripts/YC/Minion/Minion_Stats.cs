using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;//unity 내장시간함수? 어떻게 처리해야하는지 게임내 멈춰있는동안 처리같은거?

using Photon.Pun;
using Photon.Realtime;
public class Minion_Stats : MonoBehaviourPunCallbacks,IPunObservable
{
    public Camera MainCamera;
    Stopwatch stopwatch = new Stopwatch();
    public bool TeamColor;

    public float MaxHP;
    public float HP;   
    public float HPregen; 
    public float HPregenperLevel;
    public int HPPtime;
    public float hp;

    public float MaxAD;
    public float AD; 
    public float ADperTime; 
    public int ADPtime;

    public float MaxAP;
    public float AP;  
    public float APp;   
    public int APPtime;

    public float AttackSpeed;
    public float MaxMoveSpeed;
    public float MoveSpeed;
    public float AttackRange;
    public int MoveSpeedp;
    public int MoveSpeedptime;
    public float Recover_MoveSpeed;

    public byte MinionNum;

    public int Gold_Normal;
    public int Gold_Advanced;
    public float EXP;
    public float EXPperTime;

    public Vector3 MyPos;

    //for minion target setting
    public bool isAttack_Minion;
    public bool isAttack_Player;
    public bool isDead;
    [SerializeField] private GameObject DamagedEffect;

    Animator animator;

  
    void Start()
    {
        animator = GetComponent<Animator>();
       if(TryGetComponent(out Minion1_Stats Minion_Num1))
        {
            //TeamColor = GetComponent<Minion1_Stats>().TeamColor;

            MaxHP = GetComponent<Minion1_Stats>().MaxHP;
            //HP = GetComponent<Minion1_Stats>().HP;
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

            AttackSpeed = GetComponent<Minion1_Stats>().AttackSpeed;
            MaxMoveSpeed = GetComponent<Minion1_Stats>().MaxMoveSpeed;
            MoveSpeed = GetComponent<Minion1_Stats>().MoveSpeed;
            AttackRange = GetComponent<Minion1_Stats>().AttackRange;
            MoveSpeedp = GetComponent<Minion1_Stats>().MoveSpeedp;
            MoveSpeedptime = GetComponent<Minion1_Stats>().MoveSpeedptime;
            Recover_MoveSpeed = MoveSpeed;

            MinionNum = GetComponent<Minion1_Stats>().Minion_Number;

            Gold_Normal = GetComponent<Minion1_Stats>().Gold_Normal;
            Gold_Advanced = GetComponent<Minion1_Stats>().Gold_Advanced;
            EXP = GetComponent<Minion1_Stats>().EXP;
            EXPperTime = GetComponent<Minion1_Stats>().EXPperTime;


        }
        else if (TryGetComponent(out Minion2_Stats Minion_Num2))
        {
           // TeamColor = GetComponent<Minion2_Stats>().TeamColor;

            MaxHP = GetComponent<Minion2_Stats>().MaxHP;
           // HP = GetComponent<Minion2_Stats>().HP;
            HPregen = GetComponent<Minion2_Stats>().HPregen;
            HPPtime = GetComponent<Minion2_Stats>().HPPtime;

            MaxAD = GetComponent<Minion2_Stats>().MaxAD;
            AD = GetComponent<Minion2_Stats>().AD;
            ADperTime = GetComponent<Minion2_Stats>().ADperTime;
            ADPtime = GetComponent<Minion2_Stats>().ADPtime;

            AttackSpeed = GetComponent<Minion2_Stats>().AttackSpeed;
            MaxMoveSpeed = GetComponent<Minion2_Stats>().MaxMoveSpeed;
            MoveSpeed = GetComponent<Minion2_Stats>().MoveSpeed;
            AttackRange = GetComponent<Minion2_Stats>().AttackRange;
            MoveSpeedp = GetComponent<Minion2_Stats>().MoveSpeedp;
            MoveSpeedptime = GetComponent<Minion2_Stats>().MoveSpeedptime;
            Recover_MoveSpeed = MoveSpeed;

            MinionNum = GetComponent<Minion2_Stats>().Minion_Number;

            Gold_Normal = GetComponent<Minion2_Stats>().Gold_Normal;
            Gold_Advanced = GetComponent<Minion2_Stats>().Gold_Advanced;
            EXP = GetComponent<Minion2_Stats>().EXP;
            EXPperTime = GetComponent<Minion2_Stats>().EXPperTime;
        }
        else if (TryGetComponent(out Minion3_Stats Minion_Num3))
        {
           // TeamColor = GetComponent<Minion3_Stats>().TeamColor;

            MaxHP = GetComponent<Minion3_Stats>().MaxHP;
           // HP = GetComponent<Minion3_Stats>().HP;
            HPregen = GetComponent<Minion3_Stats>().HPregen;
            HPPtime = GetComponent<Minion3_Stats>().HPPtime;

            MaxAD = GetComponent<Minion3_Stats>().MaxAD;
            AD = GetComponent<Minion3_Stats>().AD;
            ADperTime = GetComponent<Minion3_Stats>().ADperTime;
            ADPtime = GetComponent<Minion3_Stats>().ADPtime;

            AttackSpeed = GetComponent<Minion3_Stats>().AttackSpeed;
            MaxMoveSpeed = GetComponent<Minion3_Stats>().MaxMoveSpeed;
            MoveSpeed = GetComponent<Minion3_Stats>().MoveSpeed;
            AttackRange = GetComponent<Minion3_Stats>().AttackRange;
            MoveSpeedp = GetComponent<Minion3_Stats>().MoveSpeedp;
            MoveSpeedptime = GetComponent<Minion3_Stats>().MoveSpeedptime;
            Recover_MoveSpeed = MoveSpeed;

            MinionNum = GetComponent<Minion3_Stats>().Minion_Number;

            Gold_Normal = GetComponent<Minion3_Stats>().Gold_Normal;
            Gold_Advanced = GetComponent<Minion3_Stats>().Gold_Advanced;
            EXP = GetComponent<Minion3_Stats>().EXP;
            EXPperTime = GetComponent<Minion3_Stats>().EXPperTime;
        }
        else if (TryGetComponent(out Minion4_Stats Minion_Num4))
        {
            // TeamColor = GetComponent<Minion4_Stats>().TeamColor;

            MaxHP = GetComponent<Minion4_Stats>().HP;
            HPregen = GetComponent<Minion4_Stats>().HPregen;
            HPPtime = GetComponent<Minion4_Stats>().HPPtime;

            AD = GetComponent<Minion4_Stats>().AD;
            ADperTime = GetComponent<Minion4_Stats>().ADperTime;
            ADPtime = GetComponent<Minion4_Stats>().ADPtime;

            AttackSpeed = GetComponent<Minion4_Stats>().AttackSpeed;
            MaxMoveSpeed = GetComponent<Minion4_Stats>().MaxMoveSpeed;
            MoveSpeed = GetComponent<Minion4_Stats>().MoveSpeed;
            AttackRange = GetComponent<Minion4_Stats>().AttackRange;
            MoveSpeedp = GetComponent<Minion4_Stats>().MoveSpeedp;
            MoveSpeedptime = GetComponent<Minion4_Stats>().MoveSpeedptime;
            Recover_MoveSpeed = MoveSpeed;

            MinionNum = GetComponent<Minion4_Stats>().Minion_Number;

            Gold_Normal = GetComponent<Minion4_Stats>().Gold_Normal;
            Gold_Advanced = GetComponent<Minion4_Stats>().Gold_Advanced;
            EXP = GetComponent<Minion4_Stats>().EXP;
            EXPperTime = GetComponent<Minion4_Stats>().EXPperTime;
        }

        GetComponentInChildren<HP_Bar>().SetMaxHP(MaxHP, 0.038f);
        hp = MaxHP;
        DamagedEffect.SetActive(false);
    }

    private void Awake()
    {
        isDead = false;
        if (transform.position.x < 0) TeamColor = true;
        else TeamColor = false;
        stopwatch.Start();
    }

 
    private void FixedUpdate()
    {
        long elapsedTime = stopwatch.ElapsedMilliseconds;
        if (TryGetComponent(out Minion1_Stats Minion_Num1))
        {
            if (elapsedTime % HPPtime == 0)
            {
                if (hp <= MaxHP)
                {
                    hp += HPregen;
                    HPregen += HPregenperLevel;
                    if (hp > MaxHP) hp = MaxHP;
                    GetComponentInChildren<HP_Bar>().SetHP(hp);
                }
                if (AD <= MaxAD)
                {
                    AD += ADperTime;
                    if (AD > MaxAD) AD = MaxAD;
                }
            }
            if (elapsedTime % APPtime == 0)
            {
                if (AP <= MaxAP)
                {
                    AP += APp;
                    if (AP > MaxAP) AP = MaxAP;
                }
            }
            if (elapsedTime % MoveSpeedptime == 0)
            {
                if (MoveSpeed <= MaxMoveSpeed)
                {
                    MoveSpeed += MoveSpeedp;
                    if (MoveSpeed > MaxMoveSpeed) MoveSpeed = MaxMoveSpeed;
                    Recover_MoveSpeed = MoveSpeed;
                }
            }

            if(elapsedTime % 60000 ==0)
            {
                EXP += 1.53f;
            }
        }
        else if (TryGetComponent(out Minion2_Stats Minion_Num2))
        {
            if (elapsedTime % HPPtime == 0)
            {
                if (hp <= MaxHP)
                {
                    hp += HPregen;
                    if (hp > MaxHP) hp = MaxHP;
                    GetComponentInChildren<HP_Bar>().SetHP(hp);
                }
                if (AD <= MaxAD)
                {
                    AD += ADperTime;
                    if (AD > MaxAD) AD = MaxAD;
                }

            }

            if (elapsedTime % MoveSpeedptime == 0)
            {
                if (MoveSpeed <= MaxMoveSpeed)
                {
                    MoveSpeed += MoveSpeedp;
                    if (MoveSpeed > MaxMoveSpeed) MoveSpeed = MaxMoveSpeed;
                    Recover_MoveSpeed = MoveSpeed;
                }
            }
            if (elapsedTime % 60000 == 0)
            {
                EXP += 0.92f;
            }
        }
        else if (TryGetComponent(out Minion3_Stats Minion_Num3))
        {
            if (elapsedTime % HPPtime == 0)
            {
                if (hp <= MaxHP)
                {
                    hp += HPregen;
                    if (hp > MaxHP) hp = MaxHP;
                    GetComponentInChildren<HP_Bar>().SetHP(hp);
                }
                if (AD <= MaxAD)
                {
                    AD += ADperTime;
                    if (AD > MaxAD) AD = MaxAD;
                }
            }

            if (elapsedTime % MoveSpeedptime == 0)
            {
                if (MoveSpeed <= MaxMoveSpeed)
                {
                    MoveSpeed += MoveSpeedp;
                    if (MoveSpeed > MaxMoveSpeed) MoveSpeed = MaxMoveSpeed;
                    Recover_MoveSpeed = MoveSpeed;
                }
            }
        }
        else if (TryGetComponent(out Minion4_Stats Minion_Num4))
        {
            if (elapsedTime % HPPtime == 0)
            {

                hp += HPregen;
                HP += HPregen;
                GetComponentInChildren<HP_Bar>().SetMaxHP(HP, 0.038f);
                GetComponentInChildren<HP_Bar>().SetHP(hp);

                AD += ADperTime;
            }

            if (elapsedTime % MoveSpeedptime == 0)
            {
                if (MoveSpeed <= MaxMoveSpeed)
                {
                    MoveSpeed += MoveSpeedp;
                    if (MoveSpeed > MaxMoveSpeed) MoveSpeed = MaxMoveSpeed;
                    Recover_MoveSpeed = MoveSpeed;
                }
            }

        }
    }


    public void DropHP(float damage, Transform obj)
    {

        damage *= (1 - AP / (100 + AP));

        hp -= damage;
        GetComponentInChildren<HP_Bar>().SetHP(hp);
        //photonView.RPC("damaged", RpcTarget.AllViaServer, damage);
        //damage *= (1-AP/(100+AP));

        //hp -= damage;
        //GetComponentInChildren<HP_Bar>().SetHP(hp);

        if (hp <= 0)
        {
            isDead = true;
            if (obj.CompareTag("Player")) //플레이어에게 사망한경우
            {
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, 16.0f);
                int i = 0;
                foreach (Collider col in colliderArray)
                {
                    if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                     && (player.TeamColor != TeamColor))
                    {
                        i++;
                        col.GetComponent<Player_Level>().GetEXP(EXP * 0.66f); //경험치 분배
                        if(obj!=col) //처치한 플레이어 외의 다른 플레이어 골드분배보상
                        col.GetComponent<Player_Level>().GetGold(Gold_Normal);
                    }
                }
                if (i >= 2) //두명 이상에게 경험치 분배한 경우
                {
                    obj.GetComponent<Player_Level>().GetEXP(EXP * 0.34f); //처치한 플레이어에게 경험치추가
                   
                }
                obj.GetComponent<Player_Level>().GetGold(Gold_Advanced); //처치골드 추가
            }

            else//미니언에게 사망한 경우
            {
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, 16.0f);

                int i = 0;
                foreach (Collider col in colliderArray)
                {
                    if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                     && (player.TeamColor != TeamColor))
                    {
                        col.GetComponent<Player_Level>().GetEXP(EXP*0.66f); //경험치 분배
                        col.GetComponent<Player_Level>().GetGold(Gold_Normal);
                        obj = col.transform;   
                        i++;   
                    }
                }
                if(i==1) //주위에 플레이어가 한명인 경우
                {
                    obj.GetComponent<Player_Level>().GetEXP(EXP * 0.34f); //경험치 추가
                }
            }
                animator.SetBool("Die", true);
                StartCoroutine("Dying");
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
        MoveSpeed = Recover_MoveSpeed; //미니언 렙업시 변경시켜주기
    }

    public void Stun(float StunTime)
    {
        DropSpeed(0, StunTime); 
        //stop attack
    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Die", false);
        Destroy(gameObject);
    }

    //주기적으로 자동 실행되는 동기화 메서드
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //동기화되는 변수들 추가
            stream.SendNext(hp);
            stream.SendNext(MaxHP);
            stream.SendNext(AP);
            stream.SendNext(AD);
            stream.SendNext(MoveSpeed);
            stream.SendNext(EXP);
        }
        else
        {
            //받아오는 변수들 추가
            hp = (float)stream.ReceiveNext();
            MaxHP = (float)stream.ReceiveNext();
            AP = (float)stream.ReceiveNext();
            AD = (float)stream.ReceiveNext();
            MoveSpeed = (float)stream.ReceiveNext();
            EXP = (float)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void damaged(float damage)
    {

    }
}

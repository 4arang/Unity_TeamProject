using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WhiteTiger_Skill : MonoBehaviour
{
    PhotonView PV;

    private Animator animator;
    public byte WildPoint; //0~4
    public bool isWild;
    public bool isBasicAttack = false;

    [SerializeField] private GameObject Direction;
    [SerializeField] private GameObject Range;
    [SerializeField] private Transform DirectionPos;


    private float DirecAngle;
    private Vector3 mouseVector;



    [Header("W_Skill")]
    [SerializeField] private GameObject W_Shield;
    [SerializeField] private GameObject adv_W_Shield;
    private bool W_Ready = true;
    private float W_CoolTime = 16; //14.5 13 11.5 10
    private float W_AD = 50; //80 110 140 170   //범위 500, 50%회복 
    private float W_AD_monster = 65; //몬스터 + a 65 80 95 110 130
    private float W_AD_adv = 70; //110 150 190 250 //80%회복
    public Skill_BarW skillW;
    private int levelW = 1;

    [Header("E_Skill")]
    [SerializeField] private GameObject E_Aura;
    [SerializeField] private GameObject adv_E_Aura;
    private float E_CoolTime = 12; // fixed
    private bool E_Ready = true;
    public Skill_BarE skillE;
    private int levelE = 1;
    private float E_Recover = 0.1f; //0.12 0.14 0.18 0.20 //데미지 비례 체력 회복
    private float E_Recover_adv = 0.14f; //0.14 0.16 0.18 0.22 0.24
    private float E_Attackspeed = 0.3f; //0.4 0.5 0.6 0.7
    private float E_Attackspeed_adv = 0.5f; //0.6 0.7 0.8 0.9 잃은체력 50%회복


    [Header("R_Skill")]
    public float ref_Dist_time = 0.1f;
    public float ref_flyingSpeed = 1000f;
    private float Distance_Player2Target;
    public Transform Target; //적 위치
    public bool R_Targeted;
    private bool R_Ready = true;
    private float R_CoolTime = 110; // 110 90 70
   public  Skill_BarR skillR;
    private int levelR = 1;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        isWild = false;

        Direction.SetActive(false);
        Range.SetActive(false);

        W_Shield.SetActive(false);
        adv_W_Shield.SetActive(false);
        E_Aura.SetActive(false);
        adv_E_Aura.SetActive(false);

        animator = GetComponent<Animator>();
        WildPoint = 0;

        //skillW = FindObjectOfType<Skill_BarW>();
        //skillE = FindObjectOfType<Skill_BarE>();
        //skillR = FindObjectOfType<Skill_BarR>();
    }


    void Update()
    {
        if (PV.IsMine)
        {
            if (WildPoint == 4)
            {
                isWild = true;
                animator.SetBool("Wildness", true);
                  StartCoroutine("Wild_State");
                WildPoint = 0;
            }



            if (Input.GetKeyDown(KeyCode.W)&&W_Ready)
            {
                skillW.OnSkill(W_CoolTime);
                if (animator.GetBool("W_WT") == false)
                    StartCoroutine("Active_W");
                animator.SetBool("W_WT", true);
                if (!isWild) WildPoint++;
            }

            if (Input.GetKeyDown(KeyCode.E)&&E_Ready)
            {
                skillE.OnSkill(E_CoolTime);
                StartCoroutine("Active_E");
                if (!isWild) WildPoint++;
            }

            if (Input.GetKey(KeyCode.R)&&R_Ready)
            {
                Direction.SetActive(true);
                Range.SetActive(true);
                GetMousePos();//마우스 위치받아오기
                Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up);//화살표방향

                movingManager.Instance.PlayerDirection = DirecAngle; //플레이어에 방향전달
                GetComponentInChildren<WT_Rskill_Collider>().Skill(); //트리거+타겟위치 받아서 Target_pos 저장

            }
            if (Input.GetKeyUp(KeyCode.R)&&R_Ready)
            {
                skillR.OnSkill(R_CoolTime);
                Direction.SetActive(false);
                Range.SetActive(false);

                if (!R_Targeted) //타겟을 찾지 못한경우 끝으로 가게

                {
                    Debug.Log("Non Target");
                    Target = DirectionPos;
                }
                Distance_Player2Target = Vector3.Distance(Range.transform.position, Target.position); //타겟까지 거리 구하기


                movingManager.Instance.PlayerTargetPos = Target.position;
                movingManager.Instance.PlayerClickedPos = Target.position;

                if (animator.GetBool("R_WT") == false)
                {
                    animator.SetBool("R_WT", true);
                    Debug.Log("Distan2Time" + Distance2Time());
                    StartCoroutine("Active_R", Target);
                    if (!isWild) WildPoint++;
                }
            }
        }
    }

    IEnumerator Wild_state()
    {
        while (true)
        {
            yield return new WaitForSeconds(8.0f);
            isWild = false;
            animator.SetBool("Wildness", false);
            break;
        }
    }



    IEnumerator Active_W()
    {
        while (true)
        {
            W_Ready = false;
            if (!isWild)
            {
                yield return new WaitForSeconds(0.3f);
                animator.SetBool("W_WT", false);
                PV.RPC("activeW", RpcTarget.AllViaServer, true);
                W_Shield.GetComponentInChildren<WhiteTiger_W_Damage>().WT_W_AD = W_AD;
                GetComponent<Player_Stats>().GetHP(GetComponent<Player_Stats>().DamageStorage * 0.5f);
                yield return new WaitForSeconds(1.5f);
                PV.RPC("activeW", RpcTarget.AllViaServer, false);
                yield return new WaitForSeconds(W_CoolTime - 1.8f);
                W_Ready = true;
                break;
            }
            else
            {
               yield return new WaitForSeconds(0.3f);
                animator.SetBool("W_WT", false);
                PV.RPC("activeW_adv", RpcTarget.AllViaServer, true);
                adv_W_Shield.GetComponentInChildren<WhiteTiger_Wp_Damage>().WT_WP_AD = W_AD_adv;
                GetComponent<Player_Stats>().GetHP(GetComponent<Player_Stats>().DamageStorage * 0.8f); //데미지 회복
                yield return new WaitForSeconds(1.5f);
                PV.RPC("activeW_adv", RpcTarget.AllViaServer, false);
                yield return new WaitForSeconds(W_CoolTime - 1.8f);
                W_Ready = true;
                break;
            }

        }
    }

    IEnumerator Active_E()
    {
        while (true)
        {
            E_Ready = false;
            if (!isWild)
            {
                GetComponent<WhiteTiger>().activeE(E_Recover);
                GetComponent<Player_Stats>().AttackSpeed *= (1 + E_Attackspeed);
                PV.RPC("activeE", RpcTarget.AllViaServer, true);
                yield return new WaitForSeconds(6.0f);
                GetComponent<WhiteTiger>().disactiveE();
                GetComponent<Player_Stats>().AttackSpeed /= (1 + E_Attackspeed);
                PV.RPC("activeE", RpcTarget.AllViaServer, false);
                yield return new WaitForSeconds(E_CoolTime - 6.0f);
                E_Ready = true;
                break;
            }
            else
            {
                GetComponent<WhiteTiger>().activeE(E_Recover_adv);
                GetComponent<Player_Stats>().AttackSpeed *= (1 + E_Attackspeed_adv);
                PV.RPC("activeE_adv", RpcTarget.AllViaServer, true);
                yield return new WaitForSeconds(6.0f);
                GetComponent<Player_Stats>().AttackSpeed /= (1 + E_Attackspeed_adv);
                PV.RPC("activeE_adv", RpcTarget.AllViaServer, false);
                yield return new WaitForSeconds(E_CoolTime - 6.0f);
                GetComponent<WhiteTiger>().disactiveE();
                E_Ready = true;
                break;
            }
        }
    }

    IEnumerator Active_R(Transform target)
    {
        while (true)
        {
            R_Ready = false;
            float attacktime = Distance2Time();
            yield return new WaitForSeconds(attacktime);
            animator.SetBool("R_WT", false);
            if (R_Targeted) GetComponent<WhiteTiger>().R_Attack(target);
            yield return new WaitForSeconds(R_CoolTime - attacktime);
            R_Ready = true;
            break;
        }

    }




    Vector3 GetMousePos()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        //마우스 위치 받아서 각도계산
        DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
            hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;
        mouseVector = hit.point;


        return hit.point;
    }
    float Distance2Time()
    {
        float time;
        time = Distance_Player2Target * ref_Dist_time;
        return time;
    }

    [PunRPC]
    void activeW(bool b)
    {
        W_Shield.SetActive(b);
    }
    [PunRPC]
    void activeW_adv(bool b)
    {
        adv_W_Shield.SetActive(b);
    }
    [PunRPC]
    void activeE(bool b)
    {
        E_Aura.SetActive(b);
    }
    [PunRPC]
    void activeE_adv(bool b)
    {
        adv_E_Aura.SetActive(b);
    }
    public void levelUpW()
    {
        levelW++;
        W_CoolTime -= 1.5f;
        W_AD += 30;
        if (levelW == 5)
        {
            W_AD_adv += 60;
            W_AD_monster += 20;
        }
        else
        {
            W_AD_adv += 40;
            W_AD_monster += 15;
        }
    }
    public void levelUpE()
    {
        levelE++;
        if (levelE == 3)
        {
            E_Recover += 0.04f;
            E_Recover_adv += 0.04f;
        }
        else
        {
            E_Recover += 0.02f;
            E_Recover_adv += 0.02f;
        }
        E_Attackspeed += 0.1f;
        E_Recover_adv += 0.1f;
    }
    public void levelUpR()
    {
        levelR++;
        R_CoolTime -= 20;
        if (levelR == 2) GetComponent<WhiteTiger>().R_AD = 300;
        else if (levelR == 3) GetComponent<WhiteTiger>().R_AD = 500;
    }
}

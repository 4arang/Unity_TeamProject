using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColD_W : MonoBehaviour
{
    PhotonView PV;

    Animator animator;

    public bool isSkillon = false;

    [Header("Q_Skill")]
    [SerializeField] private GameObject flame;
    private float Q_CoolTime = 10; // 9 8 7 6 
    private bool Q_Ready = true;
    Skill_BarQ skillQ;
    private int levelQ = 1;

    [Header("W_Skill")]
    [SerializeField] private GameObject aura;
    [SerializeField] private GameObject shield;
    private float W_SpeedUp = 1.1f; // 1.15  1.20  1.25  1.30
    private float W_HP = 60;        // 95 135 165 200
    private bool W_Ready = true;
    private float W_CoolTime = 7; //6.75 6.75 6.25 6
    Skill_BarW skillW;
    private int levelW = 1;

    [Header("E_Skill")]
    [SerializeField] private GameObject Direction;
    [SerializeField] private GameObject grenade_L;
    [SerializeField] private GameObject grenade_R;
    [SerializeField] private Transform grenade_Bomb;
    private float E_AD = 90; // 120 160 200 240
    private float E_dropspeed = 0.15f; //0.20 0.25 0.30 0.35
    protected float DirecAngle; //eŰ ���Ⱒ��
    private bool grenade_Left = true;
    private bool E_SkillOn = false;
    private float E_CoolTime = 6; //fixed
    private bool E_Ready = true;
    Skill_BarE skillE;
    private int levelE = 1;

    [Header("R_Skill")]
    [SerializeField] private GameObject missile;
    [SerializeField] private GameObject Range;
    [SerializeField] private GameObject RangeDirection;
    [SerializeField] private GameObject missile_target_effect;
    protected float R_DirecAngle; //rŰ ���Ⱒ��// protected Vector3 BombDirec;
    public float R_Distance = 10.0f; //��Ÿ� �ۿ� �ִ°�� �̵��ؼ� ��ų �ߵ�
    private bool isR_ready; //r��ų �ߵ���������
    private Vector3 Rdirection; //r������ǥ ����
    private bool R_Ready = true;
    private float R_CoolTime = 100; // 85 70
    Skill_BarR skillR;
    private int levelR = 1;

    //���콺 ��ǥ �����(�ӽ�)
    Vector3 mouseVector;

    void Start()
    {
        PV = GetComponent<PhotonView>();

        aura.SetActive(false);
        shield.SetActive(false);

        flame.SetActive(false);

        Direction.SetActive(false);
        grenade_L.SetActive(false);
        grenade_R.SetActive(false);

        missile.SetActive(false);
        Range.SetActive(false);
        RangeDirection.SetActive(false);
        //  missile_target_effect.SetActive(false);
        animator = GetComponent<Animator>();
        isR_ready = false;

        skillQ = FindObjectOfType<Skill_BarQ>();
        skillW = FindObjectOfType<Skill_BarW>();
        skillE = FindObjectOfType<Skill_BarE>();
        skillR = FindObjectOfType<Skill_BarR>();
    }


    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.W) && GetComponent<Player_Stats>().Helium >= 20 && W_Ready)
            {
                skillW.OnSkill(W_CoolTime);
                isSkillon = true;
                if (animator.GetBool("W_ColD") == false)
                    StartCoroutine("Active_W");
                animator.SetBool("W_ColD", true);
            }

            if (Input.GetKeyDown(KeyCode.Q) && GetComponent<Player_Stats>().Helium >= 20 &Q_Ready)
            {
                skillQ.OnSkill(Q_CoolTime);
                isSkillon = true;
                if (animator.GetBool("Q_ColD") == false)
                    StartCoroutine("Active_Q");
                animator.SetBool("Q_ColD", true);
            }

            if (Input.GetKey(KeyCode.E) && GetComponent<Player_Stats>().Helium >= 20&E_Ready)
            {
                isSkillon = true;
                E_SkillOn = true;
                movingManager.Instance.PlayerClickedPos = transform.position; //��ġ����
                Direction.transform.position = Range.transform.position; //ĳ���Ͱ���� ȭ���̵�
                Direction.SetActive(true); //ȭ����� ���� -> ȭ�� active
                GetMousePos();  //���콺 ��ġ �޾ƿͼ� ���� �ٶ󺸰� �ϱ�
                Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //����setting
                movingManager.Instance.PlayerDirection = DirecAngle; //�÷��̾ ��������
            }
            if (Input.GetKeyUp(KeyCode.E) && E_SkillOn)  //EŰ ���� ���� ��ų ����
            {
                Direction.SetActive(false);
                GetComponent<Player_Stats>().DropHe();
                skillE.OnSkill(E_CoolTime);

                if (grenade_Left)   //ùź
                {
                    Vector3 nextDir = new Vector3(mouseVector.x, grenade_L.transform.position.y, mouseVector.z);
                    Vector3 shootDir = (nextDir - grenade_L.transform.position).normalized; //���콺��ǥ -�߻���ǥ

                    //                    Transform grenadeTransform = Instantiate(grenade_Bomb, grenade_L.transform.position,
                    //Quaternion.identity); //��ź�߻� and transform ����
                    //                    grenadeTransform.GetComponent<Projectile_Grenade>().Setup(shootDir, E_AD
                    //                        , gameObject); //��ź�� ��������
                    PV.RPC("instantiateE", RpcTarget.AllViaServer, shootDir, grenade_Left);

                    grenade_Left = false;
                }
                else        //��°ź
                {
                    Vector3 nextDir = new Vector3(mouseVector.x, grenade_R.transform.position.y, mouseVector.z);
                    Vector3 shootDir = (nextDir - grenade_R.transform.position).normalized; //���콺��ǥ -�߻���ǥ

                    //                    Transform grenadeTransform = Instantiate(grenade_Bomb, grenade_R.transform.position,
                    //Quaternion.identity); //��ź�߻� and transform ����

                    //                    grenadeTransform.GetComponent<Projectile_Grenade>().Setup(shootDir, E_AD
                    //                        , gameObject); //��ź�� ��������
                    PV.RPC("instantiateE", RpcTarget.AllViaServer, shootDir, grenade_Left);
                    grenade_Left = true;
                }


                if (animator.GetBool("E_ColD") == false)
                {
                    StartCoroutine("Active_E");
                }
                animator.SetBool("E_ColD", true);
                E_SkillOn = false;
            }

            if (Input.GetKeyDown(KeyCode.R)&R_Ready)
            {
                isSkillon = true;
                mouseVector = GetMousePos(); //ȭ�� ���� ���콺���⿡ �̸� �̵�
                Direction.SetActive(true); //����ȭ�� active
                                           //ȭ��ǥ ��ġ ����
            }
            if (Input.GetKey(KeyCode.R)&R_Ready)
            {
                Direction.transform.position = mouseVector;
                Range.SetActive(true);
                GetRdirect(); //get R_DirecAngle
                Direction.transform.rotation =
                    Quaternion.AngleAxis(R_DirecAngle, Vector3.up);

            }
            if (Input.GetKeyUp(KeyCode.R)&R_Ready)
            {
                skillR.OnSkill(R_CoolTime);
                Rdirection = Direction.transform.position;
                Range.SetActive(false);
                Direction.SetActive(false);
                if ((transform.position - Rdirection).magnitude > R_Distance)
                {//��Ÿ� �ܿ� ���� ��� �̵�
                    movingManager.Instance.PlayerClickedPos = Rdirection;
                    GetComponent<ColD>().agent.SetDestination(Rdirection);
                    isR_ready = true;
                }
                else        //��Ÿ� ���� ��� �ٷ� ����
                {
                    if (animator.GetBool("R_ColD") == false)
                        StartCoroutine("Active_R");
                }

            }
            if (isR_ready && (transform.position - Rdirection).magnitude <= R_Distance)
            {
                movingManager.Instance.PlayerClickedPos = transform.position; //�ڸ�����
                GetComponent<ColD>().agent.SetDestination(transform.position);
                if (animator.GetBool("R_ColD") == false)
                    StartCoroutine("Active_R");
                isR_ready = false;
            }
        }
    }

    IEnumerator Active_Q()
    {
        while (true)
        {
            Q_Ready = false;
            GetComponent<Player_Stats>().DropHe();
            yield return new WaitForSeconds(0.1f);
            PV.RPC("activeQ", RpcTarget.AllViaServer, true);
            flame.GetComponentInChildren<Player_Skill_Attack>().setup(levelQ);
            yield return new WaitForSeconds(2.5f);
            animator.SetBool("Q_ColD", false);
            yield return new WaitForSeconds(0.5f);
            PV.RPC("activeQ", RpcTarget.AllViaServer, false);
            isSkillon = false;
            yield return new WaitForSeconds(Q_CoolTime-3.1f);
            Q_Ready = true;
            break;
        }

    }
    IEnumerator Active_W()
    {
        while (true)
        {
            GetComponent<Player_Stats>().DropHe();
            PV.RPC("activeW", RpcTarget.AllViaServer, true);
            W_Ready = false;
            if (GetComponent<Player_Stats>().isDanger)
            {
                GetComponent<Player_Stats>().MoveSpeed *= 1.5f;
                GetComponent<Player_Stats>().MaxHP += W_HP * 1.5f;
                GetComponent<Player_Stats>().GetHP(W_HP * 1.5f);
                yield return new WaitForSeconds(1.5f);
                GetComponent<Player_Stats>().MoveSpeed *= 1 /  1.5f;
                GetComponent<Player_Stats>().MaxHP -= W_HP * 1.5f;
                GetComponent<Player_Stats>().GetHP(0);
            }
            else
            {
                GetComponent<Player_Stats>().MoveSpeed *= W_SpeedUp;
                GetComponent<Player_Stats>().MaxHP += W_HP;
                GetComponent<Player_Stats>().GetHP(W_HP);
                yield return new WaitForSeconds(1.5f);
                GetComponent<Player_Stats>().MoveSpeed *= 1 / W_SpeedUp;
                GetComponent<Player_Stats>().MaxHP -= W_HP;
                GetComponent<Player_Stats>().GetHP(0);
            }
            yield return new WaitForSeconds(0.5f);
            PV.RPC("activeW", RpcTarget.AllViaServer, false);
            animator.SetBool("W_ColD", false);
            yield return new WaitForSeconds(W_CoolTime - 2.0f);
            W_Ready = true;
            break;
        }
        isSkillon = false;
    }
    IEnumerator Active_E()
    {
        while (true)
        {
            E_Ready = false;
            if (grenade_Left)
            {
                PV.RPC("activeE_L", RpcTarget.AllViaServer, true);

                yield return new WaitForSeconds(0.5f);
                PV.RPC("activeE_L", RpcTarget.AllViaServer, false);
                animator.SetBool("E_ColD", false);
            }
            else
            {
                PV.RPC("activeE_R", RpcTarget.AllViaServer, true);

                yield return new WaitForSeconds(0.5f);
                PV.RPC("activeE_R", RpcTarget.AllViaServer, false);
                animator.SetBool("E_ColD", false);
            }
            isSkillon = false;
            yield return new WaitForSeconds(E_CoolTime - 0.5f);
            E_Ready = true;
            break;
        }

    }
    IEnumerator Active_R()
    {
        R_Ready = false;
        while (true)
        {
            animator.SetBool("R_ColD", true);
            PV.RPC("activeR", RpcTarget.AllViaServer, true);
            yield return new WaitForSeconds(0.67f);
           //missile.SetActive(false);
            PV.RPC("activeR", RpcTarget.AllViaServer, false);
            animator.SetBool("R_ColD", false);
            break;
        }
        while (true)
        {
   
            //   missile_target_effect.transform.position =
            //       new Vector3(Rdirection.x, 12.5f, Rdirection.z);
            //   //�̻��� Ÿ�� ��ġ
            //  GameObject Missile = Instantiate(missile_target_effect, missile_target_effect.transform.position,
            //Quaternion.AngleAxis(R_DirecAngle, Vector3.up)); //��ź�߻�
            PV.RPC("instantiateR", RpcTarget.AllViaServer, R_DirecAngle);
            //yield return new WaitForSeconds(7.0f);
            // GameObject.Destroy(Missile);
            break;
        }
        isSkillon = false;
        yield return new WaitForSeconds(R_CoolTime-0.67f);
        R_Ready = true;

    }

    Vector3 GetMousePos()
    {

        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        //���콺 ��ġ �޾Ƽ� �������
        DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
            hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;

        mouseVector = hit.point;

        return hit.point;
    }

    void GetRdirect()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);

        R_DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
        hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;
    }
    [PunRPC]
    void activeQ(bool b)
    {
        flame.SetActive(b);
    }
    [PunRPC]
    void activeW(bool b)
    {
        aura.SetActive(b);
        shield.SetActive(b);
    }
    [PunRPC]
    void activeE_L(bool b)
    {
        grenade_L.SetActive(b);
    }
    [PunRPC]
    void activeE_R(bool b)
    {
        grenade_R.SetActive(b);
    }
    [PunRPC]
    void instantiateE(Vector3 dir, bool isLeft)
    {
        if (isLeft)
        {
            Transform grenadeTransform = Instantiate(grenade_Bomb, grenade_L.transform.position,
    Quaternion.identity); //��ź�߻� and transform ����
            grenadeTransform.GetComponent<Projectile_Grenade>().Setup(dir, E_AD
    , E_dropspeed,gameObject); //��ź�� ��������
        }
        else
        {
            Transform grenadeTransform = Instantiate(grenade_Bomb, grenade_R.transform.position,
    Quaternion.identity); //��ź�߻� and transform ����
            grenadeTransform.GetComponent<Projectile_Grenade>().Setup(dir, E_AD
    , E_dropspeed,gameObject); //��ź�� ��������
        }

    }
    [PunRPC]
    void activeR(bool b)
    {
        missile.SetActive(b);
    }
    [PunRPC]
    void instantiateR(float dir)
    {
        missile_target_effect.transform.position =
    new Vector3(Rdirection.x, 12.5f, Rdirection.z);
        //�̻��� Ÿ�� ��ġ
        GameObject Missile = Instantiate(missile_target_effect, missile_target_effect.transform.position,
      Quaternion.AngleAxis(dir, Vector3.up)); //��ź�߻�
        Missile.GetComponentInChildren<ColD_R_Skill_damage>().setup(levelR, this.transform);
    }

    public void levelupQ()
    {
        levelQ++;
        Q_CoolTime--; 
    }
    public void levelupW()
    {
        levelW++;

        W_SpeedUp += 0.05f;
        if (levelW == 2)
        {
            W_HP += 35;
            W_CoolTime -= 0.25f;
        }
        else if (levelW == 3)
            W_HP += 40;
        else if (levelW == 4)
        {
            W_HP += 30;
            W_CoolTime -= 0.5f;
        }
        else if (levelW == 5)
        {
            W_HP += 35;
            W_CoolTime -= 0.25f;
        }
    }
    public void levelupE()
    {
        levelE++;
        if (levelE == 2)
            E_AD += 30;
        else
            E_AD += 40;
        E_dropspeed += 0.05f;

    }
    public void levelupR()
    {
        levelR++;
        R_CoolTime -= 15; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Realtime;
using Photon.Pun;

public class Xerion : MonoBehaviour
{
    //Network Components
    PhotonView PV;
    
    //Animation
    Animator animator;
    public float runSpeed = 10.0f;
    Vector3 Direction;

    NavMeshAgent agent;
    float motionSmoothTime = 0.1f;
    public float rotateSpeedMovement = 360.0f;
    public float rotateVelocity;


    //saved variable for lateupdate
    Vector3 PlayerDest;
    RaycastHit hit_;
    bool isupdate = false;

    //skill direction
    private float skillDir;


    //for NavPathLine
    public static Vector3[] path = new Vector3[0];
    LineRenderer lr;
    public GameObject linerenderobj;

    //Basic Attack
    [SerializeField] private GameObject BasicRange;
    [SerializeField] private GameObject BasicRange_col;
    [SerializeField] private GameObject GunShot_Effect;
    [SerializeField] private Transform BasicShot;
    private bool isBasicAttack = false;
    public bool CheckEnemy = false;
    public Transform TargetEnemy;
    private float BasicRangef;
    private float AttackSpeed;
    private float BasicRange_Ref = 0.004f;
    private bool OnAttack = false;
    private float Xerion_BasicAD;
    private byte Xerion_BasicAD_Level = 1;

    //Pasiive
    [SerializeField] private GameObject PassiveEffect;
    private float TotalAgentDistance = 0;
    private bool passiveOn = false;

    //public GameObject cameraObj;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        //  lr.sharedMaterial.SetColor("_color", Color.white);

        skillDir = movingManager.Instance.PlayerDirection;

        BasicRange.SetActive(false);
        BasicRange_col.SetActive(false);
        BasicRangef = GetComponent<Player_Stats>().AttackRange * BasicRange_Ref;
        BasicRange.transform.localScale = new Vector3(BasicRangef, BasicRangef, 0);
        AttackSpeed = GetComponent<Player_Stats>().AttackSpeed;

        PassiveEffect.SetActive(false);

        //cameraObj = Camera.main.gameObject;
        //cameraObj.GetComponent<MainCamera_CameraRoam>().player = this.transform;

    }



    private void Update()
    {
        if (PV.IsMine)
        {
 

            agent.speed = GetComponent<Player_Stats>().MoveSpeed / 100;


            RightMouseClicked();


            animator.SetFloat("Speed", agent.velocity.magnitude);
            if (animator.GetFloat("Speed") > 0.1f)
            {
                TotalAgentDistance += Time.deltaTime;
            }
            if (TotalAgentDistance > 2.0f)
            {
                TotalAgentDistance = 0;
                GetComponent<Player_Stats>().Energy += 1;
                //  Debug.Log("Xerion_Energy " + GetComponent<Player_Stats>().Energy);
            }

            if (skillDir != movingManager.Instance.PlayerDirection)
            {
                skillDir = movingManager.Instance.PlayerDirection;
                agent.transform.rotation = Quaternion.AngleAxis(skillDir, Vector3.up);

            }


            if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //���������
            else { movingManager.Instance.isFree = false; } //�������


            //////////////�⺻���ö�/////////////
            if (Input.GetKeyDown(KeyCode.A))
            {
                BasicRange_col.SetActive(true);
                BasicRange.SetActive(true);
                isBasicAttack = true;
            }

            if (isBasicAttack)       //AŰ �Է� ���Ŀ� ���콺����Ű �Է� ����
            {
                LeftMouseClicked();
            }
            if (CheckEnemy && !isBasicAttack) //�� üũ �Ϸ��Ѱ��
            {
                if (TargetEnemy)//Ÿ�ټ����� �Ǿ��ٸ� 
                {

                    GetComponentInChildren<Xerion_Basic_Range_collider>().isAttackReady();
                    agent.SetDestination(TargetEnemy.transform.position); //Ÿ�� ��ġ�� �̵�
                    AttackTargetEnemy(TargetEnemy); //Ÿ�� ����
                }
                else
                {       //Ÿ���� ������ �缳��

                    GetComponentInChildren<Xerion_Basic_Range_collider>().isAttackReady();
                }
            }

            if (GetComponent<Xerion_Shooting_Skill>().isSkillon)
            {
                CheckEnemy = false;
            }
        }

    }


    void LeftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {

            isBasicAttack = false;

            BasicRange_col.SetActive(false);
            CheckEnemy = true;

            RaycastHit hit; //ĳ���� �̵�

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//�̵���ǥ ����
                hit_ = hit;
            }
            isupdate = true;
            BasicRange_col.SetActive(true);
            BasicRange.SetActive(false); //��������Ʈ ����
        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }

    void RightMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            BasicRange_col.SetActive(false);
            isBasicAttack = false; //��Ŭ���� a(�⺻����) �̵� false
            TargetEnemy = null;
            CheckEnemy = false;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//�̵���ǥ ����
                hit_ = hit;
            }
            isupdate = true;
        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }

    private void LateUpdate()       //update���� ��ǥ�� ���� �Ŀ� lateupdate���� ������
    {
        if (PV.IsMine)
        {
            if (isupdate)
            {
                PlayerMove();
            }
        }

    }



    void PlayerMove()
    {
        if (hit_.collider.CompareTag("Floor"))
        {

            //Move
            agent.SetDestination(PlayerDest);
            agent.stoppingDistance = 0;

            //LinePath
            if (path != null && path.Length > 1)
            {
                lr.positionCount = path.Length;
                for (int i = 0; i < path.Length; i++)
                {
                    lr.SetPosition(i, path[i]);
                }
            }

            Xerion.path = agent.path.corners;
        }
        else return;

    }


    void AttackTargetEnemy(Transform target)
    {
        if ((transform.position - target.transform.position).magnitude <
            target.transform.localScale.magnitude * 10.0f)
        {
            movingManager.Instance.PlayerClickedPos = transform.position;
            //Debug.Log("TargetSet, Player Stop");
            if (animator.GetBool("A_Xerion") == false)
            {

                float shootDir = GetDirection(transform.position, target.transform.position);
                movingManager.Instance.PlayerDirection = shootDir;
                agent.transform.rotation = Quaternion.AngleAxis(shootDir + 45, Vector3.up);

                if (!OnAttack)
                {
                    GetComponent<Player_Stats>().Energy += 10;
                    PV.RPC("instantiateBullet", RpcTarget.AllViaServer, target.position);

                    if (passiveOn)
                    {
                        GetComponent<Player_Stats>().Energy = 0;    //�нú� ���
                        GetComponent<Player_Stats>().DropMP(-GetComponent<Player_Stats>().MaxMP / 10); //�ִ�MP/10 ����
                        PV.RPC("activePassive", RpcTarget.AllViaServer, false);
                        passiveOn = false;
                    }
                    StartCoroutine("Active_A", target);
                }

            }
        }
    }
    IEnumerator Active_A(Transform target)
    {
        OnAttack = true;
        while (OnAttack)
        {
            animator.SetBool("A_Xerion", true);
            GunShot_Effect.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            GunShot_Effect.SetActive(false);
            animator.SetBool("A_Xerion", false);
            Xerion_BasicAD = GetComponent<Player_Stats>().AD;
            if (target)damageEnemy(target);
            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
        OnAttack = false;
    }
    float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(away.x - home.x, away.z - home.z) * Mathf.Rad2Deg;
    }

    public void OnPassive()
    {
        passiveOn = true;
        PV.RPC("activePassive", RpcTarget.AllViaServer, true);
    }

    private void damageEnemy(Transform target)
    {

        if (target.CompareTag("Minion"))
        {
            target.GetComponent<Minion_Stats>().DropHP(Xerion_BasicAD, this.transform);
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<Player_Stats>().DropHP(Xerion_BasicAD, this.transform);
        }
        else if (target.CompareTag("Turret"))
        {
            target.GetComponent<Turret_Stats>().DropHP(Xerion_BasicAD);
        }
        else if (target.CompareTag("Monster"))
        {
            if (target.GetComponent<Monster_Stats>().hp > 0)
            {
                target.GetComponent<Monster_Stats>().DropHP(Xerion_BasicAD, this.transform);
            }
            else
            {
                target = null;
                CheckEnemy = false;
            }
        }

    }

    [PunRPC]
    void instantiateBullet(Vector3 target)
    {
        Transform BasicShotTransform = Instantiate(BasicShot, GunShot_Effect.transform.position,
                   Quaternion.identity).transform;
        Vector3 shootingDir = (target - transform.position).normalized;
        if (BasicShotTransform != null)
        {
            BasicShotTransform.GetComponent<PFX_ProjectileObject>().Setup(shootingDir, target);
        }
    }

    [PunRPC]
    void activePassive(bool b)
    {
       PassiveEffect.SetActive(b);
    }
}
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WhiteTiger : MonoBehaviourPunCallbacks
{
    //Animation
    Animator animator;
    private float originalSpeed;
    public float skillSpeed = 10.0f;
    Vector3 Direction;

    NavMeshAgent agent;
    float motionSmoothTime = 0.1f;
    public float rotateSpeedMovement = 360.0f;
    public float rotateVelocity;


    //saved variable for lateupdate
    Vector3 PlayerDest;
    RaycastHit hit_;
    bool isupdate = false;


    //for NavPathLine
    public static Vector3[] path = new Vector3[0];
    LineRenderer lr;
    public GameObject linerenderobj;

    //grenade direction
    private bool onSkill;
    private float playerDir;

    //to move to targetPos
    private Vector3 TargetPos;

    private Transform Enemy; //�� ��ġ �޾ƿ���
    private bool SpeedFull = false;

    //Basic Attack
    [Header("A_Basic")]
    [SerializeField] private GameObject BasicRange;
    [SerializeField] private GameObject BasicRange_Col;
    // [SerializeField] private GameObject BasicAttack_Effect_L;
    // [SerializeField] private GameObject BasicAttack_Effect_R;
    [SerializeField] private GameObject BasicAttack_Effect_Slash;
    private bool isBasicAttack = false;
    public bool CheckEnemy = false;
    public Transform TargetEnemy;
    private float BasicRangef;
    private float AttackSpeed;
    private float BasicRange_Ref = 0.004f;
    private bool OnAttack = false;
    private float WT_BasicAD;
    private byte WT_BasicAD_Level = 1;
    private float PassiveRange = 2000;
    private bool TeamColor;

    [Header("Q_Skill")]
    [SerializeField] private GameObject Q_Punch_L;
    [SerializeField] private GameObject Q_Punch_R;
    [SerializeField] private GameObject adv_Q_Punch;
    // [SerializeField] private GameObject adv_Q_Punch_col;
    private bool On_adv_Q = false;
    //[SerializeField] private GameObject Q_Effect;
    //[SerializeField] private GameObject adv_Q_Effect;
    private float Q_AD;
    private byte Q_Level = 1;



    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        playerDir = movingManager.Instance.PlayerDirection;
        onSkill = false;
        TargetPos = movingManager.Instance.PlayerTargetPos;

        agent.speed = GetComponent<Player_Stats>().MoveSpeed / 100;
        originalSpeed = agent.speed;

        //Enemy = GameObject.FindWithTag("Minion").transform;

        BasicRange.SetActive(false);
        BasicRange_Col.SetActive(false);
        BasicRangef = GetComponent<Player_Stats>().AttackRange * BasicRange_Ref;
        BasicRange.transform.localScale = new Vector3(BasicRangef, BasicRangef, 0);
        //  BasicAttack_Effect_L.SetActive(false);
        // BasicAttack_Effect_R.SetActive(false);
        //  BasicAttack_Effect_Slash.SetActive(false);

        AttackSpeed = GetComponent<Player_Stats>().AttackSpeed;

        Q_Punch_L.SetActive(false);
        Q_Punch_R.SetActive(false);
        adv_Q_Punch.SetActive(false);
        // adv_Q_Punch_col.SetActive(false);
        //Q_Effect.SetActive(false);
        //adv_Q_Effect.SetActive(false);

        TeamColor = GetComponent<Player_Stats>().TeamColor;
        InvokeRepeating("Passive", 0f, 0.5f); //passiveRange �� è�Ǿ�߽߰� �ӵ� up
    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            //Debug.Log("Tiger speed " + agent.speed);

            agent.speed = GetComponent<Player_Stats>().MoveSpeed / 100;

            RightMouseClicked();

            animator.SetFloat("Speed", agent.velocity.magnitude);

            if (playerDir != movingManager.Instance.PlayerDirection) //�÷��̾� ����
            {
                playerDir = movingManager.Instance.PlayerDirection;
                agent.transform.rotation = Quaternion.AngleAxis(playerDir, Vector3.up);
            }
            if (TargetPos != movingManager.Instance.PlayerTargetPos) //�÷��̾� �̵�(R��ų�� �̵�����)
            {
                TargetPos = movingManager.Instance.PlayerTargetPos;
                onSkill = true;
            }

            if (!onSkill)
            {
                agent.speed = GetComponent<Player_Stats>().MoveSpeed / 100; //��ųx�϶� ���ǵ尪�ޱ�
            }


            if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //���������
            else { movingManager.Instance.isFree = false; } //�������


            //////////////�⺻���ö�/////////////
            if (Input.GetKeyDown(KeyCode.A))
            {
                BasicRange_Col.SetActive(true);
                BasicRange.SetActive(true);
                isBasicAttack = true;
            }
            if (isBasicAttack) //AŰ �Է� ���Ŀ� Ȱ��ȭ
            {
                LeftMouseClicked(); //���ʸ��콺 Ŭ��
            }
            if (CheckEnemy && !isBasicAttack) //�� üũ �Ϸ��Ѱ��
            {
                if (TargetEnemy)//Ÿ�ټ����� �Ǿ��ٸ� 
                {
                    Debug.Log("Targeted");
                    GetComponentInChildren<WhiteTiger_Basic_Range_Collider>().isAttackReady();
                    agent.SetDestination(TargetEnemy.transform.position); //Ÿ�� ��ġ�� �̵�
                    AttackTargetEnemy(TargetEnemy); //Ÿ�� ����
                }
                else if (!isBasicAttack)
                {       //Ÿ���� ������ �缳��
                    Debug.Log("Targeting");
                    GetComponentInChildren<WhiteTiger_Basic_Range_Collider>().isAttackReady();
                }
            }


            /////Q���� ���ݷ�up A����////
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine("Active_Q");
                if (!GetComponent<WhiteTiger_Skill>().isWild) GetComponent<WhiteTiger_Skill>().WildPoint++;
            }
        }
    }

    void LeftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {

            isBasicAttack = false;


            CheckEnemy = true;
            BasicRange_Col.SetActive(false); //�ݶ��̴� Ŭ�� ����
            RaycastHit hit; //ĳ���� �̵�

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//�̵���ǥ ����
                hit_ = hit;
            }
            isupdate = true;
            BasicRange_Col.SetActive(true);
            BasicRange.SetActive(false); //��������Ʈ ����
        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }

    void AttackTargetEnemy(Transform target)
    {

        if ((transform.position - target.transform.position).magnitude <
            target.transform.localScale.magnitude * 0.85f)
        {
            movingManager.Instance.PlayerClickedPos = transform.position; //���ݹ��� ���̸� ���߰� ������ȯ
            if (animator.GetBool("A_WT") == false)
            {
                float shootDir = GetDirection(transform.position, target.transform.position);
                movingManager.Instance.PlayerDirection = shootDir;
                agent.transform.rotation = Quaternion.AngleAxis(shootDir, Vector3.up);

                Debug.Log("TargetSet, Player Stop");
                if (!OnAttack)
                {
                    Debug.Log("Active animation");
                    StartCoroutine("Active_A", target);
                }
            }
        }
        else
            target = null; //Ÿ�� �缳��
    }

    //void SpeedUp()
    //{ 
    //    if (!SpeedFull)
    //    {
    //        if ((transform.position - Enemy.position).magnitude < 10/*���� �±�minion->Enemy ������*/)
    //        {
    //            GetComponent<Player_Stats>().MoveSpeed += 30;
    //            SpeedFull = true; //�ѹ��� ����
    //            Debug.Log("Tiger speedup " + agent.speed);
    //        }
    //    }
    //    else
    //    {
    //        if ((transform.position - Enemy.position).magnitude >= 10/*���� �±�minion->Enemy ������*/)
    //        {
    //            GetComponent<Player_Stats>().MoveSpeed -= 30;
    //            SpeedFull = false; //�ѹ��� ����
    //            Debug.Log("Tiger speeddown " + agent.speed);
    //        }
    //    }
    //}

    void RightMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            BasicRange_Col.SetActive(false);
            TargetEnemy = null;
            CheckEnemy = false;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//�̵���ǥ ����
                hit_ = hit;
            }
            isupdate = true;
            PlayerDest = movingManager.Instance.PlayerClickedPos;


            isBasicAttack = false;
        }


    }

    private void LateUpdate()       //update���� ��ǥ�� ���� �Ŀ� lateupdate���� ������
    {
        if (onSkill)
        {
            agent.speed = skillSpeed;
            agent.SetDestination(TargetPos);
            PlayerDest = TargetPos;
            if (Vector3.Distance(TargetPos, agent.transform.position) < 0.2f)
            {
                onSkill = false;
                agent.speed = originalSpeed;
            }
        }
        if (isupdate && !onSkill)
        {
            PlayerMove();
        }
    }


    void PlayerMove()
    {
        if (hit_.collider.CompareTag("Floor"))
        {
            PlayerDest = movingManager.Instance.PlayerClickedPos;
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

            WhiteTiger.path = agent.path.corners;
        }

    }

    IEnumerator Active_A(Transform target)
    {
        OnAttack = true;
        while (true)
        {
            if (!On_adv_Q)
            {
                animator.SetBool("A_WT", true);
                yield return new WaitForSeconds(1.0f);
                if (target) damageEnemy(target);

                animator.SetBool("A_WT", false);
                yield return new WaitForSeconds(AttackSpeed);
                OnAttack = false;
                break;
            }
            else
            {
                animator.SetBool("A_WT", true);
                yield return new WaitForSeconds(1.0f);
                if (target) damageEnemy(target);
                animator.SetBool("A_WT", false);
                yield return new WaitForSeconds(AttackSpeed);
                OnAttack = false;
                break;
            }

        }
        OnAttack = false;

    }


    IEnumerator Active_Q()
    {
        while (true)
        {

            if (!GetComponent<WhiteTiger_Skill>().isWild)
            {
                GetComponent<Player_Stats>().AD += 10 * Q_Level;
                Q_Punch_L.SetActive(true);
                Q_Punch_R.SetActive(true);
                yield return new WaitForSeconds(10.0f);
                Q_Punch_L.SetActive(false);
                Q_Punch_R.SetActive(false);
                GetComponent<Player_Stats>().AD -= 10 * Q_Level;
                break;
            }
            else
            {
                On_adv_Q = true;    //true�ϰ�� ��ġ->������
                GetComponent<Player_Stats>().AD += 20 * Q_Level;
                adv_Q_Punch.SetActive(true);
                yield return new WaitForSeconds(10.0f);
                adv_Q_Punch.SetActive(false);
                GetComponent<Player_Stats>().AD -= 20 * Q_Level;
                On_adv_Q = false;
                //yield return new WaitForSeconds(10.0f);
                //adv_Q_Punch.SetActive(false);
                break;
            }
        }
    }


    private float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(away.x - home.x, away.z - home.z) * Mathf.Rad2Deg;
    }

    public void R_Attack()
    {
        // StartCoroutine("Active_R_Attack");
        /// �� è�Ǿ𿡰Ը� ����. ���� ������
        ///////
        ///

        Debug.Log("RSkill Targeted");
    }
    private void damageEnemy(Transform target)
    {
        WT_BasicAD = GetComponent<Player_Stats>().AD;

        if (target.CompareTag("Minion"))
        {
            target.GetComponent<Minion_Stats>().DropHP(WT_BasicAD);
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<Player_Stats>().DropHP(WT_BasicAD);
        }
        else if (target.CompareTag("Turret"))
        {
            target.GetComponent<Turret_Stats>().DropHP(WT_BasicAD);
        }

    }

    private void Passive()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, PassiveRange);

        foreach (Collider col in colliderArray)
        {
            bool foundPlayer = false;


            if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                 && (player.TeamColor != TeamColor))
            {
                if (!foundPlayer)
                {
                    agent.speed = originalSpeed + 0.3f; //30*0.01
                    foundPlayer = true; //�ߺ����� ����
                }
            }
            else
                agent.speed = originalSpeed;    //�������

        }
    }

}



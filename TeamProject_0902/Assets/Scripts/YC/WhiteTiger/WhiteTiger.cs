using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteTiger : MonoBehaviour
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
    [SerializeField] private GameObject BasicAttack_Effect_L;
    [SerializeField] private GameObject BasicAttack_Effect_R;
    [SerializeField] private GameObject BasicAttack_Effect_Slash;
    private bool isBasicAttack = false;
    public bool CheckEnemy = false;
    public Collider TargetEnemy;
    private float BasicRangef;
    private float AttackSpeed;
    private float BasicRange_Ref = 0.004f;
    private bool OnAttack = false;

    [Header("Q_Skill")]
    [SerializeField] private GameObject Q_Punch_L;
    [SerializeField] private GameObject Q_Punch_R;
    [SerializeField] private GameObject adv_Q_Punch;
    [SerializeField] private GameObject Q_Effect;
    [SerializeField] private GameObject adv_Q_Effect;


    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        playerDir = movingManager.Instance.PlayerDirection;
        onSkill = false;
        TargetPos = movingManager.Instance.PlayerTargetPos;

        agent.speed = GetComponent<WhiteTiger_Stats>().MoveSpeed / 100;
        originalSpeed = agent.speed;

        Enemy = GameObject.FindWithTag("Minion").transform;

        BasicRange.SetActive(false);
        BasicRange_Col.SetActive(false);
        BasicRangef = GetComponent<WhiteTiger_Stats>().AttackRange * BasicRange_Ref;
        BasicRange.transform.localScale = new Vector3(BasicRangef, BasicRangef, 0);
        BasicAttack_Effect_L.SetActive(false);
        BasicAttack_Effect_R.SetActive(false);
      //  BasicAttack_Effect_Slash.SetActive(false);

        AttackSpeed = GetComponent<WhiteTiger_Stats>().AttackSpeed;
    }


    private void Update()
    {

        Debug.Log("Tiger speed " + agent.speed);

        agent.speed = GetComponent<WhiteTiger_Stats>().MoveSpeed / 100;

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

        if(!onSkill)
        {
            agent.speed = GetComponent<WhiteTiger_Stats>().MoveSpeed / 100; //��ųx�϶� ���ǵ尪�ޱ�
        }


        if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //���������
        else { movingManager.Instance.isFree = false; } //�������

        SpeedUp(); //������ ��ų �� è�Ǿ� ���ٽ� �̵��ӵ� 30 ����


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
        if (CheckEnemy) //�� üũ �Ϸ��Ѱ��
        {
            if (TargetEnemy)//Ÿ�ټ����� �Ǿ��ٸ� 
            {
                Debug.Log("Targeted");
                GetComponentInChildren<WhiteTiger_Basic_Range_Collider>().isAttackReady();
                agent.SetDestination(TargetEnemy.transform.position); //Ÿ�� ��ġ�� �̵�
                AttackTargetEnemy(); //Ÿ�� ����
            }
            else
            {       //Ÿ���� ������ �缳��
                Debug.Log("Targeting");
                GetComponentInChildren<WhiteTiger_Basic_Range_Collider>().isAttackReady();
            }
        }


        /////Q���� ���ݷ�up A����////
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (animator.GetBool("Q_WT") == false)
                StartCoroutine("Active_Q");
            animator.SetBool("Q_WT", true);
            if (!GetComponent<WhiteTiger_Skill>().isWild) GetComponent<WhiteTiger_Skill>().WildPoint++;
        }
    }

    void LeftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBasicAttack = false; 

            BasicRange.SetActive(false); //��������Ʈ ����
            CheckEnemy = true;

            RaycastHit hit; //ĳ���� �̵�

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//�̵���ǥ ����
                hit_ = hit;
            }
            isupdate = true;

        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }

    void AttackTargetEnemy()
    {
        if ((transform.position - TargetEnemy.transform.position).magnitude <
            TargetEnemy.transform.localScale.magnitude/2)
        {
            movingManager.Instance.PlayerClickedPos = transform.position; //���ݹ��� ���̸� ���߰� ������ȯ

            float shootDir = GetDirection(transform.position, TargetEnemy.transform.position);
            agent.transform.rotation = Quaternion.AngleAxis(shootDir, Vector3.up);

            Debug.Log("TargetSet, Player Stop");
            if (!OnAttack)
            {
                Debug.Log("Active animation");
                StartCoroutine("Active_A");
            }
        }
        else
        {
            TargetEnemy = null; //���ݹ��� ���̸� Ÿ�پ����Ƿ� �缳��
        }
    }

    void SpeedUp()
    {
        if (!SpeedFull)
        {
            if ((transform.position - Enemy.position).magnitude < 10/*���� �±�minion->Enemy ������*/)
            {
                GetComponent<WhiteTiger_Stats>().MoveSpeed += 30;
                SpeedFull = true; //�ѹ��� ����
                Debug.Log("Tiger speedup " + agent.speed);
            }
        }
        else
        {
            if ((transform.position - Enemy.position).magnitude >= 10/*���� �±�minion->Enemy ������*/)
            {
                GetComponent<WhiteTiger_Stats>().MoveSpeed -= 30;
                SpeedFull = false; //�ѹ��� ����
                Debug.Log("Tiger speeddown " + agent.speed);
            }
        }
    }

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
        if(onSkill)
        {
            agent.speed = skillSpeed;
            agent.SetDestination(TargetPos);
            PlayerDest = TargetPos;
            if (Vector3.Distance(TargetPos, agent.transform.position) < 0.1f)
            {
                onSkill = false;
                agent.speed = originalSpeed;
            }
        }
        if (isupdate&&!onSkill)
        {
            PlayerMove();
        }
    }


    void PlayerMove()
    {
        if (hit_.collider.tag == "Floor")
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

    IEnumerator Active_A()
    {
        OnAttack = true;
        while (true)
        {

            animator.SetBool("A_WT", true);
            BasicAttack_Effect_R.SetActive(true);
            GetComponentInChildren<WT_Punch_Collider_R>().Skill();
            yield return new WaitForSeconds(0.5f);
            BasicAttack_Effect_R.SetActive(false);

            BasicAttack_Effect_L.SetActive(true);
            GetComponentInChildren<WT_Punch_Collider_L>().Skill();
            yield return new WaitForSeconds(0.5f);
            BasicAttack_Effect_L.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            //BasicAttack_Effect_Slash.SetActive(true);

            animator.SetBool("A_WT", false);

            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
        OnAttack = false;
    }


    IEnumerator Active_Q()
    {
        while (true)
        {

            if (!GetComponent<WhiteTiger_Skill>().isWild)
            {
                Q_Punch_L.SetActive(true);
                Q_Punch_R.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                Q_Punch_L.SetActive(false);
                Q_Punch_R.SetActive(false);
                animator.SetBool("Q_WT", false);
                break;
            }
            else
            {
                adv_Q_Punch.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                adv_Q_Punch.SetActive(false);
                animator.SetBool("Q_WT", false);
                break;
            }
        }
    }

    float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(away.x - home.x, away.z - home.z) * Mathf.Rad2Deg;
    }

}


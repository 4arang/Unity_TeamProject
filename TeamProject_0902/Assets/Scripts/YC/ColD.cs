using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ColD : MonoBehaviour
{
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


    //for NavPathLine
    public static Vector3[] path = new Vector3[0];
    LineRenderer lr;
    public GameObject linerenderobj;

    //grenade direction
    private bool onSkill;
    private float grenadeDir;

    //Basic Attack
    [SerializeField] private GameObject BasicRange;
    [SerializeField] private GameObject BasicRange_Col;
    [SerializeField] private GameObject BasicAttack_Effect;
    [SerializeField] private GameObject BasicAttack_Effect_Slash;

    private bool isBasicAttack = false;
    public bool CheckEnemy = false;
    public Collider TargetEnemy;
    private float BasicRangef;
    private float AttackSpeed;
    private float BasicRange_Ref = 0.04f;
    private bool OnAttack = false;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        grenadeDir = movingManager.Instance.PlayerDirection;
        onSkill = false;

        BasicRange.SetActive(false);
        BasicRange_Col.SetActive(false);
        BasicRangef = GetComponent<ColD_Stats>().AttackRange * BasicRange_Ref;
        BasicRange.transform.localScale = new Vector3(BasicRangef, BasicRangef, 0);
        BasicAttack_Effect.SetActive(false);
        BasicAttack_Effect_Slash.SetActive(false);

        AttackSpeed = GetComponent<ColD_Stats>().AttackSpeed;
    }


    private void Update()
    {


        agent.speed = GetComponent<ColD_Stats>().MoveSpeed / 100;
        //Debug.Log("Speed " + agent.speed);
        RightMouseClicked();

        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (grenadeDir != movingManager.Instance.PlayerDirection)
        {
            onSkill = true;
            grenadeDir = movingManager.Instance.PlayerDirection;
            agent.transform.rotation = Quaternion.AngleAxis(grenadeDir, Vector3.up);
        }
        else
            onSkill = false;

        if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //���������
        else { movingManager.Instance.isFree = false; } //�������



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
        if(CheckEnemy) //�� üũ �Ϸ��Ѱ��
        {
            if (TargetEnemy)//Ÿ�ټ����� �Ǿ��ٸ� 
            {
                Debug.Log("Targeted");
                GetComponentInChildren<ColD_Basic_Range_collider>().isAttackReady();
                agent.SetDestination(TargetEnemy.transform.position); //Ÿ�� ��ġ�� �̵�
                AttackTargetEnemy(); //Ÿ�� ����
            }
            else
            {       //Ÿ���� ������ �缳��
                Debug.Log("Targeting");
                GetComponentInChildren<ColD_Basic_Range_collider>().isAttackReady();
            }
        }
                
    }


    void LeftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBasicAttack = false;

        
            CheckEnemy = true;
            BasicRange_Col.SetActive(false);
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
        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }

    private void LateUpdate()       //update���� ��ǥ�� ���� �Ŀ� lateupdate���� ������
    {
        if (isupdate)
        {
            PlayerMove();
        }
    }



    void PlayerMove()
    {
        if (hit_.collider.tag == "Floor")
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

            ColD.path = agent.path.corners;
        }
    }

    void AttackTargetEnemy()
    {
        if ((transform.position - TargetEnemy.transform.position).magnitude < 
            TargetEnemy.transform.localScale.magnitude*0.85f)
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

    IEnumerator Active_A()
    {
        OnAttack = true;
        while (true)
        {
           
            animator.SetBool("A_ColD", true);
            yield return new WaitForSeconds(0.2f);
            BasicAttack_Effect.SetActive(true);
            GetComponentInChildren<ColD_Punch_Collider>().Skill();
            BasicAttack_Effect_Slash.SetActive(true);
            yield return new WaitForSeconds(0.6f);
            BasicAttack_Effect.SetActive(false);
            animator.SetBool("A_ColD", false);
            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
        OnAttack = false;
    }

    float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(away.x - home.x, away.z - home.z) * Mathf.Rad2Deg;
    }
}

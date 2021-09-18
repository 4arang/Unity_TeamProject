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
    private bool isBasicAttack = false;
    public bool CheckEnemy = false;
    public Collider TargetEnemy;
    private float BasicRangef;
    private float AttackSpeed;
    private float BasicRange_Ref = 0.04f;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        grenadeDir = movingManager.Instance.PlayerDirection;
        onSkill = false;

        BasicRange.SetActive(false);
        BasicRangef = GetComponent<ColD_Stats>().AttackRange * BasicRange_Ref;
        BasicRange.transform.localScale = new Vector3(BasicRangef, BasicRangef, 0);

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

        if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //비전투모드
        else { movingManager.Instance.isFree = false; } //전투모드



        if (Input.GetKeyDown(KeyCode.A))
        {
            BasicRange.SetActive(true);
            isBasicAttack = true;
        }
        if (isBasicAttack) //A키 입력 이후에 활성화
        {
            LeftMouseClicked(); //왼쪽마우스 클릭
        }
        if(CheckEnemy) //적 체크 완료한경우
        {
            if (TargetEnemy)//타겟설정이 되었다면 
            {
                agent.SetDestination(TargetEnemy.transform.position); //타겟 위치로 이동
                AttackTargetEnemy(); //타겟 공격
            }
            else
            {       //타겟이 없으면 재설정
                GetComponentInChildren<ColD_Basic_Range_collider>().isAttackReady();
            }
        }
                
    }


    void LeftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBasicAttack = false;
            BasicRange.SetActive(false); //범위이펙트 종료
            CheckEnemy = true;

            RaycastHit hit; //캐릭터 이동

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//이동좌표 저장
                hit_ = hit;
            }
            isupdate = true;

        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }


    void RightMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TargetEnemy = null;
            CheckEnemy = false;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//이동좌표 저장
                hit_ = hit;
            }
            isupdate = true;
        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }

    private void LateUpdate()       //update에서 좌표값 갱신 후에 lateupdate에서 움직임
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
            TargetEnemy.transform.localScale.magnitude)
        {
            movingManager.Instance.PlayerClickedPos = transform.position; //공격범위 안이면 멈추고 방향전환

            float shootDir = GetDirection(transform.position, TargetEnemy.transform.position);
            agent.transform.rotation = Quaternion.AngleAxis(shootDir, Vector3.up);

            Debug.Log("TargetSet, Player Stop");
            if (animator.GetBool("A_ColD") == false)
            {
                StartCoroutine("Active_A");
            }
        }
    }

    IEnumerator Active_A()
    {
        while (true)
        {
            animator.SetBool("A_ColD", true);
            yield return new WaitForSeconds(1.0f);
            animator.SetBool("A_ColD", false);
            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
    }

    float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(home.x - away.x, home.z - away.z) * Mathf.Rad2Deg;
    }
}

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

    private Transform Enemy; //적 위치 받아오기
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
    [SerializeField] private GameObject adv_Q_Punch_col;
    private bool On_adv_Q = false;
    //[SerializeField] private GameObject Q_Effect;
    //[SerializeField] private GameObject adv_Q_Effect;

    private byte Q_SkillLevel;


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
        BasicAttack_Effect_L.SetActive(false);
        BasicAttack_Effect_R.SetActive(false);
      //  BasicAttack_Effect_Slash.SetActive(false);

        AttackSpeed = GetComponent<Player_Stats>().AttackSpeed;

        Q_Punch_L.SetActive(false);
        Q_Punch_R.SetActive(false);
        adv_Q_Punch.SetActive(false);
        adv_Q_Punch_col.SetActive(false);
         //Q_Effect.SetActive(false);
         //adv_Q_Effect.SetActive(false);
        Q_SkillLevel = 1;
    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            //Debug.Log("Tiger speed " + agent.speed);

            agent.speed = GetComponent<Player_Stats>().MoveSpeed / 100;

            RightMouseClicked();

            animator.SetFloat("Speed", agent.velocity.magnitude);

            if (playerDir != movingManager.Instance.PlayerDirection) //플레이어 방향
            {
                playerDir = movingManager.Instance.PlayerDirection;
                agent.transform.rotation = Quaternion.AngleAxis(playerDir, Vector3.up);
            }
            if (TargetPos != movingManager.Instance.PlayerTargetPos) //플레이어 이동(R스킬시 이동제한)
            {
                TargetPos = movingManager.Instance.PlayerTargetPos;
                onSkill = true;
            }

            if (!onSkill)
            {
                agent.speed = GetComponent<Player_Stats>().MoveSpeed / 100; //스킬x일때 스피드값받기
            }


            if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //비전투모드
            else { movingManager.Instance.isFree = false; } //전투모드

            if (Enemy) //적 타겟이 있는경우
            {
                SpeedUp(); //포식자 스킬 적 챔피언 접근시 이동속도 30 증가
            }

            //////////////기본어택땅/////////////
            if (Input.GetKeyDown(KeyCode.A))
            {
                BasicRange_Col.SetActive(true);
                BasicRange.SetActive(true);
                isBasicAttack = true;
            }
            if (isBasicAttack) //A키 입력 이후에 활성화
            {
                LeftMouseClicked(); //왼쪽마우스 클릭
            }
            if (CheckEnemy) //적 체크 완료한경우
            {
                if (TargetEnemy)//타겟설정이 되었다면 
                {
                    Debug.Log("Targeted");
                    GetComponentInChildren<WhiteTiger_Basic_Range_Collider>().isAttackReady();
                    agent.SetDestination(TargetEnemy.transform.position); //타겟 위치로 이동
                    AttackTargetEnemy(); //타겟 공격
                }
                else if (!isBasicAttack)
                {       //타겟이 없으면 재설정
                    Debug.Log("Targeting");
                    GetComponentInChildren<WhiteTiger_Basic_Range_Collider>().isAttackReady();
                }
            }


            /////Q사용시 공격력up A공격////
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
            BasicRange_Col.SetActive(false); //콜라이더 클릭 방지
            RaycastHit hit; //캐릭터 이동

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//이동좌표 저장
                hit_ = hit;
            }
            isupdate = true;
            BasicRange_Col.SetActive(true);
            BasicRange.SetActive(false); //범위이펙트 종료
        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }

    void AttackTargetEnemy()
    {
        if ((transform.position - TargetEnemy.transform.position).magnitude <
            TargetEnemy.transform.localScale.magnitude*0.85f)
        {
            movingManager.Instance.PlayerClickedPos = transform.position; //공격범위 안이면 멈추고 방향전환

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
            TargetEnemy = null; //공격범위 밖이면 타겟없으므로 재설정
        }
    }

    void SpeedUp()
    { 
        if (!SpeedFull)
        {
            if ((transform.position - Enemy.position).magnitude < 10/*범위 태그minion->Enemy 수정요*/)
            {
                GetComponent<Player_Stats>().MoveSpeed += 30;
                SpeedFull = true; //한번만 가능
                Debug.Log("Tiger speedup " + agent.speed);
            }
        }
        else
        {
            if ((transform.position - Enemy.position).magnitude >= 10/*범위 태그minion->Enemy 수정요*/)
            {
                GetComponent<Player_Stats>().MoveSpeed -= 30;
                SpeedFull = false; //한번만 가능
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
                movingManager.Instance.PlayerClickedPos = hit.point;//이동좌표 저장
                hit_ = hit;
            }
            isupdate = true;
            PlayerDest = movingManager.Instance.PlayerClickedPos;


            isBasicAttack = false;
        }
 

    }

    private void LateUpdate()       //update에서 좌표값 갱신 후에 lateupdate에서 움직임
    {
        if(onSkill)
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
        if (isupdate&&!onSkill)
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

    IEnumerator Active_A()
    {
        OnAttack = true;
        while (true)
        {
            if (!On_adv_Q)
            {
                animator.SetBool("A_WT", true);
                BasicAttack_Effect_R.SetActive(true);
                GetComponentInChildren<WT_Punch_Collider_R>().Skill();
                yield return new WaitForSeconds(0.6f);
                BasicAttack_Effect_R.SetActive(false);
                yield return new WaitForSeconds(0.1f);
                BasicAttack_Effect_L.SetActive(true);
                GetComponentInChildren<WT_Punch_Collider_L>().Skill();
                yield return new WaitForSeconds(0.5f);
                BasicAttack_Effect_L.SetActive(false);


                //BasicAttack_Effect_Slash.SetActive(true);
                animator.SetBool("A_WT", false);
                yield return new WaitForSeconds(AttackSpeed);
                OnAttack = false;
                break;
            }
            else
            {
                animator.SetBool("A_WT", true);
                yield return new WaitForSeconds(0.2f);
                adv_Q_Punch_col.SetActive(true);
                GetComponentInChildren<WT_Bite_Collider>().Skill();
                yield return new WaitForSeconds(0.3f);
                adv_Q_Punch_col.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("A_WT", false);
                yield return new WaitForSeconds(AttackSpeed);
                OnAttack = false;
                break;
            }
           
        }

    }

    IEnumerator Active_Q()
    {
        while (true)
        {
        
            if (!GetComponent<WhiteTiger_Skill>().isWild)
            {
                GetComponent<Player_Stats>().AD += 10 * Q_SkillLevel;
                Q_Punch_L.SetActive(true);
                Q_Punch_R.SetActive(true);
                yield return new WaitForSeconds(10.0f);
                Q_Punch_L.SetActive(false);
                Q_Punch_R.SetActive(false);
                GetComponent<Player_Stats>().AD -= 10 * Q_SkillLevel;
                break;
            }
            else
            {
                On_adv_Q = true;    //true일경우 펀치->물어뜯기
                GetComponent<Player_Stats>().AD += 20 * Q_SkillLevel;
                adv_Q_Punch.SetActive(true);
                yield return new WaitForSeconds(10.0f);
                adv_Q_Punch.SetActive(false);
                GetComponent<Player_Stats>().AD -= 20 * Q_SkillLevel;
                On_adv_Q = false;
                //yield return new WaitForSeconds(10.0f);
                //adv_Q_Punch.SetActive(false);
                break;
            }
        }
    }


    float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(away.x - home.x, away.z - home.z) * Mathf.Rad2Deg;
    }

    public void R_Attack()
    {
        // StartCoroutine("Active_R_Attack");
        /// 적 챔피언에게만 가능. 추후 수정요
        ///////
        ///

        Debug.Log("RSkill Targeted");
    }

}


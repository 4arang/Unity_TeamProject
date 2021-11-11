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
    public Transform TargetEnemy;
    private float BasicRangef;
    private float AttackSpeed;
    private float BasicRange_Ref = 0.04f;
    private bool OnAttack = false;
    private float ColD_BasicAD;
    private byte ColD_BasicAD_Level = 1;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        grenadeDir = movingManager.Instance.PlayerDirection;
        onSkill = false;

        BasicRange.SetActive(false);
        BasicRange_Col.SetActive(false);
        BasicRangef = GetComponent<Player_Stats>().AttackRange * BasicRange_Ref;
        BasicRange.transform.localScale = new Vector3(BasicRangef, BasicRangef, 0);
        BasicAttack_Effect.SetActive(false);
        BasicAttack_Effect_Slash.SetActive(false);

        AttackSpeed = GetComponent<Player_Stats>().AttackSpeed;
    }


    private void Update()
    {


        agent.speed = GetComponent<Player_Stats>().MoveSpeed / 100;
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
            BasicRange_Col.SetActive(true);
            BasicRange.SetActive(true);
            isBasicAttack = true;
        }
        if (isBasicAttack) //A키 입력 이후에 활성화
        {
            LeftMouseClicked(); //왼쪽마우스 클릭
        }
        if(CheckEnemy && !isBasicAttack) //적 체크 완료한경우
        {
            if (TargetEnemy)//타겟설정이 되었다면 
            {
                Debug.Log("Targeted");
                GetComponentInChildren<ColD_Basic_Range_collider>().isAttackReady();
                agent.SetDestination(TargetEnemy.transform.position); //타겟 위치로 이동
                AttackTargetEnemy(TargetEnemy); //타겟 공격
            }
            else
            {       //타겟이 없으면 재설정
                Debug.Log("Targeting");
                GetComponentInChildren<ColD_Basic_Range_collider>().isAttackReady();
            }
        }
        if(GetComponent<ColD_W>().isSkillon)
        {
            CheckEnemy = false;
        }
                
    }


    void LeftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBasicAttack = false;

        
            CheckEnemy = true;
            BasicRange_Col.SetActive(false);
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


    void RightMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            BasicRange_Col.SetActive(false);
            isBasicAttack = false;
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
        else return;
    }

    void AttackTargetEnemy(Transform target)
    {
        if ((transform.position - target.transform.position).magnitude <
            target.transform.localScale.magnitude * 0.85f)
        {
            movingManager.Instance.PlayerClickedPos = transform.position; //공격범위 안이면 멈추고 방향전환
            if (animator.GetBool("A_ColD") == false)
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
        else target =  null;

    }

    IEnumerator Active_A(Transform target)
    {
        OnAttack = true;
        while (true)
        {
           
            animator.SetBool("A_ColD", true);
            yield return new WaitForSeconds(0.2f);
            BasicAttack_Effect.SetActive(true);
            //GetComponentInChildren<ColD_Punch_Collider>().Skill();
            BasicAttack_Effect_Slash.SetActive(true);
            yield return new WaitForSeconds(0.6f);
            BasicAttack_Effect.SetActive(false);
            animator.SetBool("A_ColD", false);

            if(target)damageEnemy(target);
            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
        OnAttack = false;
    }

    private void damageEnemy(Transform target)
    {
        ColD_BasicAD = GetComponent<ColD_Stats>().AD;

        if (target.CompareTag("Minion"))
        {
            target.GetComponent<Minion_Stats>().DropHP(ColD_BasicAD);
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<Player_Stats>().DropHP(ColD_BasicAD);
        }
        else if (target.CompareTag("Turret"))
        {
            target.GetComponent<Turret_Stats>().DropHP(ColD_BasicAD);
        }
        else if (target.CompareTag("Monster"))
        {
            target.GetComponent<Monster_Stats>().DropHP(ColD_BasicAD);
        }
    }

    float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(away.x - home.x, away.z - home.z) * Mathf.Rad2Deg;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Xerion : MonoBehaviour
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
    private bool isBasicAttack=false;
    public bool CheckEnemy = false;
    public Collider TargetEnemy;
    private float BasicRangef;
    private float AttackSpeed;
    private float BasicRange_Ref = 0.004f;
    private bool OnAttack = false;

    //Pasiive
    [SerializeField] private GameObject PassiveEffect;
    private float TotalAgentDistance=0;
    private bool passiveOn = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        //  lr.sharedMaterial.SetColor("_color", Color.white);

        skillDir = movingManager.Instance.PlayerDirection;

        BasicRange.SetActive(false);
        BasicRange_col.SetActive(false);
        BasicRangef = GetComponent<Xerion_Stats>().AttackRange*BasicRange_Ref;
        BasicRange.transform.localScale = new Vector3(BasicRangef, BasicRangef, 0);
        AttackSpeed = GetComponent<Xerion_Stats>().AttackSpeed;

        PassiveEffect.SetActive(false);
    }


    private void Update()
    {
        agent.speed = GetComponent<Xerion_Stats>().MoveSpeed / 100;

        RightMouseClicked();

        animator.SetFloat("Speed", agent.velocity.magnitude);
        if(animator.GetFloat("Speed")>0.1f)
        {
            TotalAgentDistance += Time.deltaTime;
        }
        if(TotalAgentDistance>2.0f)
        {
            TotalAgentDistance = 0;
            GetComponent<Xerion_Stats>().Energy += 1;
            Debug.Log("Xerion_Energy " + GetComponent<Xerion_Stats>().Energy);
        }

        if (skillDir != movingManager.Instance.PlayerDirection)
        {
            skillDir = movingManager.Instance.PlayerDirection;
            agent.transform.rotation = Quaternion.AngleAxis(skillDir, Vector3.up);

        }


        if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //비전투모드
        else { movingManager.Instance.isFree = false;} //전투모드


        //////////////기본어택땅/////////////
        if (Input.GetKeyDown(KeyCode.A))
        {
            BasicRange_col.SetActive(true);
            BasicRange.SetActive(true);
            isBasicAttack = true;
        }

        if (isBasicAttack)       //A키 입력 이후에 마우스왼쪽키 입력 가능
        {
            LeftMouseClicked();
        }
        if (CheckEnemy && !isBasicAttack) //적 체크 완료한경우
        {
            if (TargetEnemy)//타겟설정이 되었다면 
            {
                Debug.Log("Targeted");
                GetComponentInChildren<Xerion_Basic_Range_collider>().isAttackReady();
                agent.SetDestination(TargetEnemy.transform.position); //타겟 위치로 이동
                AttackTargetEnemy(); //타겟 공격
            }
            else
            {       //타겟이 없으면 재설정
                Debug.Log("Targeting");
                GetComponentInChildren<Xerion_Basic_Range_collider>().isAttackReady();
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

            RaycastHit hit; //캐릭터 이동

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//이동좌표 저장
                hit_ = hit;
            }
            isupdate = true;
            BasicRange_col.SetActive(true);
            BasicRange.SetActive(false); //범위이펙트 종료
        }
        PlayerDest = movingManager.Instance.PlayerClickedPos;
    }

    void RightMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            BasicRange_col.SetActive(false);
            isBasicAttack = false; //우클릭시 a(기본공격) 이동 false
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

            Xerion.path = agent.path.corners;
        }
       
    }
    

    void AttackTargetEnemy()
    {
        if((transform.position - TargetEnemy.transform.position).magnitude<
            TargetEnemy.transform.localScale.magnitude*10.0f)
        {
            movingManager.Instance.PlayerClickedPos = transform.position;
            Debug.Log("TargetSet, Player Stop");
            if (animator.GetBool("A_Xerion") == false)
            {
    
                float shootDir = GetDirection(transform.position, TargetEnemy.transform.position);
                movingManager.Instance.PlayerDirection = shootDir;
                agent.transform.rotation = Quaternion.AngleAxis(shootDir+45, Vector3.up);
         
                if (!OnAttack)
                {
                    GetComponent<Xerion_Stats>().Energy += 10;

                    Transform BasicShotTransform = Instantiate(BasicShot, GunShot_Effect.transform.position,
                     Quaternion.identity);
                    Vector3 shootingDir = (TargetEnemy.transform.position - transform.position).normalized;
                    BasicShotTransform.GetComponent<PFX_ProjectileObject>().Setup(shootingDir);
                    Debug.Log("Fire!");
                    
                    if(passiveOn)
                    {
                        GetComponent<Xerion_Stats>().Energy = 0;    //패시브 사용
                        GetComponent<Xerion_Stats>().DropMP(-GetComponent<Xerion_Stats>().MP / 10); //최대MP/10 충전
                        PassiveEffect.SetActive(false);
                        passiveOn = false;
                    }
                    StartCoroutine("Active_A");
                }
      
            }
        }
    }
    IEnumerator Active_A()
    {
        OnAttack = true;
        while (true)
        {
            animator.SetBool("A_Xerion", true);
            GunShot_Effect.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            GunShot_Effect.SetActive(false);
            animator.SetBool("A_Xerion", false);
            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
        OnAttack = false;
    }
    float GetDirection(Vector3 home, Vector3 away)
    {
       return Mathf.Atan2(away.x-home.x,away.z-home.z) * Mathf.Rad2Deg;
    }

    public void OnPassive()
    {
        passiveOn = true;
        PassiveEffect.SetActive(true);
    }



}
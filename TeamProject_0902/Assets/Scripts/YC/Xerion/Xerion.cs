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
    private bool isBasicAttack=false;
    public bool CheckEnemy = false;
    public Collider TargetEnemy;
    private float BasicRangef;
    public GameObject GunShot;
    private float AttackSpeed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        //  lr.sharedMaterial.SetColor("_color", Color.white);

        skillDir = movingManager.Instance.PlayerDirection;
        BasicRange.SetActive(false);
        GunShot.SetActive(false);
        BasicRangef = GetComponent<Xerion_Stats>().AttackRange/100;
        AttackSpeed = GetComponent<Xerion_Stats>().AttackSpeed;
    }


    private void Update()
    {
        agent.speed = GetComponent<Xerion_Stats>().MoveSpeed / 100;

        RightMouseClicked();

        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (skillDir != movingManager.Instance.PlayerDirection)
        {
            skillDir = movingManager.Instance.PlayerDirection;
            agent.transform.rotation = Quaternion.AngleAxis(skillDir, Vector3.up);

        }


        if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //비전투모드
        else { movingManager.Instance.isFree = false;} //전투모드

        if(Input.GetKeyDown(KeyCode.A))
        {
            BasicRange.SetActive(true);
            isBasicAttack = true;
        }

        if(isBasicAttack)       //A키 입력 이후에 마우스왼쪽키 입력 가능
        {
            LeftMouseClicked();

            if (TargetEnemy)
            {
                agent.SetDestination(TargetEnemy.transform.position);

                AttackTargetEnemy();
            }
            else
            {       //타겟이 없으면 재설정
                GetComponentInChildren<Xerion_Basic_Range_collider>().isAttackReady();
            }
        }
    
    }


    void LeftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BasicRange.SetActive(false); //범위이펙트 종료
            CheckEnemy = true;
            GetComponentInChildren<Xerion_Basic_Range_collider>().isAttackReady();

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

    void RightMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isBasicAttack = false; //우클릭시 a(기본공격) 이동 false
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
        if((transform.position - TargetEnemy.transform.position).magnitude<BasicRangef)
        {
            movingManager.Instance.PlayerClickedPos = transform.position;
            Debug.Log("TargetSet, Player Stop");
            if (animator.GetBool("A_Xerion") == false)
            {
                animator.SetBool("A_Xerion", true);
                float shootDir = GetDirection(transform.position, TargetEnemy.transform.position);
                movingManager.Instance.PlayerDirection = shootDir;
                agent.transform.rotation = Quaternion.AngleAxis(shootDir, Vector3.up);
                StartCoroutine("Active_A");
            }
        }
    }
    IEnumerator Active_A()
    {
        while (true)
        {
            GunShot.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            GunShot.SetActive(false);
            animator.SetBool("A_Xerion", false);
            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
    }
    float GetDirection(Vector3 home, Vector3 away)
    {
       return Mathf.Atan2(away.x-home.x,away.z-home.z) * Mathf.Rad2Deg;
    }

}
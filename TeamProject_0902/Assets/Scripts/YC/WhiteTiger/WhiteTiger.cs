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

    private Transform Enemy; //적 위치 받아오기
    private bool SpeedFull = false; 


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
    }


    private void Update()
    {

        Debug.Log("Tiger speed " + agent.speed);

        agent.speed = GetComponent<WhiteTiger_Stats>().MoveSpeed / 100;

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

        if(!onSkill)
        {
            agent.speed = GetComponent<WhiteTiger_Stats>().MoveSpeed / 100; //스킬x일때 스피드값받기
        }


        if (agent.velocity.magnitude < 0.1f) { movingManager.Instance.isFree = true; } //비전투모드
        else { movingManager.Instance.isFree = false; } //전투모드

        SpeedUp(); //포식자 스킬 적 챔피언 접근시 이동속도 30 증가
    }

    void SpeedUp()
    {
        if (!SpeedFull)
        {
            if ((transform.position - Enemy.position).magnitude < 10/*범위 태그minion->Enemy 수정요*/)
            {
                GetComponent<WhiteTiger_Stats>().MoveSpeed += 30;
                SpeedFull = true; //한번만 가능
                Debug.Log("Tiger speedup " + agent.speed);
            }
        }
        else
        {
            if ((transform.position - Enemy.position).magnitude >= 10/*범위 태그minion->Enemy 수정요*/)
            {
                GetComponent<WhiteTiger_Stats>().MoveSpeed -= 30;
                SpeedFull = false; //한번만 가능
                Debug.Log("Tiger speeddown " + agent.speed);
            }
        }
    }

    void RightMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                movingManager.Instance.PlayerClickedPos = hit.point;//이동좌표 저장
                hit_ = hit;
            }
            isupdate = true;
            PlayerDest = movingManager.Instance.PlayerClickedPos;
        }


    }

    private void LateUpdate()       //update에서 좌표값 갱신 후에 lateupdate에서 움직임
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

}

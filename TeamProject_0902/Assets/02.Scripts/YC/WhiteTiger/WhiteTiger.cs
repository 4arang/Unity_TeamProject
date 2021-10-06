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


    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        playerDir = ycManager.Instance.PlayerDirection;
        onSkill = false;
        TargetPos = ycManager.Instance.PlayerTargetPos;
        originalSpeed = agent.speed;
    }


    private void Update()
    {

        RightMouseClicked();

        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (playerDir != ycManager.Instance.PlayerDirection)
        {
            playerDir = ycManager.Instance.PlayerDirection;
            agent.transform.rotation = Quaternion.AngleAxis(playerDir, Vector3.up);
        }
        if (TargetPos != ycManager.Instance.PlayerTargetPos)
        {
            TargetPos = ycManager.Instance.PlayerTargetPos;
            onSkill = true;
        }

        if (agent.velocity.magnitude < 0.1f) { ycManager.Instance.isFree = true; } //비전투모드
        else { ycManager.Instance.isFree = false; } //전투모드
    }

    void RightMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                ycManager.Instance.PlayerClickedPos = hit.point;//이동좌표 저장
                hit_ = hit;
            }
            isupdate = true;
            PlayerDest = ycManager.Instance.PlayerClickedPos;
        }

    }

    private void LateUpdate()       //update에서 좌표값 갱신 후에 lateupdate에서 움직임
    {
        if(onSkill)
        {
            agent.speed = skillSpeed;
            agent.SetDestination(TargetPos);
            PlayerDest = TargetPos;
            if (Vector3.Distance(TargetPos, agent.transform.position) < 0.01f)
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

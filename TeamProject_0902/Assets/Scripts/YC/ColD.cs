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

   
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lr = linerenderobj.GetComponent<LineRenderer>();
        grenadeDir = ycManager.Instance.PlayerDirection;
        onSkill = false;
    }


    private void Update()
    {
        agent.speed = GetComponent<ColD_Stats>().MoveSpeed / 100;
        Debug.Log("Speed " + agent.speed);
        RightMouseClicked();

        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (grenadeDir != ycManager.Instance.PlayerDirection)
        {
            onSkill = true;
            grenadeDir = ycManager.Instance.PlayerDirection;
            agent.transform.rotation = Quaternion.AngleAxis(grenadeDir, Vector3.up);
        }
        else
            onSkill = false;

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
        }
        PlayerDest = ycManager.Instance.PlayerClickedPos;
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
            //Play Animation
           // animator.SetTrigger("Walk");
          

            //Move
            agent.SetDestination(PlayerDest);
            agent.stoppingDistance = 0;

            // Rotation
            //if (!onSkill)
            //{
            //    Quaternion rotationToLookAt = Quaternion.LookRotation(PlayerDest - transform.position);
            //    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            //        rotationToLookAt.eulerAngles.y,
            //        ref rotateVelocity,
            //        rotateSpeedMovement * (Time.deltaTime * 5));
            //    transform.eulerAngles = new Vector3(0, rotationY, 0);
            //}
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

}

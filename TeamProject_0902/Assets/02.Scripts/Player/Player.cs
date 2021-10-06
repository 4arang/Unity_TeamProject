using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    public NavMeshAgent agent;

    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    private Player_Combat heroCombatScript;

    //saved variable for lateupdate
    Vector3 PlayerDest;
    RaycastHit hit_;
    bool isupdate = false;


    //for NavPathLine
    public static Vector3[] path = new Vector3[0];
   LineRenderer lr;
    public GameObject linerenderobj;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<Player_Combat>();
        lr = linerenderobj.GetComponent<LineRenderer>();
      //  lr.sharedMaterial.SetColor("_color", Color.white);
    }


    private void Update()
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
        if(isupdate)
        PlayerMove();
    }


    void PlayerMove()
    {
        if (hit_.collider.tag == "Floor")
        {
            //Move
            agent.SetDestination(PlayerDest);
            heroCombatScript.targetedEnemy = null;
            agent.stoppingDistance = 0;

            //Rotation
            Quaternion rotationToLookAt = Quaternion.LookRotation(PlayerDest - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref rotateVelocity,
                rotateSpeedMovement * (Time.deltaTime * 5));

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            if (heroCombatScript.targetedEnemy != null)
            {
                if (heroCombatScript.targetedEnemy.GetComponent<Player_Combat>() != null)
                {
                    if (heroCombatScript.targetedEnemy.GetComponent<Player_Combat>().isHeroAlive)
                    {
                        heroCombatScript.targetedEnemy = null;
                    }
                }

            }

            if (path != null && path.Length > 1)
            {
                lr.positionCount = path.Length;
                for (int i = 0; i < path.Length; i++)
                {
                    lr.SetPosition(i, path[i]);
                }
            }
            Player.path = agent.path.corners;
           
        }
    }
}

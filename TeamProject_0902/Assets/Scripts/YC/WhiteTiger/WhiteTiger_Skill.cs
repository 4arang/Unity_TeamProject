using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WhiteTiger_Skill : MonoBehaviour
{
    PhotonView PV;

    private Animator animator;
    public byte WildPoint; //0~4
    public bool isWild;
    public bool isBasicAttack = false;

    [SerializeField] private GameObject Direction;
    [SerializeField] private GameObject Range;
    [SerializeField] private Transform DirectionPos;


    private float DirecAngle;
    private Vector3 mouseVector;



    [Header("W_Skill")]
    [SerializeField] private GameObject W_Shield;
    [SerializeField] private GameObject adv_W_Shield;

    [Header("E_Skill")]
    [SerializeField] private GameObject E_Aura;
    [SerializeField] private GameObject adv_E_Aura;

    [Header("R_Skill")]

    public float ref_Dist_time = 0.1f;
    public float ref_flyingSpeed = 1000f;
    private float Distance_Player2Target;
    public Transform Target; //�� ��ġ
    public bool R_Targeted;


    void Start()
    {
        PV = GetComponent<PhotonView>();
        isWild = false;

        Direction.SetActive(false);
        Range.SetActive(false);

        W_Shield.SetActive(false);
        adv_W_Shield.SetActive(false);
        E_Aura.SetActive(false);
        adv_E_Aura.SetActive(false);

        animator = GetComponent<Animator>();
        WildPoint = 0;
    }


    void Update()
    {
        if (PV.IsMine)
        {
            if (WildPoint == 4)
            {
                isWild = true;
                animator.SetBool("Wildness", true);
                //  StartCoroutine("Wild_State");
                WildPoint = 0;
            }



            if (Input.GetKeyDown(KeyCode.W))
            {
                if (animator.GetBool("W_WT") == false)
                    StartCoroutine("Active_W");
                animator.SetBool("W_WT", true);
                if (!isWild) WildPoint++;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine("Active_E");
                if (!isWild) WildPoint++;
            }

            if (Input.GetKey(KeyCode.R))
            {
                Direction.SetActive(true);
                Range.SetActive(true);
                GetMousePos();//���콺 ��ġ�޾ƿ���
                Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up);//ȭ��ǥ����

                movingManager.Instance.PlayerDirection = DirecAngle; //�÷��̾ ��������
                GetComponentInChildren<WT_Rskill_Collider>().Skill(); //Ʈ����+Ÿ����ġ �޾Ƽ� Target_pos ����

            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                Direction.SetActive(false);
                Range.SetActive(false);

                if (!R_Targeted) //Ÿ���� ã�� ���Ѱ�� ������ ����

                {
                    Debug.Log("Non Target");
                    Target = DirectionPos;
                }
                Distance_Player2Target = Vector3.Distance(Range.transform.position, Target.position); //Ÿ�ٱ��� �Ÿ� ���ϱ�


                movingManager.Instance.PlayerTargetPos = Target.position;
                movingManager.Instance.PlayerClickedPos = Target.position;

                if (animator.GetBool("R_WT") == false)
                {
                    animator.SetBool("R_WT", true);
                    Debug.Log("Distan2Time" + Distance2Time());
                    StartCoroutine("Active_R", Target);
                    if (!isWild) WildPoint++;
                }
            }
        }
    }

    IEnumerator Wild_state()
    {
        while (true)
        {
            yield return new WaitForSeconds(8.0f);
            isWild = false;
            animator.SetBool("Wildness", false);
            break;
        }
    }



    IEnumerator Active_W()
    {
        while (true)
        {
            if (!isWild)
            {
                yield return new WaitForSeconds(0.3f);
                animator.SetBool("W_WT", false);
                PV.RPC("activeW", RpcTarget.AllViaServer, true);
                yield return new WaitForSeconds(1.5f);
                PV.RPC("activeW", RpcTarget.AllViaServer, false);
                break;
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
                animator.SetBool("W_WT", false);
                PV.RPC("activeW_adv", RpcTarget.AllViaServer, true);
                yield return new WaitForSeconds(1.5f);
                PV.RPC("activeW_adv", RpcTarget.AllViaServer, false);
                break;
            }
        }
    }

    IEnumerator Active_E()
    {
        while (true)
        {
            if (!isWild)
            {
                PV.RPC("activeE", RpcTarget.AllViaServer, true);
                yield return new WaitForSeconds(6.0f);
                PV.RPC("activeE", RpcTarget.AllViaServer, false);
                break;
            }
            else
            {
                PV.RPC("activeE_adv", RpcTarget.AllViaServer, true);
                yield return new WaitForSeconds(6.0f);
                PV.RPC("activeE_adv", RpcTarget.AllViaServer, false);
                break;
            }
        }
    }

    IEnumerator Active_R(Transform target)
    {
        while (true)
        {
            yield return new WaitForSeconds(Distance2Time());
            animator.SetBool("R_WT", false);
            break;
        }
        if (R_Targeted) GetComponent<WhiteTiger>().R_Attack(target);
    }




    Vector3 GetMousePos()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        //���콺 ��ġ �޾Ƽ� �������
        DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
            hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;
        mouseVector = hit.point;


        return hit.point;
    }
    float Distance2Time()
    {
        float time;
        time = Distance_Player2Target * ref_Dist_time;
        return time;
    }

    [PunRPC]
    void activeW(bool b)
    {
        W_Shield.SetActive(b);
    }
    [PunRPC]
    void activeW_adv(bool b)
    {
        adv_W_Shield.SetActive(b);
    }
    [PunRPC]
    void activeE(bool b)
    {
        E_Aura.SetActive(b);
    }
    [PunRPC]
    void activeE_adv(bool b)
    {
        adv_E_Aura.SetActive(b);
    }
}

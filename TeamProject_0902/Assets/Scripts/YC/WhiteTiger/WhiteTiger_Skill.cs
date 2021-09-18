using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger_Skill : MonoBehaviour
{
    private Animator animator;
    public byte WildPoint; //0~4
    public bool isWild;
    public bool isBasicAttack=false;

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
    public Vector3 Target_pos; //적 위치
    public bool R_Targeted;
    

    void Start()
    {
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
        if(WildPoint==4)
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
            if(!isWild) WildPoint++;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
                StartCoroutine("Active_E");
            if(!isWild) WildPoint++;
        }

        if (Input.GetKey(KeyCode.R))
        {
            Direction.SetActive(true);
            Range.SetActive(true);
            GetMousePos();//마우스 위치받아오기
            Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up);//화살표방향

            movingManager.Instance.PlayerDirection = DirecAngle; //플레이어에 방향전달
            GetComponentInChildren<WT_Rskill_Collider>().Skill(); //트리거+타겟위치 받아서 Target_pos 저장

        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Direction.SetActive(false);
            Range.SetActive(false);

            if (!R_Targeted) //타겟을 찾지 못한경우 끝으로 가게

            {
                Debug.Log("Non Target");
                Target_pos = DirectionPos.position;
            }   
                Distance_Player2Target = Vector3.Distance(Range.transform.position, Target_pos); //타겟까지 거리 구하기
            

            movingManager.Instance.PlayerTargetPos = Target_pos;
            movingManager.Instance.PlayerClickedPos = Target_pos;

            if (animator.GetBool("R_WT") == false)
            {
                animator.SetBool("R_WT", true);
                Debug.Log("Distan2Time" + Distance2Time());
                StartCoroutine("Active_R");
                if(!isWild) WildPoint++;
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
                W_Shield.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                W_Shield.SetActive(false);
                break;
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
                animator.SetBool("W_WT", false);
                adv_W_Shield.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                adv_W_Shield.SetActive(false);
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
                E_Aura.SetActive(true);
                yield return new WaitForSeconds(6.0f);
                E_Aura.SetActive(false);
                break;
            }
            else
            {
                adv_E_Aura.SetActive(true);
                yield return new WaitForSeconds(6.0f);
                adv_E_Aura.SetActive(false);
                break;
            }
        }
    }

    IEnumerator Active_R()
    {
        while (true)
        {     
            yield return new WaitForSeconds(Distance2Time());
            animator.SetBool("R_WT", false);
            break;
        }
        if (R_Targeted) GetComponent<WhiteTiger>().R_Attack();
    }




    Vector3 GetMousePos()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        //마우스 위치 받아서 각도계산
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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    Collider collider;

    private bool setup = false;
    private Vector3 orgPos; //기존 스폰 위치
    public float moveRange=5.0f; //움직일 수 있는 범위
    private Transform Target;
    private float TargetRange;
    private bool TargetFound;
    private float AttackSpeed;
    private float Monster_AD;

    private float PassiveRange = 0.8f;
    private bool attacked = false;
    private bool isAttack = false;
    private byte attackNum = 1; //1번공격 특수스킬, 234 -> 일반스킬

    [SerializeField] private GameObject Monster2;
    [SerializeField] private GameObject Monster1;


    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }
    public void Start_()
    {
        collider.enabled = true;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        TargetRange = GetComponent<Monster_Stats>().AttackRange * 0.05f;
        TargetFound = false;
        AttackSpeed = GetComponent<Monster_Stats>().AttackSpeed;

        agent.speed = GetComponent<Monster_Stats>().MoveSpeed / 100;
        animator.SetFloat("Speed", agent.velocity.magnitude);

        InvokeRepeating("Passive", 0f, 0.5f);

        orgPos = transform.position;
        setup = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (setup)
        {
            if (attacked && Target) AttackTarget(Target);
            else agent.SetDestination(orgPos); //기존 위치로 돌아오기

            if (Vector3.Distance(transform.position, orgPos) >= moveRange) //일정 거리 이상 못나가게
            {
                agent.speed = 0;
            }
        }
    }

    private void Passive()  //PassiveRange 안에 챔피언 0~1명 있을경우 발동
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, PassiveRange);

        byte i = 0;
        foreach (Collider col in colliderArray)
        {
            if (col.TryGetComponent<Player_Stats>(out Player_Stats player))
            {
                i++;
            }
        }

        if (i <= 1)
        {
            GetComponent<Monster_Stats>().PassiveOn();
        }
        else
            GetComponent<Monster_Stats>().PassiveOff();
    }
    public void Attacked() //마지막 피격 이후 5초당안 공격 유지
    {
        attacked = true;
        if (attacked)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, TargetRange);

            float dist = 0;
            float dist_ = Mathf.Infinity;

            foreach (Collider col in colliderArray) //가장 가까이 있는 player 타겟 설정
            {
                if (col.TryGetComponent<Player_Stats>(out Player_Stats player))
                {
                    dist = Vector3.Distance(transform.position, player.transform.position);
                    if (dist < dist_)
                    {
                        Target = player.transform;
                        dist_ = dist;
                    }

                }
                if (dist == 0) //player 찾지 못한 경우
                {
                    agent.SetDestination(orgPos); //기존 위치로 돌아오기
                }
            }
            StartCoroutine("AttackedTimeCheck");
        }
    }
    private void AttackTarget(Transform target)
    {
        
        agent.SetDestination(target.position);
        Debug.Log("Monster move");
        if (Vector3.Distance(agent.transform.position, target.position) <= TargetRange)
        {
            //Stop 
            agent.SetDestination(transform.position);
            Debug.Log("monster stop");
            //To look at the Target
            float agentDir = GetDirection(transform.position, target.position);
            agent.transform.rotation = Quaternion.AngleAxis(agentDir, Vector3.up);
            //Attack
            if (!isAttack)
            {
                Debug.Log("monster Attack");
                StartCoroutine("Attacking", target);
            }
        }
        //else if (Vector3.Distance(agent.transform.position, target.position) > TargetRange_ * 1.5 || target == null) //out of target setting Range
        //{
        //    TargetFound = false;
        //}
    }

    IEnumerator Attacking(Transform target)
    {
        bool skill_2= false;
        isAttack = true;
        Monster_AD = GetComponent<Monster_Stats>().AD;
        if (attackNum == 1)
        {
            Monster_AD *= 0.8f;
            skill_2 = true; //특수공격
        }
        else
        {
            if (attackNum == 4) //initialize attackNum
            {
                attackNum = 0;
            }
        }
        attackNum++;
        
        if(skill_2)
        {
            while (true)
            {
                animator.SetBool("Skill2", true);
                yield return new WaitForSeconds(1.5f);
                if (target) damageEnemy(target);//광역스킬이므로 변경해야함 스킬에서 스턴걸기도 같이
                animator.SetBool("Skill2", false);
                yield return new WaitForSeconds(AttackSpeed);
                break;
            }
        }
            else //일반공격
        {
            while (true)
            {
                animator.SetBool("Skill1", true);
                yield return new WaitForSeconds(1.3f);
                if (target) damageEnemy(target);
                animator.SetBool("Skill1", false);
                yield return new WaitForSeconds(AttackSpeed);
                break;
            }
        }

        isAttack = false;
    }

    private void damageEnemy(Transform target)
    {
        target.GetComponent<Player_Stats>().DropHP(Monster_AD);
    }

    float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(away.x - home.x, away.z - home.z) * Mathf.Rad2Deg;
    }

    public void Die()
    {
        Debug.Log("Monster Die");
        agent.speed = 0;
        animator.SetBool("Die", true);
        collider.enabled = false;
        Target = null;
        attacked = false;
        StartCoroutine("Respawn");
        setup = false;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(10.0f);
        Instantiate(Monster2, transform.position, Quaternion.AngleAxis(180, Vector3.up));
        animator.SetBool("Spawn", true);
        yield return new WaitForSeconds(9.2f);
        animator.SetBool("Spawn", false);
        Debug.Log("destroy monsterself");

        Destroy(gameObject);

    }

    IEnumerator AttackedTimeCheck()
    {
        yield return new WaitForSeconds(5.0f);
        Target = null;
        attacked = false;
        Debug.Log("attacked false");
    }
}

//	공격 패턴
//(특수공격 – 일반 공격 – 일반 공격 – 일반 공격) (특수 공격 – 일반 공격 – 일반 공격 – 일반 공격)의 형태
//	특수 공격  사정거리 950 내의 가장 가까운 챔피언 두명이 현재 위치하는 
//지점에 광역 공격(0.8 AD)과 0.5초의 광역 기절기를 지닌 공격
// 제리온의 W 스킬과 동일한 공격 판정과 피해 범위를 지닌다.
//	일반 공격 일반 단일 타겟 기본 공격 (1.0 AD)


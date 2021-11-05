using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Minion2 : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    private bool isAttack;

   [SerializeField] private GameObject AttackEffect; //아직 안달아줌
    [SerializeField] private Transform AttackBullet;
    private Transform Target;
    private float TargetRange;
    private bool TargetFound;
    private float AttackSpeed;
    private float Minion2_AD;

    public bool TeamColor;
    private bool OnUpdateTarget = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        AttackEffect.SetActive(false);
        isAttack = false;

        TargetRange = GetComponent<Minion_Stats>().AttackRange * 0.01f;
        TargetFound = false;
        AttackSpeed = GetComponent<Minion_Stats>().AttackSpeed;

        TeamColor = GetComponent<Minion_Stats>().TeamColor;

        InvokeRepeating("FindTarget", 0f, 0.5f);
    }


    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (GetComponent<Minion_Stats>().HP <= 0)
        {
            animator.SetBool("Die", true);
            StartCoroutine("Dying");
        }

        ///TargetCheck
        if (TargetFound&&Target) AttackTarget(Target);
    }
    private void FindTarget()
    {
        if (!Target || Vector3.Distance(agent.transform.position, Target.position) > TargetRange * 1.5f) //Non-set Target or Missing Target
        {
            Debug.Log("Targeting2 " + TargetRange);
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, TargetRange * 1.5f);
            foreach (Collider col in colliderArray)
            {

                if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                    && player.isAttack_Player && (player.TeamColor != TeamColor))
                {
                    Target = player.transform;
                    GetComponent<Minion_Stats>().isAttack_Player = true;
                    Debug.Log("Target Priority 1 " + Target);
                }
                else if (col.TryGetComponent<Minion_Stats>(out Minion_Stats enemy)
                    && enemy.isAttack_Player && (enemy.TeamColor != TeamColor))
                {
                    Target = enemy.transform;
                    Debug.Log("Target Priority 2 " + Target);
                }
                else if (col.TryGetComponent<Minion_Stats>(out Minion_Stats enemy_)
                     && enemy.isAttack_Player && (enemy_.TeamColor != TeamColor))
                {
                    Target = enemy_.transform;
                    GetComponent<Minion_Stats>().isAttack_Minion = true;
                    Debug.Log("Target Priority 2 " + Target);
                }
                //else if (col/* 아군 미니언을 공격하는 적 포탑*/)

                else if (col.TryGetComponent<Player_Stats>(out Player_Stats player_)
                    && player_.isAttack_Minion && (player_.TeamColor != TeamColor))
                {
                    Target = player_.transform;
                    GetComponent<Minion_Stats>().isAttack_Player = true;
                    Debug.Log("Target Priority 5 " + Target);
                }

                else if (col.TryGetComponent<Minion_Stats>(out Minion_Stats enemy__) && (enemy__.TeamColor != TeamColor)) //Targeting Minion
                {
                    Target = enemy__.transform;
                    GetComponent<Minion_Stats>().isAttack_Minion = true;
                    Debug.Log("Target Priority 6 " + Target);
                    Debug.Log("My Teamcolor " + TeamColor + "Target TeamcOlor " + enemy__.TeamColor);
                }
                else if (col.TryGetComponent<Player_Stats>(out Player_Stats player__) && (player__.TeamColor != TeamColor)) //Minion > Champion
                {
                    Target = player__.transform;
                    GetComponent<Minion_Stats>().isAttack_Player = true;
                    Debug.Log("Target Priority 7 " + Target);
                }
            }
        }
        else
        {
            TargetFound = true;
            GetComponent<Minion_Stats>().isAttack_Player = false;
            GetComponent<Minion_Stats>().isAttack_Minion = false;

            UpdateTarget(); //every 1 second
        }

    }
    private void AttackTarget(Transform target)
    {
        agent.SetDestination(target.position);
        Debug.Log("Targetpos " + target.position);
        Debug.Log("Mypos " + transform.position);

        if (Vector3.Distance(agent.transform.position, target.position) <= TargetRange)
        {
            //Stop 
            agent.SetDestination(transform.position);
            //To look at the Target
            float agentDir = GetDirection(transform.position, target.position);
            agent.transform.rotation = Quaternion.AngleAxis(agentDir, Vector3.up);
            //Attack
            if (!isAttack)
            {
                Transform BasicShotTransform = Instantiate(AttackBullet, AttackEffect.transform.position,
                    Quaternion.AngleAxis(agentDir, Vector3.up));
                Vector3 shootingDir = (target.transform.position - transform.position).normalized;
                BasicShotTransform.GetComponent<SkillSetting>().Setup(shootingDir, target.transform.position);

                animator.SetBool("Attack", true);
                StartCoroutine("Attacking", target);
            }
        }
        else
        if (Vector3.Distance(agent.transform.position, target.position) > TargetRange * 1.5 || target == null) //out of target setting Range
        {
            TargetFound = false;
        }

    }
    IEnumerator Dying()
    {
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("Die", false);
        Destroy(gameObject);
    }

    IEnumerator Attacking(Transform target)
    {
        isAttack = true;
        while (true)
        {
            AttackEffect.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            AttackEffect.SetActive(false);
            Debug.Log("Attack");
            if (target) damageEnemy(target);
            animator.SetBool("Attack", false);
            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
        isAttack = false;
    }

    private void damageEnemy(Transform target)
    {

        Minion2_AD = GetComponent<Minion_Stats>().AD;

        if (target.CompareTag("Minion"))
        {
            target.GetComponent<Minion_Stats>().DropHP(Minion2_AD);
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<Player_Stats>().DropHP(Minion2_AD);
        }
        else if (target.CompareTag("Turret"))
        {
            target.GetComponent<Turret_Stats>().DropHP(Minion2_AD);
        }

    }

    float GetDirection(Vector3 home, Vector3 away)
    {
        return Mathf.Atan2(away.x - home.x, away.z - home.z) * Mathf.Rad2Deg;
    }

    void UpdateTarget()
    {
        StartCoroutine("TargetDelete");
    }
    IEnumerator TargetDelete()
    {
        if (OnUpdateTarget)
        {
            OnUpdateTarget = false;
            yield return new WaitForSeconds(1.0f);
            Target = null;
            Debug.Log("Update Target");
            OnUpdateTarget = true;
        }
    }

}

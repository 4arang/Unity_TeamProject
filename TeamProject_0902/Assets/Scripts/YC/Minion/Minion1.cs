using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class Minion1 : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    private bool isAttack;

   [SerializeField] private GameObject AttakEffect;
    private Transform Target;
    private float TargetRange;
    private bool TargetFound;
    private float AttackSpeed;
    private float Minion1_AD;

    [SerializeField] private Transform Turret1;
    [SerializeField] private Transform Turret2;
    [SerializeField] private Transform Turret3;

    public bool TeamColor;
    private bool OnUpdateTarget = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        isAttack = false;

        TargetRange = GetComponent<Minion_Stats>().AttackRange*0.01f;
        TargetFound = false;
        AttackSpeed = GetComponent<Minion_Stats>().AttackSpeed;

        TeamColor = GetComponent<Minion_Stats>().TeamColor;

        if (Turret1) Target = Turret1;
        else if (Turret2) Target = Turret2;
        else if (Turret3) Target = Turret3;
        InvokeRepeating("FindTarget", 0f, 0.5f);
    }

    
    void Update()
    {
        agent.speed = GetComponent<Minion_Stats>().MoveSpeed;
        animator.SetFloat("Speed", agent.velocity.magnitude);


        ///HP Check
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
            Debug.Log("Targeting1 " + TargetRange);
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, TargetRange * 1.5f);
            foreach (Collider col in colliderArray)
            {

                if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                    && player.isAttack_Player &&(player.TeamColor != TeamColor))
                {
                    Target = player.transform;
                    GetComponent<Minion_Stats>().isAttack_Player = true;
                    Debug.Log("Target Priority 1 " + Target);
                }
                else if (col.TryGetComponent<Minion_Stats>(out Minion_Stats enemy)
                    && enemy.isAttack_Player && (enemy.TeamColor != TeamColor) )
                {
                    Target = enemy.transform;
                    Debug.Log("Target Priority 2 " + Target);
                }
                else if (col.TryGetComponent<Minion_Stats>(out Minion_Stats enemy_)
                     && enemy.isAttack_Player && (enemy_.TeamColor != TeamColor) )
                {
                    Target = enemy_.transform;
                    GetComponent<Minion_Stats>().isAttack_Minion = true;
                    Debug.Log("Target Priority 2 " + Target);
                }
                //else if (col.TryGetComponent<>)

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

                StartCoroutine("Attacking", target);
            }
        }
        else
        if (Vector3.Distance(agent.transform.position, target.position) > TargetRange*1.5 || target == null) //out of target setting Range
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
           animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Attack");
           if(target) damageEnemy(target);

            animator.SetBool("Attack", false);
            yield return new WaitForSeconds(AttackSpeed);
            break;
        }
        isAttack = false;
    }

    private void damageEnemy(Transform target)
    {
        Instantiate(AttakEffect, target.transform.position, Quaternion.identity);
        Minion1_AD = GetComponent<Minion_Stats>().AD;

        if (target.CompareTag("Minion"))
        {
            target.GetComponent<Minion_Stats>().DropHP(Minion1_AD);
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<Player_Stats>().DropHP(Minion1_AD);
        }
        else if (target.CompareTag("Turret"))
        {
            target.GetComponent<Turret_Stats>().DropHP(Minion1_AD);
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

///적 챔피언이 아군 챔피언 공격시
///
///적 미니언이 아군 챔피언 공격시
/// 적 미니언이 아군 미니언 공격시
///
///적 포탑이 아군 미니언 공격시
///
///적 미니언
///적 챔피언
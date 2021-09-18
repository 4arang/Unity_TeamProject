using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Minion2 : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    private bool isAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        isAttack = false;
    }


    void Update()
    {
        agent.speed = GetComponent<Minion_Stats>().MoveSpeed;
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (GetComponent<Minion_Stats>().HP <= 0)
        {
            animator.SetBool("Die", true);
            StartCoroutine("Dying");
        }

        if (isAttack)
        {
            animator.SetBool("Attack", true);
            StartCoroutine("Attacking");
        }
    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("Die", false);
        Destroy(gameObject);
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Attack", false);
    }

}

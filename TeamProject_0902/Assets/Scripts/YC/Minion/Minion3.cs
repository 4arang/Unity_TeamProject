using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion3 : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    private bool isAttack;
    [SerializeField] private Transform Laser;
    [SerializeField] private Transform Barrel;
    [SerializeField] private Transform BarrelArm;

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
        if(Input.GetKey(KeyCode.T))
        {
            isAttack = true;
        }

        if (isAttack)
        {
            animator.SetBool("Attack", true);

            isAttack = false;
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
        yield return new WaitForSeconds(0.3f);
        Transform LaserTransform = Instantiate(Laser, Barrel.position, Quaternion.identity);
        Vector3 Direction = (Barrel.transform.position - BarrelArm.transform.position).normalized;
        LaserTransform.GetComponent<Minion3_Laser>().Setup(Direction);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Attack", false);

    }
}

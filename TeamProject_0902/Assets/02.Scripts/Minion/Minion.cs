using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Minion : MonoBehaviour
{
    float speed = 100.0f;                        //Get value from stats
    public Transform target;
    private int wavePointIndex = 0;             //Node Index
    void Start()
    {
        target = WayPoints.wayPoints[0];            //WayPoint Initialize
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 10.0f)      //0.2f->get value from stat attack range
            {
                if (target.CompareTag("Building"))
                {
                    //Attack if Target is Inhibitator or Nexus.
                }
                else
                {
                    GetNextWayPoint();
                }

            }

        }
        else
        {
            Debug.Log("Minion Target is null");
            //Combat when get destination
        }
    }

    void GetNextWayPoint()
    {
        wavePointIndex++;
        target = WayPoints.wayPoints[wavePointIndex];
    }
}    //NavMeshAgent navAgent;
    //public Transform target;
    //private int wavePointIndex = 0;         //Node Index
    //void Start()
    //{
    //    navAgent = GetComponent<NavMeshAgent>();
    //    target = WayPoints.wayPoints[0];   //WayPoint Initialize
    //}

    //void Update()
    //{
    //    if (target != null)
    //    {
    //        navAgent.SetDestination(target.position);
    //        if(navAgent.remainingDistance<=0.1f)
    //        {
    //            GetNextWayPoint();
    //        }
    //    }
    //    else
    //    {
    //        //Combat when get destination
    //    }
    //}

    //void GetNextWayPoint()
    //{
    //    wavePointIndex++;
    //    target = WayPoints.wayPoints[wavePointIndex];
    //    navAgent.SetDestination(target.position);
    //}

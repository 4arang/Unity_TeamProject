using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Minion : MonoBehaviour
{

    float speed = 100.0f;                        //get value from stats
    public Transform target;
    private int wavepointindex = 0;              //node index
    void start()
    {
        target = WayPoints.wayPoints[0];         //waypoint initialize
    }

    void update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 10.0f)      //0.2f->get value from stat attack range
            {
                //if (target.comparetag("building"))
                //{
                //    //attack if target is inhibitator or nexus.
                //}
                //else
                {
                    getnextwaypoint();
                }

            }

        }
        else
        {
            Debug.Log("minion target is null");
            //combat when get destination
        }
    }

    void getnextwaypoint()
    {
        wavepointindex++;
        target = WayPoints.wayPoints[wavepointindex];
    }
}




//NavMeshAgent navAgent;
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
//        if (navAgent.remainingDistance <= 0.1f)
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
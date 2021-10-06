using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Minion : MonoBehaviour
{
    NavMeshAgent navAgent;
    public Transform target;
    private int wavePointIndex = 0;         //Node Index

    public GameObject DamagedEffect;

    //void Start()
    //{
    //    navAgent = GetComponentInChildren<NavMeshAgent>();
    //    target = WayPoints.wayPoints[0];
    //}

    //void Update()
    //{
    //    if (target != null)
    //    {
    //        //navAgent.SetDestination(target.position);
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
}

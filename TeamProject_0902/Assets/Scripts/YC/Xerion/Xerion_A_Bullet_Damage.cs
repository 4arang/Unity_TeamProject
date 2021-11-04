using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_A_Bullet_Damage : MonoBehaviour
{
    private float Xerion_AD;
    private byte skillLevel = 1;
    private bool myTeamColor;

    private void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {   
        //if (other.CompareTag("Minion")&&other.GetComponent<Minion_Stats>().TeamColor!=
        //{ 추가예정

        //    Destroy(gameObject);
        //}
    }
}

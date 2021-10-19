using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_A_Bullet_Damage : MonoBehaviour
{
    private float Xerion_AD;

    private void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<Minion_Stats>().DropHP(Xerion_AD);


        }
    }
}

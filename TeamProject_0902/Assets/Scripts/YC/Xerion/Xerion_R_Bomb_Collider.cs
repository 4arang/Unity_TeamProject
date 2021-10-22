using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_R_Bomb_Collider : MonoBehaviour
{
    public float Xerion_R_AD = 100;  //100 150 250 
    private int Xerion_R_Level = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<Minion_Stats>().DropHP(Xerion_R_AD);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_R_DroneBomb_Collider : MonoBehaviour
{
    public float Xerion_R_AD = 150;  //150  225 375
    private int Xerion_R_Level = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<Minion_Stats>().DropHP(Xerion_R_AD,this.transform);
        }
    }
}

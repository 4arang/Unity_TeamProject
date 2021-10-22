using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_W_Skill_Colider : MonoBehaviour
{
    private float Xerion_W_CenterAD = 100; //100 130 160 190 220
    private float Xerion_W_CenterDS = 0.6f; // Drop Speed 60 65 70 75 80
    private float Xerion_W_SideDS = 0.2f; //fixed
    private byte Xerion_W_Level = 1;
    // private float Xerion_W_SideAD; //0.8*centerAD // 20
    public float Xerion_W_CenterR = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            if ((transform.position - other.transform.position).magnitude <= Xerion_W_CenterR)
            {
                other.GetComponent<Minion_Stats>().DropHP(Xerion_W_CenterAD);
                other.GetComponent<Minion_Stats>().DropSpeed(1-Xerion_W_CenterDS, 2.5f);
            }
            else
            { 
                other.GetComponent<Minion_Stats>().DropHP(Xerion_W_CenterAD * 0.8f);
                other.GetComponent<Minion_Stats>().DropSpeed(1 - Xerion_W_SideDS, 2.5f);
            }
        }
      
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_Q_Laser_Collider : MonoBehaviour
{
    private float Xerion_Q_AD=70;   //110 150 190 230
    private float Xerion_Q_ADp=20; //20 30 40 50 70


    public bool Xerion_Q_ColliderOn=false;
    public bool Xerion_Q_Full;

    void Start()
    {
        Xerion_Q_ColliderOn = false;
        Xerion_Q_Full = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Xerion_Q_ColliderOn)
        {
            if (other.CompareTag("Minion"))
            {
                if (GetComponentInParent<Xerion_Shooting_Skill>().Q_LaserFull)
                {
                    other.GetComponent<Minion_Stats>().DropHP(Xerion_Q_AD+Xerion_Q_ADp,this.transform);
                }
                else
                {
                    other.GetComponent<Minion_Stats>().DropHP(Xerion_Q_AD,this.transform);
                }
            }
        }
    }

}

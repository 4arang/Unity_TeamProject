using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_E_Skill_Collider : MonoBehaviour
{
    public float Xerion_E_AD = 80;  //80 110 140 170 200 
    private int Xerion_E_Level = 1;
    private float StunTime; //0.5~2.0
    private Vector3 StartPos;

    private void Start()
    {
        StartPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            StunTime = (StartPos - other.transform.position).magnitude / 5.0f;
            if (StunTime < 0.5f) StunTime = 0.5f;
            else if (StunTime > 2.0f) StunTime = 2.0f;

            other.GetComponent<Minion_Stats>().DropHP(Xerion_E_AD);
            other.GetComponent<Minion_Stats>().Stun(StunTime);

            Destroy(gameObject);
        }
    }
}

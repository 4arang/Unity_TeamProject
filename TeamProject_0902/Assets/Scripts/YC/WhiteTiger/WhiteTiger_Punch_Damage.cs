using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger_Punch_Damage : MonoBehaviour
{

    public float WT_Punch_AD = 10;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Minion"))
        {
            other.GetComponent<Minion_Stats>().DropHP(WT_Punch_AD,this.transform);
        }
    }
}

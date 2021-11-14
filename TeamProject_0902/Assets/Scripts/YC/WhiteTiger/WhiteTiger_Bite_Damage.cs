using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger_Bite_Damage : MonoBehaviour
{
    public float WT_Bite_AD = 20;


    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<Minion_Stats>().DropHP(WT_Bite_AD, this.transform);
        }
    }
}

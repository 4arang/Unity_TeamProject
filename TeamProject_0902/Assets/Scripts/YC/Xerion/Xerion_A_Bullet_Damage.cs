using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_A_Bullet_Damage : MonoBehaviour
{
    private float Xerion_AD;
    private byte skillLevel = 1;


    private void OnTriggerEnter(Collider other)
    {
        Xerion_AD = Xerion_Manager.Instance.Xerion_AD;
        
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<Minion_Stats>().DropHP(Xerion_AD);
            Destroy(gameObject);
        }
    }
}

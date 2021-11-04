using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion1_Attack_Collider : MonoBehaviour
{
    private float Minion1_AD;
   // [SerializeField] private GameObject TargetEffect;


    private void OnTriggerEnter(Collider other)
    {
        Minion1_AD = GetComponentInParent<Minion_Stats>().AD;

          if (other.CompareTag("Minion") && 
            other.GetComponent<Minion_Stats>().TeamColor != GetComponentInParent<Minion_Stats>().TeamColor)
            {
            Debug.Log("Enemy Hit" + Minion1_AD);
            other.GetComponent<Minion_Stats>().DropHP(Minion1_AD);
              }
            if( other.CompareTag("Player")&& 
                other.GetComponent<Player_Stats>().TeamColor != GetComponentInParent<Minion_Stats>().TeamColor)
            {
                Debug.Log("Enemy Hit" + Minion1_AD);
            other.GetComponent<Player_Stats>().DropHP(Minion1_AD);

            }
     }
    
}

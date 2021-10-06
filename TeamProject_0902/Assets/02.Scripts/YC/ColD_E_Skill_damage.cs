using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_E_Skill_damage : MonoBehaviour
{ 
    public float ColD_Grenade_AD = 90;
    public float ColD_grenade_MD = 0.2f;
    private int skillLevel = 1;
    private bool IsDanger=false;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            IsDanger = other.GetComponent<ColD_Stats>().isDanger;
            if (IsDanger)
            {
                ColD_Grenade_AD *= 1.5f; //위험상태 50% 증가
                ColD_grenade_MD *= ColD_grenade_MD * 1.5f;
                Debug.Log("cold ad" + ColD_Grenade_AD);
            }
            else
            {
                ColD_Grenade_AD = 90; //복구용
                ColD_grenade_MD = 0.2f;
            }
        }
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<Minion1_Stats>().DropHP(ColD_Grenade_AD);
            other.GetComponent<Minion1_Stats>().DropSpeed(1-ColD_grenade_MD, 2.0f);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WT_Punch_Collider_L : MonoBehaviour
{
    public bool onSkill = false;
    private float WT_BasicAD;
    [SerializeField] private GameObject TargetEffect;

    public void Skill()
    {
        onSkill = true;
        WT_BasicAD = GetComponentInParent<Player_Stats>().AD;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onSkill)
        {
            if (other.CompareTag("Minion"))
            {
                Debug.Log("Enemy Hit" + WT_BasicAD);
                Instantiate(TargetEffect, other.transform.position, Quaternion.identity);
                other.GetComponent<Minion_Stats>().DropHP(WT_BasicAD,this.transform);
                onSkill = false;    //한번에 한명만 공격하게
            }
        }
    }
}

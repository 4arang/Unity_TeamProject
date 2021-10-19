using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WT_Rskill_Collider : MonoBehaviour
{
    public bool onSkill = false;

    public void Skill()
    {
        onSkill = true;
        GetComponentInParent<WhiteTiger_Skill>().R_Targeted = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (onSkill)
        {
            if (other.CompareTag("Minion"))
            {
                GetComponentInParent<WhiteTiger_Skill>().R_Targeted = true;
                GetComponentInParent<WhiteTiger_Skill>().Target_pos = other.transform.position;
                onSkill = false;    //한번에 한명만 공격하게
            }
        }
    }
}

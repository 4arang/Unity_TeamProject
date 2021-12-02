using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WT_Rskill_Collider : MonoBehaviour
{
    public bool onSkill = false;
    private bool teamcolor;
    private void Start()
    {
        teamcolor = GetComponentInParent<Player_Stats>().TeamColor;
    }
    public void Skill()
    {
        onSkill = true;
        GetComponentInParent<WhiteTiger_Skill>().R_Targeted = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (onSkill)
        {
            if (other.CompareTag("Player")&&other.GetComponent<Player_Stats>().TeamColor!=teamcolor)
            {
                GetComponentInParent<WhiteTiger_Skill>().R_Targeted = true;
                GetComponentInParent<WhiteTiger_Skill>().Target = other.transform;
                onSkill = false;    //한번에 한명만 공격하게
            }
        }
    }
}

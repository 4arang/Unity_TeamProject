using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WT_Punch_Collider_R : MonoBehaviour
{
    public bool onSkill = false;
    private float WT_BasicAD;
    [SerializeField] private GameObject TargetEffect;

    public void Skill()
    {
        onSkill = true;
        WT_BasicAD = GetComponentInParent<WhiteTiger_Stats>().AD;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onSkill)
        {
            if (other.CompareTag("Minion"))
            {
                Debug.Log("Enemy Hit" + WT_BasicAD);
                Instantiate(TargetEffect, other.transform.position, Quaternion.identity);
                other.GetComponent<Minion_Stats>().DropHP(WT_BasicAD);
                onSkill = false;    //�ѹ��� �Ѹ� �����ϰ�
            }
        }
    }
}

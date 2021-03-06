using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_Punch_Collider : MonoBehaviour
{
    public bool onSkill = false;
    private float ColD_BasicAD;
    [SerializeField] private GameObject TargetEffect;

    public void Skill()
    {
        onSkill = true;
        ColD_BasicAD = GetComponentInParent<Player_Stats>().AD;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onSkill)
        {
            if (other.CompareTag("Minion"))
            {
                Debug.Log("Enemy Hit" + ColD_BasicAD);
                Instantiate(TargetEffect, other.transform.position, Quaternion.identity);
                other.GetComponent<Minion_Stats>().DropHP(ColD_BasicAD, this.transform);
                onSkill = false;    //한번에 한명만 공격하게
            }
        }
    }
}

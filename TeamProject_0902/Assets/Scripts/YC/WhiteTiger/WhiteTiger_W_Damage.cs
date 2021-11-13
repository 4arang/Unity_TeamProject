using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger_W_Damage : MonoBehaviour
{
    public float WT_W_AD = 50;
    public float WT_W_Recover = 0.5f;
    private float MaxHP;
    private float HP;

    private void Start()
    {       //hp 회복 ; max보다 작을경우 실행 -> max넘어가는경우 max로
        MaxHP = GetComponentInParent<Player_Stats>().HP;
        HP = GetComponentInParent<Player_Stats>().hp;
        if (HP<MaxHP)
        {
            HP += GetComponentInParent<Player_Stats>().DamageStorage * WT_W_Recover;
            if (HP >= MaxHP) HP = MaxHP; 
            GetComponentInParent<Player_Stats>().HP = HP;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<Minion_Stats>().DropHP(WT_W_AD,this.transform);
        }
    }
}

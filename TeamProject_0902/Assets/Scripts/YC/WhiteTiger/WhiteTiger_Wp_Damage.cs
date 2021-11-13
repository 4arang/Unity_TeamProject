using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger_Wp_Damage : MonoBehaviour
{
    public float WT_WP_AD = 70;
    public float WT_WP_Recover = 0.8f;
    private float MaxHP;
    private float HP;

    private void Start()
    {       //hp 회복 ; max보다 작을경우 실행 -> max넘어가는경우 max로
        MaxHP = GetComponentInParent<Player_Stats>().HP;
        HP = GetComponentInParent<Player_Stats>().hp;
        if (HP < MaxHP)
        {
            HP += GetComponentInParent<Player_Stats>().DamageStorage * WT_WP_Recover;
            if (HP >= MaxHP) HP = MaxHP;
            GetComponentInParent<Player_Stats>().HP = HP;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<Minion_Stats>().DropHP(WT_WP_AD,this.transform);
        }
    }
}

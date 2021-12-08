using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger_Wp_Damage : MonoBehaviour
{
    public float WT_WP_AD = 70;
    private bool TeamColor;
    private Transform player;

    private void Start()
    {
        player = GetComponentInParent<Player_Stats>().gameObject.transform;
        TeamColor = GetComponentInParent<Player_Stats>().TeamColor;
    }
    private void OnTriggerEnter(Collider other)
    {
       // other.GetComponent<Minion_Stats>().DropHP(WT_WP_AD, this.transform);

        if ((other.CompareTag("Minion") && other.GetComponent<Minion_Stats>().TeamColor != TeamColor)
          || (other.CompareTag("Player") && other.GetComponent<Player_Stats>().TeamColor != TeamColor)
          || other.CompareTag("Monster")
          || (other.CompareTag("Turret") && other.GetComponent<Turret_Stats>().TeamColor != TeamColor))
        {
            damageEnemy(WT_WP_AD, other.transform);
        }


    }


    private void damageEnemy(float AD, Transform target)
    {

        if (target.CompareTag("Minion"))
        {
            target.GetComponent<Minion_Stats>().DropHP(AD, player);
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<Player_Stats>().DropHP(AD, player);
        }
        else if (target.CompareTag("Turret"))
        {
            target.GetComponent<Turret_Stats>().DropHP(AD);
        }
        else if (target.CompareTag("Monster"))
        {
            if (target.GetComponent<Monster_Stats>().hp > 0)
            {
                target.GetComponent<Monster_Stats>().DropHP(AD, player);
            }

        }
    }
}

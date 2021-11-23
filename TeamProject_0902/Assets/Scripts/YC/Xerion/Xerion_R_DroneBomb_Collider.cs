using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_R_DroneBomb_Collider : MonoBehaviour
{
    public float Xerion_R_AD = 150;  //150  225 375
    private int Xerion_R_Level = 1;

    private bool TeamColor;
    private Transform player;

    void Start()
    {
        player = PlayerStatManager.Instance.Player;
        TeamColor = player.GetComponent<Player_Stats>().TeamColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Minion") && other.GetComponent<Minion_Stats>().TeamColor != TeamColor)
                  || (other.CompareTag("Player") && other.GetComponent<Player_Stats>().TeamColor != TeamColor)
                  || other.CompareTag("Monster")
                  || (other.CompareTag("Turret") && other.GetComponent<Turret_Stats>().TeamColor != TeamColor))
        {
            damageEnemy(Xerion_R_AD, other.transform);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_E_Skill_Collider : MonoBehaviour
{
    public float Xerion_E_AD = 80;  //80 110 140 170 200 
    private int Xerion_E_Level = 1;
    private float StunTime; //0.5~2.0
    private Vector3 StartPos;
    private bool TeamColor;
    private Transform player;

    private void Start()
    {
        StartPos = transform.position;
        player = PlayerStatManager.Instance.Player;
        TeamColor = player.GetComponent<Player_Stats>().TeamColor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Minion")&&other.GetComponent<Minion_Stats>().TeamColor!= TeamColor)
            || (other.CompareTag("Player")&&other.GetComponent<Player_Stats>().TeamColor!= TeamColor)
            ||other.CompareTag("Monster")
            ||(other.CompareTag("Turret")&&other.GetComponent<Turret_Stats>().TeamColor!= TeamColor))
        {
            
                StunTime = (StartPos - other.transform.position).magnitude / 5.0f;
                if (StunTime < 0.5f) StunTime = 0.5f;
                else if (StunTime > 2.0f) StunTime = 2.0f;
            if (other.CompareTag("Minion"))
                other.GetComponent<Minion_Stats>().Stun(StunTime);
            else if (other.CompareTag("Player"))
                other.GetComponent<Player_Stats>().Stun(StunTime);

            damageEnemy(Xerion_E_AD, other.transform);


            Destroy(gameObject);
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

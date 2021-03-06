using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_Q_Laser_Collider : MonoBehaviour
{
    private float Xerion_Q_AD=70;   //110 150 190 230
    private float Xerion_Q_ADp=20; //20 30 40 50 70
    private int Q_Level = 1;

    public bool Xerion_Q_ColliderOn=false;
    public bool Xerion_Q_Full;

    private bool TeamColor;
    private Transform player;

    public void setup(int Q_Level)
    {
        if(Q_Level==2)
        {
             Xerion_Q_AD = 110;  
             Xerion_Q_ADp = 30; 
        }
        else if (Q_Level==3)
        {
            Xerion_Q_AD = 150;
            Xerion_Q_ADp = 40;
        }
        else if (Q_Level == 4)
        {
            Xerion_Q_AD = 190;
            Xerion_Q_ADp = 50;
        }
        else if (Q_Level == 5)
        {
            Xerion_Q_AD = 230;
            Xerion_Q_ADp = 70;
        }



        Xerion_Q_ColliderOn = true;
        Xerion_Q_Full = false;

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
            if (Xerion_Q_ColliderOn)
            {
                if (GetComponentInParent<Xerion_Shooting_Skill>().Q_LaserFull)
                  {
                    damageEnemy(Xerion_Q_ADp, other.transform);
                  }
                else
                  {
                    damageEnemy(Xerion_Q_AD, other.transform);
                }
            }
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
            Debug.Log("player AD " + AD);
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_W_Skill_Colider : MonoBehaviour
{
    private float Xerion_W_CenterAD = 100; //100 130 160 190 220
    private float Xerion_W_CenterDS = 0.6f; // Drop Speed 60 65 70 75 80
    private float Xerion_W_SideDS = 0.2f; //fixed
    private byte Xerion_W_Level = 1;
    // private float Xerion_W_SideAD; //0.8*centerAD // 20
    public float Xerion_W_CenterR = 1.0f;

    private bool TeamColor;
    private Transform player;


    public void setup(Transform Player, int Wlevel)
    {
        player = Player;
        TeamColor = player.GetComponent<Player_Stats>().TeamColor;
        if(Wlevel ==2)
        {
            Xerion_W_CenterAD = 130;
            Xerion_W_CenterDS = 0.65f;
        }
        else if (Wlevel == 3)
        {
            Xerion_W_CenterAD = 160;
            Xerion_W_CenterDS = 0.70f;
        }
        else if (Wlevel == 4)
        {
            Xerion_W_CenterAD = 190;
            Xerion_W_CenterDS = 0.75f;
        }
        else if (Wlevel ==5)
        {
            Xerion_W_CenterAD = 220;
            Xerion_W_CenterDS = 0.80f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Minion") && other.GetComponent<Minion_Stats>().TeamColor != TeamColor)
            || (other.CompareTag("Player") && other.GetComponent<Player_Stats>().TeamColor != TeamColor)
            || other.CompareTag("Monster")
            || (other.CompareTag("Turret") && other.GetComponent<Turret_Stats>().TeamColor != TeamColor))
        {
        if ((transform.position - other.transform.position).magnitude <= Xerion_W_CenterR)
        {
                if(other.CompareTag("Minion"))
            other.GetComponent<Minion_Stats>().DropSpeed(1 - Xerion_W_CenterDS, 2.5f);
                else if (other.CompareTag("Player"))
                    other.GetComponent<Player_Stats>().DropSpeed(1 - Xerion_W_CenterDS, 2.5f);

                damageEnemy(Xerion_W_CenterAD, other.transform);
        }
        else
        {
                if (other.CompareTag("Minion"))
                    other.GetComponent<Minion_Stats>().DropSpeed(1 - Xerion_W_SideDS, 2.5f);
                else if (other.CompareTag("Player"))
                    other.GetComponent<Player_Stats>().DropSpeed(1 - Xerion_W_SideDS, 2.5f);

                damageEnemy(Xerion_W_CenterAD * 0.8f, other.transform);
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

        //Debug.Log("wskill " + target);

    }
}

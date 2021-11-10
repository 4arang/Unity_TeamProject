using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger_Basic_Range_Collider : MonoBehaviour
{
    private bool CheckEnemy = false;
    private bool myTeamColor;

    public void isAttackReady()
    {

        CheckEnemy = true;
        myTeamColor = GetComponentInParent<Player_Stats>().TeamColor;
    }

    private void OnTriggerStay(Collider other)
    {
        if (CheckEnemy)
        {
            if ((other.CompareTag("Minion") && other.GetComponent<Minion_Stats>().TeamColor != myTeamColor)
                || ((other.CompareTag("Player")) && other.GetComponent<Player_Stats>().TeamColor != myTeamColor)
                 || ((other.CompareTag("Turret")) && other.GetComponent<Turret_Stats>().TeamColor != myTeamColor))
            {
                GetComponentInParent<WhiteTiger>().TargetEnemy = other.transform;
                CheckEnemy = false;
            }
        }
    }
}


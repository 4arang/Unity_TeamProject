using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_Basic_Range_collider : MonoBehaviour
{
    private bool CheckEnemy = false;

    private Collider Enemy;

    public void isAttackReady()
    {

        CheckEnemy = true;
    }

    private void OnTriggerStay(Collider other)
    {

        if (CheckEnemy)
        {
            if (other.CompareTag("Minion") || other.CompareTag("Enemy"))
            {
                Debug.Log("Checking Enemy");
                GetComponentInParent<ColD>().TargetEnemy = other;
                CheckEnemy = false;
            }
        }
    }
}

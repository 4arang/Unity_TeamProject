using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Baisc_Attack : MonoBehaviour
{

    private bool Trigger = true;
    public bool AttackOn = false;
    private Collider Enemy;


    private void OnTriggerEnter(Collider other)
    {
        if (Trigger)// 가장 먼저번 충돌 1회만
        {
            if (GetComponentInParent<WhiteTiger>().isBasicAttack)
            {
                if (other.CompareTag("Minion"))  //범위 내에 적 발견시 이동
                {
                    Enemy = other;
                    movingManager.Instance.PlayerClickedPos = other.transform.position;
                    AttackOn = true;
                    Trigger = false;
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(!Trigger)
        {
            Trigger = true;
            AttackOn = false;
        }
    }
    private void Update()
    {
        if (AttackOn)
        {
            AttackThis(Enemy);
        }
    }

    void AttackThis(Collider Enemy)
    {

            if ((transform.position - Enemy.transform.position).magnitude < 1.0f)
            {
                GetComponentInParent<WhiteTiger_Skill>().isBasicAttack = true;
                movingManager.Instance.PlayerClickedPos = transform.position;
                Debug.Log("Stop");

        }

    }
}

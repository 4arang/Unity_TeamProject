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
        if (Trigger)// ���� ������ �浹 1ȸ��
        {
            if (GetComponentInParent<WhiteTiger>().isBasicAttack)
            {
                if (other.CompareTag("Minion"))  //���� ���� �� �߽߰� �̵�
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

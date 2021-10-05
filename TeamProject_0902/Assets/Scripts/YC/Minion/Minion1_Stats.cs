using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion1_Stats : MonoBehaviour
{
    public float HP;   //Health Point
    public float AD; //Attack Damage
    public float AP;    //Armor Point
    public float MoveSpeed;


    void Start()
    {
        HP = Minion1StatManager.Instance.HP;
        AD = Minion1StatManager.Instance.AD;
        AP = Minion1StatManager.Instance.AP;
        MoveSpeed = Minion1StatManager.Instance.MoveSpeed;
    }

    void Update()
    {
        Debug.Log("HP " + HP);
        Debug.Log("Speed " + MoveSpeed);
    }

    public void DropHP(float damage)
    {
        HP -= damage;
    }
    public void DropSpeed(float damage, float time)
    {
        MoveSpeed *= damage;
        StartCoroutine("Active_SpeedReturn", time);
    }
    IEnumerator Active_SpeedReturn(float time)
    {
        yield return new WaitForSeconds(time); //1초후에 스피드 복구
        MoveSpeed = Minion1StatManager.Instance.MoveSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion2_Stats : MonoBehaviour
{
    public float HP;   //Health Point
    public float AD; //Attack Damage
    public float AP;    //Armor Point
    public float MoveSpeed;

    [SerializeField] private GameObject DamagedEffect;

    void Start()
    {
        HP = Minion2StatManager.Instance.HP;
        AD = Minion2StatManager.Instance.AD;
        AP = Minion2StatManager.Instance.AP;
        MoveSpeed = Minion2StatManager.Instance.MoveSpeed;
        DamagedEffect.SetActive(false);
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
        MoveSpeed = Minion2StatManager.Instance.MoveSpeed;
    }
}

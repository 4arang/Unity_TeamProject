using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats_Text : MonoBehaviour
{
    [SerializeField] private Text AD; //공격력
    [SerializeField] private Text MP; //주문력
    [SerializeField] private Text Armor; //물리방어력
    [SerializeField] private Text MRP; //마법방어력
    [SerializeField] private Text CoolTime; //쿨타임
    [SerializeField] private Text MoveSpeed; //이동속도
    [SerializeField] private Text CriticalRate; //치명타확률
    [SerializeField] private Text AttackSpeed; //공격속도

    //void Start()
    //{
    //    AD = GetComponent<Text>();
    //    MP = GetComponent<Text>();
    //    Armor = GetComponent<Text>();
    //    MRP = GetComponent<Text>();
    //    CoolTime = GetComponent<Text>();
    //    MoveSpeed = GetComponent<Text>();
    //    CriticalRate = GetComponent<Text>();
    //    AttackSpeed = GetComponent<Text>();
    //}

    public void SetAD(float ad)
    {
        AD.text = ad.ToString();
    }
    public void SetMP(float mp)
    {
        MP.text = mp.ToString();
    }
    public void SetArmor(float armor)
    {
        Armor.text = armor.ToString();
    }
    public void SetMRP(float mrp)
    {
        MRP.text = mrp.ToString();
    }
    public void SetCoolTime(float cooltime)
    {
        CoolTime.text = cooltime.ToString();
    }
    public void SetMoveSpeed(float movespeed)
    {
        MoveSpeed.text = movespeed.ToString();
    }
    public void SetCriticalRate(float criticalrate)
    {
        CriticalRate.text = criticalrate.ToString();
    }
    public void SetAttackSpeed(float attackspeed)
    {
        AttackSpeed.text = attackspeed.ToString();
    }
}

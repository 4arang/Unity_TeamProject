using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats_Text : MonoBehaviour
{
    [SerializeField] private Text AD; //���ݷ�
    [SerializeField] private Text MP; //�ֹ���
    [SerializeField] private Text Armor; //��������
    [SerializeField] private Text MRP; //��������
    [SerializeField] private Text CoolTime; //��Ÿ��
    [SerializeField] private Text MoveSpeed; //�̵��ӵ�
    [SerializeField] private Text CriticalRate; //ġ��ŸȮ��
    [SerializeField] private Text AttackSpeed; //���ݼӵ�

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

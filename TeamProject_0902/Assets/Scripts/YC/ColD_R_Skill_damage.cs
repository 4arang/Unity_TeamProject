using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_R_Skill_damage : MonoBehaviour
{
    public float ColD_Missile_AD = 70;
    private float cooldown = 0.5f;
    private int skillLevel = 1;

    private Dictionary<Collider, float> _table = new Dictionary<Collider, float>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Minion") && !_table.ContainsKey(other)) //���ο� other �߰��ϴ°��
        {
            _table[other] = float.NegativeInfinity; //�����Ҵ�
        }
        if (other.CompareTag("Enemy") && !_table.ContainsKey(other)) //���ο� other �߰��ϴ°��
        {
            _table[other] = float.NegativeInfinity; //�����Ҵ�
        }
    }


    private void OnTriggerStay(Collider other)
    {

        float timer;

        if (!_table.TryGetValue(other, out timer)) return; //if not in table, it's not an enemy

        if (Time.time > timer)
        {
            if (timer < 0) other.GetComponent<Minion_Stats>().DropSpeed(0.65f, 1.0f); //���� 1ȸ
             _table[other] = Time.time + cooldown;
            other.GetComponent<Minion_Stats>().DropHP(ColD_Missile_AD); //damage the enemy
            if (timer > 0) _table[other] = float.PositiveInfinity;//2ȸ�̻��ΰ��
        }


    }
}

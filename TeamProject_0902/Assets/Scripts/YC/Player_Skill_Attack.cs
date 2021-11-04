using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_Attack : MonoBehaviour
{
    /// <summary>
    /// ��� 20 �Ҹ�
    /// ��Ÿ� 600
    /// 0.5�ʸ��� 30AD
    /// 2���̻� ������� 15%�̵��ӵ� ����
    /// ������� ->���ݷ� 50%����?
    /// </summary>

    public float ColD_Flame_AD;
    private bool isReady;
    public float cooldown = 0.5f;
    private int  skillLevel = 1;
    public float DamagingTime = 2.0f;
    byte timer_ = 0; //to check 2.0f

    private Dictionary<Collider, float> _table = new Dictionary<Collider, float>();

    void Start()
    {
        isReady = false;
        ColD_Flame_AD = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<Player_Stats>().isDanger)  ColD_Flame_AD *= 1.5f; //������� 50% ����
        else ColD_Flame_AD = 30; //������
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

      if(Time.time>timer)
        { 
            _table[other] = Time.time + cooldown;
            other.GetComponent<Minion_Stats>().DropHP(ColD_Flame_AD); //damage the enemy
            timer_++;
            if(timer_>=4)
            {
                if (other.CompareTag("Enemy")) //��è�Ǿ��� ��츸
                {
                    other.GetComponent<Minion_Stats>().DropSpeed(0.85f, 1.0f); //è�Ǿ����� ����
                    timer_ = 0;
                }
            }
        }
     
    
    }

}

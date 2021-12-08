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
    private float plusDamage; //for minion 
    private bool isReady;
    public float cooldown = 0.5f;
    private int  skillLevel = 1;
    public float DamagingTime = 2.0f;
    byte timer_ = 0; //to check 2.0f
    Transform player;
    private bool TeamColor;

    private Dictionary<Collider, float> _table = new Dictionary<Collider, float>();

    void Start()
    {
        player = GetComponentInParent<Player_Stats>().gameObject.transform;
        TeamColor = player.GetComponent<Player_Stats>().TeamColor;
        isReady = false;
        ColD_Flame_AD = 30;
        plusDamage = 1.6f;
    }

    public void setup(int skillLevel)
    {
        switch (skillLevel)
        {
            case 1:
                {
                    ColD_Flame_AD = 30;
                    plusDamage = 1.6f;
                    break;
                }
            case 2:
                {
                    ColD_Flame_AD = 36.65f;
                    plusDamage = 1.65f;
                    break;
                }
            case 3:
                {
                    ColD_Flame_AD = 43.3f;
                    plusDamage = 1.7f;
                    break;
                }
            case 4:
                {
                    ColD_Flame_AD = 50;
                    plusDamage = 1.75f;
                    break;
                }
            case 5:
                {
                    ColD_Flame_AD = 56.65f;
                    plusDamage = 1.8f;
                    break;
                }
        }
        if (GetComponentInParent<Player_Stats>().isDanger) ColD_Flame_AD *= 1.5f; //������� 50% ����
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Minion") && other.GetComponent<Minion_Stats>().TeamColor != TeamColor
            &&!_table.ContainsKey(other)) //���ο� other �߰��ϴ°��
        {
            _table[other] = float.NegativeInfinity; //�����Ҵ�
        }
        if (other.CompareTag("Player") && other.GetComponent<Player_Stats>().TeamColor != TeamColor
            &&!_table.ContainsKey(other)) //���ο� other �߰��ϴ°��
        {
            _table[other] = float.NegativeInfinity; //�����Ҵ�
        }
        if(other.CompareTag("Turret") && other.GetComponent<Turret_Stats>().TeamColor != TeamColor
            && !_table.ContainsKey(other))
        {
            _table[other] = float.NegativeInfinity; //�����Ҵ�
        }
        if(other.CompareTag("Monster") && !_table.ContainsKey(other))
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
            //other.GetComponent<Minion_Stats>().DropHP(ColD_Flame_AD,this.transform); //damage the enemy
            damageEnemy(ColD_Flame_AD, other.transform);

            timer_++;
            if(timer_>=4)
            {
                if (other.CompareTag("Player")) //��è�Ǿ��� ��츸
                {
                    other.GetComponent<Player_Stats>().DropSpeed(0.85f, 1.0f); 
                    timer_ = 0;
                }
            }
        }   
    }

    private void damageEnemy(float AD, Transform target)
    {

        if (target.CompareTag("Minion"))
        {
            target.GetComponent<Minion_Stats>().DropHP(AD, player);
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<Player_Stats>().DropHP(AD, player);
        }
        else if (target.CompareTag("Turret"))
        {
            target.GetComponent<Turret_Stats>().DropHP(AD);
        }
        else if (target.CompareTag("Monster"))
        {
            if (target.GetComponent<Monster_Stats>().hp > 0)
            {
                target.GetComponent<Monster_Stats>().DropHP(AD, player);
            }

        }
    }

}

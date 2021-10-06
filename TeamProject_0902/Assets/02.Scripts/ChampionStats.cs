using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class ChampionStats : MonoBehaviour
{
    public int TeamID;
    [Header("Health")]
    public float maxHealth;
    public float health;
    public float healthRegen;

    [Header("Offense")]
    public float attackDmg;
    public float attackSpeed;
    public float lifeSteal;         //����� ���
    public float criticalStrike;    //ġ��Ÿ��
    public float lethality;         //���������

    [Header("Defense")]
    public float armor;
    public float magicResist;
    public float disablingEffect;   //������

    [Header("Ability")]
    public float abilityPower;
    public float abilityHaste;
    public float magicPenetration;  //���������

    [Header("Others")]
    public float resource;           //�ڿ�: ��ų�� ����ϴµ� �ʿ��� �ڿ�
    public float resourceRegen;
    public float moveSpeed;
    public float attackRange;

    [Header("Others")]
    public int gold;
    public int level;
    public float expValue;


    public int Kills;
    public int Deaths;
    public int Assists;
    public int MinionScore;

    //Player_Combat heroCombatScript;
    NavMeshAgent agent;

    private GameObject player;

    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        //moveSpeed = agent.speed;


        //heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Combat>();
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (health <= 0)
        {
            //If Object Destroy -> Stop melee attack and targeted initialize.
            Destroy(gameObject);
            //heroCombatScript.targetedEnemy = null;
            //heroCombatScript.performMeleeAttack = false;

            //Give Exp
            //player.GetComponent<LevelUpStats>().SetExperience(expValue);
        }
    }

    public float Percent(bool type)
    {
        if(type)
        {
            //return curr - Mathf.FloorToInt(curr);
            return 0;
        }
        else
        {
            //return curr / maxHealth;
            return 0;
        }
    }
}

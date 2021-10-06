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
    public float lifeSteal;         //생명력 흡수
    public float criticalStrike;    //치명타율
    public float lethality;         //물리관통력

    [Header("Defense")]
    public float armor;
    public float magicResist;
    public float disablingEffect;   //강인함

    [Header("Ability")]
    public float abilityPower;
    public float abilityHaste;
    public float magicPenetration;  //마법관통력

    [Header("Others")]
    public float resource;           //자원: 스킬을 사용하는데 필요한 자원
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Stats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float health;

    [Header("Attack")]
    public float attackDmg;
    public float attackSpeed;
    public float attackTime;

    [Header("Ability")]
    public float moveSpeed;
    public float sight;         
    public float expValue;

    Player_Combat heroCombatScript;
    NavMeshAgent agent;

    private GameObject player;

    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();

        moveSpeed = agent.speed;
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Combat>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (health <= 0)
        {
            //If Object Destroy -> Stop melee attack and targeted initialize.
            Destroy(gameObject);
            heroCombatScript.targetedEnemy = null;
            heroCombatScript.performMeleeAttack = false;           

            //Give Exp
            player.GetComponent<LevelUpStats>().SetExperience(expValue);
        }
    }
}

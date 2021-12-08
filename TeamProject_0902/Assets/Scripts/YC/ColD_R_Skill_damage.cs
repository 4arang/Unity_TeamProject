using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_R_Skill_damage : MonoBehaviour
{
    public float ColD_Missile_AD = 70;
    private float cooldown = 0.5f;
    private int skillLevel = 1;
    Transform player;
    private bool TeamColor;

    private Dictionary<Collider, float> _table = new Dictionary<Collider, float>();

    public void setup(int Level, Transform Player)
    {
        if (Level == 1) ColD_Missile_AD = 70;
        else if (Level == 2) ColD_Missile_AD = 105;
        else ColD_Missile_AD = 140;

        player = Player;
        TeamColor = player.GetComponent<Player_Stats>().TeamColor;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion") && other.GetComponent<Minion_Stats>().TeamColor != TeamColor
                  && !_table.ContainsKey(other)) //새로운 other 추가하는경우
        {
            _table[other] = float.NegativeInfinity; //음수할당
        }
        if (other.CompareTag("Player") && other.GetComponent<Player_Stats>().TeamColor != TeamColor
            && !_table.ContainsKey(other)) //새로운 other 추가하는경우
        {
            _table[other] = float.NegativeInfinity; //음수할당
        }
        if (other.CompareTag("Turret") && other.GetComponent<Turret_Stats>().TeamColor != TeamColor
            && !_table.ContainsKey(other))
        {
            _table[other] = float.NegativeInfinity; //음수할당
        }
        if (other.CompareTag("Monster") && !_table.ContainsKey(other))
        {
            _table[other] = float.NegativeInfinity; //음수할당
        }
    }


    private void OnTriggerStay(Collider other)
    {

        float timer;

        if (!_table.TryGetValue(other, out timer)) return; //if not in table, it's not an enemy

        if (Time.time > timer)
        {
            if (timer < 0)
            {
                if (other.CompareTag("Minion"))
                    other.GetComponent<Minion_Stats>().DropSpeed(0.65f, 1.0f); //최초 1회
                else if (other.CompareTag("Player"))
                    other.GetComponent<Player_Stats>().DropSpeed(0.65f, 1.0f);
            }
             _table[other] = Time.time + cooldown;
            // other.GetComponent<Minion_Stats>().DropHP(ColD_Missile_AD, this.transform); 
            damageEnemy(ColD_Missile_AD, other.transform);//damage the enemy
          //  if (timer > 0) _table[other] = float.PositiveInfinity;//2회이상인경우
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

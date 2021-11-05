using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_Attack : MonoBehaviour
{
    /// <summary>
    /// 헬륨 20 소모
    /// 사거리 600
    /// 0.5초마다 30AD
    /// 2초이상 맞을경우 15%이동속도 저하
    /// 위험상태 ->공격력 50%증가?
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
        if (GetComponentInParent<Player_Stats>().isDanger)  ColD_Flame_AD *= 1.5f; //위험상태 50% 증가
        else ColD_Flame_AD = 30; //복구용
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Minion") && !_table.ContainsKey(other)) //새로운 other 추가하는경우
        {
            _table[other] = float.NegativeInfinity; //음수할당
        }
        if (other.CompareTag("Enemy") && !_table.ContainsKey(other)) //새로운 other 추가하는경우
        {
            _table[other] = float.NegativeInfinity; //음수할당
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
                if (other.CompareTag("Enemy")) //적챔피언인 경우만
                {
                    other.GetComponent<Minion_Stats>().DropSpeed(0.85f, 1.0f); //챔피언으로 수정
                    timer_ = 0;
                }
            }
        }
     
    
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    public Transform target;

    [Header("To Stats")]
    public float range;
    public float rotateSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    private float AttackSpeed;
    public bool TeamColor;

    [Header("Unity Setup Fields")]
    public Transform rotatePart;

    public Transform firePoint;
    public Transform bulletPrefab;
    public GameObject shootEffPrefab;

 

    private void Start()
    {
        range = GetComponent<Turret_Stats>().AttackRange * 0.015f;
        AttackSpeed = GetComponent<Turret_Stats>().AttackSpeed;
        TeamColor = GetComponent<Turret_Stats>().TeamColor;

        InvokeRepeating("UpdateTarget", 0.5f, 0.1f);

    }

    void UpdateTarget()
    {

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in colliderArray)
        {
            //1	아군 챔피언을 공격한 적 챔피언
            //2 챔피언이 소환한 오브젝트<<없음
            if (col.TryGetComponent<Player_Stats>(out Player_Stats player) &&
    player.GetComponent<Player_Stats>().TeamColor != TeamColor && player.isAttack_Player)
            {
                GetComponent<Turret_Stats>().isAttack_Minion = false;
                target = player.transform;
                if (player.GetComponent<Player_Stats>().isDead == true) target = null;
            }
            //3 슈퍼 미니언 > 공성 미니언 > 근거리 미니언 > 원거리 미니언
            else if (col.TryGetComponent<Minion4>(out Minion4 minion4)
     && minion4.GetComponent<Minion_Stats>().TeamColor != TeamColor)
            {
                GetComponent<Turret_Stats>().isAttack_Minion = true;
                target = minion4.transform;
            }
            else if (col.TryGetComponent<Minion3>(out Minion3 minion3)
&& minion3.GetComponent<Minion_Stats>().TeamColor != TeamColor)
            {
                GetComponent<Turret_Stats>().isAttack_Minion = true;
                target = minion3.transform;
            }
            else if (col.TryGetComponent<Minion2>(out Minion2 minion2)
        && minion2.GetComponent<Minion_Stats>().TeamColor != TeamColor)
            {
                GetComponent<Turret_Stats>().isAttack_Minion = true;
                target = minion2.transform;
            }
            else if (col.TryGetComponent<Minion1>(out Minion1 minion1)
&& minion1.GetComponent<Minion_Stats>().TeamColor != TeamColor)
            {
                GetComponent<Turret_Stats>().isAttack_Minion = true;
                target = minion1.transform;
            }
            //4 아군 챔피언을 공격하지 않은 적 챔피언
            else if (col.TryGetComponent<Player_Stats>(out Player_Stats player_)
                    && player_.GetComponent<Player_Stats>().TeamColor != TeamColor)
            {
                GetComponent<Turret_Stats>().isAttack_Minion = false;
                target = player_.transform;
                if (player_.GetComponent<Player_Stats>().isDead == true) target = null;
            }

            else
            {
                GetComponent<Turret_Stats>().isAttack_Minion = false;
            }

        }

    }


    private void Update()
        {
            if (target==null )
            {
                return;
            }

            //Target lock on
            Vector3 dir = target.position - transform.transform.position;     //Head to target
            Quaternion lookRotation = Quaternion.LookRotation(dir);

            Vector3 rotation = Quaternion.Lerp(rotatePart.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
            rotatePart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCountdown <= 0f)
            {
                if(target) Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;

        //ReTargeting
        if (Vector3.Distance(transform.position, target.transform.position) > range)
            target = null;
    }

        void Shoot()
        {
            GameObject shootEffGo = (GameObject)Instantiate(shootEffPrefab, firePoint.position, firePoint.rotation);

            Transform bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
     
        if (bullet != null)
            {
            bullet.GetComponent<TurretBullet>().Seek(target, GetComponent<Turret_Stats>().AD);
            }
            Destroy(shootEffGo);
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//수정사항 게임시작 5분동안 피해 50% 덜받게
public class Turret : MonoBehaviour
{
    public Transform target;
    
    [Header("To Stats")]
    public float range = 15f;
    public float rotateSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    private float TurretAD;
    
    [Header("Unity Setup Fields")]
    public string enemyTag = "Player";
    public Transform rotatePart;

    //private LineRenderer lr;
    public Transform firePoint;

    public GameObject bulletPrefab;
    public GameObject shootEffPrefab;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        TurretAD = GetComponent<Turret_Stats>().AD;
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if(distanceToEnemy<shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            if (target.CompareTag("Player")&& (target == nearestEnemy.transform))
            {
                if (TurretAD < GetComponent<Turret_Stats>().AD * 2.2f)
                {
                    //타겟이 챔피언이면서 기존 타겟과 같은경우 40%씩 피해량이 증가
                    //최대 120%증가
                    TurretAD += GetComponent<Turret_Stats>().AD * 0.4f;
                }
            }
            else if(target.CompareTag("Minion")&&(target==nearestEnemy.transform))
            {
                //미니언별 각 5% 14% 45% 70%씩 증가
                if (target.TryGetComponent(out Minion1_Stats Minion_Num1))
                {
                    TurretAD += GetComponent<Turret_Stats>().AD * 0.05f;
                }
                else if (target.TryGetComponent(out Minion2_Stats Minion_Num2))
                {
                    TurretAD += GetComponent<Turret_Stats>().AD * 0.14f;
                }
                else if (target.TryGetComponent(out Minion3_Stats Minion_Num3))
                {
                    TurretAD += GetComponent<Turret_Stats>().AD * 0.45f;
                }
                else if (target.TryGetComponent(out Minion4_Stats Minion_Num4))
                {
                    TurretAD += GetComponent<Turret_Stats>().AD * 0.70f;
                }
            }
        else
        {
                TurretAD = GetComponent<Turret_Stats>().AD;
            target = nearestEnemy.transform;
        }

        }
    }
    private void Update()
    {
        if (target == null)
        {
            return;
        }

        //Target lock on
        Vector3 dir = target.position - transform.transform.position;     //Head to target
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(rotatePart.rotation, lookRotation, Time.deltaTime*rotateSpeed).eulerAngles;
        rotatePart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown<=0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject shootEffGo = (GameObject)Instantiate(shootEffPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletGo=(GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        TurretBullet bullet = bulletGo.GetComponent<TurretBullet>();

        if(bullet!=null)
        {
            bullet.Seek(target, TurretAD);
        }
        Destroy(shootEffGo);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

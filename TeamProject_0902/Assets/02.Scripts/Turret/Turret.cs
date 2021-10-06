using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Status")]
    private Transform target;       //터렛 공격대상
    public float range = 15f;       //터렛 사정거리
    public float fireRate = 1f;                 //터렛 공격 속도
    private float fireCountdown = 0f;

    [Header("Setup Fields")]
    public string enemyTag = "Champion";        //공격대상 태그

    public GameObject bulletPrefab;
    public Transform firePoint;                 //미사일 발사 위치 지정
    public float turnSpeed = 10f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()             //범위에 들어온 타겟을 설정
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

        if(nearestEnemy!=null&&shortestDistance<=range)          //적을 발견했을 경우
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
        
    void Update()
    {
        if (target == null)
            return;

        //Vector3 dir = target.position - target.position;        //적 방향으로 조준
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = lookRotation.eulerAngles;
        //partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject turretBulletObject=(GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        TurretBullet turretBullet = turretBulletObject.GetComponent<TurretBullet>();

        if(turretBullet!=null)
        {
            turretBullet.Seek(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

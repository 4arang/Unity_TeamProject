using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    
    [Header("To Stats")]
    public float range = 15f;
    public float rotateSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    
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

        if(nearestEnemy!=null&&shortestDistance<=range)
        {
            target = nearestEnemy.transform;
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
            bullet.Seek(target, GetComponent<Turret_Stats>().AD);
        }
        Destroy(shootEffGo);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

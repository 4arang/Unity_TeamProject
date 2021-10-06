using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Status")]
    private Transform target;       //�ͷ� ���ݴ��
    public float range = 15f;       //�ͷ� �����Ÿ�
    public float fireRate = 1f;                 //�ͷ� ���� �ӵ�
    private float fireCountdown = 0f;

    [Header("Setup Fields")]
    public string enemyTag = "Champion";        //���ݴ�� �±�

    public GameObject bulletPrefab;
    public Transform firePoint;                 //�̻��� �߻� ��ġ ����
    public float turnSpeed = 10f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()             //������ ���� Ÿ���� ����
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

        if(nearestEnemy!=null&&shortestDistance<=range)          //���� �߰����� ���
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

        //Vector3 dir = target.position - target.position;        //�� �������� ����
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

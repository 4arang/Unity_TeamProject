using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private Transform target;
    private float turretAD;
    public float speed = 50f;
    public void Seek(Transform _target, float AD)
    {
        target = _target;
        turretAD = AD;
    }

    void Update()
    {
        if(target==null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude<=distanceThisFrame)
        {
            HitTarget(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget(Transform target)
    {
        //Need to add damaged effect
        //Debug.Log("Hit Something");
        //Destroy(target.gameObject);     //Stats추가해서 제거 부분

        if (target.CompareTag("Minion"))
        {
            target.GetComponent<Minion_Stats>().DropHP(turretAD, this.transform);
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<Player_Stats>().DropHP(turretAD);
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private Transform target;

    public float speed = 50f;
    public void Seek(Transform _target)
    {
        target = _target;
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
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //Need to add damaged effect
        Debug.Log("Hit Something");
        //Destroy(target.gameObject);     //Stats추가해서 제거 부분
        Destroy(gameObject);
    }
}

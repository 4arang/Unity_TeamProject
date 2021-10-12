using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion3_Laser : MonoBehaviour
{
    [HideInInspector]
    public GameObject ImpactFX;
    [HideInInspector]
    public float ImpactFXDestroyDelay = 2f;
    [HideInInspector]
    public float ImpactOffset = 0.15f;
    public float Speed = 20f;
    private Vector3 LaserDir;

    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

    public void Setup(Vector3 ShootDir)
    {
        LaserDir = ShootDir;
    }

    private void FixedUpdate()
    {
        if (Speed == 0) return;
        transform.position += LaserDir * (Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ignore collisions with projectile
        var contact = collision.contacts[0];
        if (contact.otherCollider.name.Contains("Projectile"))
            return;

        Speed = 0;

        var hitPosition = contact.point + contact.normal * ImpactOffset;

        if (ImpactFX != null)
        {
            var impact = Instantiate(ImpactFX, hitPosition, Quaternion.identity);
            impact.transform.localScale = transform.localScale;
            Destroy(impact, ImpactFXDestroyDelay);
        }


        Destroy(gameObject);
    }
}

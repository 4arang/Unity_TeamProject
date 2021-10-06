using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_Drone_Grenade : MonoBehaviour
{
    public ParticleSystem FXToDeatch;
    [HideInInspector]
    public float Speed = 1.5f;
    [HideInInspector]
    public GameObject ImpactFX;
    [HideInInspector]
    public float ImpactFXDestroyDelay = 2f;
    [HideInInspector]
    public float ImpactOffset = 0.15f;
   

    private Vector3 grenadeDir;
    private float time=0.5f;
    private Rigidbody myRigidbody;
    Vector3 target;
    Vector3 origin;
    public float gravity = 10f;
    public float height = 7.5f;
    public float height_near = 2.5f;
    public float distance_grenade = 8.5f;
    public float ref_whenTofall = 0.7f;

    private void Start()
    {

        Destroy(gameObject, 3.0f);
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;
    }
    public void Setup(Vector3 ShootDir, Vector3 Dest)
    {
        origin = ShootDir;
        target = Dest;
        float flyingDistance = (target - origin).magnitude;
        if (flyingDistance < distance_grenade)
        {
            height = height_near;
            time = 0.015f*flyingDistance;
            //Debug.Log("near distance " + time + " " + flyingDistance);
        }
        else
        {
            height = 7.5f;
            time = 0.5f;
        }
    }

    public void SetupVelocity(Vector3 ShootDir, Vector3 Dest)
    {
    
        //Define the distance
        Vector3 distance = Dest - ShootDir;
        Vector3 distanceXZ = distance;
       
        distanceXZ.y = 0;

            //create float distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;


        float Vxz = Sxz / time;
        float Vy0 = Sy / time + 0.5f * gravity * time;
    
       float Vy = Vy0 * time - 0.5f * gravity * time * time;

        // Debug.Log("Sy/time " +  Sy / time);
        grenadeDir = distanceXZ.normalized;
        grenadeDir *= Vxz;

        grenadeDir.y = Vy;

    }


    private void FixedUpdate()
    {
        if (Speed == 0)
            return;

        if ((transform.position - target).magnitude > (origin - target).magnitude*ref_whenTofall)
        {
            target.y = height;
        }
        else
            target.y = 0;
            SetupVelocity(transform.position, target);
        
       
        transform.rotation = Quaternion.LookRotation(grenadeDir);
        myRigidbody.velocity = grenadeDir;
       // Debug.Log("Direction Y : " + grenadeDir.y);
       
    }

    private void SetDirection()
    {

    }

    void OnCollisionEnter(Collision collision)
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

        FXToDeatch.transform.parent = null;
        FXToDeatch.Stop(true);
        Destroy(FXToDeatch.gameObject, ImpactFXDestroyDelay);

        Destroy(gameObject);
    }
}
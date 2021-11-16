using UnityEngine;

public class PFX_ProjectileObject : MonoBehaviour
{
    public ParticleSystem FXToDeatch;
    [HideInInspector]
    public float Speed = 17f;
    [HideInInspector]
    public GameObject ImpactFX;
    [HideInInspector]
    public float ImpactFXDestroyDelay = 2f;
    [HideInInspector]
    public float ImpactOffset = 0.15f;

    public float grenadeRange=0.72f;

    private Vector3 grenadeDir;
    private Transform target;


   public void Setup(Vector3 ShootDir)
    {
       grenadeDir = ShootDir;
    }
    public void Setup(Vector3 ShootDir, Transform target_)
    {
        grenadeDir = ShootDir;
        target = target_;
    }

    private void Start()
    {
       
        Destroy(gameObject, grenadeRange);
    }

    private void FixedUpdate()
    {
        if (Speed == 0)
            return;
        //transform.position += grenadeDir * (Speed * Time.deltaTime);
        else if (Speed != 0 && target)
        {
            //transform.position += shootDir * (speed * Time.deltaTime);
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = Speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

            if (dir.magnitude <= distanceThisFrame)
            {
 
                Speed = 0;
                //GetComponent<Rigidbody>().isKinematic = true;

                //ContactPoint contact = target.contacts[0];
                //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                //Vector3 pos = contact.point;

                if (FXToDeatch != null)
                {
                    var hitVFX = Instantiate(FXToDeatch, transform.position, Quaternion.identity);
                    var ps = hitVFX.GetComponent<ParticleSystem>();
                    if (ps == null)
                    {
                        var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitVFX, psChild.main.duration);
                    }
                    else
                        Destroy(hitVFX, ps.main.duration);
                }

            }
        }



    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    //ignore collisions with projectile
    //    var contact = collision.contacts[0];
    //    if (contact.otherCollider.name.Contains("Projectile"))
    //        return;

    //    Speed = 0;

    //    var hitPosition = contact.point + contact.normal * ImpactOffset;

    //    if (ImpactFX != null)
    //    {
    //        var impact = Instantiate(ImpactFX, hitPosition, Quaternion.identity);
    //        impact.transform.localScale = transform.localScale;
    //        Destroy(impact, ImpactFXDestroyDelay);
    //    }

    //    FXToDeatch.transform.parent = null;
    //    FXToDeatch.Stop(true);
    //    Destroy(FXToDeatch.gameObject, ImpactFXDestroyDelay);

    //    Destroy(gameObject);
    //}
}

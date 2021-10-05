using UnityEngine;

public class PFX_ProjectileObject : MonoBehaviour
{
    public ParticleSystem FXToDeatch;
    [HideInInspector]
    public float Speed = 15f;
    [HideInInspector]
    public GameObject ImpactFX;
    [HideInInspector]
    public float ImpactFXDestroyDelay = 2f;
    [HideInInspector]
    public float ImpactOffset = 0.15f;

    public float grenadeRange=0.72f;

    private Vector3 grenadeDir;

   public void Setup(Vector3 ShootDir)
    {
       grenadeDir = ShootDir;
    }

    private void Start()
    {
       
        Destroy(gameObject, grenadeRange);
    }

    private void FixedUpdate()
    {
        if (Speed == 0)
            return;
        transform.position += grenadeDir * (Speed * Time.deltaTime);
        //transform.localPosition += new Vector3(Direct.x * (Speed * Time.deltaTime),
        //    Direct.y, Direct.z * (Speed * Time.deltaTime));
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

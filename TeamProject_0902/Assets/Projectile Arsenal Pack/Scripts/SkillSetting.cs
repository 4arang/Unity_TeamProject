using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillSetting : MonoBehaviour {

	public float speed;
	[Tooltip("From 0% to 100%")]
	public float accuracy;
    public float lifeSpellTime;
	public float lifeHitTime;
	public float fireRate;
	private float nextFire;
	public GameObject muzzlePrefab;
	public GameObject hitPrefab;
    public AudioClip shotFX;
	public List<GameObject> trails;

    private Transform target;
	private Vector3 offset;
    private Vector3 shootDir;
    private Vector3 targetDir;

    public void Setup(Vector3 dir, Transform target_)
    {
        Vector3 dir2 = target_.position;
        target = target_;
        shootDir = dir;
        targetDir = dir2;
    }

	void Start () {	

		if (accuracy != 100) {
			accuracy = 1 - (accuracy / 100);

			for (int i = 0; i < 2; i++) {
				var val = 1 * Random.Range (-accuracy, accuracy);
				var index = Random.Range (0, 2);
				if (i == 0) {
					if (index == 0)
						offset = new Vector3 (0, -val, 0);
					else
						offset = new Vector3 (0, val, 0);
				} else {
					if (index == 0)
						offset = new Vector3 (0, offset.y, -val);
					else
						offset = new Vector3 (0, offset.y, val);
				}
			}
		}

		if (muzzlePrefab != null) {
			var muzzleVFX = Instantiate (muzzlePrefab, transform.position, Quaternion.identity);
			muzzleVFX.transform.forward = gameObject.transform.forward + offset;
			var ps = muzzleVFX.GetComponent<ParticleSystem>();
			if (ps != null)
				Destroy (muzzleVFX, ps.main.duration);
			else {
				var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
				Destroy (muzzleVFX, psChild.main.duration);
				Destroy (gameObject, lifeSpellTime);
			}
		}

		if (shotFX != null && GetComponent<AudioSource>()) {
			GetComponent<AudioSource> ().PlayOneShot (shotFX);
		}
	}


	

	void Update () {
        if (speed != 0 && target)
        {
            //transform.position += shootDir * (speed * Time.deltaTime);
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

            if(dir.magnitude<=distanceThisFrame)
            {
                if (trails.Count > 0)
                {
                    for (int i = 0; i < trails.Count; i++)
                        trails[i].transform.parent = null;
                }
                speed = 0;
                GetComponent<Rigidbody>().isKinematic = true;

                //ContactPoint contact = target.contacts[0];
                //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                //Vector3 pos = contact.point;

                if (hitPrefab != null)
                {
                    var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.identity);
                    var ps = hitVFX.GetComponent<ParticleSystem>();
                    if (ps == null)
                    {
                        var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitVFX, psChild.main.duration);
                    }
                    else
                        Destroy(hitVFX, ps.main.duration);
                }

                StartCoroutine(DestroyParticle(0f));
            }
        }

        

        if(target ==null)
        {
            Destroy(gameObject);
            return;
        }

    }

    //void OnCollisionEnter(Collision co)
    //{
    //    if (Vector3.Distance(co.transform.position, targetDir) <= 0.1f)
    //    {
    //        if (trails.Count > 0)
    //        {
    //            for (int i = 0; i < trails.Count; i++)
    //                trails[i].transform.parent = null;
    //        }
    //        speed = 0;
    //        GetComponent<Rigidbody>().isKinematic = true;

    //        ContactPoint contact = co.contacts[0];
    //        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //        Vector3 pos = contact.point;

    //        if (hitPrefab != null)
    //        {
    //            var hitVFX = Instantiate(hitPrefab, pos, rot);
    //            var ps = hitVFX.GetComponent<ParticleSystem>();
    //            if (ps == null)
    //            {
    //                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
    //                Destroy(hitVFX, psChild.main.duration);
    //            }
    //            else
    //                Destroy(hitVFX, ps.main.duration);
    //        }

    //        StartCoroutine(DestroyParticle(0f));
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{

    //        if (trails.Count > 0)
    //        {
    //            for (int i = 0; i < trails.Count; i++)
    //                trails[i].transform.parent = null;
    //        }

    //        speed = 0;
    //        GetComponent<Rigidbody>().isKinematic = true;
    //        if (Vector3.Distance(transform.position, targetDir) <= 0.1f)
    //        {

    //            var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.identity);
    //            var ps = hitVFX.GetComponent<ParticleSystem>();
    //            Destroy(hitVFX, ps.main.duration);

    //        }
    //        StartCoroutine(DestroyParticle(0f));
    //    }
    //}


    public IEnumerator DestroyParticle (float waitTime) {

		if (transform.childCount > 0 && waitTime != 0) {
			List<Transform> tList = new List<Transform> ();

			foreach (Transform t in transform.GetChild(0).transform) {
				tList.Add (t);
			}		

			while (transform.GetChild(0).localScale.x > 0) {
				yield return new WaitForSeconds (0.01f);
				transform.GetChild(0).localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				for (int i = 0; i < tList.Count; i++) {
					tList[i].localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				}
			}
		}
		
		yield return new WaitForSeconds (waitTime);
		Destroy (gameObject);
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile_Grenade : MonoBehaviour
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
    private float SkillAD; //공격력
    private float SkillMD=0.2f; //주문력
    private Transform target;
    public GameObject hitPrefab;

    private GameObject player;
    private bool teamcolor;

    private float stunTime; //xerion : 0.5~2.0
    private Vector3 startPos;


   public void Setup(Vector3 ShootDir, float AD, GameObject player_)
    {
       grenadeDir = ShootDir;
        SkillAD = AD;
        player = player_;
        startPos = player_.transform.position;
        teamcolor = player.GetComponent<Player_Stats>().TeamColor;
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



    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.transform.CompareTag("Player") && collision.transform.GetComponent<Player_Stats>().TeamColor
            != teamcolor) || (collision.transform.CompareTag("Minion") && collision.transform.GetComponent<Minion_Stats>().TeamColor
            != teamcolor) || (collision.transform.CompareTag("Turret") && collision.transform.GetComponent<Turret_Stats>().TeamColor
            != teamcolor) || collision.transform.CompareTag("Monster"))
        {
            //ignore collisions with projectile
            var contact = collision.contacts[0];
            if (contact.otherCollider.name.Contains("Projectile"))
                return;

            damageEnemy(collision.transform);

            Speed = 0;

            var hitPosition = contact.point + contact.normal * ImpactOffset;

            if (ImpactFX != null)
            {
                var impact = Instantiate(ImpactFX, hitPosition, Quaternion.identity);
                impact.transform.localScale = transform.localScale;
                Destroy(impact, ImpactFXDestroyDelay);
            }
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

                StartCoroutine(DestroyParticle(0f));
            }

            //FXToDeatch.transform.parent = null;
            //FXToDeatch.Stop(true);
            //Destroy(FXToDeatch.gameObject, ImpactFXDestroyDelay);

            //Destroy(gameObject);
        }
    }
    public IEnumerator DestroyParticle(float waitTime)
    {

        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> tList = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                tList.Add(t);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);
                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < tList.Count; i++)
                {
                    tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    private void damageEnemy(Transform other)
    {
        if (player.GetComponent<Player_Stats>().AttackAbility == 8) //coldy
        {
            if (other.CompareTag("Player") && other.GetComponent<Player_Stats>().TeamColor !=
                player.GetComponent<Player_Stats>().TeamColor)
            {
                if (player.GetComponent<Player_Stats>().isDanger)
                {
                    SkillAD *= 1.5f; //위험상태 50% 증가
                    SkillMD *= 1.5f; //주문력 추후 수정
                }
                other.GetComponent<Player_Stats>().DropHP(SkillAD, this.transform);
                other.GetComponent<Player_Stats>().DropSpeed(1 - SkillMD, 2.0f);
            }
            else if (other.CompareTag("Minion") && other.GetComponent<Minion_Stats>().TeamColor !=
                 player.GetComponent<Player_Stats>().TeamColor)
            {
                other.GetComponent<Minion_Stats>().DropHP(SkillAD, player.transform);
                other.GetComponent<Minion_Stats>().DropSpeed(1 - SkillMD, 2.0f); //이미당한경우 속박? 수정

            }
            else if (other.CompareTag("Monster"))
            {
                other.GetComponent<Monster_Stats>().DropHP(SkillAD, this.transform);
            }
        }
        else //xerion
        {
            stunTime = (startPos - other.transform.position).magnitude / 5.0f;
            if (stunTime < 0.5f) stunTime = 0.5f;
            else if (stunTime > 2.0f) stunTime = 2.0f;

            if (other.CompareTag("Player") && other.GetComponent<Player_Stats>().TeamColor !=
            player.GetComponent<Player_Stats>().TeamColor)
            {
                other.GetComponent<Player_Stats>().DropHP(SkillAD, this.transform);
                other.GetComponent<Player_Stats>().Stun(stunTime);
            }
            else if (other.CompareTag("Minion") && other.GetComponent<Minion_Stats>().TeamColor !=
                 player.GetComponent<Player_Stats>().TeamColor)
            {
                other.GetComponent<Minion_Stats>().DropHP(SkillAD, player.transform);
                other.GetComponent<Minion_Stats>().Stun(stunTime);

            }
            else if (other.CompareTag("Monster"))
            {
                other.GetComponent<Monster_Stats>().DropHP(SkillAD, this.transform);
            }
        }
    }
}


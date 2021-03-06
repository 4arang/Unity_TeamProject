using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class TestBasicAttack : MonoBehaviourPunCallbacks
{
    public enum ChampAttackType { Melee,Ranged};
    public ChampAttackType champAttackType;

    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private TestMovement moveScript;
    private Player_Stats statsScript;
    private Animator anim;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;

    [Header("Ranged Varialbes")]
    public bool performRangedAttack = true;
    public GameObject projPrefab;
    public Transform projSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<TestMovement>();
        statsScript = GetComponent<Player_Stats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        { 
            return;
        }
        else
        {
            if (targetedEnemy != null)
            {
                Debug.Log("타겟이 설정되었다");
                if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
                {
                    Debug.Log("사거리에 닿을 때 까지, 이동한다");
                    moveScript.agent.SetDestination(targetedEnemy.transform.position);
                    moveScript.agent.stoppingDistance = attackRange;
                }
                else
                {
                    //MELEE CHARACTRER
                    if (champAttackType == ChampAttackType.Melee)
                    {
                        //ROTATION
                        Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                            rotationToLookAt.eulerAngles.y,
                            ref moveScript.rotateVelocity,
                            rotateSpeedForAttack * (Time.deltaTime * 5));

                        transform.eulerAngles = new Vector3(0, rotationY, 0);

                        moveScript.agent.SetDestination(transform.position);

                        if (performMeleeAttack)
                        {
                            Debug.Log("Attack The Minion");                            
                            Debug.Log($"타겟 미니언의 체력={targetedEnemy.transform.gameObject.GetComponent<Minion_Stats>().HP}");
                            Debug.Log($"챔피언의 물리 공격력={statsScript.AD}");
                            //Start Coroutine To Attack
                            StartCoroutine(MeleeAttackInterval());
                        }
                    }

                    //RANGED CHARACTER
                    if (champAttackType == ChampAttackType.Ranged)
                    {
                        //ROTATION
                        Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                            rotationToLookAt.eulerAngles.y,
                            ref moveScript.rotateVelocity,
                            rotateSpeedForAttack * (Time.deltaTime * 5));

                        transform.eulerAngles = new Vector3(0, rotationY, 0);

                        moveScript.agent.SetDestination(transform.position);

                        if (performRangedAttack)
                        {
                            Debug.Log("Attack The Minion");

                            //Start Coroutine To Attack
                            StartCoroutine(RangedAttackInterval());
                        }
                    }

                }
            }
        }        
    }
    IEnumerator MeleeAttackInterval()
    {
        Debug.Log("코루틴 진입");
        if(photonView.IsMine)
        {
            photonView.RPC("RPC_PerformMeleeAttack", RpcTarget.All);
            Debug.Log("RPC_MeleeAttack Interval");
        }
        yield return null;
        //yield return new WaitForSeconds(statsScript.AttackSpeed / ((100 + statsScript.AttackSpeed) * 0.01f));   //Adjust Attack speed
    }

    IEnumerator RangedAttackInterval()
    {
        performRangedAttack = false;
        anim.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(statsScript.AttackSpeed / 100 + (statsScript.AttackSpeed) * 0.01f);

        if (targetedEnemy == null)
        {
            anim.SetBool("Basic Attack", false);
            performRangedAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (targetedEnemy != null)
        {
            if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
            {
                targetedEnemy.GetComponent<Stats>().health -= statsScript.AD;
            }
        }

        performMeleeAttack = true;
    }

    //public void RangedAttack()
    //{
    //    if (targetedEnemy != null)
    //    {
    //        if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
    //        {
    //            SpawnRangedProj("Minion", targetedEnemy);
    //        }
    //    }

    //    performRangedAttack = true;
    //}

    //void SpawnRangedProj(string typeOfEnemy, GameObject targetedEnemyObj)
    //{
    //    float dmg = statsScript.AD;

    //    Instantiate(projPrefab, projSpawnPoint.transform.position, Quaternion.identity);

    //    if (typeOfEnemy == "Minion")
    //    {
    //        projPrefab.GetComponent<RangedProjectile>().targetType = typeOfEnemy;

    //        projPrefab.GetComponent<RangedProjectile>().target = targetedEnemyObj;
    //        projPrefab.GetComponent<RangedProjectile>().targetSet = true;
    //    }
    //}

    #region PHOTON RPC
    [PunRPC]
    void RPC_PerformMeleeAttack()
    {
        performMeleeAttack = false;
        anim.SetBool("Basic Attack", true);
        Debug.Log("PerformMeleeAttack");
        if(PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Health update to masterclient");
            //체력연산
            float currentHealth;
            currentHealth = targetedEnemy.GetComponent<Minion_Stats>().HP -= this.GetComponent<Player_Stats>().AD;

            //서버에서 클라이언트로 동기화
            photonView.RPC("RPC_UpdateHealth", RpcTarget.Others , currentHealth);
        }        

        if (targetedEnemy == null)
        {
            anim.SetBool("Basic Attack", false);
            performMeleeAttack = true;
        }
    }

    [PunRPC]
    void RPC_UpdateHealth(float currentHealth)      //체력 동기화
    {
        targetedEnemy.GetComponent<Minion_Stats>().HP = currentHealth;
        Debug.Log($"체력 동기화, 현재체력 {currentHealth}");
    }
    #endregion
}

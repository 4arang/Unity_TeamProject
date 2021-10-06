using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class BaekLang_Abilities : MonoBehaviour
{
    public Animator anim;
    Player_Combat playerAttack;
    Player player;
    Stats stats;
    NavMeshAgent navMesh;           //캐릭터 이동속도 조절을 위한 변수

    [Header("Ability 0")]           //Passive
    public Image abilityImage0;
    public float cooldown0 = 5;
    //bool isCooldown0 = false;

    [Header("Ability 1")]           //Q
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown1 = false;
    public KeyCode ability1;

    [Header("Ability 2")]           //W
    public Image abilityImage2;
    public float cooldown2 = 10;
    bool isCooldown2 = false;
    public KeyCode ability2;

    [Header("Ability 3")]           //E
    public Image abilityImage3;
    public float cooldown3 = 7;
    bool isCooldown3 = false;
    public KeyCode ability3;

    public ParticleSystem lightningAura;

    [Header("Ability 4")]           //Ultimate(R)
    public Image abilityImage4;
    public float cooldown4 = 60;
    bool isCooldown4 = false;
    public KeyCode ability4;

    //Input variable
    public Canvas ability4Canvas;
    public Image skillshot;
    Vector3 position;


    private void Awake()            
    {
        playerAttack = GetComponent<Player_Combat>();
        player = GetComponent<Player>();
        navMesh = GetComponent<NavMeshAgent>();
        stats = GetComponent<Stats>();

        skillshot.GetComponent<Image>().enabled = false;
        //targetCircle.GetComponent<Image>().enabled = false;
        //indicatorRangeCircle.GetComponent<Image>().enabled = false;


        lightningAura.Stop();           //Ability3 스킬 아우라
        lightningAura.Clear();
    }

    void Start()
    {
        abilityImage0.fillAmount = 0;
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;
        Debug.Log(navMesh.speed);

        ability1 = KeyCode.Q;
        ability2 = KeyCode.W;
        ability3 = KeyCode.E;
        ability4 = KeyCode.R;
    }
    void Update()
    {
        Ability0();
        Ability1();
        Ability2();
        Ability3();
        Ability4();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ability 4 Inputs
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        ability4Canvas.transform.rotation = Quaternion.Lerp(transRot, ability4Canvas.transform.rotation, 0f);
    }

    void Ability0()
    {
        //if (Vector3.Distance(playerAttack.targetedEnemy.transform.position, playerAttack.targetedEnemy.transform.position) < stats.sight
        //    &&playerAttack.targetedEnemy==null) //캐릭터 시야범위 안에 있으면
        //{
        //    Debug.Log(playerAttack.targetedEnemy);
            //Debug.Log("시야 안에옴");
            //if (playerAttack.targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
            //{
            //    //Debug.Log("뛰어가라");
            //    navMesh.speed = 10;
            //    anim.SetTrigger("Ability0");
            //}
            //else if (playerAttack.targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
            //{
            //    //Debug.Log("미니언한테 갈 때는 천천히");
            //    navMesh.speed = 5;
            //}
            //else
            //{
            //    navMesh.speed = 5;
            //}
        //}
    }
              
    void Ability1()
    {
        if (Input.GetKey(ability1) && isCooldown1 == false)
        {
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;            
        }

        if (isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }

    void Ability2()
    {
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            anim.SetTrigger("Ability2");
        }        

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;                
            }
        }
    }

    void Ability3()
    {
        if (Input.GetKey(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
            lightningAura.Play();
        }

        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }

    void Ability4()
    {
        if (Input.GetKey(ability4) && isCooldown4 == false)
        {
            skillshot.GetComponent<Image>().enabled = true;

            //Disable Other UI
            //indicatorRangeCircle.GetComponent<Image>().enabled = false;
            //targetCircle.GetComponent<Image>().enabled = false;            
        }

        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isCooldown4 = true;
            abilityImage4.fillAmount = 1;
            anim.SetTrigger("Ability4");
        }


        if (isCooldown4)
        {
            abilityImage4.fillAmount -= 1 / cooldown4 * Time.deltaTime;

            skillshot.GetComponent<Image>().enabled = false;            //스킬 클릭하고나면 이미지 다시 지우기

            if (abilityImage4.fillAmount <= 0)
            {
                abilityImage4.fillAmount = 0;
                isCooldown4 = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Xerion_Shooting_Skill : MonoBehaviour
{
    PhotonView PV;

    Animator animator;

    public bool isSkillon = false;


    [Header("Q_Skill")]
    [SerializeField] private GameObject Direction;
    [SerializeField] private GameObject Range;
    [SerializeField] private GameObject Laser;
    [SerializeField] private GameObject Laser_ball;
    [SerializeField] private GameObject Laser_Range;
    [SerializeField] private GameObject Q_WeaponEffect;
    // public GameObject RangeDirection;
    private Vector3 Laser_Range_Size;
    public float Laser_Size_vel_ref = 0.5f;
    private Vector3 Laser_org_Size;
    private Vector3 Direction_Size;
    public float Direction_Size_vel_ref = 0.5f;
    private float Q_MP = 60;
    private bool Q_On = false;
    public bool Q_LaserFull = false;
    private float Q_CoolTime = 9.0f; // 9 8 7 6 5
    private bool Q_Ready = true;
    public Skill_BarQ skillQ;
    private int levelQ = 1;

    [Header("W_Skill")]
    [SerializeField] private GameObject satellite;
    [SerializeField] private GameObject satellite_range;
    private float W_MP = 70; // 80 90 100 110
    private bool W_On = false;
    private bool isW_ready = false;
    public float W_Distance = 9.5f;
    private Vector3 Wdirection; //W방향좌표 저장
    private bool W_Ready = true;
    private float W_CoolTime = 14; //14 13 12 11 10
    public Skill_BarW skillW;
    private int levelW = 1;

    [Header("E_Skill")]
    [SerializeField] private Transform grenade;
    [SerializeField] private GameObject grenadeEffect;
    protected float DirecAngle; //e키 방향각도
    private float E_AD = 80; // 80 110 140 170 200 0.45주문력 거리에 비례 기절시간 0.5~2s
    private float E_MP = 60;
    private bool E_On = false;
    private float E_CoolTime = 13; // 13 11 9 8 7 
    private bool E_Ready = true;
    public Skill_BarE skillE;
    private int levelE = 1;


    [Header("R_Skill")]
    [SerializeField] private GameObject Drone_Range;
    [SerializeField] private GameObject Drone;
    [SerializeField] private Transform Drone_grenade;
    [SerializeField] private Transform Drone_grenade_self;
    [SerializeField] private Camera mainCamera;
    private bool R_On = false;
    private float R_MP = 100;
    public float R_camFOV = 20.0f;
    private bool R_Ready = true;
    private float R_CoolTime = 130; // 130 115 100
    public Skill_BarR skillR;
    private int levelR = 1;

    private bool DroneReload;
    private int DroneShot = 4;
    public float R_time = 7.0f;
    private float delayTime;
    private bool R_active = false;

    //마우스 좌표 저장용(임시)
    Vector3 mouseVector;

   // private GameObject UIprefab;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        Laser_Range_Size = new Vector3(1, 1, 1);
        Laser_org_Size = new Vector3(1, 1, 1);
        Direction_Size = new Vector3(0.08f, 0.08f, 0.08f);

        Direction.SetActive(false);
        Range.SetActive(false);

        Laser.SetActive(false);
        Laser_ball.SetActive(false);
        Laser_Range.SetActive(false);
        Q_WeaponEffect.SetActive(false);

      // UIprefab = FindObjectOfType<PlayerUI>().gameObject;
        // RangeDirection.SetActive(false);
        satellite_range.SetActive(false);
        DroneReload = false;
        Drone.SetActive(false);

        Drone_Range.SetActive(false);

        animator = GetComponent<Animator>();
        if(PV.IsMine) mainCamera = Camera.main;


    }


    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Q) && GetComponent<Player_Stats>().mp >= Q_MP && Q_Ready)
            {
                isSkillon = true;
                GetComponent<Player_Stats>().DropMP(Q_MP);
                Q_On = true;
            }

            if (Input.GetKey(KeyCode.Q) && Q_On)
            {
                Range.SetActive(true);
                Laser_Range.SetActive(true);

                if (Laser_Range.transform.localScale.x <= Range.transform.localScale.x)
                    Laser_Range.transform.localScale += Laser_Range_Size * Time.deltaTime
                            * Laser_Size_vel_ref; //laer range ++
                else
                {
                    Q_LaserFull = true; //완충시 추가AD
                    PV.RPC("activeQWeapenEffect", RpcTarget.AllViaServer, true);
                   // Q_WeaponEffect.SetActive(true);
                }

                if (Laser.transform.localScale.x <= Range.transform.localScale.x)
                    Laser.transform.localScale += new Vector3(Laser_org_Size.x * Time.deltaTime
                        * Laser_Size_vel_ref, 0, Laser_org_Size.z * Time.deltaTime
                        * Laser_Size_vel_ref);
                //laser ++;
                if (Direction.transform.localScale.z <= 0.17f)
                    Direction.transform.localScale += new Vector3(0, 0, Direction_Size.x * Time.deltaTime
                        * Direction_Size_vel_ref);


                Laser_ball.SetActive(true); // laser ball effect on

                Direction.transform.position = Range.transform.position; //캐릭터가운데로 화살이동
                Direction.SetActive(true); //총방향 설정 -> 총 active
                GetMousePos();  //마우스 위치 받아와서 방향 바라보게 하기
                Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //각도setting
                movingManager.Instance.PlayerDirection = DirecAngle + 52f; //플레이어에 방향전달
                                                                           //애니메이션 총구 이동각 때문에 보정
            }
            if (Input.GetKeyUp(KeyCode.Q) && Q_On)  //E키 떼는 순간 스킬 시작
            {
                Q_Ready = false;
                skillQ.OnSkill(Q_CoolTime);
                //Q_WeaponEffect.SetActive(false);
                PV.RPC("activeQWeapenEffect", RpcTarget.AllViaServer, false);
                Range.SetActive(false);
                Direction.SetActive(false);
                Laser_Range.SetActive(false);
                Laser_ball.SetActive(false);


                if (animator.GetBool("A_Xerion") == false)
                {
                    StartCoroutine("Active_Q");
                }
                animator.SetBool("A_Xerion", true);

                Laser_Range.transform.localScale = Laser_Range_Size; //laser range initialize
                Direction.transform.localScale = Direction_Size; //direction length initialize;
                Q_On = false;
            }

            if (Input.GetKeyDown(KeyCode.W) && GetComponent<Player_Stats>().mp >= W_MP &&W_Ready)
            {
                isSkillon = true;
                W_On = true;
                GetComponent<Player_Stats>().DropMP(W_MP);
                satellite_range.SetActive(true);    //위성공격 범위 active
                Range.SetActive(true); //스킬범위 active
            }
            if (Input.GetKey(KeyCode.W) && W_On)
            {

                satellite_range.transform.position =
                    new Vector3(GetMousePos().x, 0.28f, GetMousePos().z); //위성공격범위 마우스위치에 이동
            }
            if (Input.GetKeyUp(KeyCode.W) && W_On)  //E키 떼는 순간 스킬 시작
            {
                skillW.OnSkill(W_CoolTime);
                Wdirection = satellite_range.transform.position;
                if ((transform.position - Wdirection).magnitude > W_Distance) //사거리 외에있을 경우 이동
                {
                    movingManager.Instance.PlayerClickedPos = Wdirection;
                    isW_ready = true;
                }
                else
                {
                    if (animator.GetBool("W_Xerion") == false)
                    {
                        StartCoroutine("Active_W");
                    }
                    animator.SetBool("W_Xerion", true);
                }

                Range.SetActive(false);
                satellite_range.SetActive(false);

                W_On = false;
            }
            if (isW_ready && (transform.position - Wdirection).magnitude <= W_Distance)
            {
                movingManager.Instance.PlayerClickedPos = transform.position; //위치고정
                if (animator.GetBool("W_Xerion") == false)
                {
                    StartCoroutine("Active_W");
                }
                animator.SetBool("W_Xerion", true);
                isW_ready = false;
            }

            if (Input.GetKeyDown(KeyCode.E) && GetComponent<Player_Stats>().mp >= E_MP &&E_Ready)
            {
                isSkillon = true;
                GetComponent<Player_Stats>().DropMP(E_MP);
                E_On = true;
            }

            if (Input.GetKey(KeyCode.E) && E_On)
            {
                Direction.transform.position = Range.transform.position; //캐릭터가운데로 화살이동
                Direction.SetActive(true); //화살방향 설정 -> 화살 active
                GetMousePos();  //마우스 위치 받아와서 방향 바라보게 하기
                Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //각도setting
                movingManager.Instance.PlayerDirection = DirecAngle + 45; //플레이어에 방향전달
            }
            if (Input.GetKeyUp(KeyCode.E) && E_On)  //E키 떼는 순간 스킬 시작
            {
                skillE.OnSkill(E_CoolTime);
                GetComponent<Player_Stats>().DropMP(E_MP);
                Direction.SetActive(false);



                Vector3 nextDir = new Vector3(mouseVector.x, grenadeEffect.transform.position.y, mouseVector.z);
                Vector3 shootDir = (nextDir - grenadeEffect.transform.position).normalized; //마우스좌표 -발사좌표

                //                Transform grenadeTransform = Instantiate(grenade, grenadeEffect.transform.position,
                //Quaternion.AngleAxis(DirecAngle, Vector3.up)); //유탄발사 and transform 저장
                //                grenadeTransform.GetComponent<Projectile_Grenade>().Setup(shootDir, E_AD,
                //                   gameObject); //유탄에 방향전달
                PV.RPC("instantiateE", RpcTarget.AllViaServer, shootDir, DirecAngle);

                if (animator.GetBool("E_Xerion") == false)
                {
                    StartCoroutine("Active_E");
                }
                animator.SetBool("E_Xerion", true);
                E_On = false;
            }
            if (!R_active)
            {
                if (Input.GetKeyDown(KeyCode.R) && GetComponent<Player_Stats>().mp >= R_MP &&R_Ready)
                {
                    skillR.OnSkill(R_CoolTime);
                    isSkillon = true;
                    GetComponent<Player_Stats>().DropMP(R_MP);
                    satellite_range.SetActive(true);
                    Drone_Range.SetActive(true);
                    if (animator.GetBool("R_Xerion") == false)
                    {
                        animator.SetBool("R_Xerion", true);
                        StartCoroutine("Active_R");
                    }
                }

            }
            if (Input.GetKey(KeyCode.R))
            {
                satellite_range.transform.position = new Vector3(GetMousePos().x, 0.28f, GetMousePos().z);
                delayTime = Time.time;

                if (Input.GetKeyUp(KeyCode.R)) //r키 재입력 위해서
                {


                }
            }
        }
    }

    private void LateUpdate()
    {
        if (PV.IsMine)
        {
            if (R_active)
            {
                // Drone.SetActive(true);
                PV.RPC("activeDrone", RpcTarget.AllViaServer, true);
                satellite_range.transform.position = new Vector3(GetMousePos().x, 0.28f, GetMousePos().z);
                mainCamera.fieldOfView += R_camFOV;
                if (DroneShot >= 1)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        Vector3 nextDir = new Vector3(mouseVector.x, Drone.transform.position.y, mouseVector.z);
                        Vector3 shootDir = (nextDir - Drone.transform.position).normalized;
                        Drone.transform.rotation = Quaternion.AngleAxis(DirecAngle - 90, Vector3.up); //방향보정
                        if (DroneShot == 1) //드론공격
                        {

                            //   Transform DroneGrenadeTransform = Instantiate(Drone_grenade_self, Drone.transform.position/*new Vector3(
                            //Drone.transform.position.x, 1.0f, Drone.transform.position.z)*/,
                            //    Quaternion.identity); //드론좌표에서 스킬 발사
                            //   DroneGrenadeTransform.GetComponent<Xerion_Drone_Grenade>().Setup(Drone.transform.position, mouseVector);
                            //   //드론에 방향, 포격위치 전달
                            //   Drone.SetActive(false);
                            //   //드론 없애고 다시 발사
                            PV.RPC("instantiateR_Drone", RpcTarget.AllViaServer, mouseVector);
                            PV.RPC("activeDrone", RpcTarget.AllViaServer, false);
                        }
                        else //드론 미사일 공격
                        {
                            //Transform DroneGrenadeTransform = Instantiate(Drone_grenade, new Vector3(
                            // Drone.transform.position.x, Drone.transform.position.y + 1.0f, Drone.transform.position.z),
                            //    Quaternion.identity); //드론좌표에서 스킬 발사
                            //DroneGrenadeTransform.GetComponent<Xerion_Drone_Grenade>().Setup(Drone.transform.position, mouseVector);
                            ////드론에 방향, 포격위치 전달
                            PV.RPC("instantiateR_Grenade", RpcTarget.AllViaServer, mouseVector);                          
                        }
                        DroneShot--;
                    }
                }

                if (Time.time > delayTime + R_time || DroneShot <= 0)
                {

                    satellite_range.SetActive(false);
                    Drone_Range.SetActive(false);
                    //Drone.SetActive(false);
                    PV.RPC("activeDrone", RpcTarget.AllViaServer, false);
                    DroneShot = 4;
                    mainCamera.fieldOfView -= R_camFOV;
                    R_active = false; //시간초과시 or 스킬모두 사용시 r스킬 종료
                }
            }
        }
    }
    IEnumerator Active_R()
    {
        while (true)
        {
            R_Ready = false;
            yield return new WaitForSeconds(0.1f);
            R_active = true;
            yield return new WaitForSeconds(1.0f);
            animator.SetBool("R_Xerion", false);
            isSkillon = false;
            yield return new WaitForSeconds(R_CoolTime - 1.1f);
            R_Ready = true;
            break;
        }

    }



    IEnumerator Active_E()
    {
        while (true)
        {
            E_Ready = false;
            //grenadeEffect.SetActive(true);
            PV.RPC("activeE", RpcTarget.AllViaServer, true);
            yield return new WaitForSeconds(0.5f);
            //grenadeEffect.SetActive(false);
            PV.RPC("activeE", RpcTarget.AllViaServer, false);
            animator.SetBool("E_Xerion", false);
            isSkillon = false;
            yield return new WaitForSeconds(E_CoolTime - 0.5f);
            E_Ready = true;
            break;
        }

    }
    IEnumerator Active_W()
    {
        while (true)
        {
            W_Ready = false;
            yield return new WaitForSeconds(1.0f);
            // Instantiate(satellite, Wdirection, Quaternion.identity);
            PV.RPC("instantiateWSatellite", RpcTarget.AllViaServer, Wdirection);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("W_Xerion", false);
            yield return new WaitForSeconds(1.0f);
            isSkillon = false;
            yield return new WaitForSeconds(W_CoolTime - 2.1f);
            W_Ready = true;
            break;
        }

    }
    IEnumerator Active_Q()
    {
        while (true)
        {
            Q_Ready = false;
            // Laser.SetActive(true);
            PV.RPC("activeQLaser", RpcTarget.AllViaServer, true, Laser.transform.localScale);
            Laser.GetComponent<Xerion_Q_Laser_Collider>().setup(levelQ);
           // GetComponentInChildren<Xerion_Q_Laser_Collider>().Xerion_Q_ColliderOn = true;
            yield return new WaitForSeconds(1.0f);
            //GetComponentInChildren<Xerion_Q_Laser_Collider>().Xerion_Q_Full = Q_LaserFull;
            // Laser.SetActive(false);
            PV.RPC("activeQLaser", RpcTarget.AllViaServer, false, Laser_org_Size);
            animator.SetBool("A_Xerion", false);
            Laser.transform.localScale = Laser_org_Size;
            Q_LaserFull = false;
            isSkillon = false;
            yield return new WaitForSeconds(Q_CoolTime - 1.0f);
            Q_Ready = true;
            break;
        }

    }
    Vector3 GetMousePos()
    {

        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        //마우스 위치 받아서 각도계산
        DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
            hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;

        mouseVector = hit.point;

        return hit.point;
    }

  [PunRPC]
  void activeQWeapenEffect(bool b)
    {
        Q_WeaponEffect.SetActive(b);
    }

    [PunRPC]
    void activeQLaser(bool b, Vector3 size)
    {
        Laser.transform.localScale = size;
        Laser.SetActive(b);
    }

    [PunRPC]
    void instantiateWSatellite(Vector3 Wdirection)
    {
        GameObject satelliteTransform = Instantiate(satellite, Wdirection, Quaternion.identity);
        satelliteTransform.GetComponentInChildren<Xerion_W_Skill_Colider>().setup(this.transform, levelW);
    }

    [PunRPC]
    void activeE(bool b)
    {
        grenadeEffect.SetActive(b);
    }
    [PunRPC]
    void instantiateE(Vector3 dir, float DirecAngle)
    {
        Transform grenadeTransform = Instantiate(grenade, grenadeEffect.transform.position,
Quaternion.AngleAxis(DirecAngle, Vector3.up)); //유탄발사 and transform 저장
        grenadeTransform.GetComponent<Projectile_Grenade>().Setup(dir, E_AD,
           gameObject); //유탄에 방향전달
    }
    [PunRPC]
    void activeDrone(bool b)
    {
       Drone.SetActive(b);
    }
    [PunRPC]
    void instantiateR_Grenade(Vector3 dir)
    {
        Transform DroneGrenadeTransform = Instantiate(Drone_grenade, new Vector3(
   Drone.transform.position.x, Drone.transform.position.y + 1.0f, Drone.transform.position.z),
      Quaternion.identity); //드론좌표에서 스킬 발사
        DroneGrenadeTransform.GetComponent<Xerion_Drone_Grenade>().Setup(Drone.transform.position, dir);
        DroneGrenadeTransform.GetComponentInChildren<Xerion_R_Bomb_Collider>().Setup(this.transform, levelR);
        //드론에 방향, 포격위치 전달
    }
    [PunRPC]
    void instantiateR_Drone(Vector3 dir)
    {
        Transform DroneGrenadeTransform = Instantiate(Drone_grenade_self, Drone.transform.position/*new Vector3(
                         Drone.transform.position.x, 1.0f, Drone.transform.position.z)*/,
               Quaternion.identity); //드론좌표에서 스킬 발사
        DroneGrenadeTransform.GetComponent<Xerion_Drone_Grenade>().Setup(Drone.transform.position, dir);
        DroneGrenadeTransform.GetComponentInChildren<Xerion_R_DroneBomb_Collider>().Setup(this.transform, levelR);
        //드론에 방향, 포격위치 전달
    }


    public void levelupQ()
    {
        levelQ++;
        Q_CoolTime--; //1씩 감소
    }
    public void levelupW()
    {
        levelW++;
        W_CoolTime--; //1씩 감소
        W_MP += 10;
    }
    public void levelupE()
    {
        levelE++;
        E_MP += 5;
        E_AD += 30;
        if (levelE == 2) E_CoolTime = 11;
        else if (levelE == 3) E_CoolTime = 9;
        else if (levelE == 4) E_CoolTime = 8;
        else if (levelE == 5) E_CoolTime = 7;
    }
    public void levelupR()
    {
        levelR++;
        R_CoolTime -= 15; //1씩 감소
    }
}
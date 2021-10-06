using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_Shooting_Skill : MonoBehaviour
{
    Animator animator;

    [SerializeField] private GameObject GunShot;

    [Header("Q_Skill")]
    [SerializeField] private GameObject Direction;
    [SerializeField] private GameObject Range;
    [SerializeField] private GameObject Laser;
    [SerializeField] private GameObject Laser_ball;
    [SerializeField] private GameObject Laser_Range;
    // public GameObject RangeDirection;
    private Vector3 Laser_Range_Size;
    public float Laser_Size_vel_ref = 0.5f;
    private Vector3 Laser_org_Size;
    private Vector3 Direction_Size;
    public float Direction_Size_vel_ref = 0.5f;

    [Header("W_Skill")]
    [SerializeField] private GameObject satellite;
    [SerializeField] private GameObject satellite_range;

    [Header("E_Skill")]
    [SerializeField] private Transform grenade;
    [SerializeField] private GameObject grenadeEffect;
    protected float DirecAngle; //e키 방향각도


    [Header("R_Skill")]
    [SerializeField] private GameObject Drone_Range;
    [SerializeField] private GameObject Drone;
    [SerializeField] private Transform Drone_grenade;
    [SerializeField] private Transform Drone_grenade_self;
    [SerializeField] private Camera mainCamera;
    public float R_camFOV = 20.0f;

    private bool DroneReload;
    private int DroneShot = 4;
    public float R_time = 7.0f;
    private float delayTime;
    private bool R_active = false;

    //마우스 좌표 저장용(임시)
    Vector3 mouseVector;

    void Start()
    {
        Laser_Range_Size = new Vector3(1, 1, 1);
        Laser_org_Size = new Vector3(1, 1, 1);
        Direction_Size = new Vector3(0.08f, 0.08f, 0.08f);

        GunShot.SetActive(false);
        Direction.SetActive(false);
        Range.SetActive(false);

        Laser.SetActive(false);
        Laser_ball.SetActive(false);
        Laser_Range.SetActive(false);



        // RangeDirection.SetActive(false);
        satellite_range.SetActive(false);
        DroneReload = false;
        Drone.SetActive(false);

        Drone_Range.SetActive(false);

        animator = GetComponent<Animator>();


    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (animator.GetBool("A_Xerion") == false)
                StartCoroutine("Active_A");
            animator.SetBool("A_Xerion", true);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Range.SetActive(true);
            Laser_Range.SetActive(true);

            if (Laser_Range.transform.localScale.x <= Range.transform.localScale.x)
                Laser_Range.transform.localScale += Laser_Range_Size * Time.deltaTime
                        * Laser_Size_vel_ref; //laer range ++
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
            ycManager.Instance.PlayerDirection = DirecAngle + 52f; //플레이어에 방향전달
            //애니메이션 총구 이동각 때문에 보정
        }
        if (Input.GetKeyUp(KeyCode.Q))  //E키 떼는 순간 스킬 시작
        {
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
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            satellite_range.SetActive(true);    //위성공격 범위 active
            Range.SetActive(true); //스킬범위 active
        }
        if (Input.GetKey(KeyCode.W))
        {
            satellite_range.transform.position =
                new Vector3(GetMousePos().x, 0.28f, GetMousePos().z); //위성공격범위 마우스위치에 이동
        }
        if (Input.GetKeyUp(KeyCode.W))  //E키 떼는 순간 스킬 시작
        {
            Range.SetActive(false);
            satellite_range.SetActive(false);

            if (animator.GetBool("W_Xerion") == false)
            {
                StartCoroutine("Active_W");
            }
            animator.SetBool("W_Xerion", true);
        }

        if (Input.GetKey(KeyCode.E))
        {
            Direction.transform.position = Range.transform.position; //캐릭터가운데로 화살이동
            Direction.SetActive(true); //화살방향 설정 -> 화살 active
            GetMousePos();  //마우스 위치 받아와서 방향 바라보게 하기
            Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //각도setting
            ycManager.Instance.PlayerDirection = DirecAngle + 45; //플레이어에 방향전달
        }
        if (Input.GetKeyUp(KeyCode.E))  //E키 떼는 순간 스킬 시작
        {
            Direction.SetActive(false);

            Transform grenadeTransform = Instantiate(grenade, grenadeEffect.transform.position,
Quaternion.identity); //유탄발사 and transform 저장

            Vector3 nextDir = new Vector3(mouseVector.x, grenadeEffect.transform.position.y, mouseVector.z);
            Vector3 shootDir = (nextDir - grenadeEffect.transform.position).normalized; //마우스좌표 -발사좌표

            grenadeTransform.GetComponent<PFX_ProjectileObject>().Setup(shootDir); //유탄에 방향전달

            if (animator.GetBool("E_Xerion") == false)
            {
                StartCoroutine("Active_E");
            }
            animator.SetBool("E_Xerion", true);
        }
        if (!R_active)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

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

    private void LateUpdate()
    {
        if (R_active)
        {
            Drone.SetActive(true);
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

                        Transform DroneGrenadeTransform = Instantiate(Drone_grenade_self, Drone.transform.position/*new Vector3(
                         Drone.transform.position.x, 1.0f, Drone.transform.position.z)*/, 
                         Quaternion.identity); //드론좌표에서 스킬 발사
                        DroneGrenadeTransform.GetComponent<Xerion_Drone_Grenade>().Setup(Drone.transform.position, mouseVector);
                        //드론에 방향, 포격위치 전달
                        Drone.SetActive(false);
                        //드론 없애고 다시 발사
                    }
                    else //드론 미사일 공격
                    {
                        Transform DroneGrenadeTransform = Instantiate(Drone_grenade,new Vector3(
                         Drone.transform.position.x, Drone.transform.position.y+1.0f, Drone.transform.position.z),
                            Quaternion.identity); //드론좌표에서 스킬 발사
                        DroneGrenadeTransform.GetComponent<Xerion_Drone_Grenade>().Setup(Drone.transform.position, mouseVector);
                        //드론에 방향, 포격위치 전달
                    }
                    DroneShot--;
                }
            }

            if (Time.time > delayTime + R_time || DroneShot <= 0)
            {

                satellite_range.SetActive(false);
                Drone_Range.SetActive(false);
                Drone.SetActive(false);
                DroneShot = 4;
                mainCamera.fieldOfView -= R_camFOV;
                R_active = false; //시간초과시 or 스킬모두 사용시 r스킬 종료
            }
        }
    }
    IEnumerator Active_R()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            R_active = true;
            yield return new WaitForSeconds(1.0f);
            animator.SetBool("R_Xerion", false);
            break;
        }
    }


    IEnumerator Active_A()
    {
        while (true)
        {
            GunShot.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            GunShot.SetActive(false);
            animator.SetBool("A_Xerion", false);
            break;
        }
    }
    IEnumerator Active_E()
    {
        while (true)
        {
            grenadeEffect.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            grenadeEffect.SetActive(false);
            animator.SetBool("E_Xerion", false);
            break;
        }
    }
    IEnumerator Active_W()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Instantiate(satellite, satellite_range.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("W_Xerion", false);
            yield return new WaitForSeconds(1.0f);

            break;
        }
    }
    IEnumerator Active_Q()
    {
        while (true)
        {
            Laser.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            Laser.SetActive(false);
            animator.SetBool("A_Xerion", false);
            break;
        }
        Laser.transform.localScale = Laser_org_Size;
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
}
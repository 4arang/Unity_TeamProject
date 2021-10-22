using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_Shooting_Skill : MonoBehaviour
{
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

    [Header("W_Skill")]
    [SerializeField] private GameObject satellite;
    [SerializeField] private GameObject satellite_range;
    private float W_MP = 70;
    private byte W_Level = 1;
    private bool W_On = false;
    private bool isW_ready = false;
    public float W_Distance = 9.5f;
    private Vector3 Wdirection; //W������ǥ ����
    

    [Header("E_Skill")]
    [SerializeField] private Transform grenade;
    [SerializeField] private GameObject grenadeEffect;
    protected float DirecAngle; //eŰ ���Ⱒ��
    private float E_MP = 60;
    private byte E_Level = 1;
    private bool E_On = false;



    [Header("R_Skill")]
    [SerializeField] private GameObject Drone_Range;
    [SerializeField] private GameObject Drone;
    [SerializeField] private Transform Drone_grenade;
    [SerializeField] private Transform Drone_grenade_self;
    [SerializeField] private Camera mainCamera;
    private bool R_On = false;
    private float R_MP = 100;
    public float R_camFOV = 20.0f;

    private bool DroneReload;
    private int DroneShot = 4;
    public float R_time = 7.0f;
    private float delayTime;
    private bool R_active = false;

    //���콺 ��ǥ �����(�ӽ�)
    Vector3 mouseVector;

    void Start()
    {
        Laser_Range_Size = new Vector3(1, 1, 1);
        Laser_org_Size = new Vector3(1, 1, 1);
        Direction_Size = new Vector3(0.08f, 0.08f, 0.08f);

        Direction.SetActive(false);
        Range.SetActive(false);

        Laser.SetActive(false);
        Laser_ball.SetActive(false);
        Laser_Range.SetActive(false);
        Q_WeaponEffect.SetActive(false);


        // RangeDirection.SetActive(false);
        satellite_range.SetActive(false);
        DroneReload = false;
        Drone.SetActive(false);

        Drone_Range.SetActive(false);

        animator = GetComponent<Animator>();


    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && GetComponent<Xerion_Stats>().mp >= Q_MP)
        {
            isSkillon = true;
            GetComponent<Xerion_Stats>().DropMP(Q_MP);
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
                Q_LaserFull = true; //����� �߰�AD
                Q_WeaponEffect.SetActive(true);
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

            Direction.transform.position = Range.transform.position; //ĳ���Ͱ���� ȭ���̵�
            Direction.SetActive(true); //�ѹ��� ���� -> �� active
            GetMousePos();  //���콺 ��ġ �޾ƿͼ� ���� �ٶ󺸰� �ϱ�
            Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //����setting
            movingManager.Instance.PlayerDirection = DirecAngle + 52f; //�÷��̾ ��������
            //�ִϸ��̼� �ѱ� �̵��� ������ ����
        }
        if (Input.GetKeyUp(KeyCode.Q)&& Q_On)  //EŰ ���� ���� ��ų ����
        {
            Q_WeaponEffect.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.W) && GetComponent<Xerion_Stats>().mp >= W_MP)
        {
            isSkillon = true;
            W_On = true;
            GetComponent<Xerion_Stats>().DropMP(W_MP);
            satellite_range.SetActive(true);    //�������� ���� active
            Range.SetActive(true); //��ų���� active
        }
        if (Input.GetKey(KeyCode.W)&&W_On)
        {
          
            satellite_range.transform.position =
                new Vector3(GetMousePos().x, 0.28f, GetMousePos().z); //�������ݹ��� ���콺��ġ�� �̵�
        }
        if (Input.GetKeyUp(KeyCode.W)&&W_On)  //EŰ ���� ���� ��ų ����
        {
            Wdirection = satellite_range.transform.position;
         if((transform.position-Wdirection).magnitude>W_Distance) //��Ÿ� �ܿ����� ��� �̵�
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
        if(isW_ready && (transform.position-Wdirection).magnitude<=W_Distance)
        {
            movingManager.Instance.PlayerClickedPos = transform.position; //��ġ����
            if (animator.GetBool("W_Xerion") == false)
            {
                StartCoroutine("Active_W");
            }
            animator.SetBool("W_Xerion", true);
            isW_ready = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && GetComponent<Xerion_Stats>().mp >= E_MP)
        {
            isSkillon = true;
            GetComponent<Xerion_Stats>().DropMP(E_MP);
            E_On = true;
        }

        if (Input.GetKey(KeyCode.E) && E_On)
        {
            Direction.transform.position = Range.transform.position; //ĳ���Ͱ���� ȭ���̵�
            Direction.SetActive(true); //ȭ����� ���� -> ȭ�� active
            GetMousePos();  //���콺 ��ġ �޾ƿͼ� ���� �ٶ󺸰� �ϱ�
            Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //����setting
            movingManager.Instance.PlayerDirection = DirecAngle + 45; //�÷��̾ ��������
        }
        if (Input.GetKeyUp(KeyCode.E)&&E_On)  //EŰ ���� ���� ��ų ����
        {
            GetComponent<Xerion_Stats>().DropMP(E_MP);
            Direction.SetActive(false);

            Transform grenadeTransform = Instantiate(grenade, grenadeEffect.transform.position,
Quaternion.identity); //��ź�߻� and transform ����

            Vector3 nextDir = new Vector3(mouseVector.x, grenadeEffect.transform.position.y, mouseVector.z);
            Vector3 shootDir = (nextDir - grenadeEffect.transform.position).normalized; //���콺��ǥ -�߻���ǥ

            grenadeTransform.GetComponent<PFX_ProjectileObject>().Setup(shootDir); //��ź�� ��������

            if (animator.GetBool("E_Xerion") == false)
            {
                StartCoroutine("Active_E");
            }
            animator.SetBool("E_Xerion", true);
            E_On = false;
        }
        if (!R_active)
        {
            if (Input.GetKeyDown(KeyCode.R) && GetComponent<Xerion_Stats>().mp >= R_MP)
            {
                isSkillon = true;
                GetComponent<Xerion_Stats>().DropMP(R_MP);
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

            if (Input.GetKeyUp(KeyCode.R)) //rŰ ���Է� ���ؼ�
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
                    Drone.transform.rotation = Quaternion.AngleAxis(DirecAngle - 90, Vector3.up); //���⺸��
                    if (DroneShot == 1) //��а���
                    {

                        Transform DroneGrenadeTransform = Instantiate(Drone_grenade_self, Drone.transform.position/*new Vector3(
                         Drone.transform.position.x, 1.0f, Drone.transform.position.z)*/, 
                         Quaternion.identity); //�����ǥ���� ��ų �߻�
                        DroneGrenadeTransform.GetComponent<Xerion_Drone_Grenade>().Setup(Drone.transform.position, mouseVector);
                        //��п� ����, ������ġ ����
                        Drone.SetActive(false);
                        //��� ���ְ� �ٽ� �߻�
                    }
                    else //��� �̻��� ����
                    {
                        Transform DroneGrenadeTransform = Instantiate(Drone_grenade,new Vector3(
                         Drone.transform.position.x, Drone.transform.position.y+1.0f, Drone.transform.position.z),
                            Quaternion.identity); //�����ǥ���� ��ų �߻�
                        DroneGrenadeTransform.GetComponent<Xerion_Drone_Grenade>().Setup(Drone.transform.position, mouseVector);
                        //��п� ����, ������ġ ����
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
                R_active = false; //�ð��ʰ��� or ��ų��� ���� r��ų ����
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
        isSkillon = false;
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
        isSkillon = false;
    }
    IEnumerator Active_W()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Instantiate(satellite, Wdirection, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("W_Xerion", false);
            yield return new WaitForSeconds(1.0f);

            break;
        }
        isSkillon = false;
    }
    IEnumerator Active_Q()
    {
        while (true)
        {
            Laser.SetActive(true);
            GetComponentInChildren<Xerion_Q_Laser_Collider>().Xerion_Q_ColliderOn = true;
            yield return new WaitForSeconds(1.0f);
            //GetComponentInChildren<Xerion_Q_Laser_Collider>().Xerion_Q_Full = Q_LaserFull;
            Laser.SetActive(false);
            animator.SetBool("A_Xerion", false);
            break;
        }
        Laser.transform.localScale = Laser_org_Size;
        Q_LaserFull = false;
        isSkillon = false;
    }
    Vector3 GetMousePos()
    {

        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        //���콺 ��ġ �޾Ƽ� �������
        DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
            hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;

        mouseVector = hit.point;

        return hit.point;
    }
}
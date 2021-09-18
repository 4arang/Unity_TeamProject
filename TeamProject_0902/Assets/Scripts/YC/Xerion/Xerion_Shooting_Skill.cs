using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_Shooting_Skill : MonoBehaviour
{
    Animator animator;

    public GameObject GunShot;

    public GameObject Laser;

    public GameObject Direction;
    public GameObject Range;
    // public GameObject RangeDirection;



    [SerializeField] private GameObject satellite;
    [SerializeField] private GameObject satellite_range;


    protected float DirecAngle; //eŰ ���Ⱒ��
    [SerializeField] private Transform grenade;
    [SerializeField] private GameObject grenadeEffect;


    [SerializeField] private GameObject Drone_Range;
    private bool DroneReload;

    //���콺 ��ǥ �����(�ӽ�)
    Vector3 mouseVector;

    void Start()
    {
        GunShot.SetActive(false);
        Direction.SetActive(false);
        Range.SetActive(false);

        // RangeDirection.SetActive(false);
        satellite_range.SetActive(false);
        DroneReload = false;

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
            Direction.transform.position = Range.transform.position; //ĳ���Ͱ���� ȭ���̵�
            Direction.SetActive(true); //�ѹ��� ���� -> �� active
            GetMousePos();  //���콺 ��ġ �޾ƿͼ� ���� �ٶ󺸰� �ϱ�
            Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //����setting
            ycManager.Instance.PlayerDirection = DirecAngle; //�÷��̾ ��������
        }
        if (Input.GetKeyUp(KeyCode.Q))  //EŰ ���� ���� ��ų ����
        {
            Direction.SetActive(false);

            if (animator.GetBool("A_Xerion") == false)
            {
                StartCoroutine("Active_Q");
            }
            animator.SetBool("A_Xerion", true);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            satellite_range.SetActive(true);    //�������� ���� active
            Range.SetActive(true); //��ų���� active
        }
        if (Input.GetKey(KeyCode.W))
        {
            satellite_range.transform.position = GetMousePos(); //�������ݹ��� ���콺��ġ�� �̵�
        }
        if (Input.GetKeyUp(KeyCode.W))  //EŰ ���� ���� ��ų ����
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
            Direction.transform.position = Range.transform.position; //ĳ���Ͱ���� ȭ���̵�
            Direction.SetActive(true); //ȭ����� ���� -> ȭ�� active
            GetMousePos();  //���콺 ��ġ �޾ƿͼ� ���� �ٶ󺸰� �ϱ�
            Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //����setting
            ycManager.Instance.PlayerDirection = DirecAngle; //�÷��̾ ��������
        }
        if (Input.GetKeyUp(KeyCode.E))  //EŰ ���� ���� ��ų ����
        {
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
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            satellite_range.SetActive(true);    
            Drone_Range.SetActive(true);
            satellite_range.transform.position = GetMousePos();
        }
        if (Input.GetKey(KeyCode.R))
        {
            satellite_range.transform.position = GetMousePos(); 
        }
        if (Input.GetKeyUp(KeyCode.R))  
        {
            Drone_Range.SetActive(false);
            satellite_range.SetActive(false);

            if (animator.GetBool("W_Xerion") == false)
            {
                StartCoroutine("Active_W");
            }
            animator.SetBool("W_Xerion", true);
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
            yield return new WaitForSeconds(0.5f);
            Instantiate(satellite, satellite_range.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            Destroy(satellite);
            animator.SetBool("W_Xerion", false);
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
    }
    Vector3 GetMousePos()
    {

        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        //���콺 ��ġ �޾Ƽ� �������
        DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
            hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;

        return hit.point;
    }
}

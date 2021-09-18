using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_W : MonoBehaviour
{
    Animator animator;
    public GameObject aura;
    public GameObject shield;

    public GameObject flame;

    public GameObject Direction;
    public GameObject grenade;
    
   [SerializeField] private Transform grenade_Bomb;
   protected float DirecAngle; //eŰ ���Ⱒ��
    protected float R_DirecAngle; //rŰ ���Ⱒ��
   // protected Vector3 BombDirec;

    public GameObject missile;
    public GameObject Range;
    public GameObject RangeDirection;
    public GameObject missile_target_effect;

    //���콺 ��ǥ �����(�ӽ�)
    Vector3 mouseVector;
    
    void Start()
    {
        aura.SetActive(false);
        shield.SetActive(false);

        flame.SetActive(false);

        Direction.SetActive(false);
        grenade.SetActive(false);

        missile.SetActive(false);
        Range.SetActive(false);
        RangeDirection.SetActive(false);
      //  missile_target_effect.SetActive(false);
        animator = GetComponent<Animator>();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (animator.GetBool("A_ColD") == false)
                StartCoroutine("Active_A");
            animator.SetBool("A_ColD", true);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(animator.GetBool("W_ColD")==false)
            StartCoroutine("Active_W");
            animator.SetBool("W_ColD", true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (animator.GetBool("Q_ColD") == false)
                StartCoroutine("Active_Q");
            animator.SetBool("Q_ColD", true);
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

         Transform grenadeTransform =  Instantiate(grenade_Bomb, grenade.transform.position,
        Quaternion.identity); //��ź�߻� and transform ����

            Vector3 nextDir = new Vector3(mouseVector.x, grenade.transform.position.y, mouseVector.z);
            Vector3 shootDir = (nextDir- grenade.transform.position).normalized; //���콺��ǥ -�߻���ǥ
           
            grenadeTransform.GetComponent<PFX_ProjectileObject>().Setup(shootDir); //��ź�� ��������



            if (animator.GetBool("E_ColD") == false)
            {
                StartCoroutine("Active_E");
            }
            animator.SetBool("E_ColD", true);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            mouseVector = GetMousePos(); //ȭ�� ���� ���콺���⿡ �̸� �̵�
            Direction.SetActive(true); //����ȭ�� active
            //ȭ��ǥ ��ġ ����
        }
        if (Input.GetKey(KeyCode.R))
        {
            Direction.transform.position = mouseVector;
            Range.SetActive(true);
            GetRdirect(); //get R_DirecAngle
            Direction.transform.rotation = 
                Quaternion.AngleAxis(R_DirecAngle, Vector3.up);

        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Range.SetActive(false);
           Direction.SetActive(false);
            if (animator.GetBool("R_ColD") == false)
                StartCoroutine("Active_R");
            animator.SetBool("R_ColD", true);
        }
    }
    IEnumerator Active_A()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            animator.SetBool("A_ColD", false);
            break;
        }
    }
    IEnumerator Active_Q()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            flame.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            animator.SetBool("Q_ColD", false);
            yield return new WaitForSeconds(0.5f);
            flame.SetActive(false);
     
            break;
        }
    }
    IEnumerator Active_W()
    {
        while (true)
        {
            aura.SetActive(true);
            shield.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            aura.SetActive(false);
            shield.SetActive(false);
            animator.SetBool("W_ColD", false);
            break;
        }
    }
    IEnumerator Active_E()
    {
        while (true)
        {
            grenade.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            grenade.SetActive(false);
            animator.SetBool("E_ColD", false);
            break;
        }
    }
    IEnumerator Active_R()
    {
        while (true)
        {
            missile.SetActive(true);
            yield return new WaitForSeconds(0.67f);
            missile.SetActive(false);
            animator.SetBool("R_ColD", false);
            break;
        }
        while (true)
        {
          
            missile_target_effect.transform.position =
                new Vector3(Direction.transform.position.x, 12.5f, Direction.transform.position.z);
            //�̻��� Ÿ�� ��ġ

            Instantiate(missile_target_effect, missile_target_effect.transform.position,
          Direction.transform.rotation); //��ź�߻�

            yield return new WaitForSeconds(4.5f);
            break;
        }
    }

   Vector3 GetMousePos()
    {
   
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        //���콺 ��ġ �޾Ƽ� �������
        DirecAngle = Mathf.Atan2( hit.point.x - Direction.transform.position.x,
            hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;

        mouseVector = hit.point;

        return hit.point;
    }

    void GetRdirect()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);

        R_DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
        hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;
    }
}

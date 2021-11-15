using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_W : MonoBehaviour
{
    Animator animator;

    public bool isSkillon = false;

    [Header("Q_Skill")]
    [SerializeField] private GameObject flame;

    [Header("W_Skill")]
    [SerializeField] private GameObject aura;
    [SerializeField] private GameObject shield;
    private float W_cooldown = 7.0f; //6.75  6.75  6.25  6
    private float W_SpeedUp = 1.1f; // 1.15  1.20  1.25  1.30
    private float W_HP = 60;        // 95 135 165 200

    [Header("E_Skill")]
    [SerializeField] private GameObject Direction;
    [SerializeField] private GameObject grenade_L;
    [SerializeField] private GameObject grenade_R;
    [SerializeField] private Transform grenade_Bomb;
    protected float DirecAngle; //eŰ ���Ⱒ��
    private bool grenade_Left=true;


    [Header("R_Skill")] 
    [SerializeField] private GameObject missile;
    [SerializeField] private GameObject Range;
    [SerializeField] private GameObject RangeDirection;
    [SerializeField] private GameObject missile_target_effect;
    protected float R_DirecAngle; //rŰ ���Ⱒ��// protected Vector3 BombDirec;
    public float R_Distance=10.0f; //��Ÿ� �ۿ� �ִ°�� �̵��ؼ� ��ų �ߵ�
    private bool isR_ready; //r��ų �ߵ���������
    private Vector3 Rdirection; //r������ǥ ����

    //���콺 ��ǥ �����(�ӽ�)
    Vector3 mouseVector;

    void Start()
    {
        aura.SetActive(false);
        shield.SetActive(false);

        flame.SetActive(false);

        Direction.SetActive(false);
        grenade_L.SetActive(false);
        grenade_R.SetActive(false);

        missile.SetActive(false);
        Range.SetActive(false);
        RangeDirection.SetActive(false);
        //  missile_target_effect.SetActive(false);
        animator = GetComponent<Animator>();
        isR_ready = false;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && GetComponent<Player_Stats>().Helium >= 20)
        {
            isSkillon = true;
            if (animator.GetBool("W_ColD") == false)
                StartCoroutine("Active_W");
            animator.SetBool("W_ColD", true);
        }

        if (Input.GetKeyDown(KeyCode.Q) && GetComponent<Player_Stats>().Helium >= 20)
        {
            isSkillon = true;
            if (animator.GetBool("Q_ColD") == false)
                StartCoroutine("Active_Q");
            animator.SetBool("Q_ColD", true);
        }

        if (Input.GetKey(KeyCode.E) && GetComponent<Player_Stats>().Helium >= 20)
        {
            isSkillon = true;
            movingManager.Instance.PlayerClickedPos = transform.position; //��ġ����
            Direction.transform.position = Range.transform.position; //ĳ���Ͱ���� ȭ���̵�
            Direction.SetActive(true); //ȭ����� ���� -> ȭ�� active
            GetMousePos();  //���콺 ��ġ �޾ƿͼ� ���� �ٶ󺸰� �ϱ�
            Direction.transform.rotation = Quaternion.AngleAxis(DirecAngle, Vector3.up); //����setting
            movingManager.Instance.PlayerDirection = DirecAngle; //�÷��̾ ��������
        }
        if (Input.GetKeyUp(KeyCode.E))  //EŰ ���� ���� ��ų ����
        {
            Direction.SetActive(false);
            GetComponent<Player_Stats>().DropHe();

            if (grenade_Left)   //ùź
            {
                Transform grenadeTransform = Instantiate(grenade_Bomb, grenade_L.transform.position,
               Quaternion.identity); //��ź�߻� and transform ����

                Vector3 nextDir = new Vector3(mouseVector.x, grenade_L.transform.position.y, mouseVector.z);
                Vector3 shootDir = (nextDir - grenade_L.transform.position).normalized; //���콺��ǥ -�߻���ǥ

                grenadeTransform.GetComponent<PFX_ProjectileObject>().Setup(shootDir); //��ź�� ��������
                grenade_Left = false;
            }
            else        //��°ź
            {
                Transform grenadeTransform = Instantiate(grenade_Bomb, grenade_R.transform.position,
Quaternion.identity); //��ź�߻� and transform ����

                Vector3 nextDir = new Vector3(mouseVector.x, grenade_R.transform.position.y, mouseVector.z);
                Vector3 shootDir = (nextDir - grenade_R.transform.position).normalized; //���콺��ǥ -�߻���ǥ

                grenadeTransform.GetComponent<PFX_ProjectileObject>().Setup(shootDir); //��ź�� ��������
                grenade_Left = true;
            }


            if (animator.GetBool("E_ColD") == false)
            {
                StartCoroutine("Active_E");
            }
            animator.SetBool("E_ColD", true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isSkillon = true;
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
            Rdirection = Direction.transform.position;
            Range.SetActive(false);
            Direction.SetActive(false);
            if ((transform.position - Rdirection).magnitude > R_Distance)
            {//��Ÿ� �ܿ� ���� ��� �̵�
                movingManager.Instance.PlayerClickedPos = Rdirection;
                isR_ready = true;
            }
            else        //��Ÿ� ���� ��� �ٷ� ����
            {
                if (animator.GetBool("R_ColD") == false)
                    StartCoroutine("Active_R");
            }
        
        }
        if(isR_ready && (transform.position - Rdirection).magnitude <= R_Distance)
        {
            movingManager.Instance.PlayerClickedPos = transform.position; //�ڸ�����
            if (animator.GetBool("R_ColD") == false)
                StartCoroutine("Active_R");
            isR_ready = false;
        }
    }

    IEnumerator Active_Q()
    {
        while (true)
        {
            GetComponent<Player_Stats>().DropHe();
            yield return new WaitForSeconds(0.1f);
            flame.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            animator.SetBool("Q_ColD", false);
            yield return new WaitForSeconds(0.5f);
            flame.SetActive(false);

            break;
        }
        isSkillon = false;
    }
    IEnumerator Active_W()
    {
        while (true)
        {
            GetComponent<Player_Stats>().DropHe();
            aura.SetActive(true);
            shield.SetActive(true);

            if (GetComponent<Player_Stats>().isDanger)
            {
                GetComponent<Player_Stats>().MoveSpeed *= W_SpeedUp*1.5f;
                GetComponent<Player_Stats>().MaxHP += W_HP*1.5f;
                yield return new WaitForSeconds(1.5f);
                GetComponent<Player_Stats>().MoveSpeed *= 1 / (W_SpeedUp*1.5f);
                GetComponent<Player_Stats>().MaxHP -= W_HP * 1.5f;
            }
            else
            {
                GetComponent<Player_Stats>().MoveSpeed *= W_SpeedUp;
                GetComponent<Player_Stats>().MaxHP += W_HP;
                yield return new WaitForSeconds(1.5f);
                GetComponent<Player_Stats>().MoveSpeed *= 1 / W_SpeedUp;
                GetComponent<Player_Stats>().MaxHP -= W_HP;
            }




  
            yield return new WaitForSeconds(0.5f);
            aura.SetActive(false);
            shield.SetActive(false);
            animator.SetBool("W_ColD", false);
            break;
        }
        isSkillon = false;
    }
    IEnumerator Active_E()
    {
        while (true)
        {
            if (grenade_Left)
            {
                grenade_L.SetActive(true);

                yield return new WaitForSeconds(0.5f);
                grenade_L.SetActive(false);
                animator.SetBool("E_ColD", false);
            }
            else
            {
                grenade_R.SetActive(true);

                yield return new WaitForSeconds(0.5f);
                grenade_R.SetActive(false);
                animator.SetBool("E_ColD", false);
            }
            break;
        }
        isSkillon = false;
    }
    IEnumerator Active_R()
    {
        while (true)
        {
                animator.SetBool("R_ColD", true);
                missile.SetActive(true);
                yield return new WaitForSeconds(0.67f);
                missile.SetActive(false);
                animator.SetBool("R_ColD", false);
                break;
        }
        while (true)
        {

            missile_target_effect.transform.position =
                new Vector3(Rdirection.x, 12.5f, Rdirection.z);
            //�̻��� Ÿ�� ��ġ

           GameObject Missile = Instantiate(missile_target_effect, missile_target_effect.transform.position,
         Quaternion.AngleAxis(R_DirecAngle, Vector3.up)); //��ź�߻�

            yield return new WaitForSeconds(7.0f);
            GameObject.Destroy(Missile);
            break;
        }
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

    void GetRdirect()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);

        R_DirecAngle = Mathf.Atan2(hit.point.x - Direction.transform.position.x,
        hit.point.z - Direction.transform.position.z) * Mathf.Rad2Deg;
    }
}

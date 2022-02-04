using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    /// <summary>
    /// �ΰ���(Ŭ���̺�Ʈ)
    ///  ���콺 Ŀ�� ����
    ///  Ŭ���̺�Ʈ(�ִϸ��̼�)�߰�
    /// </summary> 
    public GameObject ping;//�� ��ġ�� ǥ��
    public GameObject ping_Map; //�̴ϸʿ� ��
    Vector3 PingPos;
    public float PingYpos = 1.5f; //���� ����

    [SerializeField] Texture2D cursorImg;
    Vector3 mousePosition;

    public GameObject rightClickAnimation; //��Ŭ�� �ִϸ��̼�
    Vector3 rightClickPos; //��Ŭ����ǥ

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; //Ŀ���� ȭ���������� �ʵ���
        //Alt + Q �Է½� ������ֵ��� �ص�
        cursorSet(cursorImg); //Ŀ������
 
    }

    void cursorSet(Texture2D tex)
    {
        float xspot, yspot;
        CursorMode mode = CursorMode.ForceSoftware;
        xspot = tex.width / 2;
        yspot = tex.height / 2;
        Vector2 hotSpot = new Vector2(xspot, yspot);
        Cursor.SetCursor(tex, hotSpot, mode);
    }


    void Update()
    {
        Ping_Alert(); //���콺 Ŭ����ġ �ʿ� ǥ��

        //�ӽ� Ŀ������ (���ӽ�ũ������ ��������)
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
         {
            Cursor.lockState = CursorLockMode.None;
         }


    }
    private void LateUpdate()
    {
        StartCoroutine("RightClickedEvent");
    }
    IEnumerator RightClickedEvent()
    {
       if(Input.GetMouseButtonDown(1))
        { 
            while (true)
            {
                rightClickPos = movingManager.Instance.PlayerClickedPos;
                GameObject obj = Instantiate(rightClickAnimation, rightClickPos, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                Destroy(obj);
                break;
            }
        }
    }
    void Ping_Alert()
    {

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(0)) //Alt + leftMouseButton Clicked Event
        {
            PingPos = Input.mousePosition; //��ũ����ǥ
            Active_Ping_Position(); //��ġ ��Ÿ���� �ִϸ��̼� 
        }

    }


    void Active_Ping_Position()
    {

       // Debug.Log("�� ���� x : " + PingPos.x + ",  y : " + PingPos.y);
        StartCoroutine("Ping_Spawn");
    }
    IEnumerator Ping_Spawn()
    {
        while (true)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                PingPos = hit.point;
                PingPos.y = PingYpos;
                GameObject obj = Instantiate(ping, PingPos, Quaternion.identity);
                
                GameObject obj_Map = Instantiate(ping_Map, PingPos,Quaternion.Euler(90.0f,0,0));
                yield return new WaitForSeconds(3.0f);
                Destroy(obj);
                Destroy(obj_Map);
            }
            break;
        }
    }
}
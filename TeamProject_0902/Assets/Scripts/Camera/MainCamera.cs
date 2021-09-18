using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCamera : MonoBehaviour
{
    public GameObject ping;//�� ��ġ�� ǥ��
    Vector3 PingPos;

    Camera mainCamera;

  

    float defaultZoom;

    [SerializeField] Texture2D cursorImg;
    Vector3 mousePosition; 
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Confined; //Ŀ���� ȭ���������� �ʵ���
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
        // StartCoroutine("MoveCamera"); //�÷��̾� �����ӿ� ������ ���� ī�޶� �̵�

        ZoomCamerabyWheel(); //���콺 �ٿ� ���� ī�޶� ����/�ƿ�
        Ping_Alert(); //���콺 Ŭ����ġ �ʿ� ǥ��
        MoveCamerabyMouse(); //���콺�� ��ũ���� ����� ��ο� ���� ī�޶� �̵�
        
    }

    void MoveCamerabyMouse()
    {
   
         mousePosition = Input.mousePosition;   //���콺 ��ġ�� �޾Ƽ� ��ũ���� �����ڸ��� ���� �̺�Ʈ 

        if (mousePosition.x <= 0)
        {
            transform.Translate(-0.05f, 0.0f, 0.0f);
            Debug.Log("Left");
        }
        else if (mousePosition.x >= Screen.width - 5)
        {
            transform.Translate(0.05f, 0.0f, 0.0f);
            Debug.Log("Right");
        }

        if (mousePosition.y <= 5)
        {
            transform.position += Vector3.back * 0.03f;
         //   transform.Translate(0.0f, 0.0f, -0.05f);
            Debug.Log("Down");
        }
        else if (mousePosition.y >= Screen.height - 1)
        {
            transform.position += Vector3.forward * 0.03f;
            
           // transform.Translate(0.0f, 0.0f, 0.05f);
            Debug.Log("Up");
        }
      //  Debug.Log(mousePosition.x + " " + mousePosition.y);
    }

    

    void Ping_Alert()
    {
        
        if(Input.GetKey(KeyCode.LeftAlt)&&Input.GetMouseButtonDown(0)) //Alt + leftMouseButton Clicked Event
        {
            PingPos.x = Input.mousePosition.x;
            PingPos.z = Input.mousePosition.y;
            PingPos.y = 1.8f;
            Active_Ping_Position(); //��ġ ��Ÿ���� �ִϸ��̼� 
        }
        
    }


    void Active_Ping_Position()
    {
        
        Debug.Log("�� ���� x : " + PingPos.x + ",  y : " + PingPos.z );
        StartCoroutine("Ping_Spawn");
    }
    IEnumerator Ping_Spawn()
    {
        while (true)
        {
            GameObject obj = Instantiate(ping, PingPos, transform.rotation);
            yield return new WaitForSeconds(3.0f);
            DestroyObject(obj);

            break;
        }
    }

    void ZoomCamerabyWheel()
    {
        if (!mainCamera)
        {
            Debug.Log("No cam");
        }
        else
        {
            mainCamera.fieldOfView += (20 * Input.GetAxis("Mouse ScrollWheel"));
        }
    }


    //�÷��̾ ������ ���� ī�޶� (���x)
    //IEnumerator MoveCamera()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //        transform.position = player.transform.position + new Vector3(-0.2f, 10f, -7f);
    //        break;
    //    }
    //}


}

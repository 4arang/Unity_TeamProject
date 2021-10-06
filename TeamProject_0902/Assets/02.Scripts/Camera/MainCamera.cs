using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCamera : MonoBehaviour
{
    public GameObject ping;//핑 위치를 표시
    Vector3 PingPos;

    Camera mainCamera;

  

    float defaultZoom;

    [SerializeField] Texture2D cursorImg;
    Vector3 mousePosition; 
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Confined; //커서가 화면을나가지 않도록
        cursorSet(cursorImg); //커서세팅

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
        // StartCoroutine("MoveCamera"); //플레이어 움직임에 오프셋 맞춰 카메라 이동

        ZoomCamerabyWheel(); //마우스 휠에 따라 카메라 줌인/아웃
        Ping_Alert(); //마우스 클릭위치 맵에 표시
        MoveCamerabyMouse(); //마우스가 스크린을 벗어나는 경로에 따른 카메라 이동
        
    }

    void MoveCamerabyMouse()
    {
   
         mousePosition = Input.mousePosition;   //마우스 위치를 받아서 스크린의 가장자리에 가면 이벤트 

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
            Active_Ping_Position(); //위치 나타내는 애니메이션 
        }
        
    }


    void Active_Ping_Position()
    {
        
        Debug.Log("핑 찍힘 x : " + PingPos.x + ",  y : " + PingPos.z );
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


    //플레이어에 오프셋 맞춘 카메라 (사용x)
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

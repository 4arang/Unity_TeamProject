using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    /// <summary>
    /// 핑관련(클릭이벤트)
    ///  마우스 커서 관련
    ///  클릭이벤트(애니메이션)추가
    /// </summary> 
    public GameObject ping;//핑 위치를 표시
    public GameObject ping_Map; //미니맵용 핑
    Vector3 PingPos;
    public float PingYpos = 1.5f; //핑의 높이

    [SerializeField] Texture2D cursorImg;
    Vector3 mousePosition;

    public GameObject rightClickAnimation; //우클릭 애니메이션
    Vector3 rightClickPos; //우클릭좌표

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; //커서가 화면을나가지 않도록
        //Alt + Q 입력시 벗어날수있도록 해둠
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
        Ping_Alert(); //마우스 클릭위치 맵에 표시

        //임시 커서세팅 (게임스크린에서 나가도록)
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
            PingPos = Input.mousePosition; //스크린좌표
            Active_Ping_Position(); //위치 나타내는 애니메이션 
        }

    }


    void Active_Ping_Position()
    {

       // Debug.Log("핑 찍힘 x : " + PingPos.x + ",  y : " + PingPos.y);
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
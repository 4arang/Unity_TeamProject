using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class MapCamera : MonoBehaviour
{


    [SerializeField]
    Camera cam; //�̴ϸ�ķ 
    [SerializeField]
    GameObject camToMove; // ���θ�ķ ���� ����?
    [SerializeField]
    GameObject PlayerToMove; //�÷��̾� ���ΰ���
    NavMeshAgent agent;
    RaycastHit hit;
    Ray ray;

    [SerializeField]
    LayerMask mask;

    Vector3 movePoint;
    float YPos;
    [SerializeField]
    float offset;

    public GameObject Player2Dsprite;

    void Start()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
        if (PlayerToMove == null)
        {
            PlayerToMove = GameObject.FindWithTag("Player");
            agent = PlayerToMove.GetComponentInChildren<NavMeshAgent>();
        }
    }


    void Update()
    {

        if (IspointerOverUiObject())
        {
            if (Input.GetMouseButton(0))
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) //��Ŭ���� ����ī�޶� �̵�
                {
                    YPos = camToMove.transform.position.y; 

                    movePoint = new Vector3(hit.point.x, YPos, hit.point.z - offset);
                    camToMove.transform.position = movePoint;

                }
            }
        }

        if (IspointerOverUiObject())
        {
            if (Input.GetMouseButtonDown(1)) //��Ŭ���� ĳ���� �̵�
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
                movingManager.Instance.PlayerClickedPosMiniMap = Input.mousePosition; //�̴ϸʻ� Ŭ���� ����
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    YPos = PlayerToMove.transform.position.y;

                    movePoint = new Vector3(hit.point.x, YPos, hit.point.z - offset);
                    movingManager.Instance.PlayerClickedPos = movePoint;
                    movingManager.Instance.ClickedOnMinimap = true;
                   //agent.SetDestination(movePoint);

                }
            }
        }

        Line_PlayertoClick();
    }

    void Line_PlayertoClick()
    {
        if(movingManager.Instance.ClickedOnMinimap)
        {
            //Vector3 ClickonMinimap = movingManager.Instance.PlayerClickedPosMiniMap;
            //Vector3 PlayerImg = 
            //DrawLine();
        }
        movingManager.Instance.ClickedOnMinimap = false;    //�̴ϸ�Ŭ�� ����
    }

    private bool IspointerOverUiObject()    //ui Ŭ���̺�Ʈ ����
    {
        PointerEventData EventDataCurrentPosition = new PointerEventData(EventSystem.current);
        EventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(EventDataCurrentPosition, result);
        return result.Count > 0;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Return : MonoBehaviour
{

    [SerializeField] private GameObject Return_Aura;
    [SerializeField] public Camera MainCamera;
    Vector3 CameraOrgPos;
    public float Return_skillTime = 6.0f;
    public float Return_coolTime = 10.0f;
    private bool isFree;
    private bool Return_isReady;
    private Vector3 StartPoint;


    public bool TeamColor; //true = blue, false = red

    void Start()
    {
        //CameraOrgPos = MainCamera.transform.position;
        Return_isReady = true;
        isFree = true; //인게임 매니저에서 관리해야 할듯
        StartPoint = new Vector3(0, 0, 0);
        Return_Aura.SetActive(false);

        if (transform.position.x < 0) TeamColor = true;
        else TeamColor = false; //decided by start position
    }

    void Update()
    {
        if(!movingManager.Instance.isFree)
        {
            Return_Aura.SetActive(false);
            StopCoroutine("Active_B"); //전투모드일 경우 스킬실패
        }
        if (Input.GetKeyDown(KeyCode.B) && Return_isReady && isFree)
        {
            Return_Aura.SetActive(true);
            StartCoroutine("Active_B");
            StartCoroutine("CoolDown_B");
        }
    }

    IEnumerator Active_B()
    {
        while (true) //비전투모드인 경우에 귀환스킬 사용가능
        {


            yield return new WaitForSeconds(Return_skillTime);
            MainCamera.transform.position = CameraOrgPos; //카메라 위치 조정
            transform.position = StartPoint;
            movingManager.Instance.PlayerClickedPos = StartPoint; //귀환후 움직임 고정
            yield return new WaitForSeconds(1.5f); //이펙트 유지
            Return_Aura.SetActive(false);
            break;
        }
    }

    IEnumerator CoolDown_B()
    {
        Return_isReady = false;
        yield return new WaitForSeconds(Return_coolTime);
        Return_isReady = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingManager : MonoBehaviour
{
    /////��Ŭ���� �÷��̾� �̵� ��ǥ/////
    public Vector3 PlayerClickedPos; //player right clicked location
    public Vector3 PlayerClickedPosMiniMap; //player right clicked location by minimap
    public bool ClickedOnMinimap = false;
    /////////////////////////////////////
    /// �÷��̾� ������ǥ
    public float PlayerDirection;
    public Vector3 PlayerTargetPos; //�÷��̾� Ÿ����ǥ
    /// �÷��̾� ��������
    public bool isFree;

    private bool TeamColor;

    public GameObject UIPrefab;




    private static movingManager sInstance;

    public static movingManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("movingManager");
                sInstance = newGameObj.AddComponent<movingManager>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Die(Transform player)
    { 
        StartCoroutine("Respawn", player);
        TeamColor = player.GetComponent<Player_Stats>().TeamColor;
    }

    IEnumerator Respawn(Transform player)
    {
        
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        player.gameObject.SetActive(true);
        if (TeamColor)
        {
            player.position = new Vector3(-70, 0, 0);
            movingManager.Instance.PlayerClickedPos = player.position;
        }
        else
        {
            player.position = new Vector3(70, 0, 0);
            movingManager.Instance.PlayerClickedPos = player.position;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ycManager : MonoBehaviour
{
    /////우클릭시 플레이어 이동 좌표/////
    public Vector3 PlayerClickedPos; //player right clicked location
    public Vector3 PlayerClickedPosMiniMap; //player right clicked location by minimap
    public bool ClickedOnMinimap = false;
    /////////////////////////////////////
    /// 플레이어 방향좌표
    public float PlayerDirection;
    public Vector3 PlayerTargetPos; //플레이어 타겟좌표
    /// 플레이어 전투여부
    public bool isFree;




    private static ycManager sInstance;
    public static ycManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("ycManager");
                sInstance = newGameObj.AddComponent<ycManager>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
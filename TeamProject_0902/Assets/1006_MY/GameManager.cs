using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    #region SINGLETON
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<GameManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<GameManager>();        //배포 시, 활성화
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<GameManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public float GameTime;
    public int CurrentPlayerID = 0;
    public List<TeamManager> Teams;
    private bool CheckAllPlayerLoadedLevel()        //Get Hash table CustomProperties
    {
        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(GameConsts.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool)playerLoadedLevel)
                {
                    continue;
                }
            }

            return false;
        }

        return true;
    }

    void Update()
    {
        GameTime += Time.deltaTime;
    }


    #region PUN CALLBACKS
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("On Player Entered Room()" + newPlayer.NickName);

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("On Player Entered room is MasterClient {0}", PhotonNetwork.IsMasterClient);

            Debug.Log("다른 플레이어의 아바타를 추가적으로 생성하는 코드를 실행한다");
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player leftPlayer)
    {
        Debug.Log("OnPlayerLeftRoom() " + leftPlayer.NickName);     // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom            

            Debug.Log("나간 플레이어에 대한 처리를 실행한다");
        }        
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        //연결이 끊어지면 로드할 씬
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Launcher");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
        }
    }
    #endregion

}

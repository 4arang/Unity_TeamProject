using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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

    public Text InfoText;
    public float GameTime;
    public int CurrentPlayerID = 0;
    public List<TeamManager> Teams;

    public override void OnEnable()
    {
        base.OnEnable();
    }
    public void Start()
    {

    }

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


    #region START GAME SETTING

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        //characterValue = whichCharacter;

        //myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter],
        //    transform.position,
        //    transform.rotation);

        //myLobbyCharacter.transform.SetParent(NetworkManager.Instance.RoomPanel.transform);
        //Debug.Log(myCharacter.GetComponent<Transform>());
    }

    #endregion

    #region PUN CALLBACKS

    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Launcher");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            //Additional Part.
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        //CheckEndOfGame();
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey(GameConsts.PLAYER_SPELL1))
        {
            return;
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }


        // if there was no countdown yet, the master client (this one) waits until everyone loaded the level and sets a timer start
        int startTimestamp;
        bool startTimeIsSet 
            = Photon.Pun.UtilityScripts.CountdownTimer.TryGetStartTime(out startTimestamp);

        if (changedProps.ContainsKey(GameConsts.PLAYER_LOADED_LEVEL))
        {
            if (CheckAllPlayerLoadedLevel())
            {
                if (!startTimeIsSet)
                {
                    Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
                }
            }
            else
            {
                // not all players loaded yet. wait:
                Debug.Log("setting text waiting for players! ", this.InfoText);
                InfoText.text = "Waiting for other players...";
            }
        }

    }

    #endregion

}

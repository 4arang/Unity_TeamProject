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
    public static GameManager Instance;

    public Text InfoText;

    public float GameTime;
    public int CurrentPlayerID = 0;
    public List<TeamManager> Teams;


    private void Awake()
    {
        if (Instance != this)
            Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("GameManager OnEnable");
        StartGame();
        //Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
    }
    public void Start()
    {
        //Hashtable props = new Hashtable
        //{
        //        { GameConsts.PLAYER_LOADED_LEVEL, true },
        //        { GameConsts.PLAYER_CHAMPION,PlayerData.Instance.userChamp },
        //        { GameConsts.PLAYER_SPELL1, PlayerData.Instance.userDspell },       //Default
        //        { GameConsts.PLAYER_SPELL2, PlayerData.Instance.userDspell }
        //};    //Defualt

        //PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }
    public override void OnDisable()
    {
        base.OnDisable();

        //Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
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
    //private void OnCountdownTimerIsExpired()
    //{
    //    StartGame();
    //}
    private void StartGame()
    {
        Debug.Log("StartGame!!");
        //SpawningPlayer();
    }
    void SpawningPlayer()
    {
        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, points.Length);

        PhotonNetwork.Instantiate(Path.Combine("Champion", 
            PlayerData.Instance.userChamp.ToString()),
            points[idx].position, points[idx].rotation, 0);

        Debug.Log($"Player Character={PlayerData.Instance.userChamp} Spawn at {points[idx].position}");
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

    public static int GetTeamKills(int i)
    {
        int amount = 0;
        //foreach (ChampionStats champ in Instance.Team[i].Champions)
        //{
        //    amount += champ.Kills;
        //}
        return amount;
    }
}

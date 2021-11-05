using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public int nextPlayersTeam;
    public Transform[] redLobbySpawnPoints;
    public Transform[] blueLobbySpawnPoints;

    public Transform[] redSpawnPoints;
    public Transform[] blueSpawnPoints;


    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        //SceneManager.LoadScene()
    }
    public void UpdateTeam()
    {
        if (nextPlayersTeam == 1)
        {
            nextPlayersTeam = 2;
        }
        else
        {
            nextPlayersTeam = 1;
        }
    }

    #region GAMESETTING
    
    #endregion
}

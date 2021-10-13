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
    public Transform[] redTeamSpawnPoints;
    public Transform[] blueTeamSpawnPoints;

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
        if (nextPlayersTeam == 0)
        {
            nextPlayersTeam = 1;
        }
        else
        {
            nextPlayersTeam = 0;
        }

    }
}

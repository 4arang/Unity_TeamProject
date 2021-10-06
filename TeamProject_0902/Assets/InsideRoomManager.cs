using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
public class InsideRoomManager : MonoBehaviour
{
    public static InsideRoomManager RoomManager;

    public Transform[] spawnBlueTeam;
    public Transform[] spawnRedTeam;

    public Text playerNameDisplay;
    public int nextPlayersTeam;

    void Start()
    {
        if(InsideRoomManager.RoomManager==null)
        {
            InsideRoomManager.RoomManager = this;
        }
    }

    public void DisconnectPlayer()
    {
        //StartCoroutine(DisconnectAndLoad);
    }
    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
    }
    
    public void UpdateTeam()
    {
        if(nextPlayersTeam==1)
        {
            nextPlayersTeam = 2;
        }
        else
        {
            nextPlayersTeam = 1;
        }
    }
}

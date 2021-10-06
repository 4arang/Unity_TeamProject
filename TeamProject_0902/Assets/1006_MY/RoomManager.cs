using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public Transform[] redSpawnPoints;
    public Transform[] blueSpawnPoints;

    public int nextPlayerTeamId=1;

    Photon.Realtime.Player player;
    
    private void Awake()
    {
        player = PhotonNetwork.LocalPlayer;
        if (player == null)
        {
            Debug.Log("player Null" + player.ActorNumber);
            return;
        }
        else
        {
            SpawnPlayer();
        }        
    }

    private void Update()
    {
        PlayerSettingUpdate();
    }

    void PlayerSettingUpdate()
    {

    }
    void SpawnPlayer()
    {       
  
        //Get Team Spawn Position
        redSpawnPoints = GameObject.Find("RedSpawnGroup").GetComponentsInChildren<Transform>();
        blueSpawnPoints = GameObject.Find("BlueSpawnGroup").GetComponentsInChildren<Transform>();
        
        
        if (nextPlayerTeamId==1)
        {
            int spawnPicker = Random.Range(0, redSpawnPoints.Length);;
            PhotonNetwork.Instantiate("PhotonPlayer",
                redSpawnPoints[spawnPicker].position,
                redSpawnPoints[spawnPicker].rotation,
                0);
            nextPlayerTeamId = 2;
            
            Debug.Log($"Spawn Player at {redSpawnPoints[spawnPicker].position}");
            
        }
        else
        {
            int spawnPicker = Random.Range(1, blueSpawnPoints.Length); ;
            PhotonNetwork.Instantiate("PhotonPlayer",
                blueSpawnPoints[spawnPicker].position,
                blueSpawnPoints[spawnPicker].rotation,
                0);
            nextPlayerTeamId = 1;
            Debug.Log($"Spawn Player at {blueSpawnPoints[spawnPicker].position}");
        }        
    }
}

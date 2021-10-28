using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public GameObject myLobbyAvatar;
    public int myTeam;
    private void Start()
    {
        Debug.Log(myTeam == 0 ? "Red team" : "Blue Team");
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }
    }
    private void Update()
    {
        #region
        if(myAvatar==null&&myTeam!=0)
        {
            if (myTeam == 1)
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.redSpawnPoints.Length); ;

                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
                               GameSetup.GS.redLobbySpawnPoints[spawnPicker].position,
                               GameSetup.GS.redLobbySpawnPoints[spawnPicker].rotation, 0);
                    Debug.Log("RedTeam Player Avatar Spawned");
                }
            }
            else
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.blueSpawnPoints.Length); ;
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
                               GameSetup.GS.blueLobbySpawnPoints[spawnPicker].position,
                               GameSetup.GS.blueLobbySpawnPoints[spawnPicker].rotation, 0);
                    Debug.Log("BlueTeam Player Avatar Spawned");
                }
            }
        }        
    }
    #endregion

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = GameSetup.GS.nextPlayersTeam;
        PlayerInfo.PI.myTeam = myTeam;
        GameSetup.GS.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }
}

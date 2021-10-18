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
        Debug.Log(myTeam == 1 ? "Red team" : "Blue Team");
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
                               GameSetup.GS.redSpawnPoints[spawnPicker].position,
                               GameSetup.GS.redSpawnPoints[spawnPicker].rotation, 0);
                }
            }
            else
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.blueSpawnPoints.Length); ;
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
                               GameSetup.GS.blueSpawnPoints[spawnPicker].position,
                               GameSetup.GS.blueSpawnPoints[spawnPicker].rotation, 0);
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

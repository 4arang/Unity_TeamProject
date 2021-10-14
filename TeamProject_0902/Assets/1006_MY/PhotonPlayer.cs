using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int myTeam;
    private void Start()
    {
        Debug.Log("포톤 플레이어 생성");
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
        if (myAvatar == null)
        {
            if (myTeam == 0)
            {
                int spawnPicker = 0;

                if (PhotonRoom.room.mynumberInRoom!=1)
                {
                    spawnPicker = Random.Range(0, GameSetup.GS.redTeamSpawnPoints.Length);
                }                
                
                Debug.Log("spawnPicker= " + spawnPicker);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
                        GameSetup.GS.redTeamSpawnPoints[spawnPicker].position,
                        GameSetup.GS.redTeamSpawnPoints[spawnPicker].rotation, 0);

                    Debug.Log($"Spawn at {GameSetup.GS.redTeamSpawnPoints[spawnPicker].position}");
                }
            }
            else
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.blueTeamSpawnPoints.Length);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
                    GameSetup.GS.blueTeamSpawnPoints[spawnPicker].position,
                    GameSetup.GS.blueTeamSpawnPoints[spawnPicker].rotation, 0);

                    Debug.Log($"Spawn at {GameSetup.GS.blueTeamSpawnPoints[spawnPicker].position}");
                }
            }
        }
        #endregion

    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = GameSetup.GS.nextPlayersTeam;
        GameSetup.GS.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }
}

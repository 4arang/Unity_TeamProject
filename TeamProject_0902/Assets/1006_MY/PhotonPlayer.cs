using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;

    public int myNumberInRoom;
    public int myTeam;

    Photon.Realtime.Player[] allPlayers;
    private void Start()
    {
        PV = GetComponent<PhotonView>();

        myNumberInRoom = PV.Owner.ActorNumber;

        if (PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }

        if (myAvatar == null && myTeam != 0)
        {
            if (PV.IsMine && PhotonRoom.room.currentScene == 0)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
                           PhotonRoom.room.spawnPoints[myNumberInRoom - 1].position,
                           PhotonRoom.room.spawnPoints[myNumberInRoom - 1].rotation, 0);

            }
        }
        Debug.Log("my number inroom " + myNumberInRoom);
    }
    private void Update()
    {
        //if (myAvatar == null && myTeam != 0)
        //{
        //    if (PV.IsMine && PhotonRoom.room.currentScene == 0)
        //    {
        //        myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
        //                   PhotonRoom.room.spawnPoints[myNumberInRoom - 1].position,
        //                   PhotonRoom.room.spawnPoints[myNumberInRoom - 1].rotation, 0);
        //        LobbyController.Instance.playerNumber = myNumberInRoom;
        //    }
        //}
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = PhotonRoom.room.nextPlayersTeam;
        PlayerInfo.PI.myTeam = myTeam;
        Debug.Log("myTeam=" + myTeam);
        PhotonRoom.room.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }
}

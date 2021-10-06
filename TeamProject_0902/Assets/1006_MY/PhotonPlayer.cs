using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;

    public ChampionDatabase curChampion;
    public GameObject myChampion;
    public Image myChampionImage;           //Random Image Output value

    public int myTeam;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }

        //champion = Random.Range(ChampionDatabase.Instance.currentChampion);
       
    }
    void Update()
    {
        if(myChampion==null&&myTeam!=0)
        {
            if (myTeam == 1)
            {
                int spawnPicker = Random.Range(0, InsideRoomManager.RoomManager.spawnRedTeam.Length);
                if (PV.IsMine)
                {
                    myChampion = PhotonNetwork.Instantiate(Path.Combine("LobbyPlayer", "PlayerAvatar"),
                        InsideRoomManager.RoomManager.spawnRedTeam[spawnPicker].position,
                        InsideRoomManager.RoomManager.spawnRedTeam[spawnPicker].rotation, 0);
                }
            }
            if (myTeam == 2)
            {
                int spawnPicker = Random.Range(0, InsideRoomManager.RoomManager.spawnBlueTeam.Length);
                if (PV.IsMine)
                {
                    myChampion = PhotonNetwork.Instantiate(Path.Combine("LobbyPlayer", "PlayerAvatar"),
                        InsideRoomManager.RoomManager.spawnBlueTeam[spawnPicker].position,
                        InsideRoomManager.RoomManager.spawnBlueTeam[spawnPicker].rotation, 0);
                }
            }
        }
        
    }

    public void GetRandomChampion()
    {
        //Initialzize
        curChampion = null;
        myChampionImage = null;
        myChampion = null;
    }
    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = InsideRoomManager.RoomManager.nextPlayersTeam;
        InsideRoomManager.RoomManager.UpdateTeam();

        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered,myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }
}

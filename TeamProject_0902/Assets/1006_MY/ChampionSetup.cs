using Photon.Pun;
using System;
using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class ChampionSetup : MonoBehaviour
{
    private PhotonView PV;

    //GameScene Avatar;
    public GameObject myCharacter;

    //LobbyScene Avatar;
    public GameObject myLobbyCharacter;
  
    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom && myLobbyCharacter == null)
        {
            if (PV.IsMine && PhotonRoom.room.currentScene == 0) //Lobby Spawn
            {
                PV.RPC("RPC_AddLobbyCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedChampion);
            }

            if (PV.IsMine && PhotonRoom.room.currentScene == 1 && myCharacter == null)   //InGame Spawn
            {
                PV.RPC("RPC_AddGameCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedChampion);
            }
        }
    }

    [PunRPC]
     void RPC_AddLobbyCharacter(int whichCharacter)
    {
        myLobbyCharacter=Instantiate(GameDataSource.Instance.m_CharacterData[whichCharacter].LobbyAvatar,
           PhotonRoom.room.spawnPoints[PhotonRoom.room.mynumberInRoom].position,
           transform.rotation);

        myLobbyCharacter.transform.SetParent(GameObject.Find("Room Panel").transform);
    }

    [PunRPC]
    void RPC_AddGameCharacter(int whichCharacter)
    {
        Destroy(myLobbyCharacter);

        myCharacter = Instantiate(GameDataSource.Instance.m_CharacterData[whichCharacter].InGameAvatar,
           transform.position,
           transform.rotation);
    }
}

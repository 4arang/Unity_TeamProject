using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChampionSetup : MonoBehaviour
{
    private PhotonView PV;
    public int characterValue;

    //GameScene Avatar;
    public GameObject myCharacter;

    //LobbyScene Avatar;
    public GameObject myLobbyCharacter;

    //Stats
    //public int playerHealth;
    //public int playerDamage;

    //public Camera myCamera;
    //public AudioListener myAL;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            //PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedChampion);
            PV.RPC("RPC_AddLobbyCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedLobbyChampion);
        }
        else
        {
            //Destroy(myCamera);
            //Destroy(myAL);
        }

        if(NetworkManager.Instance.currentScene== NetworkManager.Instance.multiplayScene)
        {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedChampion);
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;

        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter],
            transform.position,
            transform.rotation);

        myLobbyCharacter.transform.SetParent(NetworkManager.Instance.RoomPanel.transform);
        Debug.Log(myCharacter.GetComponent<Transform>());
    }

    [PunRPC]
    void RPC_AddLobbyCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;       

        myLobbyCharacter = Instantiate(PlayerInfo.PI.allLobbyCharacters[whichCharacter],
           transform.position,
           transform.rotation);

        myLobbyCharacter.transform.SetParent(NetworkManager.Instance.RoomPanel.transform);
        Debug.Log(myLobbyCharacter.GetComponent<Transform>());
    }
}

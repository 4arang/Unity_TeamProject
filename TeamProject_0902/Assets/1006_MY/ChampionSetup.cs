using Photon.Pun;
using System;
using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChampionSetup : MonoBehaviour
{
    private PhotonView PV;
    public int characterValue;
    public Text playerNickname;
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
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedChampion);
        }
    }

    private void Update()
    {
        if (PhotonRoom.room.currentScene == PhotonRoom.room.multiplayScene)
        {
            Debug.Log("Update Character");

            if (myCharacter.activeSelf==false)
            {
                myCharacter.SetActive(true);
                myLobbyCharacter.SetActive(false);
            }  
        }
    }
    [PunRPC]
     void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;

        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter],
           transform.position,
           transform.rotation);
        myLobbyCharacter = Instantiate(PlayerInfo.PI.allLobbyCharacters[whichCharacter],
            transform.position,
            transform.rotation);

        myCharacter.SetActive(false);

        if(PhotonRoom.room.currentScene!=PhotonRoom.room.multiplayScene)
        {
            myLobbyCharacter.transform.SetParent(NetworkManager.Instance.RoomPanel.transform);
        }
        
        playerNickname.text = NetworkManager.Instance.playerNickname.ToString();
        Debug.Log("PlayerNickname=" + NetworkManager.Instance.playerNickname.ToString());
    }
}

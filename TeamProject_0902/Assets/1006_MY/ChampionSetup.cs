using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionSetup : MonoBehaviour
{
    private PhotonView PV;
    public int characterValue;
    public GameObject myCharacter;


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
        else
        {
            //Destroy(myCamera);
            //Destroy(myAL);
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter],
            transform.position,
            transform.rotation);
        Debug.Log(myCharacter.GetComponent<Transform>());
    }
}

using Photon.Pun;
using UnityEngine;
using System.IO;

public class TestSetup : MonoBehaviour
{
    private PhotonView PV;
    public int characterValue;
    public GameObject myCharacter;

    //Stats
    public int playerHealth;
    public int playerDamage;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, TestInfo.PI.mySelectedChampion);
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;

        switch(characterValue)
        {
            case 0: //Baek
                {
                    myCharacter = PhotonNetwork.Instantiate(Path.Combine("Champion", "BaekRang"),
                                  transform.position,
                                  transform.rotation);
                    break;
                }
            case 1: //ColD
                {
                    myCharacter = PhotonNetwork.Instantiate(Path.Combine("Champion", "ColD"),
                                  transform.position,
                                  transform.rotation);
                    break;
                }
            case 2: //Xerion
                {
                    myCharacter = PhotonNetwork.Instantiate(Path.Combine("Champion", "Xerion"),
                                 transform.position,
                                 transform.rotation);
                    break;
                }
        }

        myCharacter.transform.SetParent(this.gameObject.transform);
    }
}

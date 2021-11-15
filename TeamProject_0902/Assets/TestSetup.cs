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
            AddCharacter(TestInfo.PI.mySelectedChampion, TestInfo.PI.myTeam);
        }
    }

    void AddCharacter(int whichCharacter, int whichTeam)
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

        if(whichTeam==1)
        {
            GameObject.Find("Red Team").GetComponent<TeamManager>().Champions.Add(myCharacter);
        }
        else
        {
            GameObject.Find("Blue Team").GetComponent<TeamManager>().Champions.Add(myCharacter);
        }
    }
}

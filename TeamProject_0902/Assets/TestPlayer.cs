using Photon.Pun;
using UnityEngine;
using System.IO;

public class TestPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int myTeam;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }
    }
    private void Update()
    {
        #region
        if (myAvatar == null && myTeam != 0)
        {
            if (myTeam == 1)
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.redSpawnPoints.Length);
                Debug.Log("spawnPicker= " + spawnPicker);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
                        GameSetup.GS.redSpawnPoints[spawnPicker].position,
                        GameSetup.GS.redSpawnPoints[spawnPicker].rotation, 0);
                }
            }
            else
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.blueSpawnPoints.Length);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PlayerAvatar"),
                    GameSetup.GS.blueSpawnPoints[spawnPicker].position,
                    GameSetup.GS.blueSpawnPoints[spawnPicker].rotation, 0);
                }
            }
        }
        #endregion

    }

    [PunRPC]
    void RPC_GetTeam()
    {
        Debug.Log("Team Setting ");

        GameSetup.GS.UpdateTeam();
        myTeam = GameSetup.GS.nextPlayersTeam;

        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }
}

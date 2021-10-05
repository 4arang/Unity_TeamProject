using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
	public static GameManager Instance = null;

	public GameObject PlayerPrefabs;            //set random champion

    #region UNITY

    public void Awake()
    {
        Instance = this;
    }

    #endregion

    #region PUN CALLBACKS

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }
    #endregion
}

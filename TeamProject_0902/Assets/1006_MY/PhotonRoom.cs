using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;
    public int multiplayScene;
    //Player Info
    Player[] photonPlayers;
    //public int playersInRoom;
    public int mynumberInRoom;

    [Header("Spell Settings")]
    public GameObject spellSelectBox;
    public int currentSpellBtn = 0;

    //public int playersInGame;

    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiplayScene)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("CreatePlayer");
        PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }

    #region UI_BUTTONS CALLBACKS
    public void OnSpellSetBoxOpen(int whichBtn)
    {
        spellSelectBox.SetActive(true);
        currentSpellBtn = whichBtn;
    }
    public void OnSpellSetBoxClose()
    {
        spellSelectBox.SetActive(false);
    }

    #endregion
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PV.RPC("RPC_CreatePlayer", RpcTarget.All);
    }
}

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class TestRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static TestRoom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;
    public int multiplayScene;

    //Player Info
    public Photon.Realtime.Player[] photonPlayers;
    public int playersInRoom;
    public int mynumberInRoom;

    public int playersInGame;
    private void Awake()
    {
        if (TestRoom.room == null)
        {
            TestRoom.room = this;
        }
        else
        {
            if (TestRoom.room != this)
            {
                Destroy(TestRoom.room.gameObject);
                TestRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    public override void OnJoinedRoom()     //PhotonManger로 이동-> 변수들이 Room에선언되어있음
    {
        Debug.Log("Has Joined room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        mynumberInRoom = playersInRoom;
        PhotonNetwork.NickName = mynumberInRoom.ToString();
        StartGame();
    }

    void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(multiplayScene);
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
}

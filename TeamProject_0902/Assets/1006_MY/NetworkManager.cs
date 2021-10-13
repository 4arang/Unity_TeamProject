using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;        //get HashTable


public class NetworkManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static NetworkManager Instance;

    private readonly string version = "1.0";

    [Header("LoginPanel")]
    public GameObject LoginPanel;
    public InputField PlayerNameInput;

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public InputField RoomInput;
    public Text WelcomeText;
    public Text LobbyInfoText;
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Text ListText;
    public Text RoomInfoText;
    public Text[] ChatText;
    public InputField ChatInput;

    public GameObject LobbyPlayerPrefab;
    public Button StartGameButton;

    //Get custom Properties
    private Dictionary<int, GameObject> playerListEntries;

    [Header("Loading Panel")]
    private string SceneToLoad;
    [SerializeField]
    private Text ProgressText;
    [SerializeField]
    private Slider LoadingSlider;
    private AsyncOperation operation;

    [Header("ETC")]
    public Text StatusText;
    public PhotonView PV;
    public int currentScene;
    public int multiplayScene;

    [Header("Player Info")]
    Photon.Realtime.Player[] photonPlayers;
    public int playersInRoom;
    public int mynumberInRoom;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    #region Singleton
    private void Awake()
    {
        if (NetworkManager.Instance == null)
        {
            NetworkManager.Instance = this;
        }

        PhotonNetwork.GameVersion = version;
        Debug.Log(PhotonNetwork.SendRate);
    }
    #endregion

    #region RoomListUpdate

    // ◀버튼 -2 , ▶버튼 -1 , 셀 숫자
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        MyListRenewal();
    }

    void MyListRenewal()
    {
        // 최대페이지
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // 이전, 다음버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text
                = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text
                = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }

    #endregion

    #region UI CALLBACKS BUTTONS
    public void OnConnectedButton()
    {
        PhotonNetwork.ConnectUsingSettings();   //Join Photon Server
    }
    public void OnCreateRoomButton()
    {        
        if(string.IsNullOrEmpty(RoomInput.text))
        {
            RoomInput.text = "Room" + Random.Range(0, 100);
        }
        else
        {
            RoomOptions roomOptions = SetRoom();
            PhotonNetwork.CreateRoom(RoomInput.text, roomOptions);
            Debug.Log($"{RoomInput.text}is created");
        }
    }
    public void OnJoinRandomRoomButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void OnLeftRoomButton()
    {
        PhotonNetwork.LeaveRoom();

        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        LoginPanel.SetActive(false);
        RoomPanel.SetActive(false);

        foreach (GameObject entry in playerListEntries.Values)
        {
            Destroy(entry.gameObject);
        }

        playerListEntries.Clear();
        playerListEntries = null;
    }
    #endregion
    #region UI CALLBACKS
    public void LocalPlayerPropertiesUpdated()
    {
        //StartGameButton.gameObject.SetActive(CheckPlayersReady());
    }

    private bool CheckPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return false;
        }

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(GameConsts.PLAYER_READY, out isPlayerReady))
            {
                if (!(bool)isPlayerReady)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    #endregion
    #region PhotonNetwork

    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) 
            + "로비 / " + PhotonNetwork.CountOfPlayers + "접속";
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log($"Automatic Sync Scene={PhotonNetwork.AutomaticallySyncScene}");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.Inlobby={PhotonNetwork.InLobby}");

        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        LoginPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = PlayerNameInput.text;
        WelcomeText.text = PhotonNetwork.LocalPlayer.NickName + "님 환영합니다";
        myList.Clear();
    }

    public void Disconnect()
    {
        LoginPanel.SetActive(true);
        PhotonNetwork.Disconnect();
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        LoginPanel.SetActive(true);
    }

    #endregion

    #region Room
    public RoomOptions SetRoom()
    {
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 4;
        roomOption.IsOpen = true;       //Is private or public?
        roomOption.IsVisible = true;    //Visible on/off
        return roomOption;
    }

    public void StartGame() //Call by StartGame Button
    {
        Debug.Log("Game Start");
        LoadingSlider.enabled = true;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.LoadLevel("MapScene_Test");
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
        PhotonNetwork.Instantiate(Path.Combine("Champions", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PV.RPC("RPC_CreatePlayer", RpcTarget.All);
    }

    public override void OnJoinedRoom() //Callback Func when JoinedRoom
    {
        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);

        //Room Initialize
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        mynumberInRoom = playersInRoom;

        Debug.Log("Create Lobby Player");
        PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PhotonNetworkPlayer"), 
            transform.position, 
            Quaternion.identity, 0);
        RoomRenewal();        

        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";        
    }

    //Called when a remote player entered the room.This Player is already added to the playerlist.
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player player)   
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + player.NickName + "님이 참가하셨습니다</color>");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)     //Chat Alarm when new player leaved.
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");

        Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
        playerListEntries.Remove(otherPlayer.ActorNumber);

        //StartGameButton.gameObject.SetActive(CheckPlayersReady());
    }
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            //StartGameButton.gameObject.SetActive(CheckPlayersReady());
        }
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }

        GameObject entry;
        if (playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
        {
            object isPlayerReady;
            if (changedProps.TryGetValue(GameConsts.PLAYER_READY, out isPlayerReady))
            {
                entry.GetComponent<PlayerData>().SetPlayerReady((bool)isPlayerReady);
            }
        }

        //StartGameButton.gameObject.SetActive(CheckPlayersReady());
    }

    public override void OnCreateRoomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

    public override void OnJoinRandomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

    public void CreateRoom()
    {
        RoomOptions roomOption = SetRoom();        
        PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + Random.Range(0, 100) : RoomInput.text, roomOption);
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
    }

    #endregion

    #region PlayerSettings

    #endregion

    #region ChattingBox
    public void OnChatSend()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
        print(PhotonNetwork.NickName + " : " + ChatInput.text);
    }

    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    void ChatRPC(string msg)
    {
        bool isInput = false;

        for (int i = 0; i < ChatText.Length; i++)
        {
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion
}

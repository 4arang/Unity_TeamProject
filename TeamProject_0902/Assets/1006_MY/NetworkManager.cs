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
    public Text PlayerNameInput;

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public Text WelcomeText;


    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Text ListText;
    public Text RoomInfoText;
    public Text[] PlayerNickNames;
    public Text[] ChatText;
    public InputField ChatInput;

    public GameObject LobbyPlayerPrefab;
    public Button StartGameButton;

    [Header("ETC")]
    public int myActorNumber;
    public Text StatusText;
    public PhotonView PV;
    public int currentScene;
    public int multiplayScene;

    //List<RoomInfo> myList = new List<RoomInfo>();
    //int currentPage = 1, maxPage, multiple;

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
    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    #region RoomListUpdate
    //public void MyListClick(int num)
    //{
    //    if (num == -2) --currentPage;
    //    else if (num == -1) ++currentPage;
    //    else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
    //    MyListRenewal();
    //}

    //void MyListRenewal()
    //{
    //    // 최대페이지
    //    maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

    //    // 이전, 다음버튼
    //    PreviousBtn.interactable = (currentPage <= 1) ? false : true;
    //    NextBtn.interactable = (currentPage >= maxPage) ? false : true;

    //    // 페이지에 맞는 리스트 대입
    //    multiple = (currentPage - 1) * CellBtn.Length;
    //    for (int i = 0; i < CellBtn.Length; i++)
    //    {
    //        CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
    //        CellBtn[i].transform.GetChild(0).GetComponent<Text>().text
    //            = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
    //        CellBtn[i].transform.GetChild(1).GetComponent<Text>().text
    //            = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
    //    }
    //}
    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    int roomCount = roomList.Count;
    //    for (int i = 0; i < roomCount; i++)
    //    {
    //        if (!roomList[i].RemovedFromList)
    //        {
    //            if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
    //            else myList[myList.IndexOf(roomList[i])] = roomList[i];
    //        }
    //        else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
    //    }
    //    MyListRenewal();
    //}
    //public void OnCreateRoomButton()
    //{
    //    if (string.IsNullOrEmpty(RoomInput.text))
    //    {
    //        RoomInput.text = "Room" + Random.Range(0, 100);
    //    }
    //    else
    //    {
    //        RoomOptions roomOptions = SetRoom();
    //        PhotonNetwork.CreateRoom(RoomInput.text, roomOptions);
    //        Debug.Log($"{RoomInput.text}is created");
    //    }
    //}


    //public override void OnCreateRoomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

   
    #endregion

    #region UI CALLBACKS BUTTONS
    public void OnConnectedButton()
    {
        if (string.IsNullOrEmpty(PlayerNameInput.text))
        {
            Debug.Log("Please Input your Id");

            return;
        }
        PhotonNetwork.ConnectUsingSettings();   //Join Photon Server
    }

    public void OnJoinRandomRoomButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion
    #region UI CALLBACKS
    public void OnLeftRoomButton()
    {
        PhotonNetwork.LeaveRoom();

        LobbyPanel.SetActive(true);
        LoginPanel.SetActive(false);
        RoomPanel.SetActive(false);
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
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        LoginPanel.SetActive(true);
        PhotonNetwork.Disconnect();
    }

    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        LoginPanel.SetActive(false);
        PlayerPrefs.DeleteAll();            //PlayerPrefs Initialize

        PhotonNetwork.LocalPlayer.NickName = PlayerNameInput.text;
        PlayerPrefs.SetString("NickName",PlayerNameInput.text);        

        WelcomeText.text = PhotonNetwork.LocalPlayer.NickName;

        //Room panelmode
        //myList.Clear();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public void CreateRoom()
    {
        RoomOptions roomOption = SetRoom();
        PhotonNetwork.CreateRoom("Room" + Random.Range(0, 100), roomOption);
    }

    public RoomOptions SetRoom()
    {
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 4;
        roomOption.IsOpen = true;       //Is private or public?
        roomOption.IsVisible = true;    //Visible on/off
        return roomOption;
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PV.RPC("RPC_CreatePlayer", RpcTarget.All);
    }

    public override void OnJoinedRoom() //Callback Func when JoinedRoom
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            StartGameButton.gameObject.SetActive(false);
        }

        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);

        //Room Initialize
        Debug.Log($"PhotonNetwork.InRoom ={ PhotonNetwork.InRoom}");
        Debug.Log($"Player Count ={ PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
            myActorNumber = player.Value.ActorNumber;
        }

        PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PhotonNetworkPlayer"),
             transform.position, Quaternion.identity);

        //Champ Random Choice
        PlayerInfo.PI.RandomSelectMode();
        RoomRenewal();        

        //Chat Box Init
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
    }
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            //StartGameButton.gameObject.SetActive(CheckPlayersReady());
        }
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {            
            PlayerNickNames[i].text = PhotonNetwork.PlayerList[i].NickName;
            //if(PV.IsMine)
            //{
            //    if(i%2==0)
            //    LobbyController.Instance.playerTeam = true;
            //else
            //    LobbyController.Instance.playerTeam = false;
            //}
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
    }

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

using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
public class AoW_Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private Text feedbackText;
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [SerializeField]
    private LoaderAnimation loaderAnime;

    public InputField nameInput;
    bool isConnecting;
    string gameVersion = "1";

    private void Awake()
    {
        if (loaderAnime == null)
        {
            Debug.LogError("<Color=Red><b>Missing</b></Color> loaderAnime Reference.", this);
        }

        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    public void Connect()
    {
        // we want to make sure the log is clear everytime we connect, we might have several failed attempted if connection failed.
        feedbackText.text = "";
        if (nameInput.text=="")
        {
            Debug.LogError("Please Input your name");
            return;
        }
        else
        {
            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            isConnecting = true;

            // hide the Play button for visual consistency
            controlPanel.SetActive(false);
            // start the loader animation for visual effect.
            if (loaderAnime != null)
            {
                loaderAnime.StartLoaderAnimation();
            }

            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.IsConnected)
            {
                LogFeedback("Joining Room...");
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                PhotonNetwork.JoinLobby();
            }
            else
            {
                LogFeedback("Connecting...");

                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = this.gameVersion;
            }
        }       
    }

    void LogFeedback(string message)
    {
        // we do not assume there is a feedbackText defined.
        if (feedbackText == null)
        {
            return;
        }
        
        // add new messages as a new line and at the bottom of the log.
        feedbackText.text += System.Environment.NewLine + message;
        Debug.Log(message);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connecting to Master");
        if (isConnecting)
        {
            Debug.Log("Successfully Connected");
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        LogFeedback("<Color=Red>OnDisconnected</Color> " + cause);
        Debug.LogError("Aow launcher:Disconnected");

        // #Critical: we failed to connect or got disconnected. There is not much we can do. Typically, a UI system should be in place to let the user attemp to connect again.
        loaderAnime.StopLoaderAnimation();

        isConnecting = false;
        controlPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        LogFeedback("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");
        Debug.Log(nameInput.text+"has joined the Room");

        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("Lobby Scene");

            // #Critical
            // Load the Room Level. 
            PhotonNetwork.LoadLevel("LobbyScene");

        }
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
        Debug.Log(nameInput.text + "has joined the Lobby");

        base.OnJoinedLobby();
    }
    public override void OnCreatedRoom()
    {
        PhotonNetwork.LoadLevel("Room");
        Debug.Log("Create Room");

        base.OnCreatedRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.LoadLevel("Room");

        base.OnCreateRoomFailed(returnCode, message);
    }
}

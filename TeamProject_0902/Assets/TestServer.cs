using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestServer : MonoBehaviourPunCallbacks
{
    public GameObject battleButton;

    private readonly string version = "1.0";
    private string userId="TestUser";
    private void Awake()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            //포톤 서버 접속
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }
    private void Start()
    {
        PhotonNetwork.NickName = userId;
        PhotonNetwork.ConnectUsingSettings();
        battleButton.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        battleButton.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        battleButton.SetActive(false);

        Debug.Log($"<Color=Red><b>Missing</b></Color> Server Disconnected {cause}.");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)       //룸에 입장한 후 호출되는 콜백 함수
    {
        //룸 속성 정의
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        ro.IsOpen = true;       //룸 오픈 여부
        ro.IsVisible = true;    //로비에서 룸 목록 노출

        PhotonNetwork.CreateRoom("My room", ro);
    }

    //UI Callbacks
    public void OnBattleButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();

        if (TestInfo.PI != null)
        {
            TestInfo.PI.mySelectedChampion = 2;       //제리온으로 테스트
            PlayerPrefs.SetInt("MyCharacter", 2);
        }
    }

}

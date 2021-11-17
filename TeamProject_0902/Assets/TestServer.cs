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
            //���� ���� ����
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
    public override void OnJoinRandomFailed(short returnCode, string message)       //�뿡 ������ �� ȣ��Ǵ� �ݹ� �Լ�
    {
        //�� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        ro.IsOpen = true;       //�� ���� ����
        ro.IsVisible = true;    //�κ񿡼� �� ��� ����

        PhotonNetwork.CreateRoom("My room", ro);
    }

    //UI Callbacks
    public void OnBattleButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();

        if (TestInfo.PI != null)
        {
            TestInfo.PI.mySelectedChampion = 2;       //���������� �׽�Ʈ
            PlayerPrefs.SetInt("MyCharacter", 2);
        }
    }

}

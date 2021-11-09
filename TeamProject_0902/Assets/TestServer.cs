using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestServer : MonoBehaviourPunCallbacks
{
    public GameObject battleButton;

    private readonly string version = "1.0";
    private string userId;
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
        PhotonNetwork.ConnectUsingSettings();           //
        battleButton.SetActive(false);
    }
    private void Update()
    {
        if (PhotonNetwork.IsConnected)
            battleButton.SetActive(true);
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

        if (PlayerInfo.PI != null)
        {
            PlayerInfo.PI.mySelectedChampion = 0;       //������� �׽�Ʈ
            PlayerPrefs.SetInt("MyCharacter", 0);
        }
    }

}

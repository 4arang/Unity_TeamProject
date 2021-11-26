using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;
    public int multiplayScene=1;
    public int nextPlayersTeam=1;
    public int playerNumber;

    //public int playersInRoom;
    public int mynumberInRoom;

    [Header("Room Setting")]
    public Transform[] spawnPoints;

    [Header("Spell Settings")]
    public GameObject spellSelectBox;
    public int currentSpellBtn = 0;

    [Header("Loading Panel")]
    [SerializeField]
    private GameObject ChattingScrollView;
    [SerializeField]
    private GameObject LoadingPanel;
    [SerializeField]
    private Text ProgressText;
    [SerializeField]
    private Slider LoadingSlider;
    [SerializeField]
    private GameObject LoadingTextBox;


    //public int playersInGame;
    #region SINGLETON
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
    #endregion
    //public override void OnEnable()
    //{
    //    base.OnEnable();
    //    PhotonNetwork.AddCallbackTarget(this);
    //    SceneManager.sceneLoaded += OnSceneFinishedLoading;
    //}

    //public override void OnDisable()
    //{
    //    base.OnDisable();
    //    PhotonNetwork.RemoveCallbackTarget(this);
    //    SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    //}
    //void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    //{
    //    currentScene = scene.buildIndex;

    //}

    private void Start()
    {
        PV = GetComponent<PhotonView>();

        //Loading Components Disable
        ChattingScrollView.SetActive(true);
        LoadingPanel.SetActive(false);


    }

    private void Update()
    {

        //if (PV.IsMine && PhotonNetwork)  //CreatePlayer when entered room
        //{
        //    Debug.Log("방에 들어왔을 때, 캐릭터를 생성합니다");
        //    PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        //}
    }


    #region UI_BUTTONS CALLBACKS
    public void OnStartGameButton() //Call by StartGame Button
    {

        LoadingPanel.SetActive(true);

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        //Loading Progress
        //AsyncOperation operation = SceneManager.LoadSceneAsync(multiplayScene);

        //while (!operation.isDone)
        //{
        //    float progress = Mathf.Clamp01(operation.progress / .9f);

        //    LoadingSlider.value = progress;
        //    ProgressText.text = progress * 100f + "%";
        //    Debug.Log(progress);
        //}
        PhotonNetwork.LoadLevel(multiplayScene);
    }

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
    public void UpdateTeam()
    {
        if (nextPlayersTeam == 1)
        {
            nextPlayersTeam = 2;
        }
        else
        {
            nextPlayersTeam = 1;
        }
    }

    public void OnChangeBox(int seatNumber)
    {
        //if(room.spawnPoints[seatNumber].GetComponent<Image>()!=null)
        // Debug.Log("image");
        //else Debug.Log("no image");
        //if(room.spawnPoints[seatNumber].)
        //PhotonPlayer.myAvatar.transform.TransformPoint(room.spawnPoints[mynumberInRoom].position);
    }

    //[PunRPC]
    //private void RPC_CreatePlayer()
    //{
    //    Debug.Log("CreatePlayer");
    //    PhotonNetwork.Instantiate(Path.Combine("NetworkPlayer", "PhotonNetworkPlayer"),
    //        transform.position, Quaternion.identity, 0);
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    private PhotonView photonView;


    [Header("UI References")]
    public Text PlayerNameText;
    public Image PlayerChampImage;
    public Button PlayerReadyButton;
    public Image PlayerReadyImage;

    [Header("User private Settings")]
    public string userFspell;
    public string userDspell;
    public string userChamp;
    private int userrId;
    private bool isPlayerReady;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
       
        //SetRandomChamp();
    }
    private void Start()
    {
        //Initialize
        photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.LocalPlayer.ActorNumber != userrId)
        {
            PlayerReadyButton.gameObject.SetActive(false);
        }
        else
        {
            Hashtable initialProps = new Hashtable() { { GameConsts.PLAYER_READY, isPlayerReady },
                { GameConsts.PLAYER_CHAMPION, userChamp },
                { GameConsts.PLAYER_TEAM, null },
                { GameConsts.PLAYER_SPELL1, null },       //Default
                { GameConsts.PLAYER_SPELL2, null }};    //Defualt
            PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);

            PlayerReadyButton.onClick.AddListener(() =>
            {
                isPlayerReady = !isPlayerReady;
                SetPlayerReady(isPlayerReady);

                Hashtable props = new Hashtable() { { GameConsts.PLAYER_READY, isPlayerReady }};
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                if (PhotonNetwork.IsMasterClient)
                {
                    FindObjectOfType<NetworkManager>().LocalPlayerPropertiesUpdated();
                }
            });

            SetUserTeam();
            SetPlayerChampion();
            Debug.Log($"Player Init- isReady={isPlayerReady}," +
                $"userChamp={userChamp},userF={userFspell}, userD={userDspell}");
        }

    }
    public void Initialize(int playerId, string playerName)
    {
        userrId = playerId;
        PlayerNameText.text = playerName;
    }
    public void SetPlayerReady(bool playerReady)
    {
        PlayerReadyButton.GetComponentInChildren<Text>().text = playerReady ? "Ready!" : "Ready?";
        
        PlayerReadyImage.enabled = playerReady;
    }

    public void SetPlayerChampion()
    {
        System.Random rndIdx = new System.Random();
        int result = UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(ChampionDatabase.Champions)).Length);

        userChamp = System.Enum.GetName(typeof(ChampionDatabase.Champions), result);
        Debug.Log($"RandomIdx={rndIdx}Player Random Selected ={System.Enum.GetName(typeof(ChampionDatabase.Champions), result)}");

        Hashtable props = new Hashtable() { { GameConsts.PLAYER_CHAMPION, userChamp } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        PlayerChampImage.GetComponentInChildren<Image>().sprite =
            Resources.Load(Path.Combine("2D", userChamp), typeof(Sprite)) as Sprite;

        //PlayerReadyImage.enabled = playerReady;
    }
    
    public void SetUserTeam()
    {
        if(PhotonNetwork.LocalPlayer.ActorNumber==1|| PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            Hashtable props = new Hashtable() { { GameConsts.PLAYER_TEAM, GameConsts.RED_TEAM } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

            object myTeam;
            Debug.Log(props.TryGetValue(GameConsts.PLAYER_TEAM, out myTeam));
        }
        else
        {
            Hashtable props = new Hashtable() { { GameConsts.PLAYER_TEAM, GameConsts.BLUE_TEAM } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

            object myTeam;
            Debug.Log(props.TryGetValue(GameConsts.PLAYER_TEAM, out myTeam));
        }
    }
    
    public void SetUserSpell()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    private PhotonView photonView;

    [Header("UI References")]
    public Text PlayerNameText;
    public Image PlayerChampImage;
    public Button PlayerReadyButton;
    public Image PlayerReadyImage;

    private ExitGames.Client.Photon.Hashtable UserCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        this.transform.parent = GameObject.Find("Room Panel").transform;

        PlayerChampImage = GetComponent<Image>();
        photonView = GetComponent<PhotonView>();
 
        if (PlayerData.Instance == null)
        {
            PlayerData.Instance = this;
        }
    }
    private void Start()
    {
        SetRandomChamp();
    }

    private void Update()
    {

    }
    public void SetRandomChamp()
    {
        System.Random rndIdx = new System.Random();
        int result = UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(ChampionDatabase.Champions)).Length);
        
        Debug.Log($"Player Random Selected ={System.Enum.GetName(typeof(ChampionDatabase.Champions), result)}");

        switch(result)
        {
            case 0:
                {
                    Debug.Log(PlayerChampImage.GetComponent<Image>().sprite.name);

                    PlayerChampImage.GetComponent<Image>().sprite = 
                        Resources.Load(Path.Combine("2D","Xerion"),typeof(Sprite))as Sprite;
                    Debug.Log($"ChampImageSet = {PlayerChampImage.name}");

                    if (PlayerChampImage==null)
                    {
                        Debug.LogError("ImageSetting Error");
                    }
                    break;
                }
            case 1:
                {
                    Debug.Log(PlayerChampImage.GetComponent<Image>().sprite.name);
                    PlayerChampImage.GetComponent<Image>().sprite = 
                        Resources.Load(Path.Combine("2D", "BaekRang"), typeof(Sprite)) as Sprite;
                    Debug.Log($"ChampImageSet = {PlayerChampImage.name}");
                    if (PlayerChampImage == null)
                    {
                        Debug.LogError("ImageSetting Error");
                    }
                    break;
                }
            case 2:
                {
                    Debug.Log(PlayerChampImage.GetComponent<Image>().sprite.name);

                    PlayerChampImage.GetComponent<Image>().sprite = 
                        Resources.Load(Path.Combine("2D", "ColD"), typeof(Sprite)) as Sprite;
                    Debug.Log($"ChampImageSet = {PlayerChampImage.name}");
                    if (PlayerChampImage == null)
                    {
                        Debug.LogError("ImageSetting Error");
                    }
                    break;
                }
        }
    }
    public void SetUserTeam()
    {
        
    }
    
    public void SetUserSpell()
    {

    }
}

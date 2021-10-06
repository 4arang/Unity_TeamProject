using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    private PhotonView photonView;

    [Header("UI References")]
    public Text PlayerNameText;
    public Image PlayerChampImage;
    public Button PlayerReadyButton;
    public Image PlayerReadyImage;

    private ExitGames.Client.Photon.Hashtable dictionaryEntries = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        PlayerChampImage = GetComponentInChildren<Image>();
        photonView = GetComponent<PhotonView>();
 
        if (PlayerData.Instance == null)
        {
            PlayerData.Instance = this;
        }
        SetRandomChamp();    
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
                    PlayerChampImage = Resources.Load<Image>("Xerion");
                    Debug.Log($"ChampImageSet = {PlayerChampImage.name}");

                    if(PlayerChampImage==null)
                    {
                        Debug.LogError("ImageSetting Error");
                    }
                    break;
                }
            case 1:
                {
                    PlayerChampImage = Resources.Load<Image>("BaekRang");
                    Debug.Log($"ChampImageSet = {PlayerChampImage.name}");
                    if (PlayerChampImage == null)
                    {
                        Debug.LogError("ImageSetting Error");
                    }
                    break;
                }
            case 2:
                {
                    PlayerChampImage = Resources.Load<Image>("ColD");
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

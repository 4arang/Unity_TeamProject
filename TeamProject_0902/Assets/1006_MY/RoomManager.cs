using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    [Header("SpanPoint Settings")]
    public Transform[] lobbySpawnPoints;

    [Header("Spell Settings")]
    public GameObject SpellSelectBox;
    public int CurrentSpellBtn=0;
    public Button[] SpellButtons;
    public Button[] SpellSelectListButtons;



    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }

        //Get Team Spawn Position
        lobbySpawnPoints=GameObject.Find("LobbySpawnGroup").GetComponentsInChildren<Transform>();
    }

    

    #region UI_BUTTONS CALLBACKS
    public void OnSpellSetBoxOpen(int whichBtn)
    {
        SpellSelectBox.SetActive(true);
        CurrentSpellBtn = whichBtn;
    }
    public void OnSpellSetBoxClose()
    {
        SpellSelectBox.SetActive(false);
    }

    public void OnSpellSet(Image spell)
    {
        switch (RoomManager.Instance.CurrentSpellBtn)
        {
            case 0:     //D spell
                {
                    //PlayerData.Instance.userDspell = spell.name;
                    SpellButtons[0].GetComponentInChildren<Image>().sprite = spell.sprite;
                    OnSpellSetBoxClose();

                    Hashtable props = new Hashtable() { 
                        { GameConsts.PLAYER_SPELL1, PlayerData.Instance.userDspell } };
                    PhotonNetwork.LocalPlayer.SetCustomProperties(props);
                    Debug.Log($"userFSpell Set={PlayerData.Instance.userDspell}");
                    break;
                }
            case 1:     //F Spell
                {
                    PlayerData.Instance.userFspell = spell.name;
                    SpellButtons[1].GetComponentInChildren<Image>().sprite = spell.sprite;

                    Hashtable props = new Hashtable() { 
                        { GameConsts.PLAYER_SPELL2, PlayerData.Instance.userFspell } };
                    PhotonNetwork.LocalPlayer.SetCustomProperties(props);
                    OnSpellSetBoxClose();
                    Debug.Log($"userFSpell Set={PlayerData.Instance.userFspell}");
                    break;
                }
            default:
                {
                    print("Error");
                    break;
                }
        }

    }
    #endregion
    
    
}

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
    public Transform[] redSpawnPoints;
    public Transform[] blueSpawnPoints;

    [Header("Spell Settings")]
    public GameObject SpellSelectBox;
    public int CurrentSpellBtn=0;
    public Button[] SpellButtons;
    public Button[] SpellSelectListButtons;
    public int nextPlayerTeamId=1;
    
    Photon.Realtime.Player player;
    
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }

        player = PhotonNetwork.LocalPlayer;
        if (player == null)
        {
            Debug.Log("player Null" + player.ActorNumber);
            return;
        }
        else
        {
            SpawnPlayer();
        }        
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
                    print("D spell clicked and set");
                    SpellButtons[0].image = spell;
                    OnSpellSetBoxClose();
                    break;
                }
            case 1:     //F Spell
                {
                    print("F spell clicked and set");
                    SpellButtons[1].image = spell;
                    OnSpellSetBoxClose();
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

    private void Update()
    {
        PlayerSettingUpdate();
    }

    void PlayerSettingUpdate()
    {

    }
    void SpawnPlayer()
    {       
  
        //Get Team Spawn Position
        redSpawnPoints = GameObject.Find("RedSpawnGroup").GetComponentsInChildren<Transform>();
        blueSpawnPoints = GameObject.Find("BlueSpawnGroup").GetComponentsInChildren<Transform>();
        
        
        if (nextPlayerTeamId==1)
        {
            int spawnPicker = Random.Range(0, redSpawnPoints.Length);;
            PhotonNetwork.Instantiate("PhotonPlayer",
                redSpawnPoints[spawnPicker].position,
                redSpawnPoints[spawnPicker].rotation,
                0);
            nextPlayerTeamId = 2;
            
            Debug.Log($"Red Team Spawn Player at {redSpawnPoints[spawnPicker].position}");
            
        }
        else
        {
            int spawnPicker = Random.Range(1, blueSpawnPoints.Length); ;
            PhotonNetwork.Instantiate("PhotonPlayer",
            blueSpawnPoints[spawnPicker].position,        
            blueSpawnPoints[spawnPicker].rotation,
                0);

            nextPlayerTeamId = 1;
            Debug.Log($"Blue Team Spawn Player at {blueSpawnPoints[spawnPicker].position}");
        }        
    }
}

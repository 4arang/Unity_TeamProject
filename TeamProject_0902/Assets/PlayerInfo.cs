using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;
    private int selectIdx = 0;
    public int myTeam = 0;
    public bool isOnTeam = false;
    //Ingame Avatar
    public int mySelectedChampion;
    public GameObject[] allCharacters;

    //Lobby Avatar
    //public int mySelectedLobbyChampion;
    public GameObject[] allLobbyCharacters;    

    //Room Spell Select
    public int mySelectedSpell1;
    public int mySelectedSpell2;
    public GameObject[] allSpells;

    //Character 
   
    private void OnEnable()
    {
        if (PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if (PlayerInfo.PI != this)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LobbyInit()
    {

    }
    private void Start()
    {        
        //Character Select
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedChampion = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            PlayerPrefs.SetInt("MyCharacter", mySelectedChampion);
        }
        
        //Lobby Character Select
        if(PlayerPrefs.HasKey("MyLobbyCharacter"))
        {
            mySelectedChampion = PlayerPrefs.GetInt("MyLobbyCharacter");
        }
        else
        {
            PlayerPrefs.SetInt("MyLobbyCharacter", mySelectedChampion);
        }
    }

    public void RandomSelectMode()
    {
        int rndIdx = Random.Range(0, PlayerInfo.PI.allLobbyCharacters.Length);

        mySelectedChampion = rndIdx;
        PlayerPrefs.SetInt("MyCharacter", rndIdx);
        PlayerPrefs.SetInt("MyLobbyCharacter", rndIdx);//Lobby Test

        Debug.Log("My Random Champ = "+
            System.Enum.ToObject(typeof(ChampionDatabase.Champions), mySelectedChampion));
    }

    public void SpellSetting()
    {
        //Spell Select
        if (PlayerPrefs.HasKey("MySpell1"))
        {
            mySelectedSpell1 = PlayerPrefs.GetInt("MySpell1");
            Debug.Log("MySpell1 =" + mySelectedSpell1);
        }
        if (PlayerPrefs.HasKey("MySpell2"))
        {
            mySelectedSpell2 = PlayerPrefs.GetInt("MySpell2");
            Debug.Log("MySpell2 =" + mySelectedSpell2);
        }
        else
        {
            mySelectedSpell1 = 0;
            mySelectedSpell2 = 1;
            PlayerPrefs.SetInt("MySpell1", mySelectedSpell1);
            PlayerPrefs.SetInt("MySpell2", mySelectedSpell2);
            Debug.Log("MySpells =" + mySelectedSpell1 + ", " + mySelectedSpell2);
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.DeleteAll();
    }
}

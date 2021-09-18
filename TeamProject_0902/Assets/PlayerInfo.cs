using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;
    private int selectIdx = 0;
    //Ingame Avatar
    public int mySelectedChampion;
    public GameObject[] allCharacters;

    //Lobby Avatar
    public int mySelectedLobbyChampion;
    public GameObject[] allLobbyCharacters;

    //Room Spell Select
    public int mySelectedSpell1;
    public int mySelectedSpell2;
    public GameObject[] allSpells;

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

    private void Start()
    {        
        //Character Select
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedChampion = PlayerPrefs.GetInt("MyCharacter");
            Debug.Log("MySelectedChampion=" + mySelectedChampion);
        }
        else
        {
            mySelectedChampion = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedChampion);
            Debug.Log("MySelectedChampion=" + mySelectedChampion);
        }
        
        //Lobby Character Select
        if(PlayerPrefs.HasKey("MyLobbyCharacter"))
        {
            mySelectedChampion = PlayerPrefs.GetInt("MyLobbyCharacter");
            Debug.Log("MySelectedChampion=" + mySelectedChampion);

        }
        else
        {
            mySelectedChampion = 0;
            PlayerPrefs.SetInt("MyLobbyCharacter", mySelectedChampion);
            Debug.Log("MySelectedChampion=" + mySelectedChampion);
        }
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
}

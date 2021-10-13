using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;
    public int mySelectedChampion;
    public GameObject[] allCharacters;

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
    }
}

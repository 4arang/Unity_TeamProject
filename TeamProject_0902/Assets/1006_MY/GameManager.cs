using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float GameTime;
    public int CurrentPlayerID = 0;
    public List<TeamManager> Teams;


    private void Awake()
    {
        if (Instance != this)
            Instance = this;
    }

    void Update()
    {
        GameTime += Time.deltaTime;
    }

    public static int GetTeamKills(int i)
    {
        int amount = 0;
        //foreach (ChampionStats champ in Instance.Team[i].Champions)
        //{
        //    amount += champ.Kills;
        //}
        return amount;
    }
}

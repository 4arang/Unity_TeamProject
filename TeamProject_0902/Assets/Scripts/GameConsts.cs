using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class GameConsts
{
    public static int MELEE_COUNT = 3;
    public static int RANGE_COUNT = 3;
    public static int CANNON_COUNT = 1;
    public static int SUPER_COUNT = 1;
    public static int SUPER_ALL_COUNT = 2;

    public static int SPAWN_MID = 0;
    public static int SPAWN_TOP = 1;
    public static int SPAWN_BOTTOM = 2;

    public static int RED_TEAM = 0;
    public static int BLUE_TEAM = 1;


    public static int MINION_SPAWN_TIME = 5;//75
    public static int MINION_WAVE_TIME = 5;//30


    public const float PLAYER_RESPAWN_TIME = 4.0f;

    public const string PLAYER_READY = "IsPlayerReady";



    //public static ChampionDatabase.Champions GetChampion(int championChoice)
    //{
    //    switch(championChoice)
    //    {
    //        case 0: return ChampionDatabase.Champions.BaseChamp;
    //        case 1: return ChampionDatabase.Champions.Xerion;
    //        case 2: return ChampionDatabase.Champions.BaekRang;
    //        case 3: return ChampionDatabase.Champions.ColD;
    //    }
    //    return ChampionDatabase.Champions.BaseChamp; 
    //}
}

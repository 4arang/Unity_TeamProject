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


    public static int MINION_SPAWNINTERVAL_TIME = 6;
    public static int MINION_WAVESTART_TIME = 5;


    public const float PLAYER_RESPAWN_TIME = 4.0f;

    public const string PLAYER_READY = "IsPlayerReady";
    public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";


    public const string PLAYER_CHAMPION = "PlayerChampion";
    public const string PLAYER_TEAM = "PlayerTeam";
    public const string PLAYER_SPELL1 = "D Spell";
    public const string PLAYER_SPELL2 = "F Spell";
}

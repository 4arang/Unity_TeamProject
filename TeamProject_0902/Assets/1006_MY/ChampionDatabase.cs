using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionDatabase : MonoBehaviour
{
    public static ChampionDatabase Instance;

    public string randomChamp;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    public enum Champions
    {
        Xerion=0,
        BaekRang,
        ColD
    }
}

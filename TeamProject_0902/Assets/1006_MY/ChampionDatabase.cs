using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionDatabase : MonoBehaviour
{
    public static ChampionDatabase Instance;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    public enum Champions
    { 
        BaekRang,
        ColD,
        Xerion     
    }
}

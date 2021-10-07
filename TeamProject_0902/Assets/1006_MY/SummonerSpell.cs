using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummonerSpell : MonoBehaviour
{
    public static SummonerSpell Instance;
    private void Awake()
    {
        if (Instance != this)
            Instance = this;
    }
    public enum SummonerSpells
    {
        Exhaust,
        Flash,
        Ignite
    }
    public SummonerSpells spell;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummonerSpell : MonoBehaviour
{    
    public enum SummonerSpells
    {
        Exhaust,
        Flash,
        Ignite
    }
    public SummonerSpells spell;
}

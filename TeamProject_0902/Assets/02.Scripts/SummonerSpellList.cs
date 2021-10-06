using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummonerSpellList : MonoBehaviour
{    
    public enum SummonerSpells
    {
        Ignite,
        Flash,
        Exhaust,
        Heal,
        Ghost,
        Barrier,
        Teleport,
        Smite
    }
    [SerializeField]
    private Sprite[] SpellImg;
    public SummonerSpells summonerSpells;

    [SerializeField]
    private Button[] SpellList;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    //public void SetSpell(Image spell)
    //{
    //    switch(RoomManager.Instance.currentSpellBtn)
    //    {
    //        case 0:     //D spell
    //            {
    //                print("D spell clicked and set");
    //                SpellList[0].image = spell;

    //                break;
    //            }
    //        case 1:     //F Spell
    //            {
    //                print("F spell clicked and set");
    //                SpellList[1].image = spell;
    //                break;
    //            }
    //        default:
    //            {
    //                print("Error");
    //                break;
    //            }

    //    }
    //}
}

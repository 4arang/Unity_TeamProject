using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

   public enum ItemType
    {
        Potion,
        Boots_1,
        Boots_2,
        Boots_3,
        Boots_4,
        Boots_5,
        Cloth,
        Sapphire,
        Sword,
        Ruby,
        Book,
        Mantle,
        Stopwatch,
        Wand,
        Axe,
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Potion: return 150;
            case ItemType.Boots_1: return 300;
            case ItemType.Boots_2: return 500;
            case ItemType.Boots_3: return 350;
            case ItemType.Boots_4: return 800;
            case ItemType.Boots_5: return 650;
            case ItemType.Cloth: return 300;
            case ItemType.Sapphire: return 350;
            case ItemType.Sword: return 350;
            case ItemType.Ruby: return 400;
            case ItemType.Book: return 435;
            case ItemType.Mantle: return 450;
            case ItemType.Stopwatch: return 650;
            case ItemType.Wand: return 850;
            case ItemType.Axe: return 875;
        }

    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Potion: return GameAssets.Instance.s_Potion;
            case ItemType.Boots_1: return GameAssets.Instance.s_Boots_1;
            case ItemType.Boots_2: return GameAssets.Instance.s_Boots_2;
            case ItemType.Boots_3: return GameAssets.Instance.s_Boots_3;
            case ItemType.Boots_4: return GameAssets.Instance.s_Boots_4;
            case ItemType.Boots_5: return GameAssets.Instance.s_Boots_5;
            case ItemType.Cloth: return GameAssets.Instance.s_Cloth;
            case ItemType.Sapphire: return GameAssets.Instance.s_Sapphire;
            case ItemType.Sword: return GameAssets.Instance.s_Sword;
            case ItemType.Ruby: return GameAssets.Instance.s_Ruby;
            case ItemType.Book: return GameAssets.Instance.s_Book;
            case ItemType.Mantle: return GameAssets.Instance.s_Mantle;
            case ItemType.Stopwatch: return GameAssets.Instance.s_Stopwatch;
            case ItemType.Wand: return GameAssets.Instance.s_Wand;
            case ItemType.Axe: return GameAssets.Instance.s_Axe;
        }
    }

    public static string GetName(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Potion: return "?????? ????";
            case ItemType.Boots_1: return "????";
            case ItemType.Boots_2: return "???? ????";
            case ItemType.Boots_3: return "?????????? ??????";
            case ItemType.Boots_4: return "???????? ????";
            case ItemType.Boots_5: return "?????? ???????? ????";
            case ItemType.Cloth: return "??????";
            case ItemType.Sapphire: return "???????? ????";
            case ItemType.Sword: return "??????";
            case ItemType.Ruby: return "???? ????";
            case ItemType.Book: return "?????? ????";
            case ItemType.Mantle: return "???????????? ????";
            case ItemType.Stopwatch: return "??????";
            case ItemType.Wand: return "?????? ??????";
            case ItemType.Axe: return "??????";
        }

    }
    public static string GetExplain(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Potion: return "???? +125";
            case ItemType.Boots_1: return "???? ???? +25";
            case ItemType.Boots_2: return "???? ???? +45, ?????? +20";
            case ItemType.Boots_3: return "???? ???? +45, ?????? +25";
            case ItemType.Boots_4: return "???? ???? +50, ?????? +30";
            case ItemType.Boots_5: return "???? ???? +60, ?????? +10";
            case ItemType.Cloth: return "?????? +15";
            case ItemType.Sapphire: return "???? +250";
            case ItemType.Sword: return "?????? +10";
            case ItemType.Ruby: return "???? +150";
            case ItemType.Book: return "?????? +20";
            case ItemType.Mantle: return "?????? +25";
            case ItemType.Stopwatch: return "2.5?? ???? ????????";
            case ItemType.Wand: return "?????? +40";
            case ItemType.Axe: return "?????? +25";
        }

    }


}

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
            case ItemType.Potion: return "충전형 물약";
            case ItemType.Boots_1: return "장화";
            case ItemType.Boots_2: return "판금 장화";
            case ItemType.Boots_3: return "헤르메스의 발걸음";
            case ItemType.Boots_4: return "마법사의 신발";
            case ItemType.Boots_5: return "명석한 포브레의 장화";
            case ItemType.Cloth: return "천갑옷";
            case ItemType.Sapphire: return "사파이어 수정";
            case ItemType.Sword: return "롱소드";
            case ItemType.Ruby: return "루비 수정";
            case ItemType.Book: return "증폭의 고서";
            case ItemType.Mantle: return "마법무효화의 망토";
            case ItemType.Stopwatch: return "초시계";
            case ItemType.Wand: return "방출의 마법봉";
            case ItemType.Axe: return "곡괭이";
        }

    }
    public static string GetExplain(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Potion: return "체력 +125";
            case ItemType.Boots_1: return "이동 속도 +25";
            case ItemType.Boots_2: return "이동 속도 +45, 방어력 +20";
            case ItemType.Boots_3: return "이동 속도 +45, 방어력 +25";
            case ItemType.Boots_4: return "이동 속도 +50, 방어력 +30";
            case ItemType.Boots_5: return "이동 속도 +60, 방어력 +10";
            case ItemType.Cloth: return "방어력 +15";
            case ItemType.Sapphire: return "마나 +250";
            case ItemType.Sword: return "공격력 +10";
            case ItemType.Ruby: return "체력 +150";
            case ItemType.Book: return "주문력 +20";
            case ItemType.Mantle: return "방어력 +25";
            case ItemType.Stopwatch: return "2.5초 동안 무적상태";
            case ItemType.Wand: return "방어력 +40";
            case ItemType.Axe: return "공격력 +25";
        }

    }


}

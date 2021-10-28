using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "GameData/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("Item Database")]
    //Item Stats
    public string itemName;
    public string itemDescription;
    public int itemCost;
    public Image itemIcon;

    [Header("Item Ability")]
    public float HP;             //Health Point
    public int HPperLevel;     //HP increasement per Level
    public int MP;             //Mana Point
    public int MPperLevel;
    public int AP;             //Armor Point
    public float APperLevel;
    public int AD;             //Attack Damage
    public float ADperLevel;
    public int MRP;             //Magic Resistance Point
    public float MRPperLevel;
    public float AttackSpeed;
    public float AttackSpeedperLevel;
    public float MoveSpeed;
    public int AttackRange;
    public float HPregen;
    public float HPregenperLevel;
    public int MPregen;
    public float MPregenperLevel;
}

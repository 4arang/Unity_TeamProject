using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Level : MonoBehaviour, Interface_Shop
{ 

    public float Exp { get; set; } // exp to level up : 280 + (level-1)*100
    public int Gold { get; set; }
    public int Level { get; set; } //max 18

    public Lvlup UI_levelupBar;
    public GameObject uiprefab;
    UI_Setup uisetup;
    XP_Bar UI_Level;
    Gold_Text UI_Gold;
    Player_Stats stats;
    Item_Slots item_slots;
    // Start is called before the first frame update

    PhotonView PV;

    private void Start()
    {
        // GetComponent<Player_Item>().SetUI(uisetup);
        PV = GetComponent<PhotonView>();

        Exp = 0;
        Gold = 0;
        Level = 1;

       // uiprefab = FindObjectOfType<PlayerUI>().gameObject;


        //uisetup = uiprefab.GetComponent<UI_Setup>();
        UI_levelupBar = Photon.Pun.Demo.PunBasics.PlayerManager.UiInstance.GetComponentInChildren<Lvlup>();
        UI_Level = Photon.Pun.Demo.PunBasics.PlayerManager.UiInstance.GetComponentInChildren<XP_Bar>();
        UI_Gold = Photon.Pun.Demo.PunBasics.PlayerManager.UiInstance.GetComponentInChildren<Gold_Text>();

        InvokeRepeating("BasicEXP", 0, 5.0f); //5초마다 경험치 획득
        UI_levelupBar.gameObject.SetActive(false);
        stats = GetComponent<Player_Stats>();

       // item_slots = uiprefab.GetComponentInChildren<Item_Slots>();
    }

        private void BasicEXP()
    {
        GetEXP(5.0f);
    }

    public void GetEXP(float gain)
    {
        Exp += gain;
        if (Exp >= 280 + (Level - 1) * 100)
        {      
            Exp -= 280 + (Level - 1) * 100;
             Level++;
            GetComponentInChildren<Level_Text>().SetLevel(Level); //플레이어 ui 레벨
            UI_Level.SetLevel(Level);//인게임 ui 레벨
            UI_Level.SetMaxXP(280 + (Level - 1) * 100);

            UI_levelupBar.gameObject.SetActive(true);
        }
        UI_Level.SetXP(Exp);
    }

    public void GetGold(int gain)
    {
        Gold += gain;
        UI_Gold.SetGold(Gold);
    }

   public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought Item : " + itemType);
        switch (itemType)
        {
            case Item.ItemType.Potion: GetPostion(itemType); break; 
            case Item.ItemType.Boots_1: GetBoots_1(itemType); break;
            case Item.ItemType.Boots_2: GetBoots_2(itemType); break;
            case Item.ItemType.Boots_3: GetBoots_3(itemType); break; 
            case Item.ItemType.Boots_4: GetBoots_4(itemType); break; 
            case Item.ItemType.Boots_5: GetBoots_5(itemType); break; 
            case Item.ItemType.Cloth: GetCloth(itemType);  break; 
            case Item.ItemType.Sapphire: GetSapphire(itemType); break;
            case Item.ItemType.Sword: GetSword(itemType); break; 
            case Item.ItemType.Ruby: GetRuby(itemType); break; 
            case Item.ItemType.Book: GetBook(itemType); break; 
            case Item.ItemType.Mantle: GetMantle(itemType); break;
            case Item.ItemType.Stopwatch: GetStopwatch(itemType); break;
            case Item.ItemType.Wand: GetWand(itemType); break; 
            case Item.ItemType.Axe: GetAxe(itemType); break; 
        }
    }


    public bool SpendGold(int gold)
    {
        if (Gold >= gold)
        {
            Gold -= gold;
            UI_Gold.SetGold(Gold);
            return true;
        }
        else
        {
            Debug.Log("need gold");
            return false;
        }
    }


    private void GetPostion(Item.ItemType itemType)
    {
        item_slots.GetItem(itemType);
       // stats.GetHP(125);
    }
    private void GetBoots_1(Item.ItemType itemType)
    {
        stats.EquippedSpeedItem(25);
    }
    private void GetBoots_2(Item.ItemType itemType)
    {
        stats.EquippedSpeedItem(45);
        stats.EquippedArmorItem(20);
    }
    private void GetBoots_3(Item.ItemType itemType)
    {
        stats.EquippedSpeedItem(45);
        stats.EquippedArmorItem(25);
    }
    private void GetBoots_4(Item.ItemType itemType)
    {
        stats.EquippedSpeedItem(50);
        stats.EquippedArmorItem(30);
    }
    private void GetBoots_5(Item.ItemType itemType)
    {
        stats.EquippedSpeedItem(60);
        stats.EquippedArmorItem(10);
    }
    private void GetCloth(Item.ItemType itemType)
    {
        stats.EquippedArmorItem(15);
    }
    private void GetSapphire(Item.ItemType itemType)
    {
        stats.GetMP(250);
    }
    private void GetSword(Item.ItemType itemType)
    {
        stats.EquippedAttackItem(10);
    }
    private void GetRuby(Item.ItemType itemType)
    {
        stats.GetHP(150);
    }
    private void GetBook(Item.ItemType itemType)
    {

    }
    private void GetMantle(Item.ItemType itemType)
    {
        stats.EquippedArmorItem(25);
    }
    private void GetStopwatch(Item.ItemType itemType)
    {
        stats.InvincibleMode(2.5f);
    }
    private void GetWand(Item.ItemType itemType)
    {
        stats.EquippedArmorItem(40);
    }
    private void GetAxe(Item.ItemType itemType)
    {
        stats.EquippedAttackItem(25);
    }


}

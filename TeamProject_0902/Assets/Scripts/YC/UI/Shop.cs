using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Transform container;
    [SerializeField]
    private Transform shopItemTemplate;

    private Vector2 orgPos;
    private float distance;
    private Item.ItemType itemtype;
    private Interface_Shop interface_shop;
    Player_Level player;
    private bool itemClicked = false;

    [SerializeField] private GameObject[] Table;

    Item_Slots itemSlots;

    private void Awake()
    {
        distance = Screen.width *0.15f;
        //container = transform.Find("container");
        //shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.position = new Vector2(Table[0].transform.position.x, Table[0].transform.position.y);
         shopItemTemplate.gameObject.SetActive(false);
        orgPos = new Vector2(Screen.width *0.63f, Screen.height* 0.88f);
    }

    private void Start()
    {
       // player = FindObjectOfType<MapCamera>().PlayerToMove.GetComponent<Player_Level>();

        CreateItemButton(Item.ItemType.Axe, Item.GetSprite(Item.ItemType.Axe), "Axe", Item.GetCost(Item.ItemType.Axe),
            Item.GetName(Item.ItemType.Axe), Item.GetExplain(Item.ItemType.Axe), 14);
        CreateItemButton(Item.ItemType.Wand, Item.GetSprite(Item.ItemType.Wand), "Wand", Item.GetCost(Item.ItemType.Wand),
    Item.GetName(Item.ItemType.Wand), Item.GetExplain(Item.ItemType.Wand), 13);
        CreateItemButton(Item.ItemType.Stopwatch, Item.GetSprite(Item.ItemType.Stopwatch), "Stopwatch", Item.GetCost(Item.ItemType.Stopwatch),
    Item.GetName(Item.ItemType.Stopwatch), Item.GetExplain(Item.ItemType.Stopwatch), 12);
        CreateItemButton(Item.ItemType.Mantle, Item.GetSprite(Item.ItemType.Mantle), "Mantle", Item.GetCost(Item.ItemType.Mantle),
     Item.GetName(Item.ItemType.Mantle), Item.GetExplain(Item.ItemType.Mantle), 11);
        CreateItemButton(Item.ItemType.Book, Item.GetSprite(Item.ItemType.Book), "Book", Item.GetCost(Item.ItemType.Book),
     Item.GetName(Item.ItemType.Book), Item.GetExplain(Item.ItemType.Book), 10);
        CreateItemButton(Item.ItemType.Ruby, Item.GetSprite(Item.ItemType.Ruby), "Ruby", Item.GetCost(Item.ItemType.Ruby),
    Item.GetName(Item.ItemType.Ruby), Item.GetExplain(Item.ItemType.Ruby), 9);
        CreateItemButton(Item.ItemType.Sword, Item.GetSprite(Item.ItemType.Sword), "Sword", Item.GetCost(Item.ItemType.Sword),
    Item.GetName(Item.ItemType.Sword), Item.GetExplain(Item.ItemType.Sword), 8);
        CreateItemButton(Item.ItemType.Sapphire, Item.GetSprite(Item.ItemType.Sapphire), "Sapphire", Item.GetCost(Item.ItemType.Sapphire),
    Item.GetName(Item.ItemType.Sapphire), Item.GetExplain(Item.ItemType.Sapphire), 7);
        CreateItemButton(Item.ItemType.Cloth, Item.GetSprite(Item.ItemType.Cloth), "Cloth", Item.GetCost(Item.ItemType.Cloth),
    Item.GetName(Item.ItemType.Cloth), Item.GetExplain(Item.ItemType.Cloth), 6);
        CreateItemButton(Item.ItemType.Boots_5, Item.GetSprite(Item.ItemType.Boots_5), "Boots_5", Item.GetCost(Item.ItemType.Boots_5),
    Item.GetName(Item.ItemType.Boots_5), Item.GetExplain(Item.ItemType.Boots_5), 5);
        CreateItemButton(Item.ItemType.Boots_4, Item.GetSprite(Item.ItemType.Boots_4), "Boots_4", Item.GetCost(Item.ItemType.Boots_4),
     Item.GetName(Item.ItemType.Boots_4), Item.GetExplain(Item.ItemType.Boots_4), 4);
        CreateItemButton(Item.ItemType.Boots_3, Item.GetSprite(Item.ItemType.Boots_3), "Boots_3", Item.GetCost(Item.ItemType.Boots_3),
    Item.GetName(Item.ItemType.Boots_3), Item.GetExplain(Item.ItemType.Boots_3), 3);
        CreateItemButton(Item.ItemType.Boots_2, Item.GetSprite(Item.ItemType.Boots_2), "Boots_2", Item.GetCost(Item.ItemType.Boots_2),
     Item.GetName(Item.ItemType.Boots_2), Item.GetExplain(Item.ItemType.Boots_2), 2);
        CreateItemButton(Item.ItemType.Boots_1, Item.GetSprite(Item.ItemType.Boots_1), "Boots_1", Item.GetCost(Item.ItemType.Boots_1),
     Item.GetName(Item.ItemType.Boots_1), Item.GetExplain(Item.ItemType.Boots_1), 1);
        CreateItemButton(Item.ItemType.Potion, Item.GetSprite(Item.ItemType.Potion), "Potion", Item.GetCost(Item.ItemType.Potion),
     Item.GetName(Item.ItemType.Potion), Item.GetExplain(Item.ItemType.Potion), 0);

        Table[0].SetActive(true);
        itemSlots = FindObjectOfType<Item_Slots>();
    }

    public void setup(int pageNum)
    {
        switch (pageNum)
        {
            case 1:
                {
                    for (int i = 0; i < Table.Length; i++)
                    {
                        Table[i].SetActive(false);
                    }
                        Table[pageNum-1].SetActive(true);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < Table.Length; i++)
                    {
                        Table[i].SetActive(false);
                    }
                    Table[pageNum - 1].SetActive(true);
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < Table.Length; i++)
                    {
                        Table[i].SetActive(false);
                    }
                    Table[pageNum - 1].SetActive(true);
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < Table.Length; i++)
                    {
                        Table[i].SetActive(false);
                    }
                    Table[pageNum - 1].SetActive(true);
                    break;
                }
        }

    }

    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, string itemTitle, string itemExplain, int index)
    {
 
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = distance;
        float shopItemWidth = distance;

        shopItemRectTransform.position =
            new Vector2(orgPos.x + index % 5 * shopItemWidth, orgPos.y - (index / 5) * shopItemHeight);

        shopItemTransform.Find("itemCost").GetComponent<Text>().text = itemCost.ToString();
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        if (shopItemTransform) Debug.Log(" instantiated");

        shopItemTransform.Find("TextBox").transform.Find("Title").GetComponent<Text>().text = itemTitle.ToString();
        shopItemTransform.Find("TextBox").transform.Find("Text").GetComponent<Text>().text = itemExplain.ToString();


        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () => { ShowItem(itemType); };
        shopItemTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => { BuyItem(itemType); };
    }

 

    public void Show(Interface_Shop shopper)
    {
        this.interface_shop = shopper;
        itemClicked = false;
    }

    public void ShowItem(Item.ItemType itemType)
    {
        itemtype = itemType;
        itemClicked = true;
    }

    public void BuyClickedItem()
    {
        if (itemClicked) BuyItem(itemtype);
    }

    public void BuyItem(Item.ItemType itemType)
    {

        if(interface_shop.SpendGold(Item.GetCost(itemtype)))
            {
             interface_shop.BoughtItem(itemType);
            }

    }

}

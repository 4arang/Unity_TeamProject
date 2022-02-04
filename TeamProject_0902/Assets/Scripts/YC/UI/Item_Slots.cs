using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Slots : MonoBehaviour
{
    public Image[] imgItem;

    private bool on1=false;
    private bool on2=false;
    private bool on3=false;
    private bool on4=false;
    private bool on5=false;
    private bool on6=false;
    public bool isfull=false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && on1)
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && on2)
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && on3)
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && on4)
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && on5)
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && on6)
        {

        }
    }

    public void GetItem(Item.ItemType itemType)
    {

        //if (imgItem[0].sprite == null)
        //{
        //    imgItem[0].sprite = Item.GetSprite(itemType);
        //    on1 = true;
        //}
        //else if (imgItem[1].sprite == null)
        //{
        //    imgItem[1].sprite = Item.GetSprite(itemType);
        //    on2 = true;
        //}
        //else if (imgItem[2].sprite == null)
        //{
        //    imgItem[2].sprite = Item.GetSprite(itemType);
        //    on3 = true;
        //}
        //else if (imgItem[3].sprite == null)
        //{
        //    imgItem[3].sprite = Item.GetSprite(itemType);
        //    on4 = true;
        //}
        //else if (imgItem[4].sprite == null)
        //{
        //    imgItem[4].sprite = Item.GetSprite(itemType);
        //    on5 = true;
        //}
        //else if (imgItem[5].sprite == null)
        //{
        //    imgItem[5].sprite = Item.GetSprite(itemType);
        //    on6 = true;
        //}
    }
}

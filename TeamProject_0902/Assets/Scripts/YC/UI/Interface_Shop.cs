using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interface_Shop
{
    void BoughtItem(Item.ItemType itemType);

    bool SpendGold(int goldAmount);

}


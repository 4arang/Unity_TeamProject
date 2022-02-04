using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets Instance_;

    public static GameAssets Instance
    {
        get
        {
            if (Instance_ == null) Instance_ = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return Instance_;
        }
    }

    public Sprite s_Potion;
    public Sprite s_Boots_1;
    public Sprite s_Boots_2;
    public Sprite s_Boots_3;
    public Sprite s_Boots_4;
    public Sprite s_Boots_5;
    public Sprite s_Cloth;
    public Sprite s_Sapphire;
    public Sprite s_Sword;
    public Sprite s_Ruby;
    public Sprite s_Book;
    public Sprite s_Mantle;
    public Sprite s_Stopwatch;
    public Sprite s_Wand;
    public Sprite s_Axe;
}

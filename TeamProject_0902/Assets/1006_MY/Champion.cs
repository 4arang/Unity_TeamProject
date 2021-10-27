using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
    public ChampionData champData;

    public void Print()
    {
        Debug.Log(champData.DisplayedName);
    }
}

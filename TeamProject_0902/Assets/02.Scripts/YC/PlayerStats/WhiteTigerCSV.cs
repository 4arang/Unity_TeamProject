using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTigerCSV : MonoBehaviour
{

    void Start()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");
   
    }


}

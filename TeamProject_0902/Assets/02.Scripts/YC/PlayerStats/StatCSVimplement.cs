using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCSVimplement : MonoBehaviour
{

    void Start()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");
        print("ID : " + data[0]["id"]);
    }


}

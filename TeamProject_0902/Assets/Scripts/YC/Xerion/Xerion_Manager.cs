using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_Manager : MonoBehaviour
{
    public float Xerion_AD;

    private static Xerion_Manager sInstance;
    public static Xerion_Manager Instance
    {
        get
        {
            if(sInstance == null)
            {
             GameObject newGameObj = new GameObject("Xerion_Manager");
             sInstance = newGameObj.AddComponent<Xerion_Manager>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

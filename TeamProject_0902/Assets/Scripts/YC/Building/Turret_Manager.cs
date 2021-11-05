using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Manager : MonoBehaviour
{

    public Transform TargetBuilding1;
    public Transform TargetBuilding2;
    public Transform TargetBuilding3;


    private static Turret_Manager sInstance;
    public static Turret_Manager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("Turret_Manager");
                sInstance = newGameObj.AddComponent<Turret_Manager>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Manager : MonoBehaviour
{

    public Transform Blue_TargetBuilding1;
    public Transform Blue_TargetBuilding2;
    public Transform Blue_TargetBuilding3;  //suppressor
    public Transform Blue_TargetBuilding4;
    public Transform Blue_TargetBuilding5;
    public Transform Blue_TargetBuilding6;

    public Transform Red_TargetBuilding1;
    public Transform Red_TargetBuilding2;
    public Transform Red_TargetBuilding3; //suppressor
    public Transform Red_TargetBuilding4; 
    public Transform Red_TargetBuilding5;
    public Transform Red_TargetBuilding6;

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

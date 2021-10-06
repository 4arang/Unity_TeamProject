using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_MaterialChange : MonoBehaviour
{
    public GameObject aura;
    Renderer renMaterial;
    void Start()
    {
        aura.SetActive(false);
        renMaterial = GetComponentInChildren<Renderer>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
         {
            //aura.SetActive(true);
            renMaterial.material.SetColor(0, new Color(1, 1, 1));
        }
    }
}

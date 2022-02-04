using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Item : MonoBehaviour
{
    public Shop shop;
    private Vector3 shopPos;
    private bool activeshop = false;
    private bool pressshop = false;

    public GameObject uiprefab;
    UI_Setup uisetup;
    PhotonView PV;

  private void Start()
    {
        PV = GetComponent<PhotonView>();
        //}
        //public void SetUI(GameObject UIprefab)
        //{



       // uiprefab = FindObjectOfType<PlayerUI>().gameObject;

        // uisetup = movingManager.Instance.UIPrefab.GetComponent<UI_Setup>();


        if (GetComponent<Player_Stats>().TeamColor)
        {
            shopPos = Turret_Manager.Instance.Red_TargetBuilding6.position;
        }
        else
        {
            shopPos = Turret_Manager.Instance.Blue_TargetBuilding6.position;
        }
        shop = Photon.Pun.Demo.PunBasics.PlayerManager.UiInstance.GetComponentInChildren<Shop>();
        shop.gameObject.SetActive(false);
    }
    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.P) && (activeshop == false))
            {
                if (Vector3.Distance(shopPos, transform.position) <= 10)
                {
                    pressshop = true;
                }
                Debug.Log("activeshop");
            }
            if (pressshop)
            {
                if (Input.GetKeyUp(KeyCode.P))
                {
                    Interface_Shop shopper = GetComponent<Interface_Shop>();
                    if (shopper != null) shop.Show(shopper);
                    else Debug.Log("Lost shopper");
                    pressshop = false;
                    shop.gameObject.SetActive(true);
                    activeshop = true;
                }
            }

            if (activeshop)
            {
                if ((Vector3.Distance(shopPos, transform.position) > 10) || Input.GetKeyDown(KeyCode.P))
                {
                    shop.gameObject.SetActive(false);
                    activeshop = false;
                    Debug.Log("disactiveshop");
                }
            }
        }
    }
}

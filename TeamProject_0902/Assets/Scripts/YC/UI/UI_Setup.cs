using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;

public class UI_Setup : MonoBehaviour
{
    PhotonView PV;

    private GameObject PlayerUiPrefab;

    private Photon.Pun.Demo.PunBasics.PlayerManager _target;

    public void SetTarget(Photon.Pun.Demo.PunBasics.PlayerManager target)
    {
        if (target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        _target = target;
  
    }

    private void Start()
    {
       
    } 

    //private void Update()
    //{
    //    if (_target == null)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //}
    //public void SetUITarget(Transform player, int index)
    //public void SetUITarget(Transform player)
    //{
    //    //if (Photon.Pun.Demo.PunBasics.PlayerManager.LocalPlayerInstance.transform)
    //    //{
    //    //    //Index = index;
    //    //    Player = Photon.Pun.Demo.PunBasics.PlayerManager.LocalPlayerInstance.transform;
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("localplayertransform null");
    //    //}
    //    //Player.GetComponent<Player_Stats>().SetUI(this);
    //    Player = player;
    //    GetComponentInChildren<MapCamera>().PlayerToMove = Player;
    //}

    //private void Setplayer()
    //{
    //    Player = Photon.Pun.Demo.PunBasics.PlayerManager.LocalPlayerInstance.transform;
    //}

    //private void Start()
    //{
    //    Setplayer();
    //   // SetUITarget();
    //    //PV = Player.GetComponent<PhotonView>();
    //    //if (PV.IsMine)
    //    //{
    //    //    foreach (UI_Setup uisetup in FindObjectsOfType<UI_Setup>())
    //    //    {
    //    //        if (!PV.IsMine)
    //    //        {
    //    //            uisetup.gameObject.SetActive(false);
    //    //        }
    //    //    }
    //    //}

    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player_UI : MonoBehaviour
{
    //movingManager _target;
    private Photon.Pun.Demo.PunBasics.PlayerManager _target;
    private PhotonView PV;
    public GameObject PlayerUiPrefab;

    //public void SetTarget(movingManager target)
    //{
    //    if(target == null) { Debug.Log("UI target is null");  return; }

    //    _target = target;

    //}

    //void Awake()
    //{
    //    this.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
    //}

    //private void Awake()
    //{
    //    PV = GetComponent<PhotonView>();

   
    //    if (PV.IsMine && PlayerUiPrefab != null)
    //    {

      
    //            GameObject _uiGo = Instantiate(PlayerUiPrefab);
    //       // _uiGo.GetComponent<UI_Setup>().SetUITarget(this.transform);
    //       // _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
    //    }
    //    // _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    //    //GetComponent<Player_Stats>().SetUI(_uiGo);
    //    //  GetComponent<Player_Level>().SetUI(_uiGo);
    //    //   GetComponent<Player_Item>().SetUI(_uiGo);
   
    //    // GetComponent<Player_Stats>().uiprefab = _uiGo;
    

    //}

    //public void SetTarget(Photon.Pun.Demo.PunBasics.PlayerManager target)
    //{
    //    if(target==null)
    //    {
    //        Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
    //        return;
    //    }
    //    // Cache references for efficiency
    //    _target = target;
    //}
    //private void Start()
    //{
    //    foreach (UI_Setup uiprefab in FindObjectsOfType<UI_Setup>())
    //    {
    //        uiprefab.gameObject.SetActive(false);

    //    }


    //        movingManager.Instance.UIPrefab.SetActive(true);
    //}
}

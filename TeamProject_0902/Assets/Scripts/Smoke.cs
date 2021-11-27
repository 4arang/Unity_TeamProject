using Photon.Pun;
using UnityEngine;

//�÷��̾ ���⿡ ���� ���� ���� ������������ ->���濡�Դ� ���������ʴ������� �Ȱ��� ������?
public class Smoke : MonoBehaviour
{
    [SerializeField] private GameObject[] Smoke1;
    [SerializeField] private GameObject[] Smoke2;
    [SerializeField] private GameObject[] Smoke3;
    [SerializeField] private GameObject[] Smoke4;

    PhotonView PV;
    PhotonView PV_;
    private int myActorNumber;
    private bool myTeamColor;
    void Start()
    {
        myActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            PV = other.GetComponent<PhotonView>();
            myTeamColor = other.GetComponent<Player_Stats>().TeamColor;
            foreach (var pv in PhotonNetwork.PhotonViewCollection)
            {
                if (pv.TryGetComponent(out Player_Stats player) && 
                    player.GetComponent<Player_Stats>().TeamColor == myTeamColor)
                {
                    PV_ = pv;
                }
            }
            if (PV.IsMine || PV_.IsMine)
            {
                activesmoke(false);
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PV = other.GetComponent<PhotonView>();
            myTeamColor = other.GetComponent<Player_Stats>().TeamColor;
            foreach (var pv in PhotonNetwork.PhotonViewCollection)
            {
                if (pv.TryGetComponent(out Player_Stats player) &&
                  player.GetComponent<Player_Stats>().TeamColor == myTeamColor)
                {
                    PV_ = pv;
                }
            }
            if (PV.IsMine || PV_.IsMine)
            {
                activesmoke(true);
            }
            //if (PV.IsMine)
            //{
            //    activesmoke(true);
            //    foreach (var player in PhotonNetwork.PlayerList)
            //    {
            //        if (player.ActorNumber % 2 == myActorNumber % 2)
            //        {
            //            PV.RPC("activeSmoke", player, true);
            //        }

            //    }
            //}
        }
    }
    void activesmoke(bool b)
    {
        foreach (GameObject smoke in Smoke1)
        {
            smoke.SetActive(b);
        }
        foreach (GameObject smoke in Smoke2)
        {
            smoke.SetActive(b);
        }
        foreach (GameObject smoke in Smoke3)
        {
            smoke.SetActive(b);
        }
        foreach (GameObject smoke in Smoke4)
        {
            smoke.SetActive(b);
        }
    }
    [PunRPC]
   void activeSmoke(bool b)
    {
        Debug.Log("actgive");
        foreach (GameObject smoke in Smoke1)
        {
            smoke.SetActive(b);
        }
        foreach (GameObject smoke in Smoke2)
        {
            smoke.SetActive(b);
        }
        foreach (GameObject smoke in Smoke3)
        {
            smoke.SetActive(b);
        }
        foreach (GameObject smoke in Smoke4)
        {
            smoke.SetActive(b);
        }
    }
}

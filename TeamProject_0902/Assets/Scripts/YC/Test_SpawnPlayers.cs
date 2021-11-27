using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Test_SpawnPlayers : MonoBehaviour
{
    public GameObject WhiteTiger;
    public GameObject Coldy;
    public GameObject Xerion;
    private Vector3 spawnPos;

    MainCamera_CameraRoam camera;


    private int selectedChampion;

    private void Awake()
    {
        camera = FindObjectOfType<MainCamera_CameraRoam>();

        selectedChampion = LobbyController.Instance.selectedChamp;

        if (PhotonNetwork.LocalPlayer.ActorNumber%2==1)
            spawnPos = new Vector3(-5, 0, 0);
        else
            spawnPos = new Vector3(5, 0, 0);

        switch (selectedChampion)
        {
            case 0:
                {
                  GameObject Player =  PhotonNetwork.Instantiate(WhiteTiger.name, spawnPos, Quaternion.identity);
                    camera.SetCameraTarget(Player.transform);
                    break;
                }
            case 1:
                {
                    GameObject Player = PhotonNetwork.Instantiate(Coldy.name, spawnPos, Quaternion.identity);
                    camera.SetCameraTarget(Player.transform);
                    break;
                }
            case 2:
                {
                    GameObject Player = PhotonNetwork.Instantiate(Xerion.name, spawnPos, Quaternion.identity);
                    camera.SetCameraTarget(Player.transform);
                    break;
                }
        }

    }

}

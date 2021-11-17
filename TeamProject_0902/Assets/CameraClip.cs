using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraClip : MonoBehaviourPun
{
    private void Start()
    {
        CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();

        if (photonView.IsMine)
        {

            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
        else
        {
            Destroy(followCam);
        }
    }
}
